namespace CityNest
{
    public interface IAgentsRepository
    {
        Task Add(Agent agent);
        Task Delete(Guid id);
        Task<List<Agent>> Get();
        Task<Agent> GetByEmail(string email);
        Task Update(Agent agent);
    }
}