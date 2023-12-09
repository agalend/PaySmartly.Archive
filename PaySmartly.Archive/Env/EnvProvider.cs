namespace PaySmartly.Archive.Env
{
    public interface IEnvProvider
    {
        string? GetPersistanceClientUrl();
    }

    public class EnvProvider(GrpcClients? grpcClients) : IEnvProvider
    {
        private const string PERSISTANCE_URL = "PERSISTANCE_URL";

        private readonly string? defaultPersistanceUrl = grpcClients?.Persistance?.Url;

        public string? GetPersistanceClientUrl()
        {
            string? url = Environment.GetEnvironmentVariable(PERSISTANCE_URL);
            url ??= defaultPersistanceUrl;

            return url;
        }
    }
}