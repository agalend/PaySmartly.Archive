namespace PaySmartly.Archive.Endpoints
{
    public static class PaySlipEndpoints
    {
        public static PaySlipEndpoint GetEndpoint = new("/pay-slips/{id}", "getPaySlipById", "GET");
        public static PaySlipEndpoint DeleteEndpoint = new("/pay-slips/{id}", "deletePaySlipById", "DELETE");

        public static PaySlipEndpoint GetAllPaySlipsForEmployeeEndpoint = new("/pay-slips/employee", "get-all-pay-slips-for-employee", "GET");
        public static PaySlipEndpoint GetAllPaySlipsForSuperRateEndpoint = new("/pay-slips/super-rate", "get-all-pay-slips-for-super-rate", "GET");
        public static PaySlipEndpoint GetAllPaySlipsForAnnualSalaryEndpoint = new("/pay-slips/annual-salary", "get-all-pay-slips-for-annual-salary", "GET");
    }
}