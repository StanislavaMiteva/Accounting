namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public RolesService(IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.rolesRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }
    }
}
