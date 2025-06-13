#region using directives
using DevNest.Common.Base.Constants;
#endregion using directives

namespace DevNest.Infrastructure.Entity.Credentials
{
    /// <summary>
    /// Represents the entity class instance for Password Health information.
    /// </summary>
    public class PasswordHealthEntityModel
    {
        /// <summary>
        /// Gets or sets the Password score (e.g., 0-4).
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Gets or sets the score percentage (0-100%).
        /// </summary>
        public double ScorePercentage
        {
            get
            {
                if (Score != null)
                {
                    return Score switch
                    {
                        0 => PasswordHealthConstants.VeryWeakPercentage,
                        1 => PasswordHealthConstants.WeakPercentage,
                        2 => PasswordHealthConstants.FairPercentage,
                        3 => PasswordHealthConstants.StrongPercentage,
                        4 => PasswordHealthConstants.VeryStrongPercentage,
                        _ => PasswordHealthConstants.UnknownPercentage
                    };
                }
                return PasswordHealthConstants.UnknownPercentage;
            }
        }

        /// <summary>
        /// Gets or sets the password strength (e.g., Strong, Weak).
        /// </summary>
        public string? PasswordStrength
        {
            get
            {
                if (Score != null)
                {
                    return Score switch
                    {
                        0 => PasswordHealthConstants.VeryWeak,
                        1 => PasswordHealthConstants.Weak,
                        2 => PasswordHealthConstants.Fair,
                        3 => PasswordHealthConstants.Strong,
                        4 => PasswordHealthConstants.VeryStrong,
                        _ => PasswordHealthConstants.Unknown
                    };
                }
                return PasswordHealthConstants.Unknown;
            }
        }

        /// <summary>
        /// Gets or sets the calculation time to took for calculating password health.
        /// </summary>
        public long? CalculationTime { get; set; }

        /// <summary>
        /// Gets or sets the suggestions based on the password health.
        /// </summary>
        public IList<string>? Suggestions { get; set; } = [];

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