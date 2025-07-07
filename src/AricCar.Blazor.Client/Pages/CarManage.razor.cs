using AricCar.Cars;
using AricCar.Permissions;
using AricCar.Regions;
using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace AricCar.Blazor.Client.Pages
{
    public partial class CarManage
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();

        private IReadOnlyList<CarDto> CarList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        protected GetCarsInput Filter { get; set; }

        private CarCreateDto NewCar { get; set; }
        private Validations NewCarValidations { get; set; } = new();

        private Modal CreateCarModal { get; set; } = new();

        private RegionItem? SelectedProvince = null;
        private RegionItem? SelectedCity = null;
        private RegionItem? SelectedDistrict = null;
        private IEnumerable<RegionItem>? Cities => SelectedProvince?.children;
        private IEnumerable<RegionItem>? Districts => SelectedCity?.children;
        private IEnumerable<RegionItem> ProvicnesList { get; set; } = [];

        [Inject]
        protected IRegionAppService RegionsAppService { get; set; }

        public CarManage()
        {
            Filter = new GetCarsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            NewCar = new CarCreateDto();
        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
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
            Toolbar.AddButton("新增", async () =>
            {
                await OpenCreateCarModalAsync();
            }, IconName.Add, requiredPolicyName: AricCarPermissions.Regions.Create);

            return ValueTask.CompletedTask;
        }

        private async Task OpenCreateCarModalAsync()
        {
            NewCar = new CarCreateDto();
            SelectedProvince = null;
            SelectedCity = null;
            SelectedDistrict = null;
            await NewCarValidations.ClearAll();
            await CreateCarModal.Show();
        }

        private string? imageError;

        private async Task OnCarImagesChanged(FileChangedEventArgs e)
        {
            NewCar.ImageFiles = e.Files?.ToList() ?? new List<IFileEntry>();
            if (NewCar.ImageFiles.Count < 1)
                imageError = "请至少上传一张图片";
            else
                imageError = null;
        }

        private async Task GetListAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            //var result = await RegionsAppService.GetListAsync(Filter);
            //RegionList = result.Items;
            //TotalCount = (int)result.TotalCount;
        }

        private async Task CreateCarAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(imageError) || !await NewCarValidations.ValidateAll())
                {
                    return;
                }

                await CarAppService.CreateAsync(NewCar);
                //await GetRegionsAsync();
                await CloseCreateCarModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseCreateCarModalAsync()
        {
            NewCar = new CarCreateDto
            {
            };
            await CreateCarModal.Hide();
        }

        private Task OnProvinceChanged()
        {
            if (!string.IsNullOrWhiteSpace(NewCar.ProvincialCode))
            {
                SelectedProvince = ProvicnesList.FirstOrDefault(p => p.code == NewCar.ProvincialCode)!;
            }

            NewCar.CityCode = string.Empty;
            NewCar.DistrictCode = string.Empty;

            return Task.CompletedTask;
        }

        private Task OnCityChanged()
        {
            NewCar.DistrictCode = string.Empty;
            if (!string.IsNullOrWhiteSpace(NewCar.CityCode))
            {
                SelectedCity = Cities?.FirstOrDefault(p => p.code == NewCar.CityCode)!;
            }
            return Task.CompletedTask;
        }

        private Task OnDistrictChanged()
        {
            if (!string.IsNullOrWhiteSpace(NewCar.DistrictCode))
            {
                SelectedDistrict = Districts?.FirstOrDefault(p => p.code == NewCar.DistrictCode)!;
            }
            return Task.CompletedTask;
        }
    }
}