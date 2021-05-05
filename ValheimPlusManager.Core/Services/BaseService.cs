using Serilog;
using ValheimPlusManager.Core.Extensions;

namespace ValheimPlusManager.Core.Services
{
    public class BaseService<TChildClass> : IService
        where TChildClass : class
    {
        protected BaseService()
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
