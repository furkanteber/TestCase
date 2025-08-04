using System.ComponentModel.DataAnnotations;
using TestCase.Entities.Common;

namespace TestCase.Entities
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "The product title cannot be blank.")]
        [StringLength(200, ErrorMessage = "The product length max 200 character.")]
        public string Title { get; set; }

        public string Description { get; set; }
        [Range(0, 1000)]
        public int Quantity { get ; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
