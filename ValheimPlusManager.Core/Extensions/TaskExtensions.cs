using System;
using System.Threading.Tasks;

namespace ValheimPlusManager.Core.Extensions
{
    public static class TaskExtensions
    {
        public static async Task FireAndForget(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
            }
        }
    }
}
