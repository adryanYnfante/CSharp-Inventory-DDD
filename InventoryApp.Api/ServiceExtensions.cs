using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Domain.UseCases;
using InventoryApp.Domain.UseCases.Interfaces;
using InventoryApp.Infrastructure.Contexts;
using InventoryApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InventoryApp.Api
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterUseCase(this IServiceCollection services)
        {
            services.AddScoped<IInvoiceRecordUseCase, RecordUseCase>();
            services.AddScoped<IProductUseCase, ProductUseCase>();
            services.AddScoped<ICreateInvoiceUseCase, InvoiceUseCase>();
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IInvoiceRecordRepository, InvoiceRecordRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICreateInvoiceRepository, CreateInvoiceRepository>();
            return services;
        }

        public static IServiceCollection CorsConfig(this IServiceCollection services, string MyAllowOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                                  });
            });
            return services;
        }

        public static IServiceCollection DbContextConfig(this IServiceCollection services, string sqlConnection)
        {
            services.AddDbContext<InventoryEfContext>(options =>
             {
                 options.UseSqlServer(sqlConnection,
                     sqlServerOptionsAction: sqlOptions =>
                     {
                         sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10,
                             maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                         sqlOptions.MigrationsHistoryTable("Migrations", "Configuration");
                     });
             },
                   ServiceLifetime.Scoped
            );
            return services;
        }
    }
}