namespace King.TTrak
{
    using King.Azure.Data;
    using King.TTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Synchronizer
    /// </summary>
    public class Synchronizer : ISynchronizer
    {
        #region Members
        /// <summary>
        /// From Azure Table Storage
        /// </summary>
        protected readonly ITableStorage from = null;

        /// <summary>
        /// To Azure Table Storage
        /// </summary>
        protected readonly ITableStorage to = null;
        #endregion

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

            this.from = new TableStorage(config.FromTable, config.FromConnectionString);
            this.to = new TableStorage(config.ToTable, config.ToConnectionString);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run Synchronization
        /// </summary>
        /// <returns>Task</returns>
        public virtual async Task Run()
        {
            await this.to.CreateIfNotExists();

            var entities = await this.from.Query(new TableQuery());

            await this.to.Insert(entities);
        }
        #endregion
    }
}