namespace King.TTrak
{
    using King.Azure.Data;
    using King.TTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Aszure Table Synchronizer
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

        /// <summary>
        /// Mockable Constructor
        /// </summary>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        public Synchronizer(ITableStorage from, ITableStorage to)
        {
            if (null == from)
            {
                throw new ArgumentNullException("from");
            }
            if (null == to)
            {
                throw new ArgumentNullException("to");
            }

            this.from = from;
            this.to = to;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run Synchronization
        /// </summary>
        /// <returns>Task</returns>
        public virtual async Task Run()
        {
            Trace.TraceInformation("Initializing: {0}.", this.to.Name);
            await this.to.CreateIfNotExists();

            Trace.TraceInformation("Loading data from {0}.", this.from.Name);
            var entities = await this.from.Query(new TableQuery());
            if (null != entities && entities.Any())
            {
                Trace.TraceInformation("Storing data to {0}.", this.to.Name);
                await this.to.Insert(entities);
            }
            else
            {
                Trace.TraceInformation("No entities in {0}.", this.from.Name);
            }
        }
        #endregion
    }
}