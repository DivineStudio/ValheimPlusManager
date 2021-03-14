using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace ValheimPlusManager.Core.Services
{
    public abstract class BaseService : IService
    {
        protected abstract ILogger Logger { get; }
    }
}
