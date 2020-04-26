using System;
using System.IO;

namespace CodeStatistics.Resources{
    static class Embedded{
        private static Lazy<Stream> Get(string name){
            return new Lazy<Stream>(() => typeof(Embedded).Assembly.GetManifestResourceStream($"CodeStatistics.Resources.{name.Replace('/', '.')}") ?? throw new ArgumentException($"Embedded resource not found: {name}"));
        }

        private static readonly Lazy<Stream> DefaultConfigurationLazy = Get("default.codestatistics");

        public static Stream DefaultConfiguration => DefaultConfigurationLazy.Value;
    }
}
