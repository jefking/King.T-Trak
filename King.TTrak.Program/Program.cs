namespace King.TTrak.Program
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// T-Trak Azure Table Storage synchronizer
    /// </summary>
    public class Program
    {
        #region Methods
        /// <summary>
        /// Program Main Entry
        /// </summary>
        /// <param name="args">Program Arguments</param>
        public static void Main(string[] args)
        {
            Trace.TraceInformation("Starting...");

            try
            {
                var parameters = new Parameters(args);
                var config = parameters.Process();

                Trace.TraceInformation("From: '{0}'; {1}{4}{4}To: '{2}'; {3}{4}"
                    , config.FromConnectionString
                    , config.FromTable
                    , config.ToConnectionString
                    , config.ToTable
                    , Environment.NewLine);

                var sync = new Synchronizer(config);
                sync.Run().Wait();
            }
            catch (Exception ex)
            {
                Trace.Fail(ex.ToString());
            }

            Trace.TraceInformation("Completed.");

            Thread.Sleep(2000);
        }
        #endregion
    }
}