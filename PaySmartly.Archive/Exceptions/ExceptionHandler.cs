namespace PaySmartly.Archive.Exceptions
{
    public class ExceptionHandler
    {
        public IApplicationBuilder Build(IApplicationBuilder exceptionHandlerApp)
        {
            exceptionHandlerApp.Run(async context =>
            {
                await Results.Problem().ExecuteAsync(context);
            });

            return exceptionHandlerApp;
        }
    }
}