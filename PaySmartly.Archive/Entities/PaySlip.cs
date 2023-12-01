namespace PaySmartly.Archive.Entities
{
   public record class PaySlip : PaySlipRequest
    {
        public PaySlip(
            PaySlipRequest request,
            double grossIncome,
            double incomeTax,
            double netIncome,
            double super,
            DateTime createdAt)
                : base(request)
        {
            GrossIncome = grossIncome;
            IncomeTax = incomeTax;
            NetIncome = netIncome;
            Super = super;
            CreatedAt = createdAt;
        }

        public double GrossIncome { get; }
        public double IncomeTax { get; }
        public double NetIncome { get; }
        public double Super { get; }
        public DateTime CreatedAt { get; }
    }
}