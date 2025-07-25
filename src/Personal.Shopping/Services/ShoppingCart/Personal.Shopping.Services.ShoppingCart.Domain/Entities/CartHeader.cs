﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Shopping.Services.ShoppingCart.Domain.Entities;

public class CartHeader
{
    [Key]
    public int CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }

    [NotMapped]
    public double Discount { get; set; }

    [NotMapped]
    public double CartTotal { get; set; }

    [NotMapped]
    public string? FirstName { get; set; }

    [NotMapped]
    public string? LastName { get; set; }

    [NotMapped]
    public string? Email { get; set; }

    [NotMapped]
    public string? Phone { get; set; }
}
