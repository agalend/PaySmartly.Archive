using PaySmartly.Archive.Entities;
using PaySmartly.Archive.Filters;
using PaySmartly.Archive.HATEOAS;

using static PaySmartly.Archive.Helpers.PaySlipConverter;
using static PaySmartly.Archive.Endpoints.PaySlipEndpoints;
using PaySmartly.Archive.Persistance;

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

        public void Run() => app.Run();

        public void RegisterGetPaySlipMethod()
        {
            app.MapGet(GetEndpoint.Pattern, async (string id, IPersistance persistance, HttpContext context, LinkGenerator linkGenerator) =>
            {
                PaySlipRecord? record = await persistance.Get(id);

                if (record is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    IEnumerable<Link> links =
                    [
                        new (linkGenerator.GetPathByName(context, GetEndpoint.Name, values: new{record.Id}), "self", GetEndpoint.Method),
                        new (linkGenerator.GetPathByName(context, DeleteEndpoint.Name, values: new{record.Id}), DeleteEndpoint.Name, DeleteEndpoint.Method)
                    ];

                    PaySlipResponse response = Convert(record, links);

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
            app.MapDelete(DeleteEndpoint.Pattern, async (string id, IPersistance persistance) =>
            {
                bool deleted = await persistance.Delete(id);

                return !deleted
                    ? Results.NotFound()
                    : Results.NoContent();

            })
            .WithName(DeleteEndpoint.Name)
            .WithOpenApi()
            .AddEndpointFilter<DeletePaySlipValidator>();
        }

        public void RegisterGetAllPaySlipsForEmployeeMethod()
        {
            app.MapGet(GetAllPaySlipsForEmployeeEndpoint.Pattern, async (
                string firstName,
                string lastName,
                int limit,
                int offset,
                IPersistance persistance,
                HttpContext context,
                LinkGenerator linkGenerator) =>
            {
                IEnumerable<PaySlipRecord> records = await persistance.GetAllForEmployee(firstName, lastName, limit, offset);

                IEnumerable<PaySlipResponse> responses = GerResponses(records, context, linkGenerator);

                return Results.Ok(responses);
            })
            .WithName(GetAllPaySlipsForEmployeeEndpoint.Name)
            .WithOpenApi();
            // .AddEndpointFilter<DeletePaySlipValidator>(); // TODO:
        }

        public void RegisterGetAllPaySlipsForSuperRateMethod()
        {
            app.MapGet(GetAllPaySlipsForSuperRateEndpoint.Pattern, async (
                int from,
                int to,
                int limit,
                int offset,
                IPersistance persistance,
                HttpContext context,
                LinkGenerator linkGenerator) =>
            {
                IEnumerable<PaySlipRecord> records = await persistance.GetAllForSuperRate(from, to, limit, offset);

                IEnumerable<PaySlipResponse> responses = GerResponses(records, context, linkGenerator);

                return Results.Ok(responses);
            })
            .WithName(GetAllPaySlipsForSuperRateEndpoint.Name)
            .WithOpenApi();
            // .AddEndpointFilter<DeletePaySlipValidator>(); // TODO:
        }
        public void RegisterGetAllPaySlipsForAnnualSalaryEndpointMethod()
        {
            app.MapGet(GetAllPaySlipsForAnnualSalaryEndpoint.Pattern, async (
                int from,
                int to,
                int limit,
                int offset,
                IPersistance persistance,
                HttpContext context,
                LinkGenerator linkGenerator) =>
            {
                IEnumerable<PaySlipRecord> records = await persistance.GetAllForAnnualSalary(from, to, limit, offset);

                IEnumerable<PaySlipResponse> responses = GerResponses(records, context, linkGenerator);

                return Results.Ok(responses);
            })
            .WithName(GetAllPaySlipsForAnnualSalaryEndpoint.Name)
            .WithOpenApi();
            // .AddEndpointFilter<DeletePaySlipValidator>(); // TODO:
        }

        private IEnumerable<PaySlipResponse> GerResponses(
            IEnumerable<PaySlipRecord> records,
            HttpContext context,
            LinkGenerator linkGenerator)
        {
            List<PaySlipResponse> responses = [];

            foreach (var record in records)
            {
                IEnumerable<Link> links =
                [
                    new (linkGenerator.GetPathByName(context, GetEndpoint.Name, values: new{record.Id}), GetEndpoint.Name, GetEndpoint.Method),
                    new (linkGenerator.GetPathByName(context, DeleteEndpoint.Name, values: new{record.Id}), DeleteEndpoint.Name, DeleteEndpoint.Method)
                ];

                PaySlipResponse response = Convert(record, links);
                responses.Add(response);
            }

            return responses;
        }
    }
}