using System.Collections.Generic;

namespace BibTexLib.Model
{
    public class Bibliography
    {
        private List<string> abbreviations = new List<string>();

        private List<string> comments = new List<string>();

        private List<Entry> entries = new List<Entry>();

        private string preamble;

        public List<string> Abbreviations
        {
            get { return abbreviations; }
            set { abbreviations = value; }
        }

        public List<string> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public List<Entry> Entries
        {
            get { return entries; }
            set { entries = value; }
        }

        public string Preamble
        {
            get { return preamble; }
            set { preamble = value; }
        }
    }
}