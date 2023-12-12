namespace PaySmartly.Archive.Env
{
    public interface IEnvProvider
    {
        string? GetPersistanceUrl();
    }

    public class EnvProvider(GrpcClients? grpcClients) : IEnvProvider
    {
        private const string PERSISTENCE_URL = "PERSISTENCE_URL";

        private readonly string? defaultPersistenceUrl = grpcClients?.Persistence?.Url;

        public string? GetPersistanceUrl()
        {
            string? url = Environment.GetEnvironmentVariable(PERSISTENCE_URL);
            url ??= defaultPersistenceUrl;

            return url;
        }
    }
}