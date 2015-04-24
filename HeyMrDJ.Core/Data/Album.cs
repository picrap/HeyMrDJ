#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion
namespace HeyMrDJ.Data
{
    using System.Diagnostics;

    [DebuggerDisplay("{Artist} - ({ReleaseYear}) {Title}")]
    public class Album
    {
        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>
        /// The artist.
        /// </value>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the release year.
        /// </summary>
        /// <value>
        /// The release year.
        /// </value>
        public int? ReleaseYear { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        public string[] Tracks { get; set; }
    }
}
