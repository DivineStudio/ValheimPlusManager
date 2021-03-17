using System;
using System.Collections.Generic;
using System.Text;

namespace ValheimPlusManager.Core.Services
{
    public interface IFileInformationService
    {
        Version GetProductVersion(Uri filepath);
    }
}
