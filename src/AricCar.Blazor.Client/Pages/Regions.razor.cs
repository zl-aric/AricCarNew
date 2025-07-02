using AricCar.Permissions;
using AricCar.Regions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace AricCar.Blazor.Client.Pages;

public partial class Regions
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
    protected PageToolbar Toolbar { get; } = new PageToolbar();
    protected bool ShowAdvancedFilters { get; set; }
    private IReadOnlyList<RegionDto> RegionList { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }
    private bool CanCreateRegion { get; set; }
    private bool CanEditRegion { get; set; }
    private bool CanDeleteRegion { get; set; }
    private RegionCreateDto NewRegion { get; set; }
    private Validations NewRegionValidations { get; set; } = new();
    private RegionUpdateDto EditingRegion { get; set; }
    private Validations EditingRegionValidations { get; set; } = new();
    private Guid EditingRegionId { get; set; }
    private Modal CreateRegionModal { get; set; } = new();
    private Modal EditRegionModal { get; set; } = new();
    private GetRegionsInput Filter { get; set; }
    private DataGridEntityActionsColumn<RegionDto> EntityActionsColumn { get; set; } = new();

    private RegionItem? SelectedProvince = null;
    private RegionItem? SelectedCity = null;
    private RegionItem? SelectedDistrict = null;
    private IEnumerable<RegionItem>? Cities => SelectedProvince?.children;
    private IEnumerable<RegionItem>? Districts => SelectedCity?.children;
    private IEnumerable<RegionItem> ProvicnesList { get; set; } = [];

    public Regions()
    {
        NewRegion = new RegionCreateDto();
        EditingRegion = new RegionUpdateDto();
        Filter = new GetRegionsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
        RegionList = new List<RegionDto>();
    }



    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        ProvicnesList = await RegionsAppService.GetRegionJsonListAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await SetToolbarItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem("区域管理"));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton("新增区域", async () =>
        {
            await OpenCreateRegionModalAsync();
        }, IconName.Add, requiredPolicyName: AricCarPermissions.Regions.Create);

        return ValueTask.CompletedTask;
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateRegion = await AuthorizationService
            .IsGrantedAsync(AricCarPermissions.Regions.Create);
        CanEditRegion = await AuthorizationService
                        .IsGrantedAsync(AricCarPermissions.Regions.Edit);
        CanDeleteRegion = await AuthorizationService
                        .IsGrantedAsync(AricCarPermissions.Regions.Delete);
    }

    private async Task GetRegionsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await RegionsAppService.GetListAsync(Filter);
        RegionList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetRegionsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RegionDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page;
        await GetRegionsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OpenCreateRegionModalAsync()
    {
        NewRegion = new RegionCreateDto
        {
        };
        SelectedProvince = null;
        SelectedCity = null;
        SelectedDistrict = null;
        await NewRegionValidations.ClearAll();
        await CreateRegionModal.Show();
    }

    private async Task CloseCreateRegionModalAsync()
    {
        NewRegion = new RegionCreateDto
        {
        };
        await CreateRegionModal.Hide();
    }

    private async Task DeleteRegionAsync(RegionDto input)
    {
        await RegionsAppService.DeleteAsync(input.Id);
        await GetRegionsAsync();
    }

    private async Task CreateRegionAsync()
    {
        try
        {
            if (await NewRegionValidations.ValidateAll() == false)
            {
                return;
            }

            await RegionsAppService.CreateAsync(NewRegion);
            await GetRegionsAsync();
            await CloseCreateRegionModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }


    private Task OnProvinceChanged()
    {
        if (!string.IsNullOrWhiteSpace(NewRegion.ProvincialCode))
        {
            SelectedProvince = ProvicnesList.FirstOrDefault(p => p.code == NewRegion.ProvincialCode)!;
            NewRegion.ProvincialName = SelectedProvince?.name ?? string.Empty;
        }

        NewRegion.CityCode = string.Empty;
        NewRegion.CityName = string.Empty;
        NewRegion.DistrictCode = string.Empty;
        NewRegion.DistrictName = string.Empty;

        return Task.CompletedTask;
    }

    private Task OnCityChanged()
    {
        NewRegion.DistrictCode = string.Empty;
        NewRegion.DistrictName = string.Empty;
        if (!string.IsNullOrWhiteSpace(NewRegion.CityCode))
        {
            SelectedCity = Cities?.FirstOrDefault(p => p.code == NewRegion.CityCode)!;
            NewRegion.CityName = SelectedCity.name;
        }
        return Task.CompletedTask;
    }
    private Task OnDistrictChanged()
    {
        if (!string.IsNullOrWhiteSpace(NewRegion.DistrictCode))
        {
            SelectedDistrict = Districts?.FirstOrDefault(p => p.code == NewRegion.DistrictCode)!;
            NewRegion.DistrictName = SelectedDistrict.name;
        }
        return Task.CompletedTask;
    }
}