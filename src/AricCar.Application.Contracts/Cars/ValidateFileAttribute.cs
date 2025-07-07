using Blazorise;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AricCar.Cars
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public int MinCount { get; set; } = 1;
        public long MaxFileSize { get; set; } = 10 * 1024 * 1024; // 10MB

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage ?? $"请至少上传 {MinCount} 张图片");

            if (value is List<IFileEntry> files)
            {
                if (files.Count < MinCount)
                    return new ValidationResult(ErrorMessage ?? $"请至少上传 {MinCount} 张图片");

                foreach (var file in files)
                {
                    if (file.Size > MaxFileSize)
                        return new ValidationResult($"文件 {file.Name} 大小超过限制");
                }
            }

            return ValidationResult.Success;
        }
    }
}