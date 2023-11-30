using PaySmartly.Archive.HATEOAS;

namespace PaySmartly.Archive.Entities
{
    public record class PaySlipResponse(
            string Id,
            EmployeeIdentity Employee,
            double AnnualSalary,
            double SuperRate,
            DateTime PayPeriod,
            double GrossIncome,
            double IncomeTax,
            double NetIncome,
            double Super,
            RequesterIdentity Requester,
            IEnumerable<Link> Links);
}