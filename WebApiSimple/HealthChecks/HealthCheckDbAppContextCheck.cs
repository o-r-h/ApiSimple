using Base.Domain.Interfaces.BaseCommons.ServiceInterface;
using Base.Domain.Interfaces.DbApp.ServiceInterface;
using Base.Infrastructure.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiSimple.HealthChecks
{
    public class HealthCheckDbAppContextCheck : IHealthCheck
    {
        public readonly IHealthService healthService;

        public HealthCheckDbAppContextCheck(IHealthService healthService)
        {
            this.healthService = healthService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken =
         default)
        {
            try
            {
                using var db = new DbAppContext();
                try
                {
                    if (await healthService.CanConnect())
                    {
                        return  HealthCheckResult.Healthy();
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy("HealthCheckExampleDbContext could not connect to database");
                    }


                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy("HealthCheckExampleDbContext could not connect to database ", ex);
                }

            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("Error when trying to check HealthCheckExampleDbContext. ", e);
            }

        }
    }




}
