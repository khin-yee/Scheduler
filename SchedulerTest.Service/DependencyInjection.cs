using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SchedulerTest.Domain;
using SchedulerTest.Repository;
using SchedulerTest.Service.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            GlobalJobFilters.Filters.Add(new AutomaticJobDisposalAttribute());
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepo, ProductRepo>();
            return services;
        }

    }
}
