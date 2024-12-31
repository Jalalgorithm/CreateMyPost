using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using CreateMyPost.Domain.Repository;
using CreateMyPost.Infrastructure.Data;
using CreateMyPost.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    migration => migration.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentService, CurrentUserService>();


            return services;
        }
    }
}
