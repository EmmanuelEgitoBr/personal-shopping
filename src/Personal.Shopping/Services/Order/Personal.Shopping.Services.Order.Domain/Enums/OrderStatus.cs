namespace Personal.Shopping.Services.Order.Domain.Enums;

public enum OrderStatus
{
    AWAITING_PAYMENT,
    APPROVED,
    IN_SEPARATION,
    INVOICED,
    SHIPPED,
    DELIVERED,
    CANCELED,
}
