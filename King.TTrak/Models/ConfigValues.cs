namespace King.TTrak.Models
{
    /// <summary>
    /// Configuration Values
    /// </summary>
    public class ConfigValues : IConfigValues
    {
        #region Properties
        /// <summary>
        /// From Table Name
        /// </summary>
        public string FromTable
        {
            get;
            set;
        }

        /// <summary>
        /// To Table Name
        /// </summary>
        public string ToTable
        {
            get;
            set;
        }

        /// <summary>
        /// From Connection String
        /// </summary>
        public string FromConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// To Connection String
        /// </summary>
        public string ToConnectionString
        {
            get;
            set;
        }
        #endregion
    }
}