namespace Personal.Shopping.Services.Order.Application.Dtos.Reward;

public class RewardsDto
{
    public string? UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public int RewardsActivity {  get; set; }
    public int OrderId { get; set; }
}
