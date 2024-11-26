namespace CityNest
{
    public interface IJwtProvider
    {
        public string GenerateTokenForUser(User user);
        public string GenerateTokenForAgent(Agent agent);

    }
}
