#region using directives
#endregion using directives

namespace DevNest.Infrastructure.Entity.VaultX
{
    /// <summary>
    /// Represents the entity class instance for Validity information of a credential.
    /// </summary>
    public class ValidityEntityModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the credential is valid.
        /// </summary>
        public bool? IsValid { get; set; }

        /// <summary>
        /// Gets or sets the credential expiration date.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the password rotation policy in days.
        /// </summary>
        public int? RotationPolicyInDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the credential is disabled.
        /// </summary>
        public bool? IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the credential is expired.
        /// </summary>
        public bool? IsExpired { get; set; }

        /// <summary>
        /// Gets or sets the number of days remaining before the credential expires.
        /// </summary>
        public int? RemainingDaysBeforeExpiration { get; set; }

    }
}