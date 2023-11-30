using PaySmartly.Archive.Entities;
using PaySmartly.Archive.Filters;
using PaySmartly.Archive.HATEOAS;

using static PaySmartly.Archive.Helpers.PaySlipConverter;
using static PaySmartly.Archive.Endpoints.PaySlipEndpoints;

namespace PaySmartly.Archive
{
    // TODO: add proper configuration
    // TODO: add docker (publish the image to docker hub)
    // TODO: write more unit tests
    // TODO: add integration tests
    // TODO: add github actions
    // TODO: add distributed logging
    public class ApiFacade(WebApplication app)
    {
        private readonly WebApplication app = app;

        public void RegisterGetPaySlipMethod()
        {
            app.MapGet(GetEndpoint.Pattern, async (string id, IManager manager, HttpContext context, LinkGenerator linkGenerator) =>
            {
                PaySlipRecord? paySlip = await manager.GetPaySlip(id);

                if (paySlip is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    IEnumerable<Link> links =
                    [
                        new (linkGenerator.GetUriByName(context, GetEndpoint.Name, values: new{paySlip.Id}),"self", GetEndpoint.Method),
                        new (linkGenerator.GetUriByName(context, DeleteEndpoint.Name, values: new{paySlip.Id}), DeleteEndpoint.Name, DeleteEndpoint.Method)
                    ];

                    PaySlipResponse response = Convert(paySlip, links);

                    return Results.Ok(response); ;
                }
            })
            .WithName(GetEndpoint.Name)
            .WithOpenApi()
            .AddEndpointFilter<GetPaySlipValidator>();
        }

        // There is no UpdatePaySlipMethod intentionally 

        public void RegisterDeletePaySlipMethod()
        {
            app.MapDelete(DeleteEndpoint.Pattern, async (string id, IManager manager) =>
            {
                PaySlipRecord? paySlip = await manager.DeletePaySlip(id);

                if (paySlip is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    PaySlipResponse response = Convert(paySlip, new List<Link>());
                    return Results.Ok(response);
                }

            })
            .WithName(DeleteEndpoint.Name)
            .WithOpenApi()
            .AddEndpointFilter<DeletePaySlipValidator>();
        }

        public void Run()
        {
            app.Run();
        }
    }
}