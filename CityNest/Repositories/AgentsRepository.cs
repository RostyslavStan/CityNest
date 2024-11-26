using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CityNest.Interfaces;


namespace CityNest
{
    public class AgentsRepository(RealStateDbContext dbContext) : IAgentsRepository
    {
        public async Task<List<Agent>> Get()
        {
            return await dbContext.Agents.AsNoTracking().ToListAsync();
        }

        public async Task<Agent> GetByEmail(string email)
        {
            var agent = await dbContext.Agents.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return agent;
        }

        public async Task Add(Agent agent)
        {
            await dbContext.AddAsync(agent);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Agent agent)
        {
            await dbContext.Agents
            .Where(c => c.Id == agent.Id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(c => c.Name, agent.Name)
            .SetProperty(c => c.Email, agent.Email)
            .SetProperty(c => c.PasswordHash, agent.PasswordHash)
            .SetProperty(c => c.PhoneNumber, agent.PhoneNumber));
        }

        public async Task Delete(Guid id)
        {
            await dbContext.Agents
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }
    }
}