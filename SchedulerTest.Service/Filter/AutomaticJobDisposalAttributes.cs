using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Service.Filter
{
    public class AutomaticJobDisposalAttribute : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is FailedState)
            {
                var jobId = context.BackgroundJob.Id;
                var recurringJobId = context.GetJobParameter<string>("ProductTestJob");

                if (!string.IsNullOrEmpty(recurringJobId))
                {
                    RecurringJob.RemoveIfExists(recurringJobId);
                }
            }
        }
        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {

        }
    }
}
