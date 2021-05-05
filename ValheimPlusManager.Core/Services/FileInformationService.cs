using System;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Exceptions;

namespace ValheimPlusManager.Core.Services
{
    public class FileInformationService : BaseService<FileInformationService>, IFileInformationService
    {
        #region Fields

        /// <summary>
        /// IoC provided <see cref="IFileInformationRepository"/> object.
        /// </summary>
        private IFileInformationRepository _fileInformationRepository;
        #endregion

        public FileInformationService(IFileInformationRepository fileInformationRepository)
        {
            _fileInformationRepository = fileInformationRepository;
        }

        public override bool IsLoggerCreated => base.IsLoggerCreated;

        #region Methods
        /// <inheritdoc/>
        public Version GetProductVersion(string filepath)
        {
            Version version = null;
            var productVersion = string.Empty;

            if (Uri.TryCreate(filepath, UriKind.Absolute, out var filepathUri) && filepathUri.IsFile)
            {
                productVersion = _fileInformationRepository?.GetProductVersion(filepathUri);

                if (!Version.TryParse(productVersion, out version))
                {
                    throw new TryParseException($"Could not parse {nameof(Version)} from {nameof(productVersion)}. {nameof(productVersion)}={productVersion}");
                } 
            }
            else
            {
                throw new ArgumentException($"Invalid filepath. {nameof(filepath)}={filepath}{Environment.NewLine}.");
            }

            return version;
        }
        #endregion
    }
}
