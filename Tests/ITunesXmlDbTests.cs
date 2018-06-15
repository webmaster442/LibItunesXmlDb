using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Webmaster442.LibItunesXmlDb;

namespace Tests
{
    [TestFixture]
    public class ITunesXmlDbTests
    {
        private const string FilePath = "sampleiTunesLibrary.xml";
        private IITunesXmlDb Sut;

        [SetUp]
        public void Setup()
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
            var file = System.IO.Path.Combine(basedir, FilePath);
            var testoptions = new ITunesXmlDbOptions
            {
                ExcludeNonExistingFiles = false,
                ParalelParsingEnabled = false
            };
            Sut = new ITunesXmlDb(file, testoptions);
        }

        [TearDown]
        public void TearDown()
        {
            Sut = null;
        }

        [Test]
        public void EnsureThat_ITunesDb_With_ExcludeNonExistingFiles_Option_Returns_Only_Existing_Files()
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
            var file = System.IO.Path.Combine(basedir, FilePath);
            var normalizedDb = new ITunesXmlDb(file, new ITunesXmlDbOptions
            {
                ParalelParsingEnabled = false,
                ExcludeNonExistingFiles = true
            });
            Assert.AreEqual(0, normalizedDb.Tracks.Count());
        }

        [Test]
        public void EnsureThat_ITunesDb_With_ParalelParsingEnabled_Option_Returns_Same_AsNonParalel()
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
            var file = System.IO.Path.Combine(basedir, FilePath);

            var paraleldb = new ITunesXmlDb(file, new ITunesXmlDbOptions
            {
                ParalelParsingEnabled = true,
                ExcludeNonExistingFiles = false
            });

            var db = new ITunesXmlDb(file, new ITunesXmlDbOptions
            {
                ParalelParsingEnabled = false,
                ExcludeNonExistingFiles = false
            });

            List<Track> tracks = new List<Track>(db.Tracks);
            List<Track> paralel = new List<Track>(paraleldb.Tracks);

            Assert.AreEqual(tracks.Count, paralel.Count);
            for (int i= 0; i<tracks.Count; i++)
            {
                bool equality = tracks[i] == paralel[i];
                Assert.AreEqual(true, equality);
            }
        }

        [Test]
        public void EnsureThat_Tracks_Parses_XML_Correctly()
        {
            var result = Sut.Tracks.First();
            Assert.AreEqual(17714, result.TrackId);
            Assert.AreEqual("Dream Gypsy", result.Name);
            Assert.AreEqual("Bill Evans & Jim Hall", result.Artist);
            Assert.AreEqual("Judith Veevers", result.Composer);
            Assert.AreEqual("Undercurrent", result.Album);
            Assert.AreEqual("Jazz", result.Genre);
            Assert.AreEqual("AAC audio file", result.Kind);
            Assert.AreEqual(11550486, result.Size);
            Assert.AreEqual(3, result.TrackNumber);
            Assert.AreEqual(1962, result.Year);
            Assert.AreEqual(new DateTime(2012, 2, 25), result.DateModified.Value.Date);
            Assert.AreEqual(new DateTime(2012, 2, 25), result.DateAdded.Value.Date);
            Assert.AreEqual(320, result.BitRate);
            Assert.AreEqual(44100, result.SampleRate);
            Assert.AreEqual(11, result.PlayCount);
            Assert.AreEqual(new DateTime(2012, 8, 16), result.PlayDate.Value.Date);
            Assert.AreEqual(true, result.PartOfCompilation);
        }

        [Test]
        public void EnsureThat_Tracks_populates_null_values_for_nonexistent_elements()
        {
            var result = Sut.Tracks.First();
            Assert.That(result.AlbumArtist, Is.Null.Or.Empty);
        }

        [Test]
        public void EnsureThat_Tracks_sets_boolean_properties_to_false_for_nonexistent_boolean_nodes()
        {
            Assert.That(Sut.Tracks.Count(t => t.PartOfCompilation), Is.EqualTo(2));
        }


        [Test]
        public void EnsureThat_Tracks_Get_Parses_All_Results()
        {
            Assert.AreEqual(25, Sut.Tracks.Count());
        }

        [Test]
        public void EnsureThat_Albums_FiltersAlbumNames()
        {
            Assert.AreEqual(5, Sut.Albums.Count());
        }

        [Test]
        public void EnsureThat_Artists_FiltersArtists()
        {
            Assert.AreEqual(6, Sut.Artists.Count());
        }

        [Test]
        public void EnsureThat_Geneires_FiltersGenres()
        {
            Assert.AreEqual(1, Sut.Genres.Count());
        }

        [Test]
        public void EnsureThat_Years_FiltersYears()
        {
            Assert.AreEqual(5, Sut.Years.Count());
        }

        [Test]
        public void EnsureThat_ReadPlaylist_ReturnsTracks()
        {
            Assert.AreEqual(6, Sut.ReadPlaylist("Songs To Learn 10/18/08").Count());
        }

        [Test]
        public void EnsureThat_Filter_FilterKind_Album_Works()
        {
            var tracks = Sut.Filter(FilterKind.Album, "Undercurrent");
            Assert.AreEqual(8, tracks.Count());
        }

        [Test]
        public void EnsureThat_Filter_FilterKind_Artist_Works()
        {
            var tracks = Sut.Filter(FilterKind.Artist, "Bill Evans & Jim Hall");
            Assert.AreEqual(8, tracks.Count());
        }

        [Test]
        public void EnsureThat_Filter_FilterKind_Genre_Works()
        {
            var tracks = Sut.Filter(FilterKind.Genre, "Jazz");
            Assert.AreEqual(25, tracks.Count());
        }

        [Test]
        public void EnsureThat_Filter_FilterKind_Year_Works()
        {
            var tracks = Sut.Filter(FilterKind.Year, "1962");
            Assert.AreEqual(10, tracks.Count());
        }
        [Test]
        public void EnsureThat_Filter_FilterKind_None_Works()
        {
            var tracks = Sut.Filter(FilterKind.None, null);
            Assert.AreEqual(25, tracks.Count());
        }
    }
}
