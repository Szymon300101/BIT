namespace WebApi.Hubs.Clients
{
    public interface IInitiativeClient
    {
        Task RefreshCreatures();
        Task RefreshInitiative();
    }
}
