using FnBReport.BLL.Constants;
using FnBReport.BLL.Rules;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnBReport.BLL.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepo;
        private readonly IStoreGroupRepository _groupRepo;
        
        private readonly IEnumerable<IBusinessSaveRule<Store>> _storeSaveRules;
        private readonly IEnumerable<IBusinessSaveRule<StoreGroup>> _groupSaveRules;
        private readonly IEnumerable<IBusinessDeleteRule<int>> _storeDeleteRules;

        public StoreService(
            IStoreRepository storeRepo, 
            IStoreGroupRepository groupRepo, 
            IEnumerable<IBusinessSaveRule<Store>> storeSaveRules = null,
            IEnumerable<IBusinessSaveRule<StoreGroup>> groupSaveRules = null,
            IEnumerable<IBusinessDeleteRule<int>> storeDeleteRules = null)
        {
            _storeRepo = storeRepo;
            _groupRepo = groupRepo;
            _storeSaveRules = storeSaveRules ?? new List<IBusinessSaveRule<Store>>();
            _groupSaveRules = groupSaveRules ?? new List<IBusinessSaveRule<StoreGroup>>();
            _storeDeleteRules = storeDeleteRules ?? new List<IBusinessDeleteRule<int>>();
        }

        private async Task<BOProcessResult> ValidateStoreAsync(Store store)
        {
            foreach (var rule in _storeSaveRules.Where(r => r.IsApplicable(store)))
            {
                var ruleResult = await rule.ValidateAsync(store);
                if (!ruleResult.IsSuccess) return ruleResult; 
            }
            return BOProcessResult.Success();
        }

        private async Task<BOProcessResult> ValidateStoreGroupAsync(StoreGroup group)
        {
            foreach (var rule in _groupSaveRules.Where(r => r.IsApplicable(group)))
            {
                var ruleResult = await rule.ValidateAsync(group);
                if (!ruleResult.IsSuccess) return ruleResult; 
            }
            return BOProcessResult.Success();
        }

        private async Task<BOProcessResult> ValidateDeleteStoreAsync(int id)
        {
            foreach (var rule in _storeDeleteRules.Where(r => r.IsApplicable(id)))
            {
                var ruleResult = await rule.ValidateDeleteAsync(id);
                if (!ruleResult.IsSuccess) return ruleResult; 
            }
            return BOProcessResult.Success();
        }

        public async Task<IEnumerable<StoreGroup>> GetAllStoreGroupsAsync() => await _groupRepo.GetAllAsync();

        public async Task<BOProcessResult> CreateStoreGroupAsync(StoreGroup group, string createdBy)
        {
            var valResult = await ValidateStoreGroupAsync(group);
            if (!valResult.IsSuccess) return valResult;

            group.CreatedBy = createdBy;
            await _groupRepo.AddAsync(group);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> UpdateStoreGroupAsync(StoreGroup group, string updatedBy)
        {
            var valResult = await ValidateStoreGroupAsync(group);
            if (!valResult.IsSuccess) return valResult;

            var existing = await _groupRepo.GetByIdAsync(group.Id);
            if (existing == null) return BOProcessResult.Failed(DomainErrorCodes.StoreGroup.NotFound);

            existing.Name = group.Name;
            existing.Number = group.Number;
            existing.ModifiedBy = updatedBy;

            await _groupRepo.UpdateAsync(existing);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> DeleteStoreGroupAsync(int id, string deletedBy)
        {
            await _groupRepo.DeleteSoftAsync(id, deletedBy);
            return BOProcessResult.Success();
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync() => await _storeRepo.GetAllAsync();

        public async Task<Store?> GetStoreByIdAsync(int id) => await _storeRepo.GetByIdAsync(id);

        public async Task<BOProcessResult> CreateStoreAsync(Store store, string createdBy)
        {
            var valResult = await ValidateStoreAsync(store);
            if (!valResult.IsSuccess) return valResult;

            store.CreatedBy = createdBy;
            await _storeRepo.AddAsync(store);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> UpdateStoreAsync(Store store, string updatedBy)
        {
            var valResult = await ValidateStoreAsync(store);
            if (!valResult.IsSuccess) return valResult;

            // Vì StoreExistsRule đã đảm bảo id tồn tại ở trên, existing chắc chắn != null
            var existing = await _storeRepo.GetByIdAsync(store.Id);
            
            existing!.Name = store.Name;
            existing.GroupId = store.GroupId;
            existing.NumOfTables = store.NumOfTables;
            existing.MinMenuSelling = store.MinMenuSelling;
            existing.MinSalesContribution = store.MinSalesContribution;
            existing.ModifiedBy = updatedBy;

            await _storeRepo.UpdateAsync(existing);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> DeleteStoreAsync(int id, string deletedBy)
        {
            var valResult = await ValidateDeleteStoreAsync(id);
            if (!valResult.IsSuccess) return valResult;

            await _storeRepo.DeleteSoftAsync(id, deletedBy);
            return BOProcessResult.Success();
        }
    }
}
