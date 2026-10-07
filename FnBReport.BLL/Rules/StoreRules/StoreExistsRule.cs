using FnBReport.BLL.Constants;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules.StoreRules
{
    public class StoreExistsRule : IBusinessSaveRule<Store>
    {
        private readonly IStoreRepository _repo;
        public StoreExistsRule(IStoreRepository repo) => _repo = repo;

        public bool IsApplicable(Store entity) => entity.Id > 0;

        public async Task<BOProcessResult> ValidateAsync(Store entity)
        {
            var existing = await _repo.GetByIdAsync(entity.Id);
            if (existing == null)
                return BOProcessResult.Failed(DomainErrorCodes.Store.NotFound);
            return BOProcessResult.Success();
        }
    }
}
