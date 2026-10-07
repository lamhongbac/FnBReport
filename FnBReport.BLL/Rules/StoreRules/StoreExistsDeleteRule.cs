using FnBReport.BLL.Constants;
using FnBReport.DAL.Interfaces;
using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules.StoreRules
{
    public class StoreExistsDeleteRule : IBusinessDeleteRule<int>
    {
        private readonly IStoreRepository _repo;
        public StoreExistsDeleteRule(IStoreRepository repo) => _repo = repo;

        public bool IsApplicable(int id) => true;

        public async Task<BOProcessResult> ValidateDeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return BOProcessResult.Failed(DomainErrorCodes.Store.NotFound);
            return BOProcessResult.Success();
        }
    }
}
