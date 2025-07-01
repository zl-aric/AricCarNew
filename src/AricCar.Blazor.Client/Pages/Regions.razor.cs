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
    protected string SelectedCreateTab = "Region-create-tab";
    protected string SelectedEditTab = "Region-edit-tab";
    private RegionDto? SelectedRegion;


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
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Regions"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {


        Toolbar.AddButton(L["NewRegion"], async () =>
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

        SelectedCreateTab = "Region-create-tab";


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

    private async Task OpenEditRegionModalAsync(RegionDto input)
    {
        SelectedEditTab = "Region-edit-tab";


        var Region = await RegionsAppService.GetAsync(input.Id);

        EditingRegionId = Region.Id;
        EditingRegion = ObjectMapper.Map<RegionDto, RegionUpdateDto>(Region);

        await EditingRegionValidations.ClearAll();
        await EditRegionModal.Show();
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

    private async Task CloseEditRegionModalAsync()
    {
        await EditRegionModal.Hide();
    }

    private async Task UpdateRegionAsync()
    {
        try
        {
            if (await EditingRegionValidations.ValidateAll() == false)
            {
                return;
            }

            await RegionsAppService.UpdateAsync(EditingRegionId, EditingRegion);
            await GetRegionsAsync();
            await EditRegionModal.Hide();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void OnSelectedCreateTabChanged(string name)
    {
        SelectedCreateTab = name;
    }

    private void OnSelectedEditTabChanged(string name)
    {
        SelectedEditTab = name;
    }

}
