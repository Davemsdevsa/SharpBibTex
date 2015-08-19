﻿using System.Collections.Generic;

namespace BibTexLib.Model
{
    public class Entry
    {
        private string entrytrype;

        private Dictionary<string, string> tags = new Dictionary<string, string>();

        public Entry()
        {
        }

        public string EntryType
        {
            get { return entrytrype; }
            set { entrytrype = value; }
        }

        private string internalkey;

        public string InternalKey
        {
            get { return internalkey; }
            set { internalkey = value; }
        }


        public Dictionary<string, string> Tags
        {
            get { return tags; }
            set { tags = value; }
        }
    }
}