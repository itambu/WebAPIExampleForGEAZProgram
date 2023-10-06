using System.ComponentModel.DataAnnotations;

namespace WebApplication15.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
    }
}
