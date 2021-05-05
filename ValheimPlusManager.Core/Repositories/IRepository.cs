namespace ValheimPlusManager.Core.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Is used to convey whether or not the Serilog Logger is created.
        /// Should call the base class of the inheriting class' IsLoggerCreated property.
        /// </summary>
        public bool IsLoggerCreated { get; }
    }
}
