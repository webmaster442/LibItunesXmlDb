﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Webmaster442.LibItunesXmlDb.Internals;

namespace Webmaster442.LibItunesXmlDb
{
    public class ITunesXmlDb: IITunesXmlDb
    {
        private XDocument _xml;
        private List<Track> _tracks;
        private bool _normalize;

        #region ctor
        /// <summary>
        /// Load an iTunes XML File database
        /// </summary>
        /// <param name="fileLocation">full path of iTunes Music Library.xml</param>
        /// <param name="normalize">if true, then only those files will be returned that exist on the hdd</param>
        public ITunesXmlDb(string fileLocation, bool normalize = false)
        {
            _xml = XDocument.Load(fileLocation);
            _normalize = normalize;
        }
        #endregion

        private IEnumerable<XElement> LoadTrackElements()
        {
            return from x in _xml.Descendants("dict")
                   .Descendants("dict")
                   .Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
        }

        private IEnumerable<XElement> LoadPlaylists()
        {
            return from x in _xml.Descendants("dict")
                   .Descendants("array")
                   .Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
        }

        public IEnumerable<Track> Tracks
        {
            get
            {
                if (_tracks == null)
                {
                    var trackElements = LoadTrackElements();
                    _tracks = new List<Track>(from track in trackElements
                                              select Parser.CreateTrack(track, _normalize));
                }
                return _tracks;
            }
        }

        public IEnumerable<string> Albums
        {
            get { return Tracks.Select(t => t.Album).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Artists
        {
            get { return Tracks.Select(t => t.Artist).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Genres
        {
            get { return Tracks.Select(t => t.Genre).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Years
        {
            get { return Tracks.Select(t => t.Year.ToString()).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Playlists
        {
            get
            {
                var playlistNodes = LoadPlaylists();
                foreach (var item in playlistNodes)
                {
                    yield return item.ParseStringValue("Name");
                }
            }
        }

        public IEnumerable<Track> Filter(FilterKind kind, string param)
        {
            switch (kind)
            {
                case FilterKind.Album:
                    return Tracks.Where(t => t.Album == param);
                case FilterKind.Artist:
                    return Tracks.Where(t => t.Artist == param);
                case FilterKind.Genre:
                    return Tracks.Where(t => t.Genre == param);
                case FilterKind.Year:
                    return Tracks.Where(t => t.Year == int.Parse(param));
                case FilterKind.None:
                default:
                    return Tracks;
            }
        }

        public IEnumerable<Track> ReadPlaylist(string id)
        {
            var playlistNodes = LoadPlaylists();

            var query = from node in playlistNodes
                        where node.ParseStringValue("Name") == id
                        select node.Descendants("array").Descendants("dict");

            foreach (var item in query)
            {
                foreach (var subitem in item)
                {
                    var trackid = Int32.Parse(subitem.ParseStringValue("Track ID"));
                    var track = Tracks.Where(t => t.TrackId == trackid).FirstOrDefault();
                    yield return track;
                }
            }
        }

        #region static Helpers
        /// <summary>
        /// Return the default user specific path for iTunes Music Library.xml
        /// </summary>
        public static string UserItunesDbPath
        {
            get
            {
                var musicfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                return System.IO.Path.Combine(musicfolder, @"iTunes\iTunes Music Library.xml");
            }
        }

        /// <summary>
        /// Returns true, if the user has a iTunes Music Library.xml at the default location
        /// </summary>
        public static bool UserHasItunesDb
        {
            get { return System.IO.File.Exists(UserItunesDbPath); }
        }
        #endregion
    }
}