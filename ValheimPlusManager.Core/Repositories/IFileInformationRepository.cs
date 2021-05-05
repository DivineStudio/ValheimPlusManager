using System;

namespace ValheimPlusManager.Core.Repositories
{
    public interface IFileInformationRepository : IRepository
    {
        /// <summary>
        /// Retrieves the product version of a file from the provided URI.
        /// </summary>
        /// <param name="filepath">The file system location of the file.</param>
        /// <returns>The product version of the file located from the filepath.</returns>
        string GetProductVersion(Uri filepath);
    }
}
