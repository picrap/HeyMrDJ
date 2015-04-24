#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion
namespace HeyMrDJ.Data
{
    using System.Linq;
    using ParkSquare.Gracenote;

    public class GracenoteMusicLookup : IMusicLookup
    {
        private GracenoteClient _gracenoteClient;

        /// <summary>
        /// Gets the gracenote client.
        /// </summary>
        /// <value>
        /// The gracenote client.
        /// </value>
        private GracenoteClient GracenoteClient
        {
            get
            {
                if (_gracenoteClient == null)
                    _gracenoteClient = new GracenoteClient("5911808-2E02E745CE9704D2BADA6060D461A3F8");
                return _gracenoteClient;
            }
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
            try
            {
                var result = GracenoteClient.Search(new SearchCriteria { Artist = artist, AlbumTitle = albumName, SearchMode = SearchMode.BestMatch });
                var firstAlbum = result.Albums.FirstOrDefault();
                if (firstAlbum == null)
                    return null;

                return new Album
                {
                    Artist = firstAlbum.Artist,
                    Title = firstAlbum.Title,
                    ReleaseYear = firstAlbum.Year == 0 ? (int?)null : firstAlbum.Year,
                    Tracks = firstAlbum.Tracks.Select(t => t.Title).ToArray()
                };
            }
            catch { }
            return null;
        }

        public void Dispose()
        {
        }
    }
}
