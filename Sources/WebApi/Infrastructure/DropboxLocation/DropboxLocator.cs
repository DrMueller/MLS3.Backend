using System;
using System.IO;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mls3.WebApi.Infrastructure.DropboxLocation
{
    internal static class DropboxLocator
    {
        internal static FunctionResult<string> LocateDropboxSettingsPath()
        {
            var dropboxBasePathResult = LocateDropboxBasePath();
            if (!dropboxBasePathResult.IsSuccess)
            {
                return dropboxBasePathResult;
            }

            var relativePath = Path.Combine(dropboxBasePathResult.Value, @"Apps\MLS3\appsettings.json");
            if (!File.Exists(relativePath))
            {
                return FunctionResult.CreateFailure<string>();
            }

            return FunctionResult.CreateSuccess(relativePath);
        }

        private static FunctionResult<string> LocateDropboxBasePath()
        {
            const string DropboxInfoPath = @"Dropbox\info.json";
            var jsonPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), DropboxInfoPath);

            if (!File.Exists(jsonPath))
            {
                jsonPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), DropboxInfoPath);
            }

            if (!File.Exists(jsonPath))
            {
                return FunctionResult.CreateFailure<string>();
            }

            var dropboxPath = File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\", StringComparison.OrdinalIgnoreCase);
            return FunctionResult.CreateSuccess(dropboxPath);
        }
    }
}