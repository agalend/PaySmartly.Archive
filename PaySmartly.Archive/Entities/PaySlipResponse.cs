using PaySmartly.Archive.HATEOAS;

namespace PaySmartly.Archive.Entities
{
    public record class PaySlipResponse(
            string Id,
            EmployeeIdentity Employee,
            double AnnualSalary,
            double SuperRate,
            DateTime PayPeriodFrom,
            DateTime PayPeriodTo,
            double GrossIncome,
            double IncomeTax,
            double NetIncome,
            double Super,
            RequesterIdentity Requester,
            DateTime CreatedAt,
            IEnumerable<Link> Links);
}