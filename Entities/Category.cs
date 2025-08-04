using System.ComponentModel.DataAnnotations;
using TestCase.Entities.Common;

namespace TestCase.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "The CategoryName not null.")]
        public string CategoryName { get; set; }
        [Range(0,1000)]
        public int MinimumStockQuantity { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
