using CreateMyPost.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks; 

namespace CreateMyPost.Application
{
    public static  class DependencyInjection 
    {
        public static IServiceCollection AddApplication(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddMediatR(mdr =>
            {
                mdr.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            
            return services;
        }
    }
}
