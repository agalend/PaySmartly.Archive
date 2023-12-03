namespace PaySmartly.Archive.Filters
{
    public class GetAllForAnnualSalaryValidator : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var from = context.GetArgument<int>(0);
            if (from < 0)
            {
                return Results.BadRequest($"Invalid {nameof(from)} value, you should provide a positive integer value");
            }

            var to = context.GetArgument<int>(1);
            if (to < 0)
            {
                return Results.BadRequest($"Invalid {nameof(to)} value, you should provide a positive integer value");
            }

            var limit = context.GetArgument<int>(2);
            if (limit < 0)
            {
                return Results.BadRequest($"Invalid {nameof(limit)} value, you should provide a positive integer value");
            }

            var offset = context.GetArgument<int>(3);
            if (offset < 0)
            {
                return Results.BadRequest($"Invalid {nameof(offset)} value, you should provide a positive integer value");
            }

            return await next(context);
        }
    }
}