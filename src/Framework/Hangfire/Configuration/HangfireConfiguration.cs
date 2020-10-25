using Hangfire;
using MonoRepo.Framework.Core.Interfaces;

namespace MonoRepo.Framework.Hangfire.Configuration
{
    /// <summary>
    /// Registers Recurring Jobs in Hangfire.
    /// </summary>
    public class HangfireConfiguration
    {
        /// <summary>
        /// Sets up a Recurring Job in Hangfire.
        /// </summary>
        /// <param name="cronValue">Cron value for how often this recurring job should run.</param>
        /// <typeparam name="T">Handler type for this recurring job.  Must implement <see cref="IHangfireJob"/></typeparam>
        public void AddOrUpdate<T>(string cronValue) where T : IHangfireJob
        {
            // Add the recurring job to Hangfire.
            // Note [at]: Hangfire detects the user of JobCancellationToken.Null and injects a valid one.
            // https://docs.hangfire.io/en/latest/background-methods/using-cancellation-tokens.html
            RecurringJob.AddOrUpdate<T>(typeof(T).Name, command => command.Execute(JobCancellationToken.Null), cronValue);
        }
    }
}
