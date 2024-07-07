using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TeamsMasterDetail.Domain.Context;
using TeamsMasterDetail.Infrastructure.Repositories.Implementations;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Extensions
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            return
                services
                    .AddDbContext<AppDbContext>(options =>
                    {
                        if (!options.IsConfigured)
                        {
                            options
                                .UseLazyLoadingProxies()
                                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                                .UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));
                        }
                    });
        }

        public static IServiceCollection AddScopedRepositoriesAndMediatR(this IServiceCollection services)
        {
            return
                services
                    .AddScoped(typeof(IUnitOfWorkRepository<>), typeof(UnitOfWorkRepository<>))
                    .AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>))
                    .AddScoped<ITeamRepository, TeamRepository>()
                    .AddScoped<IMemberRepository, MemberRepository>()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
