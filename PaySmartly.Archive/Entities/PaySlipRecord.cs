namespace PaySmartly.Archive.Entities
{
    public record class PaySlipRecord : PaySlip
    {
        public PaySlipRecord(
            string id,
            PaySlip request)
                : base(request)
        {
            Id = id;
        }

        public string Id { get; }
    }
}