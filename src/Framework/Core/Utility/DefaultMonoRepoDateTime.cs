using MonoRepo.Framework.Core.Interfaces;
using System;

namespace MonoRepo.Framework.Core.Utility
{
    /// <summary>
    /// Default implementation of the Ships Date Time.
    /// </summary>
    public class DefaultMonoRepoDateTime : IDateTime
    {
        private const string dateFormat = "MM/dd/yyyy";
        private const string timeFormat = "h:mm tt";

        /// <inheritdoc />
        public DateTime Now => DateTime.UtcNow;

        /// <inheritdoc />
        public string ToDateString(DateTime dt)
        {
            return dt.ToString($"{dateFormat} {timeFormat}");
        }
    }
}
