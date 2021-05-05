using Serilog;
using ValheimPlusManager.Core.Extensions;

namespace ValheimPlusManager.Core.Repositories
{
    public class BaseRepository<TChildClass> : IRepository
        where TChildClass : class
    {
        protected BaseRepository()
        {
            Logger = Log.Logger.ForContext<TChildClass>();
        }

        /// <summary>
        /// Serilog Logger object.
        /// </summary>
        protected ILogger Logger { get; }
        public virtual bool IsLoggerCreated => Logger != null;
    }
}
