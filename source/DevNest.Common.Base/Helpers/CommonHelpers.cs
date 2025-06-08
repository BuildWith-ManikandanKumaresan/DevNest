#region using directives
using Newtonsoft.Json;
#endregion using directives

namespace DevNest.Common.Base.Helpers
{
    /// <summary>
    /// Represents the class instance that contains the common helper methods used across the application.
    /// </summary>
    public static class CommonHelpers
    {
        /// <summary>
        /// Parses the connection parameters from the given provider object.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Dictionary<string,object>? ParseConnectionParams(this object provider)
        {
            Dictionary<string, object> primaryConfig =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(
                    JsonConvert.SerializeObject(provider)) ?? [];
            return primaryConfig;
        }
    }
}
