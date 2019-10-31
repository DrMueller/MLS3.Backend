using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.ConnectionStrings
{
    internal static class KeyVaultProvider
    {
        public static string ProvideSecret(string secretIdentifier)
        {
            var task = Task.Run(async () =>
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(secretIdentifier).ConfigureAwait(false);
                return secret.Value;
            });

            return task.Result;
        }
    }
}