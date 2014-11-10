namespace King.TTrak
{
    using King.TTrak.Models;
    using System;
    using System.Threading.Tasks;

    public class Synchronizer : ISynchronizer
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="config">Configuration Values</param>
        public Synchronizer(IConfigValues config)
        {
            if (null == config)
            {
                throw new ArgumentNullException("config");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run Synchronization
        /// </summary>
        /// <returns>Task</returns>
        public virtual async Task Run()
        {
            await new TaskFactory().StartNew(() => { });
        }
        #endregion
    }
}