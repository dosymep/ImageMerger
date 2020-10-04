using System;

namespace ImageMerger {
    public static class StringExtesions {
        public static int? ToInt(this string value) {
            return string.IsNullOrEmpty(value) ? (int?) null : Convert.ToInt32(value);
        }

        public static int ToDefaultInt(this string value) {
            return value.ToInt() ?? 0;
        }

        public static int ToMinInt(this string value) {
            return value.ToInt() ?? int.MinValue;
        }

        public static int ToMaxInt(this string value) {
            return value.ToInt() ?? int.MaxValue;
        }
    }
}
