using System;
using System.Collections.Generic;
using System.Text;

namespace ValheimPlusManager.Core.Services
{
    public interface IFileDescriptionService
    {
        Version GetProductVersion(Uri filepath);
    }
}
