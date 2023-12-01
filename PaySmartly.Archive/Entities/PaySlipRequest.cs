namespace PaySmartly.Archive.Entities
{
     public record class PaySlipRequest(
                EmployeeIdentity Employee,
                double AnnualSalary,
                double SuperRate,
                DateTime PayPeriodFrom,
                DateTime PayPeriodTo,
                int RoundTo,
                int Months,
                RequesterIdentity Requester);
}