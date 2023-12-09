using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PaySmartly.Archive.Env;
using PaySmartly.Archive.Exceptions;
using PaySmartly.Archive.Persistance;
using static PaySmartly.Persistance.Persistance;

namespace PaySmartly.Archive
{
    public static class WebApplicationFactory
    {
        private static readonly string ServiceName = "Archive Service";

        public static WebApplication CreateWebApplication(string[] args)
        {
            // will use CreateSlimBuilder in order to be prepared for an AOT compilation
            WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);
            AddOpenTelemetryLogging(builder);
            AddServices(builder);

            WebApplication app = builder.Build();
            AddExceptionHandling(app);
            AddSwagger(app);
            return app;
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            AddOpenTelemetryService(builder);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            IConfigurationSection grpcClientsSection = builder.Configuration.GetSection("GrpcClients");
            var grpcClients = grpcClientsSection.Get<GrpcClients>();

            IEnvProvider envProvider = new EnvProvider(grpcClients);
            string? persistanceUrl = envProvider?.GetPersistanceClientUrl();

            builder.Services.AddGrpcClient<PersistanceClient>(options => options.Address = new Uri(persistanceUrl!));
            builder.Services.AddScoped<IPersistance, Persistance.Persistance>();
        }

        private static void AddOpenTelemetryLogging(WebApplicationBuilder builder)
        {
            builder.Logging.AddOpenTelemetry(options =>
            {
                ResourceBuilder resourceBuilder = ResourceBuilder.CreateDefault().AddService(ServiceName);

                options.SetResourceBuilder(resourceBuilder).AddConsoleExporter();
            });
        }

        private static void AddOpenTelemetryService(WebApplicationBuilder builder)
        {
            OpenTelemetryBuilder openTelemetryBuilder = builder.Services.AddOpenTelemetry();

            openTelemetryBuilder = openTelemetryBuilder.ConfigureResource(resource => resource.AddService(ServiceName));

            openTelemetryBuilder = openTelemetryBuilder.WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation().AddConsoleExporter();
            });
            
            openTelemetryBuilder = openTelemetryBuilder.WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation().AddConsoleExporter();
            });
        }

        private static void AddExceptionHandling(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                ExceptionHandler handler = new();
                app.UseExceptionHandler(exceptionHandlerApp => handler.Build(exceptionHandlerApp));
            }
        }

        private static void AddSwagger(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}