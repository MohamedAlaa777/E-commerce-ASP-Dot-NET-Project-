using ECommerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.ApiModels
{
    public class ItemDto
    {
        [Required(ErrorMessage = "Please enter item name")]
        public string ItemName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter Sales price")]
        [DataType(DataType.Currency, ErrorMessage = "please enter currency")]
        [Range(50, 100000, ErrorMessage = "please enter price in system range")]
        public decimal SalesPrice { get; set; }
        [Required(ErrorMessage = "Please enter Purchase price")]
        [DataType(DataType.Currency, ErrorMessage = "please enter currency")]
        [Range(50, 100000, ErrorMessage = "please enter Purchase price in system range")]
        public decimal PurchasePrice { get; set; }
        [Required(ErrorMessage = "Please enter category")]
        public int CategoryId { get; set; }
        public string? ImageName { get; set; }
        public string? Description { get; set; }
        public string? Gpu { get; set; }
        public string? HardDisk { get; set; }
        [Required(ErrorMessage = "Please enter item type")]
        public int? ItemTypeId { get; set; }
        public string? Processor { get; set; }
        [Required(ErrorMessage = "Please enter ram size")]
        [Range(1, 500, ErrorMessage = "please enter ram in ragne")]
        public int? RamSize { get; set; }
        public string? ScreenReslution { get; set; }
        public string? ScreenSize { get; set; }
        public string? Weight { get; set; }
        [Required(ErrorMessage = "Please enter os")]
        public int? OsId { get; set; }
        //public string? Os { get; set; }
        //public List<string> Customer { get; set; } = null!;
        //public string? ItemType { get; set; }
    }
}
