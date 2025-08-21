namespace Personal.Shopping.Services.Reward.Application.Dtos;

public class RewardsDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = Guid.NewGuid().ToString();
    public DateTime RewardsDate { get; set; }
    public int RewardsActivity { get; set; }
    public int OrderId { get; set; }
}
