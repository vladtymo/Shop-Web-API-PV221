using BusinessLogic.Interfaces;
using Hangfire;

namespace Shop_Api_PV221
{
    public class JobConfigurator
    {
        public static void AddJobs()
        {
            RemoveExpiredTokensJob();
        }
        public static void RemoveExpiredTokensJob()
        {
            RecurringJob.AddOrUpdate<IAccountsService>(
                nameof(RemoveExpiredTokensJob),
                (service) => service.RemoveExpiredRefreshTokens(),
                Cron.Weekly(DayOfWeek.Monday, 3));
        }
    }
}
