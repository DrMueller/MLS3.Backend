using System;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Servants
{
    internal interface IResourceTypeFetcher
    {
        string CreateResourceKey(Type requestType);

        Type FetchResourceType(string resourceKey);
    }
}