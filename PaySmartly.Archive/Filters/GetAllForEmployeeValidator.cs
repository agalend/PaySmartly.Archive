namespace PaySmartly.Archive.Filters
{
    public class GetAllForEmployeeValidator : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var firstName = context.GetArgument<string>(0);
            if (string.IsNullOrEmpty(firstName))
            {
                return Results.BadRequest($"Invalid {nameof(firstName)} value, you should provide a valid non empty string value");
            }

            var lastName = context.GetArgument<string>(1);
            if (string.IsNullOrEmpty(lastName))
            {
                return Results.BadRequest($"Invalid {nameof(lastName)} value, you should provide a valid non empty string value");
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