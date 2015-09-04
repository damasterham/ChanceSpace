using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Diagnostics;

namespace ChanceSpace.Documents
{
    /// <summary>
    /// Rss Feed, a common XmlDocument class
    /// </summary>
    public class Rss : XmlDocument
    {
        #region fields
        private string _path;
        private string _fileName;
        
        private XmlElement _root;
        private XmlProcessingInstruction _xpi;
        #endregion _fields_

        #region constructors
        /// <summary>
        /// Initialize Rss and sets its version, encoding, and rss version
        /// </summary>
        /// <param name="xmlVersion">Xml Version</param>
        /// <param name="xmlEncoding">Xml Encoding</param>
        /// <param name="rssVersion">Rss Version</param>
        public Rss(string xmlVersion, string xmlEncoding, string rssVersion)
        {
            this.Channel = new Dictionary<string, XmlElement>();
            this._xpi = this.CreateProcessingInstruction("xml", "version=\"" + xmlVersion + "\" encoding=\"" + xmlEncoding + "\"");
            this.AppendChild(this._xpi);

            this._root = this.CreateElement("rss");
            this._root.SetAttribute("version", rssVersion);
        }
        /// <summary>
        /// Initialize Rss with default values. 
        /// Xml Version 1.0, Xml Encoding UTF-8, RSS Version 2.0
        /// </summary>
        public Rss() : this("1.0", "UTF-8", "2.0") { }
        #endregion _constructors

        #region properties
        /// <summary>
        /// Get or Set the save path of the Rss Xml Document
        /// </summary>
        public string Path { get { return this._path; } set { this._path = value; } }
        /// <summary>
        /// Get the savefile path of the Rss Xml Document
        /// </summary>
        public string FullPath { get { return this._path + this._fileName; } }
        /// <summary>
        /// Get or Set the filename of the Rss Xml Document
        /// </summary>
        public string FileName { get { return this._fileName; } set { this._fileName = value; } }
        public Dictionary<string, XmlElement> Channel {get; set;}
        #endregion _properties

        #region methods
        /// <summary>
        /// Save the Rss Xml Document. Saving Appends all the channel elements to the rss element, and Appends the rss element to the Xml Document.
        /// </summary>
        public void Save()
        {
            foreach (XmlElement channel in Channel.Values)
            {
                this._root.AppendChild(channel);
            }
            this.AppendChild(this._root);
            Save(HttpContext.Current.Server.MapPath(FullPath) + ".xml");
        }
        /// <summary>
        /// Adds a Channel element to a dictionary of XmlElements that will be appended on Save
        /// </summary>
        /// <param name="title">The title of channel and the dictionary key</param>
        /// <param name="link">The link of the channel</param>
        /// <param name="description">The description of the channel</param>
        public void AddChannel(string title, string link, string description)
        {
            XmlElement channel = this.CreateElement("channel");
            XmlElement channelTitle = this.CreateElement("title");
            channelTitle.AppendChild(this.CreateTextNode(title));
            channel.AppendChild(channelTitle);
            XmlElement channelLink = this.CreateElement("link");
            channelLink.AppendChild(this.CreateTextNode(link));
            channel.AppendChild(channelLink);
            XmlElement channelDesc = this.CreateElement("description");
            channelDesc.AppendChild(this.CreateTextNode(description));
            channel.AppendChild(channelDesc);

            Channel.Add(title, channel);
        }
        /// <summary>
        /// Appends an item to a channel from the Channel dictionary
        /// </summary>
        /// <param name="channelTitle">The key of the Channel dictionary</param>
        /// <param name="title">The title of item</param>
        /// <param name="link">The link of the item</param>
        /// <param name="description">The description of the item</param>
        public void AddItem(string channelTitle, string title, string link, string description)
        {
            XmlElement item = this.CreateElement("item");
            XmlElement itemTitle = this.CreateElement("title");
            itemTitle.AppendChild(this.CreateTextNode(title));
            item.AppendChild(itemTitle);
            XmlElement itemLink = this.CreateElement("link");
            itemLink.AppendChild(this.CreateTextNode(link));
            item.AppendChild(itemLink);
            XmlElement itemDesc = this.CreateElement("description");
            itemDesc.AppendChild(this.CreateTextNode(description));
            item.AppendChild(itemDesc);

            XmlElement channel = Channel[channelTitle];
            channel.AppendChild(item);
        }
        #endregion _methods_
    }

 
}
