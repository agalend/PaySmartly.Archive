using static PaySmartly.Archive.WebApplicationFactory;

namespace PaySmartly.Archive
{
    public class App
    {
        public void Run(string[] args)
        {
            WebApplication app = CreateWebApplication(args);
            ApiFacade facade = new(app);

            facade.RegisterHealthMethod();
            facade.RegisterGetMethod();
            facade.RegisterDeleteMethod();
            facade.RegisterGetAllForEmployeeMethod();
            facade.RegisterGetAllForSuperRateMethod();
            facade.RegisterGetAllForAnnualSalaryEndpointMethod();

            facade.Run();
        }
    }
}