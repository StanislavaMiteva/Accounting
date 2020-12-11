namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.Counterparties;
    using Microsoft.EntityFrameworkCore;

    public class CounterpartiesService : ICounterpartiesService
    {
        private readonly IDeletableEntityRepository<Counterparty> counterpartiesRepository;
        private readonly IDeletableEntityRepository<City> citiesRepository;

        public CounterpartiesService(
            IDeletableEntityRepository<Counterparty> counterpartiesRepository,
            IDeletableEntityRepository<City> citiesRepository)
        {
            this.counterpartiesRepository = counterpartiesRepository;
            this.citiesRepository = citiesRepository;
        }

        public async Task CreateAsync(CreateCounterpartyInputModel input)
        {
            var city = this.citiesRepository
                .All()
                .FirstOrDefault(x => x.Name == input.CityName);
            if (city == null)
            {
                city = new City { Name = input.CityName };
            }

            var counterparty = new Counterparty
            {
                Name = input.Name,
                VAT = input.VAT,
                Address = input.Address,
                City = city,
            };
            await this.counterpartiesRepository.AddAsync(counterparty);
            await this.counterpartiesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.counterpartiesRepository.AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.counterpartiesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.counterpartiesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(int id, EditCounterpartyInputModel input)
        {
            var counterparty = await this.counterpartiesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
            counterparty.Name = input.Name;
            counterparty.VAT = input.VAT;
            counterparty.Address = input.Address;
            var city = this.citiesRepository
                .All()
                .FirstOrDefault(x => x.Name == input.CityName);
            if (city == null)
            {
                city = new City { Name = input.CityName };
            }

            counterparty.City = city;
            await this.counterpartiesRepository.SaveChangesAsync();
        }
    }
}
