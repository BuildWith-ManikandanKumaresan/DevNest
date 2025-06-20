namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Constants for text search operations.
    /// </summary>
    public partial class TextSearchConstants
    {
        public const string StartsWith = "startswith";
        public const string Contains = "contains";
        public const string EndsWith = "endswith";
        public new const string Equals = "equals";
        public const string NotEquals = "notequals";
    }

    /// <summary>
    /// Constants for date search operations.
    /// </summary>
    public partial class DateSearchConstants
    {
        public const string Exact = "exact";
        public const string NotExact = "notexact";
        public const string Range = "range";
        public const string NotInRange = "notinrange";
    }
}
