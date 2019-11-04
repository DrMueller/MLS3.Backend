using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mmu.Mls3.Common.Areas.Localization
{
    public static class SupportedCultures
    {
        public static Lazy<IReadOnlyCollection<CultureInfo>> All { get; } = new Lazy<IReadOnlyCollection<CultureInfo>>(
            () => new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("de") });

        public static CultureInfo English { get; } = new CultureInfo("en");
        public static CultureInfo German { get; } = new CultureInfo("de");
    }
}