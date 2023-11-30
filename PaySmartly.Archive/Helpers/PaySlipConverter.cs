using Google.Protobuf.WellKnownTypes;
using PaySmartly.Archive.Entities;
using PaySmartly.Archive.HATEOAS;
using PaySmartly.Persistance;

namespace PaySmartly.Archive.Helpers
{
    public static class PaySlipConverter
    {
        public static PaySlipResponse Convert(PaySlipRecord record, IEnumerable<Link> links)
        {
            return new(
                record.Id,
                record.Employee,
                record.AnnualSalary,
                record.SuperRate,
                record.PayPeriod,
                record.GrossIncome,
                record.IncomeTax,
                record.NetIncome,
                record.Super,
                record.Requester,
                links);
        }

        public static PaySlipRecord Convert(Record record)
        {
            PaySlipRequest request = new(
                new EmployeeIdentity(record.EmployeeFirstName, record.EmployeeLastName),
                record.AnnualSalary,
                record.SuperRate,
                record.PayPeriodTo.ToDateTime(),
                record.RoundTo,
                record.Months,
                new RequesterIdentity(record.RequesterFirstName, record.RequesterLastName)
            );

            PaySlip paySlip = new(
                request,
                record.GrossIncome,
                record.IncomeTax,
                record.NetIncome,
                record.Super
            );

            PaySlipRecord paySlipRecord = new(record.Id, paySlip);
            return paySlipRecord;
        }

        public static CreateRequest Convert(PaySlip paySlip, DateTime createdAt)
        {
            return new CreateRequest
            {
                Record = new()
                {
                    EmployeeFirstName = paySlip.Employee.FirstName,
                    EmployeeLastName = paySlip.Employee.LastName,
                    AnnualSalary = paySlip.AnnualSalary,
                    SuperRate = paySlip.SuperRate,
                    PayPeriodFrom = paySlip.PayPeriod.ToTimestamp(),
                    PayPeriodTo = paySlip.PayPeriod.ToTimestamp(),
                    RoundTo = paySlip.RoundTo,
                    Months = paySlip.Months,
                    GrossIncome = paySlip.GrossIncome,
                    IncomeTax = paySlip.IncomeTax,
                    NetIncome = paySlip.NetIncome,
                    Super = paySlip.Super,
                    RequesterFirstName = paySlip.Requester.FirstName,
                    RequesterLastName = paySlip.Requester.LastName,
                    CreatedAt = Timestamp.FromDateTime(createdAt)
                }
            };
        }
    }
}