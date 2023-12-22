using System.ComponentModel.DataAnnotations;

namespace ItemStore.Dtos
{
    public class ItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
