#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion

namespace HeyMrDJ.Data
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    public class CacheMusicLookup : IMusicLookup
    {
        private readonly IMusicLookup _musicLookup;

        private string _cacheDirectory;
        private string CacheDirectory
        {
            get
            {
                if (_cacheDirectory == null)
                {
                    _cacheDirectory = Path.Combine(Path.GetTempPath(), "HeyMrDJ! cache");
                    if (!Directory.Exists(_cacheDirectory))
                        Directory.CreateDirectory(_cacheDirectory);
                }
                return _cacheDirectory;
            }
        }

        public CacheMusicLookup(IMusicLookup musicLookup)
        {
            _musicLookup = musicLookup;
        }

        public void Dispose()
        {
            _musicLookup.Dispose();
#if !DEBUG
            try
            {
                Directory.Delete(CacheDirectory, true);
            }
            catch { }
#endif
        }

        /// <summary>
        /// Searches the specified artist/album name.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>
        /// null if not found or failure
        /// </returns>
        public Album Search(string artist, string albumName)
        {
            return GetFromCache(artist, albumName) ?? GetFromLookup(artist, albumName);
        }

        private Album GetFromCache(string artist, string albumName)
        {
            try
            {
                var path = GetCachePath(artist, albumName);
                if (File.Exists(path))
                {
                    if (File.GetCreationTimeUtc(path) - DateTime.UtcNow > TimeSpan.FromHours(1))
                        return null;

                    var json = File.ReadAllText(path);
                    var album = JsonConvert.DeserializeObject<Album>(json);
                    return album;
                }
            }
            catch { }
            return null;
        }

        private Album GetFromLookup(string artist, string albumName)
        {
            var album = _musicLookup.Search(artist, albumName);
            if (album == null)
                return null;

            var json = JsonConvert.SerializeObject(album, Formatting.Indented);
            File.WriteAllText(GetCachePath(artist, albumName), json);

            return album;
        }

        /// <summary>
        /// Gets the cache path.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="albumName">Name of the album.</param>
        /// <returns></returns>
        public string GetCachePath(string artist, string albumName)
        {
            return Path.Combine(CacheDirectory,  string.Format("{0} ---- {1}.json", artist, albumName)
                .Replace('?', '¿'));
        }
    }
}
