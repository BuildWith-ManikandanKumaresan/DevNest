#region using directives
using DevNest.Common.Base.Constants;
using System.Reflection;
#endregion using directives

namespace DevNest.Common.Base.Helpers
{
    /// <summary>
    /// Represents the class instance that contains the helper methods related to assebly loading.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets the referenced assemblies that match the specified search pattern.
        /// </summary>
        /// <param name="assembly">The assembly to get referenced assemblies from.</param>
        /// <returns>An enumerable collection of referenced assemblies.</returns>
        public static IEnumerable<Assembly> GetReferencedAssemblies(Assembly? assembly)
        {
            var assembliesName = assembly?.GetReferencedAssemblies().Where(referenced => IsAssemblyAccepted(referenced.Name));
            return assembliesName?.Select(Assembly.Load) ?? [];
        }

        /// <summary>
        /// Determines whether an assembly name is accepted based on a search pattern.
        /// </summary>
        /// <param name="name">The name of the assembly to check.</param>
        /// <returns><c>true</c> if the assembly name matches the search pattern; otherwise, <c>false</c>.</returns>
        private static bool IsAssemblyAccepted(string? name)
        {
            return name?.StartsWith(CommonConstants.AssemblySearchPattern) ?? false;
        }
    }
}
