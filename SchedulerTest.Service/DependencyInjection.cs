using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SchedulerTest.Domain.IRepo;
using SchedulerTest.Domain.IServices;
using SchedulerTest.Repository;
using SchedulerTest.Service.Filter;
using SchedulerTest.Service.Services;
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
            services.AddScoped<IProductScheduleService, ProductScheduleService>();
            services.AddHttpClient<IHttpClientService, HttpClientService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepo, ProductRepo>();
            return services;
        }

    }
}
