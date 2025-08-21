using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Reward.Infra.Context;

namespace Personal.Shopping.Services.Reward.Infra.Repositories;

public class RewardRepository
{
    private DbContextOptions<AppDbContext> _dbOptions;

    public RewardRepository(DbContextOptions<AppDbContext> dbOptions)
    {
        _dbOptions = dbOptions;
    }
}
