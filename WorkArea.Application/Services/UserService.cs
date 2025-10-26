using Microsoft.EntityFrameworkCore;
using WorkArea.Domain.Entities;
using WorkArea.Persistence.Repositories;

namespace WorkArea.Application.Services;

public class UserService(IRepository<User> userRepository)
{
    public async Task<int> Total()
    {
        return await userRepository.ListQueryableNoTracking.CountAsync();
    }
}