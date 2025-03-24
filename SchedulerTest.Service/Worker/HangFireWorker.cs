using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchedulerTest.Domain.IServices;
using SchedulerTest.Service.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Service.Worker;
public static class HangFireWorker
{
    [AutomaticJobDisposal]
    public static void StartRecurringJob(this IApplicationBuilder app, IConfiguration config)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

            string cronExpression = config["CronExpression:ProductSchedular"] ?? "*/2 * * * *"; // Default: Every 2 minutes

            recurringJobManager.AddOrUpdate(
                "ProductTesting",
                () => productService.CreateFileWithTxnData("test"),
                cronExpression,
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local,
                });
        }
    }
}

