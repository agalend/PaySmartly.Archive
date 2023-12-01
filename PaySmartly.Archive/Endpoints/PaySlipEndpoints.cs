namespace PaySmartly.Archive.Endpoints
{
    public static class PaySlipEndpoints
    {
        public static PaySlipEndpoint HealthEndpoint = new("health", "health", "GET");
        public static PaySlipEndpoint GetEndpoint = new("archive/pay-slips/{id}", "get-pay-slip-by-id", "GET");
        public static PaySlipEndpoint DeleteEndpoint = new("archive/pay-slips/{id}", "delete-pay-slip-by-id", "DELETE");

        public static PaySlipEndpoint GetAllPaySlipsForEmployeeEndpoint = new("archive/pay-slips/employee", "get-all-pay-slips-for-employee", "GET");
        public static PaySlipEndpoint GetAllPaySlipsForSuperRateEndpoint = new("archive/pay-slips/super-rate", "get-all-pay-slips-for-super-rate", "GET");
        public static PaySlipEndpoint GetAllPaySlipsForAnnualSalaryEndpoint = new("archive/pay-slips/annual-salary", "get-all-pay-slips-for-annual-salary", "GET");
    }
}