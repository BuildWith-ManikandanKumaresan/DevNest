#region using directives
using DevNest.Common.Base.Constants;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Credential.Response
{
    /// <summary>
    /// Represents the response DTO for Password Health information.
    /// </summary>
    public class PasswordHealthResponseDTO
    {
        /// <summary>
        /// Gets or sets the Password score (e.g., 0-4).
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Gets or sets the score percentage (0-100%).
        /// </summary>
        public double ScorePercentage { get; set; }

        /// <summary>
        /// Gets or sets the password strength (e.g., Strong, Weak).
        /// </summary>
        public string? PasswordStrength { get; set; }

        /// <summary>
        /// Gets or sets the calculation time to took for calculating password health.
        /// </summary>
        public long? CalculationTime { get; set; }

        /// <summary>
        /// Gets or sets the suggestions based on the password health.
        /// </summary>
        public IList<string>? Suggestions { get; set; }

        /// <summary>
        /// Gets or sets the warnings based on the password health.
        /// </summary>
        public string? Warnings { get; set; }

        /// <summary>
        /// Gets or sets the guesses count to crack the password based on the health.
        /// </summary>
        public double? Guesses { get; set; }
    }
}
