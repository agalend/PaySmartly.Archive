using static PaySmartly.Archive.WebApplicationFactory;

namespace PaySmartly.Archive
{
    public class App
    {
        public void Run(string[] args)
        {
            WebApplication app = CreateWebApplication(args);
            ApiFacade facade = new(app);

            facade.RegisterGetPaySlipMethod();
            facade.RegisterDeletePaySlipMethod();

            facade.Run();
        }
    }
}