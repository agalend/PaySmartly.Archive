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
                record.PayPeriodFrom,
                record.PayPeriodTo,
                record.GrossIncome,
                record.IncomeTax,
                record.NetIncome,
                record.Super,
                record.Requester,
                record.CreatedAt,
                links);
        }

        public static PaySlipRecord Convert(Record record)
        {
            PaySlipRequest request = new(
                new EmployeeIdentity(record.EmployeeFirstName, record.EmployeeLastName),
                record.AnnualSalary,
                record.SuperRate,
                record.PayPeriodFrom.ToDateTime(),
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
                record.Super,
                record.CreatedAt.ToDateTime()
            );

            PaySlipRecord paySlipRecord = new(record.Id, paySlip);
            return paySlipRecord;
        }
    }
}