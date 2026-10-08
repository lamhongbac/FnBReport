using FnBReport.BLL.Services;
using FnBReport.DAL.Models;
using FnBReport.GUI.ViewModels;
using FnBReport.Shared.Models;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FnBReport.GUI.Services
{
    public interface IStoreAppService
    {
        Task<IEnumerable<StoreGroupViewModel>> GetAllStoreGroupsAsync();
        Task<BOProcessResult> SaveStoreGroupAsync(StoreGroupViewModel vm, string username);
        Task<BOProcessResult> DeleteStoreGroupAsync(int id, string deletedBy);
        Task<IEnumerable<StoreViewModel>> GetAllStoresAsync();
        Task<BOProcessResult> SaveStoreAsync(StoreViewModel vm, string username);
        Task<BOProcessResult> DeleteStoreAsync(int id, string deletedBy);
    }

    public class StoreAppService : IStoreAppService
    {
        private readonly IStoreService _storeService;

        public StoreAppService(IStoreService storeService)
        {
            _storeService = storeService;
        }

        private string TranslateErrorCode(string code)
        {
            return code switch {
                "STORE_NUMBER_EMPTY" => "Mã Cửa hàng không được để trống.",
                "STORE_NAME_EMPTY" => "Tên Cửa hàng không được để trống.",
                "STORE_NUMBER_EXISTS" => "Mã Cửa hàng đã tồn tại.",
                "STORE_NOT_FOUND" => "Không tìm thấy Cửa hàng.",
                "STOREGROUP_NAME_EMPTY" => "Tên Nhóm Cửa hàng không được để trống.",
                "STOREGROUP_NOT_FOUND" => "Không tìm thấy Nhóm Cửa hàng.",
                _ => code // Return raw code (or exception message) if unknown, don't use generic wrapper
            };
        }

        public async Task<IEnumerable<StoreGroupViewModel>> GetAllStoreGroupsAsync()
        {
            var groups = await _storeService.GetAllStoreGroupsAsync();
            return groups.Adapt<IEnumerable<StoreGroupViewModel>>();
        }

        public async Task<BOProcessResult> SaveStoreGroupAsync(StoreGroupViewModel vm, string username)
        {
            try 
            {
                var entity = vm.Adapt<StoreGroup>();
                BOProcessResult result;

                if (vm.Id == 0)
                {
                    result = await _storeService.CreateStoreGroupAsync(entity, username);
                    if (result.IsSuccess) vm.Id = entity.Id;
                }
                else
                {
                    result = await _storeService.UpdateStoreGroupAsync(entity, username);
                }

                if (!result.IsSuccess) result.Message = TranslateErrorCode(result.Code);
                return result;
            }
            catch (System.Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<BOProcessResult> DeleteStoreGroupAsync(int id, string deletedBy)
        {
            try 
            {
                var result = await _storeService.DeleteStoreGroupAsync(id, deletedBy);
                if (!result.IsSuccess) result.Message = TranslateErrorCode(result.Code);
                return result;
            }
            catch (System.Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<IEnumerable<StoreViewModel>> GetAllStoresAsync()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return stores.Adapt<IEnumerable<StoreViewModel>>();
        }

        public async Task<BOProcessResult> SaveStoreAsync(StoreViewModel vm, string username)
        {
            try
            {
                var entity = vm.Adapt<Store>();
                BOProcessResult result;

                if (vm.Id == 0)
                {
                    result = await _storeService.CreateStoreAsync(entity, username);
                    if (result.IsSuccess) vm.Id = entity.Id;
                }
                else
                {
                    result = await _storeService.UpdateStoreAsync(entity, username);
                }

                if (!result.IsSuccess) result.Message = TranslateErrorCode(result.Code);
                return result;
            }
            catch (System.Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<BOProcessResult> DeleteStoreAsync(int id, string deletedBy)
        {
            try 
            {
                var result = await _storeService.DeleteStoreAsync(id, deletedBy);
                if (!result.IsSuccess) result.Message = TranslateErrorCode(result.Code);
                return result;
            }
            catch (System.Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }
    }
}
