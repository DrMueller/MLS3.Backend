using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mmu.Mls3.Localization.Areas
{
    public static class SupportedCultures
    {
        public static Lazy<IReadOnlyCollection<CultureInfo>> All { get; } = new Lazy<IReadOnlyCollection<CultureInfo>>(() =>
        {
            return new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("de")
            };
        });

        public static CultureInfo English { get; } = new CultureInfo("en");
        public static CultureInfo German { get; } = new CultureInfo("de");
    }
}