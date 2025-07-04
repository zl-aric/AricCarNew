using AricCar.Regions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AricCar.Cars
{
    public class CarCreateDto
    {
        [Required(ErrorMessage = "请选择省份")]
        public string ProvincialCode { get; set; }

        [Required(ErrorMessage = "请选择城市")]
        public string CityCode { get; set; }

        [Required(ErrorMessage = "请选择区域")]
        public string DistrictCode { get; set; }

        [Required]
        [StringLength(CarConsts.MaxBranchLength, MinimumLength = CarConsts.MinBranchLength, ErrorMessage = "请输入品牌，长度必须在{2}到{1}个字符之间")]
        public string Brand { get; set; }

        [Required]
        [StringLength(CarConsts.MaxTypeLength, MinimumLength = CarConsts.MinTypeLength, ErrorMessage = "请输入型号，长度必须在{2}到{1}个字符之间")]
        public string Type { get; set; }

        [StringLength(CarConsts.MaxDescriptionLength, ErrorMessage = "描述不能超过{1}个字符")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "请至少上传一张图片")]
        public List<string> Images { get; set; } = [];
    }
}
