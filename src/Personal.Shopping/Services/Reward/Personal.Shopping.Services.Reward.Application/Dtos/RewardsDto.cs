namespace Personal.Shopping.Services.Reward.Application.Dtos;

public class RewardsDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime RewardsDate { get; set; } = DateTime.Now;
    public int RewardsActivity { get; set; }
    public int OrderId { get; set; }
}
