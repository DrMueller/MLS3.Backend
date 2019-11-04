using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mls3.WebApi.Infrastructure.KeyVaults
{
    internal static class KeyVaultProvider
    {
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Actually needed")]
        public static FunctionResult<string> TryProvidingSecret(string secretIdentifier)
        {
            try
            {
                var task = Task.Run(
                    async () =>
                    {
                        var azureServiceTokenProvider = new AzureServiceTokenProvider();
                        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                        var secret = await keyVaultClient.GetSecretAsync(secretIdentifier).ConfigureAwait(false);
                        return secret.Value;
                    });

                return FunctionResult.CreateSuccess(task.Result);
            }
            catch (Exception)
            {
                return FunctionResult.CreateFailure<string>();
            }
        }
    }
}