using System.Globalization;
using System.Resources;

namespace LOTR_GameRegister.Api.Helpers
{
    /// <summary>
    /// Resolves user-facing messages from the embedded resource files
    /// (<c>Messages.resx</c> / <c>Messages.es.resx</c>) using the request UI culture.
    /// </summary>
    public static class Localizer
    {
        private static readonly ResourceManager Manager =
            new("LOTR_GameRegister.Api.Resources.Messages", typeof(Localizer).Assembly);

        /// <summary>
        /// Returns the message for the given key in the current UI culture.
        /// </summary>
        /// <param name="key">Resource key.</param>
        /// <param name="args">Optional format arguments.</param>
        public static string Get(string key, params object[] args)
        {
            var template = Manager.GetString(key, CultureInfo.CurrentUICulture);
            if (string.IsNullOrEmpty(template)) return key;

            return args.Length == 0 ? template : string.Format(template, args);
        }
    }
}
