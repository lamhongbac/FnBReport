using FnBReport.BLL.Constants;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules.StoreRules
{
    public class StoreNumberUniqueRule : IBusinessSaveRule<Store>
    {
        private readonly IStoreRepository _repo;

        public StoreNumberUniqueRule(IStoreRepository repo)
        {
            _repo = repo;
        }

        public bool IsApplicable(Store entity) => true;

        public async Task<BOProcessResult> ValidateAsync(Store entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Number))
                return BOProcessResult.Failed(DomainErrorCodes.Store.NumberEmpty);

            var existing = await _repo.GetByCodeAsync(entity.Number);
            if (existing != null && existing.Id != entity.Id)
            {
                return BOProcessResult.Failed(DomainErrorCodes.Store.NumberExists);
            }

            return BOProcessResult.Success();
        }
    }
}
