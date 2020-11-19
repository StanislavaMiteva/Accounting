﻿namespace AccountingProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.Counterparties;

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
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == input.CityName);
            if (city == null)
            {
                city = new City { Name = input.CityName };
                await this.citiesRepository.AddAsync(city);
                await this.counterpartiesRepository.SaveChangesAsync();
            }

            var counterparty = new Counterparty
            {
                Name = input.Name,
                VAT = input.VAT,
                Address = input.Address,
                CityId = city.Id,
            };

            await this.counterpartiesRepository.AddAsync(counterparty);
            await this.counterpartiesRepository.SaveChangesAsync();
        }
    }
}
