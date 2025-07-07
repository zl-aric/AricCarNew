using AricCar.Regions;
using Blazorise;
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
        [MinLength(1, ErrorMessage = "品牌不能为空")]
        [MaxLength(100, ErrorMessage = "品牌长度不能超过100个字符")]
        public string Brand { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "型号不能为空")]
        [MaxLength(100, ErrorMessage = "型号长度不能超过100个字符")]
        public string Type { get; set; }

        [MaxLength(4000, ErrorMessage = "描述不能超过4000个字符")]
        public string? Description { get; set; }


        // 只读属性用于数量验证
        [Range(1, 10, ErrorMessage = "请至少上传一张图片，最多上传10张图片")]
        public int ImageCount => ImageFiles?.Count ?? 0;

        public List<IFileEntry> ImageFiles { get; set; } = [];

        public List<string> Images { get; set; } = [];
    }
}
