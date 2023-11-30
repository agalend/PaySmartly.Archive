namespace PaySmartly.Archive.Endpoints
{
    static class PaySlipEndpoints
    {
        public static PaySlipEndpoint CreateEndpoint = new("/payslips", "createPaySlip", "POST");
        public static PaySlipEndpoint GetEndpoint = new("/payslips/{id}", "getPaySlipById", "GET");
        public static PaySlipEndpoint DeleteEndpoint = new("/payslips/{id}", "deletePaySlipById", "DELETE");
    }
}