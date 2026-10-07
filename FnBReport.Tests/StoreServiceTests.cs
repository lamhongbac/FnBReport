using FnBReport.BLL.Constants;
using FnBReport.BLL.Rules;
using FnBReport.BLL.Rules.StoreRules;
using FnBReport.BLL.Services;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FnBReport.Tests
{
    public class StoreServiceTests
    {
        [Fact]
        public async Task CreateStoreAsync_ShouldReturnError_WhenNumberIsEmpty()
        {
            var mockStoreRepo = new Mock<IStoreRepository>();
            var mockGroupRepo = new Mock<IStoreGroupRepository>();
            var rules = new List<IBusinessSaveRule<Store>> { new StoreNumberUniqueRule(mockStoreRepo.Object) };
            var service = new StoreService(mockStoreRepo.Object, mockGroupRepo.Object, rules, null);

            var store = new Store { Number = "", Name = "Test Store" };

            var result = await service.CreateStoreAsync(store, "admin");

            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.Store.NumberEmpty, result.Code);
        }

        [Fact]
        public async Task CreateStoreAsync_ShouldReturnError_WhenNumberExists()
        {
            var mockStoreRepo = new Mock<IStoreRepository>();
            mockStoreRepo.Setup(r => r.GetByCodeAsync("ST01")).ReturnsAsync(new Store { Number = "ST01", Id = 99 });
            var mockGroupRepo = new Mock<IStoreGroupRepository>();
            var rules = new List<IBusinessSaveRule<Store>> { new StoreNumberUniqueRule(mockStoreRepo.Object) };
            var service = new StoreService(mockStoreRepo.Object, mockGroupRepo.Object, rules, null);

            var store = new Store { Number = "ST01", Name = "Test Store" };

            var result = await service.CreateStoreAsync(store, "admin");

            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.Store.NumberExists, result.Code);
        }

        [Fact]
        public async Task CreateStoreAsync_ShouldReturnSuccess_WhenDataIsValid()
        {
            var mockStoreRepo = new Mock<IStoreRepository>();
            mockStoreRepo.Setup(r => r.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync((Store?)null);
            var mockGroupRepo = new Mock<IStoreGroupRepository>();
            var rules = new List<IBusinessSaveRule<Store>> { 
                new StoreNumberUniqueRule(mockStoreRepo.Object),
                new StoreNameRequiredRule() 
            };
            var service = new StoreService(mockStoreRepo.Object, mockGroupRepo.Object, rules, null);

            var store = new Store { Number = "ST02", Name = "New Store" };

            var result = await service.CreateStoreAsync(store, "admin");

            Assert.True(result.IsSuccess);
            mockStoreRepo.Verify(r => r.AddAsync(It.IsAny<Store>()), Times.Once);
        }
    }
}
