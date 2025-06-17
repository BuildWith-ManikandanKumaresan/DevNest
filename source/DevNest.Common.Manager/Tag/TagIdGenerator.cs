#region using directives
using Slugify;
#endregion using directives

namespace DevNest.Common.Manager.Tag
{
    /// <summary>
    /// Represents the class instance that contains the tag ID generation methods used across the application.
    /// </summary>
    public static class TagIdGenerator
    {
        /// <summary>
        /// Generates a unique tag ID based on a GUID.
        /// </summary>
        /// <returns></returns>
        public static string GenerateTagId()
        {
            string shortId = Guid.NewGuid().ToString("N")[..8].ToLower();
            return $"tag-{shortId}";
        }

        /// <summary>
        /// Generates a unique tag ID based on a prefix and a GUID.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GenerateTagId(this string prefix)
        {
            string shortId = Guid.NewGuid().ToString("N")[..8].ToLower();
            return $"{prefix}-{shortId}";
        }

        /// <summary>
        /// Generates a URL-friendly slug from the given string by normalizing and replacing characters.
        /// </summary>
        /// <param name="slugContent"></param>
        /// <returns></returns>
        public static string GenerateSlug(this string slugContent)
        {
            SlugHelper helper = new(new SlugHelperConfiguration()
            {
                ForceLowerCase = true,
                CollapseDashes = true,
                TrimWhitespace = true,
                MaximumLength = 100
            });
            return helper.GenerateSlug(slugContent);
        }

        /// <summary>
        /// Generates a random color code for tags, typically used for UI representation.
        /// </summary>
        /// <returns></returns>
        public static int PickRandomColorCodes()
        {
            int minColorCodes = 1000;
            int maxColorCodes = 1999;
            Random random = new();
            return random.Next(minColorCodes, maxColorCodes);
        }

        /// <summary>
        /// Generates a random color code for tags, excluding specified codes.
        /// </summary>
        /// <param name="excludedCodes"></param>
        /// <returns></returns>
        public static int PickRandomColorCodes(this int[] excludedCodes)
        {
            int minColorCodes = 1000;
            int maxColorCodes = 1999;
            Random random = new();
            int randomCode;
            do
            {
                randomCode = random.Next(minColorCodes, maxColorCodes);
            } 
            while (excludedCodes.Contains(randomCode));
            return randomCode;
        }
    }
}
