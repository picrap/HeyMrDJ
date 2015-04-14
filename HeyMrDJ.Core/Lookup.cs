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
    using System.Text;
    using DuoVia.FuzzyStrings;
    using MusicBrainz;

    public class Lookup
    {
        public void T()
        {
            var t = FindTracks(FindAlbum(FindArtist("Didier Super"), "Bin quoi?"));
            var t2 = FindTracks(FindAlbum(FindArtist("Aphrodite's child"), "666"));
            var t3 = FindTracks(FindAlbum(FindArtist("Cyndi Lauper"), "Twelve deadly cyns"));
            var t4 = FindTracks(FindAlbum(FindArtist("Cindy Lauper"), "Twelve deadly zinz"));
            var t5 = FindTracks(FindAlbum(FindArtist("Madonna"), "Mozik"));

            var a = Search.Artist("Didier Super").Data[0];
            var c = Search.Release(arid: a.Id);
            var z = Search.Release("Didier Super - Ben quoi?");
        }

        private string[][] FindTracks(IEnumerable<string> albumIDs)
        {
            return EnumerateTracks(albumIDs).ToArray();
        }

        private static IEnumerable<string[]> EnumerateTracks(IEnumerable<string> albumIDs)
        {
            foreach (var albumID in albumIDs)
            {
                var recording = Search.Recording(reid: albumID);
                if (recording == null)
                    continue;
                yield return recording.Data.Select(t => t.Title).ToArray();
            }
        }

        public string FindArtist(string name)
        {
            var artist = Search.Artist(Trim(name), limit: 1).Data.FirstOrDefault();
            if (artist == null)
                return null;
            return artist.Id;
        }

        public string[] FindAlbum(string artistID, string name)
        {
            if (artistID == null)
                return null;
            var trimmedName = Trim(name);
            var singleAlbum = Search.Release(trimmedName, arid: artistID, type: "album").Data
                .Concat(Search.Release(trimmedName, arid: artistID, type: "compilation").Data)
                .ToArray();
            if (singleAlbum.Length == 0)
            {
                var dmNameWords = ToDoubleMetaphones(trimmedName);
                var albums = Search.Release(arid: artistID, type: "album", limit: 100).Data.Concat(Search.Release(arid: artistID, type: "compilation", limit: 100).Data).ToArray();
                var orderedAlbums = from album in albums
                                    let dmTitleWords = ToDoubleMetaphones(Trim(album.Title))
                                    let distance = LevenshteinDistance(dmTitleWords, dmNameWords)
                                    group album by distance into homonyms
                                    orderby homonyms.Key
                                    select new { Distance = homonyms.Key, Albums = homonyms.ToArray() };
                var firstAlbum = orderedAlbums.FirstOrDefault();
                if (firstAlbum != null)
                    return firstAlbum.Albums.Select(a => a.Id).ToArray();
                return null;
            }
            return singleAlbum.Select(a => a.Id).ToArray();
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
