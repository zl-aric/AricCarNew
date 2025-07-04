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
        [StringLength(100, MinimumLength = 1, ErrorMessage = "请输入品牌，长度必须在1到100个字符之间")]
        public string Brand { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "请输入型号，长度必须在1到100个字符之间")]
        public string Type { get; set; }

        [StringLength(4000, ErrorMessage = "描述不能超过4000个字符")]
        public string? Description { get; set; }

        // 修改为 IFileEntry 列表
        [ValidateFile(MinCount = 1, ErrorMessage = "请至少上传一张图片")]
        public List<IFileEntry> ImageFiles { get; set; } = new();

        public List<string> Images { get; set; } = [];
    }
}
