using System.Collections.Generic;

namespace Webmaster442.LibItunesXmlDb
{
    public interface IITunesXmlDb
    {
        IEnumerable<Track> Tracks { get; }
        IEnumerable<string> Albums { get; }
        IEnumerable<string> Artists { get; }
        IEnumerable<string> Genres { get; }
        IEnumerable<string> Years { get; }
        IEnumerable<string> Playlists { get; }
        IEnumerable<Track> Filter(FilterKind kind, string param);
        IEnumerable<Track> ReadPlaylist(string id);
    }
}
