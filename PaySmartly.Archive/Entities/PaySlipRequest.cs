namespace PaySmartly.Archive.Entities
{
    public record class PaySlipRequest(
                EmployeeIdentity Employee,
                double AnnualSalary,
                double SuperRate,
                DateTime PayPeriod,
                int RoundTo,
                int Months,
                RequesterIdentity Requester);
}