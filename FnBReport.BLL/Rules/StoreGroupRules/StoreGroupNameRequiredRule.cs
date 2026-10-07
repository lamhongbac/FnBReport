using FnBReport.BLL.Constants;
using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules.StoreGroupRules
{
    public class StoreGroupNameRequiredRule : IBusinessSaveRule<StoreGroup>
    {
        public bool IsApplicable(StoreGroup entity) => true;

        public Task<BOProcessResult> ValidateAsync(StoreGroup entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                return Task.FromResult(BOProcessResult.Failed(DomainErrorCodes.StoreGroup.NameEmpty));
            return Task.FromResult(BOProcessResult.Success());
        }
    }
}
