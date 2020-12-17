namespace AccountingProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.FixedAssets;
    using AccountingProject.Web.ViewModels.Inventories;
    using Microsoft.EntityFrameworkCore;

    public class FixedAssetsService : IFixedAssetsService
    {
        private readonly IDeletableEntityRepository<FixedAsset> fixedAssetsRepository;

        public FixedAssetsService(IDeletableEntityRepository<FixedAsset> fixedAssetsRepository)
        {
            this.fixedAssetsRepository = fixedAssetsRepository;
        }

        public async Task CreateAsync(CreateFixedAssetInputModel input)
        {
            var fixedAsset = new FixedAsset
            {
                Name = input.Name,
                InventoryNumber = input.InventoryNumber,
                Quantity = input.Quantity,
                AcquisitionPrice = input.AcquisitionPrice,
                AcquisitionDate = input.AcquisitionDate,
                DerecognitionDate = input.DerecognitionDate,
                UsefulLife = input.UsefulLife,
                SalvageValue = input.SalvageValue,
                DepreciationMethod = input.DepreciationMethod,
                AccountablePerson = input.AccountablePerson,
                GLAccountId = input.MainAccountId,
            };

            await this.fixedAssetsRepository.AddAsync(fixedAsset);
            await this.fixedAssetsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asset = await this.fixedAssetsRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Id == id);
            this.fixedAssetsRepository.Delete(asset);
            await this.fixedAssetsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.fixedAssetsRepository.AllAsNoTracking()
               .OrderBy(x => x.Name)
               .To<T>()
               .ToListAsync();
        }

        public IEnumerable<T> GetAllByAccount<T>(int accountId)
        {
            return this.fixedAssetsRepository.AllAsNoTracking()
                .Where(x => x.GLAccountId == accountId)
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.fixedAssetsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.fixedAssetsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(int id, EditFixedAssetInputModel input)
        {
            var fixedAsset = await this.fixedAssetsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
            fixedAsset.Name = input.Name;
            fixedAsset.AccountablePerson = input.AccountablePerson;
            fixedAsset.Quantity = input.Quantity;
            fixedAsset.AcquisitionDate = input.AcquisitionDate;
            fixedAsset.AcquisitionPrice = input.AcquisitionPrice;
            fixedAsset.DepreciationMethod = input.DepreciationMethod;
            fixedAsset.DerecognitionDate = input.DerecognitionDate;
            fixedAsset.InventoryNumber = input.InventoryNumber;
            fixedAsset.SalvageValue = input.SalvageValue;
            fixedAsset.UsefulLife = input.UsefulLife;
            fixedAsset.GLAccountId = input.MainAccountId;
            await this.fixedAssetsRepository.SaveChangesAsync();
        }
    }
}
