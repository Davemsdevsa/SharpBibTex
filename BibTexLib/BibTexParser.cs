using BibTexLib.Exceptions;
using BibTexLib.Model;
using System.Text.RegularExpressions;

namespace BibTexLib
{
    public class BibTexParser
    {
        public BibTexParser()
        {
        }

        public string GetContentBetweenOpenCloseBraces(string toprocess, char openbrace, char closebrace)
        {
            int openb = 0;
            bool open = false;

            string retval = string.Empty;
            for (int i = 0; i < toprocess.Length; i++)
            {
                if (toprocess[i].Equals(openbrace))
                {
                    openb++;
                    open = true;
                }

                if (toprocess[i].Equals(closebrace)) openb--;

                if (open && openb == 0)
                {
                    int index = toprocess.IndexOf(openbrace);
                    retval = toprocess.Substring(index + 1, i - index - 1);
                    break;
                }
            }
            return retval;
        }

        public Bibliography Parse(string bibtexstring)
        {
            // TODO - Add Comment handling TODO - Add abbreviation handlings TODO - Add proper 
            Bibliography bib = new Bibliography();
            // exception handling 
            string tempparser = bibtexstring.Replace("\r", "").Replace("\n", "");

            int index = 0;

            while (tempparser.Length > index + 1)
            {
                switch (tempparser[index])
                {
                    case '@':
                        // Entity Detected 
                        int openindex = tempparser.IndexOf('{', index);
                        string entityname = tempparser.Substring(index + 1, openindex - index - 1);
                        if (entityname.Equals(string.Empty)) throw new BibTexParseException("Entry malformed, No entity name specified");

                        switch (entityname.ToLower())
                        {
                            case "string":
                                // TODO - handle variables and process once entity is found. 
                                break;

                            case "preamble":
                                // Ignore preamble as is not relevant for MS WORD 
                                break;

                            default:
                                Entry entry = new Entry();
                                entry.EntryType = entityname;

                                string entitycontents = GetContentBetweenOpenCloseBraces(tempparser.Substring(openindex), '{', '}');

                                ProcessEntry(openindex, entityname, entry, entitycontents);

                                bib.Entries.Add(entry);

                                int closeindex = openindex + entitycontents.Length;
                                index = closeindex;

                                break;
                        }
                        break;

                    default:
                        index++;
                        break;
                }
            }

            return bib;
        }

        private static void ProcessEntry(int openindex, string entityname, Entry entry, string entitycontents)
        {
            string matchpattern = "[,]?[A-Za-z0-9]*[ ]?[=]";
            if (entitycontents.Equals(string.Empty)) throw new BibTexParseException(string.Format("Entry malformed, no contents for entry found for entry {0} - position {1}", entityname, openindex));

            bool internalkeycheck = false;
            var matches = Regex.Matches(entitycontents, matchpattern);
            for (int i = 0; i < matches.Count; i++)
            {
                if (!internalkeycheck)
                {
                    internalkeycheck = true;
                    if (matches[i].Index > 0) entry.InternalKey = entitycontents.Substring(0, matches[i].Index).Trim();
                }

                int startindex = matches[i].Index + 1;
                int endindex = (i + 1 >= matches.Count ? entitycontents.Length : matches[i + 1].Index);

                string tagtext = entitycontents.Substring(startindex, endindex - startindex);
                var splittext = tagtext.Split('=');
                string key = splittext[0].Trim();
                string val = splittext.Length > 1 ? splittext[1].Trim() : string.Empty;
                entry.Tags.Add(key, val);
            }
        }
    }
}