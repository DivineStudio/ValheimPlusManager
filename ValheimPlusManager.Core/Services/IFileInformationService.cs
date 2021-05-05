using System;
using System.Collections.Generic;
using System.Text;

namespace ValheimPlusManager.Core.Services
{
    public interface IFileInformationService : IService
    {
        /// <summary>
        /// Retrieves the version number of an executable from a provided URI.
        /// </summary>
        /// <param name="filepath">The file system location of the executable.</param>
        /// <returns>Version number of the found executable. Null if the executable is not located.</returns>
        /// <exception cref="ArgumentException"/>
        Version GetProductVersion(string filepath);
    }
}
