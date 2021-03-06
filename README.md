# LibItunesXmlDb
An iTunes XML Database parser for C#

This project is an extended & reworked version of iTunesLibraryParser by Anthony Sciamanna

Original project url: https://github.com/asciamanna/iTunesLibraryParser

## Extended features
 * Updated tests to use Nunit3
 * Reworked API
     + Added support for Playlists
     + Added support for querying album names, artists, years, genres
 * Added build targets for multiple frameworks: 
     + .net 4.0
     + .net 4.5
     + .net 4.7 
 * Available as a nuget package

Documentation: https://github.com/webmaster442/LibItunesXmlDb/wiki/

## Version 2.0:

This version changes the ITunesXmlDb constructor. Now a ITunesXmlDbOptions instance must be passed besides the XML file path. This enables various configruation, like paralel xml parsing for speed.