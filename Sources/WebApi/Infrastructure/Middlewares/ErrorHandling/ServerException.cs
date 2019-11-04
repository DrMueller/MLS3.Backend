using System;
using JetBrains.Annotations;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mls3.WebApi.Infrastructure.Middlewares.ErrorHandling
{
    internal class ServerException
    {
        [UsedImplicitly]
        public string Message { get; }

        [UsedImplicitly]
        public string StackTrace { get; }

        [UsedImplicitly]
        public string TypeName { get; }

        private ServerException(string message, string typeName, string stackTrace)
        {
            Guard.StringNotNullOrEmpty(() => message);
            Guard.StringNotNullOrEmpty(() => typeName);
            Guard.StringNotNullOrEmpty(() => stackTrace);

            Message = message;
            TypeName = typeName;
            StackTrace = stackTrace;
        }

        public static ServerException CreateFromException(Exception ex)
        {
            return new ServerException(ex.Message, ex.GetType().Name, ex.StackTrace);
        }
    }
}