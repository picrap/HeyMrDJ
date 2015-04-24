#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion

namespace HeyMrDJ
{
    using System;
    using System.Linq;
    using Data;
    using DuoVia.FuzzyStrings;

    public class Lookup
    {
        public void T()
        {
            using (var s = new CacheMusicLookup(new GracenoteMusicLookup()))
            {
                var r5 = s.Search("Madonna", "Music");
                var r1 = s.Search("Didier Super", "Bin quoi?");
                var r2 = s.Search("Aphrodite's child", "666");
                var r3 = s.Search("Cyndi Lauper", "Twelve deadly cyns");
                var r4 = s.Search("Cindy Lauper", "Twelve deadly zinz");
            }
        }

        private static string Trim(string rawName)
        {
            var t = new string(rawName.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
            return t;
        }

        private static string[] ToDoubleMetaphones(string sentence)
        {
            var words = sentence.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var dmWords = words.Select(w => w.ToDoubleMetaphone()).ToArray();
            return dmWords;
        }

        private static int LevenshteinDistance(string[] a, string[] b)
        {
            var toCompare = Math.Min(a.Length, b.Length);
            var distance = 0;
            for (int index = 0; index < toCompare; index++)
                distance += a[index].LevenshteinDistance(b[index]);
            distance += Math.Abs(a.Length - b.Length);
            return distance;
        }
    }
}
