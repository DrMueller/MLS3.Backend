using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mls3.Localization;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Servants.Implementation
{
    internal class ResourceTypeFetcher : IResourceTypeFetcher
    {
        private const string BaseAssemblyNamespace = "Mmu.Mls3.";
        private const string BaseResourcePath = BaseAssemblyNamespace + "Localization.";

        private readonly IReadOnlyCollection<Type> _resourceTypes;

        public ResourceTypeFetcher()
        {
            _resourceTypes = typeof(MarkerClass).Assembly.GetTypes().ToList();
        }

        public string CreateResourceKey(Type requestType)
        {
            var typeNamewithNamespace = requestType.FullName.Replace(BaseAssemblyNamespace, string.Empty, StringComparison.Ordinal);
            var resourceKey = string.Concat(BaseResourcePath, typeNamewithNamespace, ".", requestType.Name);
            return resourceKey;
        }

        public Type FetchResourceType(string resourceKey)
        {
            var resourceTypes = _resourceTypes.Where(f => f.FullName == resourceKey).ToList();

            Guard.That(() => resourceTypes.Count() == 1, $"Found more than one Resource Type for {resourceKey}.");
            Guard.That(() => resourceTypes.Any(), $"No Resource Type for {resourceKey} found.");

            return resourceTypes.Single();
        }
    }
}