namespace DevNest.Common.Base.DTOs
{
    /// <summary>
    /// Represents the class instance for Metadata informations.
    /// </summary>
    public class MetadataDTO
    {

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the transaction date and time.
        /// </summary>
        public DateTime? TransactionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the transaction station or machine name.
        /// </summary>
        public string? TransactionStation { get; set; }

        /// <summary>
        /// Gets or sets the transaction initiator.
        /// </summary>
        public string? TransactionUser { get; set; }

        /// <summary>
        /// Gets or sets the transaction approval status.
        /// </summary>
        public string? TransactionApprovalStatus { get; set; }

        /// <summary>
        /// Gets or sets the transaction user role.
        /// </summary>
        public string? TransactionUserRole { get; set; }
    }
}