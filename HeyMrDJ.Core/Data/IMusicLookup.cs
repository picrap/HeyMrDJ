#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion
namespace HeyMrDJ.Data
{
    using System;

    /// <summary>
    /// Music Data Search
    /// </summary>
    public interface IMusicLookup: IDisposable
    {
        /// <summary>
        /// Searches the specified artist/album name.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>null if not found or failure</returns>
        Album Search(string artist, string albumName);
    }
}
