using System;
using System.Collections.Generic;

namespace Webmaster442.LibItunesXmlDb
{
    public class Track : IEquatable<Track>
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string Composer { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Kind { get; set; }
        public long Size { get; set; }
        public string PlayingTime { get; set; }
        public int? TrackNumber { get; set; }
        public int? Year { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateAdded { get; set; }
        public int? BitRate { get; set; }
        public int? SampleRate { get; set; }
        public int? PlayCount { get; set; }
        public DateTime? PlayDate { get; set; }
        public bool PartOfCompilation { get; set; }
        public string FilePath { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Track);
        }

        public bool Equals(Track other)
        {
            return other != null &&
                   TrackId == other.TrackId &&
                   Name == other.Name &&
                   Artist == other.Artist &&
                   AlbumArtist == other.AlbumArtist &&
                   Composer == other.Composer &&
                   Album == other.Album &&
                   Genre == other.Genre &&
                   Kind == other.Kind &&
                   Size == other.Size &&
                   PlayingTime == other.PlayingTime &&
                   EqualityComparer<int?>.Default.Equals(TrackNumber, other.TrackNumber) &&
                   EqualityComparer<int?>.Default.Equals(Year, other.Year) &&
                   EqualityComparer<DateTime?>.Default.Equals(DateModified, other.DateModified) &&
                   EqualityComparer<DateTime?>.Default.Equals(DateAdded, other.DateAdded) &&
                   EqualityComparer<int?>.Default.Equals(BitRate, other.BitRate) &&
                   EqualityComparer<int?>.Default.Equals(SampleRate, other.SampleRate) &&
                   EqualityComparer<int?>.Default.Equals(PlayCount, other.PlayCount) &&
                   EqualityComparer<DateTime?>.Default.Equals(PlayDate, other.PlayDate) &&
                   PartOfCompilation == other.PartOfCompilation &&
                   FilePath == other.FilePath;
        }

        public override int GetHashCode()
        {
            var hashCode = 404681566;
            hashCode = hashCode * -1521134295 + TrackId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Artist);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AlbumArtist);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Composer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Album);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Genre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Kind);
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PlayingTime);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(TrackNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(Year);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(DateModified);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(DateAdded);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(BitRate);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(SampleRate);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(PlayCount);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(PlayDate);
            hashCode = hashCode * -1521134295 + PartOfCompilation.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FilePath);
            return hashCode;
        }

        public static bool operator ==(Track track1, Track track2)
        {
            return EqualityComparer<Track>.Default.Equals(track1, track2);
        }

        public static bool operator !=(Track track1, Track track2)
        {
            return !(track1 == track2);
        }
    }
}
