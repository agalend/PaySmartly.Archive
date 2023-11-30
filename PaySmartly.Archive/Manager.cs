using PaySmartly.Archive.Entities;
using PaySmartly.Archive.Persistance;

namespace PaySmartly.Archive
{
    public interface IManager
    {
        Task<PaySlipRecord?> GetPaySlip(string recordId);
        Task<PaySlipRecord?> DeletePaySlip(string recordId);
    }

    public class Manager(IPersistance persistance) : IManager
    {
        private readonly IPersistance persistance = persistance;

        public async Task<PaySlipRecord?> GetPaySlip(string recordId)
        {
            PaySlipRecord? record = await persistance.Get(recordId);
            return record;
        }

        public async Task<PaySlipRecord?> DeletePaySlip(string recordId)
        {
            PaySlipRecord? record = await persistance.Delete(recordId);
            return record;
        }
    }
}