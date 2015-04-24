#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion

namespace HeyMrDJ
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DuoVia.FuzzyStrings;
    using ParkSquare.Gracenote;

    public class Lookup
    {
        public void T()
        {
            var c = new GracenoteClient("5911808-2E02E745CE9704D2BADA6060D461A3F8");
            var r5 = c.Search(new SearchCriteria { Artist = "Madonna", AlbumTitle = "Music" });
            var r1 = c.Search(new SearchCriteria { Artist = "Didier Super", AlbumTitle = "Bin quoi?" });
            var r2 = c.Search(new SearchCriteria { Artist = "Aphrodite's child", AlbumTitle = "666" });
            var r3 = c.Search(new SearchCriteria { Artist = "Cyndi Lauper", AlbumTitle = "Twelve deadly cyns" });
            var r4 = c.Search(new SearchCriteria { Artist = "Cindy Lauper", AlbumTitle = "Twelve deadly zinz" });

            //var t = FindTracks(FindAlbum(FindArtist("Didier Super"), "Bin quoi?"));
            //var t2 = FindTracks(FindAlbum(FindArtist("Aphrodite's child"), "666"));
            //var t3 = FindTracks(FindAlbum(FindArtist("Cyndi Lauper"), "Twelve deadly cyns"));
            //var t4 = FindTracks(FindAlbum(FindArtist("Cindy Lauper"), "Twelve deadly zinz"));
            //var t5 = FindTracks(FindAlbum(FindArtist("Madonna"), "Mozik"));

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
