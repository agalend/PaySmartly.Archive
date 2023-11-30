using PaySmartly.Archive.Entities;
using PaySmartly.Persistance;
using static PaySmartly.Persistance.Persistance;
using static PaySmartly.Archive.Helpers.PaySlipConverter;

namespace PaySmartly.Archive.Persistance
{
    public interface IPersistance
    {
        Task<PaySlipRecord?> Get(string id);
        Task<bool> Delete(string id);

        Task<IEnumerable<PaySlipRecord>> GetAllForEmployee(string firstName, string lastName, int limit, int offset);
        Task<IEnumerable<PaySlipRecord>> GetAllForSuperRate(double from, double to, int limit, int offset);
        Task<IEnumerable<PaySlipRecord>> GetAllForAnnualSalary(double from, double to, int limit, int offset);
    }

    public class Persistance(PersistanceClient client) : IPersistance
    {
        private readonly PersistanceClient persistanceClient = client;

        public async Task<PaySlipRecord?> Get(string recordId)
        {
            GetRequest request = new() { Id = recordId };
            Response response = await persistanceClient.GetAsync(request);

            return !response.Exists ? default : Convert(response.Record);
        }

        public async Task<bool> Delete(string recordId)
        {
            DeleteRequest request = new() { Id = recordId };
            DeleteResponse response = await persistanceClient.DeleteAsync(request);

            return response.Count > 0;
        }

        public async Task<IEnumerable<PaySlipRecord>> GetAllForEmployee(string firstName, string lastName, int limit, int offset)
        {
            GetAllForEmployeeRequest request = new() { FirstName = firstName, LastName = lastName, Limit = limit, Offset = offset };
            GetAllResponse response = await persistanceClient.GetAllForEmployeeAsync(request);

            return !response.Exists
                ? Enumerable.Empty<PaySlipRecord>()
                : response.Records.Select(Convert);
        }

        public async Task<IEnumerable<PaySlipRecord>> GetAllForSuperRate(double from, double to, int limit, int offset)
        {
            GetAllForSuperRateRequest request = new() { From = from, To = to, Limit = limit, Offset = offset };
            GetAllResponse response = await persistanceClient.GetAllForSuperRateAsync(request);

            return !response.Exists
                ? Enumerable.Empty<PaySlipRecord>()
                : response.Records.Select(Convert);
        }

        public async Task<IEnumerable<PaySlipRecord>> GetAllForAnnualSalary(double from, double to, int limit, int offset)
        {
            GetAllForAnnualSalaryRequest request = new() { From = from, To = to, Limit = limit, Offset = offset };
            GetAllResponse response = await persistanceClient.GetAllForAnnualSalaryAsync(request);

            return !response.Exists
                ? Enumerable.Empty<PaySlipRecord>()
                : response.Records.Select(Convert);
        }
    }
}