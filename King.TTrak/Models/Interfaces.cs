namespace King.TTrak.Models
{
    #region IConfigValues
    /// <summary>
    /// Configuration Values
    /// </summary>
    public interface IConfigValues
    {
        #region Properties
        /// <summary>
        /// From Table Name
        /// </summary>
        string FromTable
        {
            get;
        }

        /// <summary>
        /// To Table Name
        /// </summary>
        string ToTable
        {
            get;
        }

        /// <summary>
        /// From Connection String
        /// </summary>
        string FromConnectionString
        {
            get;
        }

        /// <summary>
        /// To Connection String
        /// </summary>
        string ToConnectionString
        {
            get;
        }
        #endregion
    }
    #endregion
}