using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.Services.Product.Domain.Entities;

public class Category
{
    [Key]
    public int CategoryNameId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
