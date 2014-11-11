namespace King.TTrak
{
    using System.Threading.Tasks;

    #region ISynchronizer
    /// <summary>
    /// Aszure Table Synchronizer Interface
    /// </summary>
    public interface ISynchronizer
    {
        #region Methods
        /// <summary>
        /// Run Synchronization
        /// </summary>
        /// <returns>Task</returns>
        Task Run();
        #endregion
    }
    #endregion
}