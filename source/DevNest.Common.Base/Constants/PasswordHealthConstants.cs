namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the password health constants used across the application.
    /// </summary>
    public partial class PasswordHealthConstants
    {
        public const string VeryWeak = "Very Weak";
        public const string Weak = "Weak";
        public const string Fair = "Fair";
        public const string Strong = "Strong";
        public const string VeryStrong = "Very Strong";
        public const string Unknown = "Unknown";

        public const double VeryWeakPercentage = 20;
        public const double WeakPercentage = 40;
        public const double FairPercentage = 60;
        public const double StrongPercentage = 80;
        public const double VeryStrongPercentage = 100;
        public const double UnknownPercentage = 10;
    }
}
