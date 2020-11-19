namespace Sandbox
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using AccountingProject.Data;
    using AccountingProject.Data.Common;
    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Data.Repositories;
    using AccountingProject.Data.Seeding;
    using AccountingProject.Services.Data;
    using AccountingProject.Services.Messaging;

    using CommandLine;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            // Seed data on application startup
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                return Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                    opts => SandboxCode(opts, serviceProvider).GetAwaiter().GetResult(),
                    _ => 255);
            }
        }

        private static async Task<int> SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
        {
            var sw = Stopwatch.StartNew();

            var settingsService = serviceProvider.GetService<ISettingsService>();
            Console.WriteLine($"Count of settings: {settingsService.GetCount()}");

            Console.WriteLine(sw.Elapsed);

            var db = serviceProvider.GetService<ApplicationDbContext>();

            //await db.GLAccounts.AddAsync(new GLAccount
            //{
            //    Code = 601,
            //    Name = "Разходи за материали cost for materials",
            //});

            //await db.GLAccounts.AddAsync(new GLAccount
            //{
            //    Code = 401,
            //    Name = "Доставчици suppliers",
            //});

            //await db.AnalyticalAccounts.AddAsync(new AnalyticalAccount
            //{
            //    Name = "Stationery",
            //    GLAccountId = 4,
            //});

            //await db.Cities.AddAsync(new City
            //{
            //    Name = "Sofia",
            //});

            //await db.Counterparties.AddAsync(new Counterparty
            //{
            //    Name = "Office 1",
            //    VAT = "898452653",
            //    Address = "bul. Vasil Levski, 101",
            //    CityId = 3,
            //});

            //await db.DocumentTypes.AddAsync(new DocumentType
            //{
            //    Name = "Invoice",
            //});

            //await db.Transactions.AddAsync(new Transaction
            //{
            //    DocumentDate = new DateTime(2020, 01, 20),
            //    DocumentTypeId = 3,
            //    Description = "purchase of stationery",
            //    DebitGLAccountId = 4,
            //    DebitAnalyticalAccountId = 3,
            //    CreditGLAccountId = 5,
            //    Amount = 150,
            //    CounterpartyId = 3,
            //    CreatorId = "2d4b0074-5a04-46e6-92bb-64de8ce5f6af",
            //    IsPurchase = true,
            //    Folder = "2",
            //    ConsecutiveNumber = "1",
            //});

            //await db.GLAccounts.AddAsync(new GLAccount
            //{
            //    Code = 302,
            //    Name = "Материали materials, stocks",
            //    IsInventory = true,
            //});

            //await db.GLAccounts.AddAsync(new GLAccount
            //{
            //    Code = 204,
            //    Name = "Машини и оборудване machines, equpment",
            //    IsFixedAsset = true,
            //});


            // await db.Inventories.AddAsync(new Inventory
            // {
            //    Name = "Printer paper",
            //    Measure = "box",
            //    Quantity = 5,
            //    Price = 17.55M,
            //    GLAccountId = 6,
            // });

            // await db.FixedAssets.AddAsync(new FixedAsset
            // {
            //    Name = "Laptop Acer",
            //    InventoryNumber = "100006",
            //    Quantity = 1,
            //    AcquisitionPrice = 2100,
            //    AcquisitionDate = new DateTime(2015, 11, 10),
            //    DerecognitionDate = new DateTime(2019, 11, 10),
            //    UsefulLife = 4,
            //    SalvageValue = 0,
            //    DepreciationMethod = DepreciationMethod.StraightLine,
            //    AccountablePerson = "Ivan Ivanov",
            //    GLAccountId = 7,
            // });
            await db.SaveChangesAsync();

            return await Task.FromResult(0);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .UseLoggerFactory(new LoggerFactory()));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
        }
    }
}
