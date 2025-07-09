using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Shopping.Services.ShoppingCart.Domain.Entities;

public class CartDetail
{
    [Key]
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    
    public int ProductId { get; set; }

    [NotMapped]
    public Product Product { get; set; }
    public int Count { get; set; }
}
