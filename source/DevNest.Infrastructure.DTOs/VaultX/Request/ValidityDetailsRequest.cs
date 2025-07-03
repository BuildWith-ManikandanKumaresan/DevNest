namespace DevNest.Infrastructure.DTOs.VaultX.Request
{
    /// <summary>
    /// Represents the class instance for validity details request.
    /// </summary>
    public class ValidityDetailsRequest
    {
        /// <summary>
        /// Gets or sets the credential expiration date.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the password rotation policy in days.
        /// </summary>
        public int? RotationPolicyInDays { get; set; }
    }
}
