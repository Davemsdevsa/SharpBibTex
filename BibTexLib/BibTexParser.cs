using BibTexLib.Model;
using System.Linq;

namespace BibTexLib
{
    public class BibTexParser
    {
        public BibTexParser()
        {
        }

        public Bibliography Parse(string bibtexstring)
        {
            // TODO - Add Comment handling TODO - Add abbreviation handlings TODO - Add proper
            // exception handling
            string tempparser = bibtexstring.Replace("\r", "").Replace("\n", "");
            Bibliography bib = new Bibliography();

            bool endofstring = false;
            while (!endofstring)
            {
                var token = tempparser.First();

                switch (token)
                {
                    case '@':
                        //Section identified
                        string sectiontype = tempparser.Substring(1, tempparser.IndexOf('{') - 1);

                        switch (sectiontype)
                        {
                            case "STRING":
                                break;

                            case "PREAMBLE":
                                break;

                            case "COMMENT":
                                break;

                            default:
                                // An actual entry 
                                Entry entry = new Entry();
                                entry.EntryType = sectiontype;
                                string content = GetContentBetweenOpenCloseBraces(tempparser, '{', '}');
                                string entrystring = tempparser.Substring(0, tempparser.IndexOf('{') + 1) + content + "}";
                                bool proccontent = false;
                                string tempcontent = content;
                                while (!proccontent)
                                {
                                    if (tempcontent.Length == 0)
                                    {
                                        proccontent = true;
                                        break;
                                    }
                                    string tag = string.Empty;
                                    int commaindex = tempcontent.IndexOf(',');
                                    int braceindex = tempcontent.IndexOf('{');
                                    int markerindex = 0;
                                    if (commaindex > 0 && commaindex < braceindex)
                                    {
                                        tag = tempcontent.Substring(0, commaindex);

                                        markerindex = commaindex + 1;
                                    }
                                    else
                                    {
                                        tag = tempcontent.Substring(0, tempcontent.IndexOf('}') + 1);
                                        markerindex = tempcontent.IndexOf(',', tempcontent.IndexOf('}')) + 1;
                                        if (markerindex == 0) markerindex = tempcontent.Length;
                                    }

                                    if (!tag.Equals(string.Empty))
                                    {
                                        var tagitems = tag.Split('=');

                                        var tagkey = tagitems[0].Trim();
                                        var tagvalue = tagitems.Length > 1 ? tagitems[1].Replace("{", "").Replace("}", "") : "";

                                        // Todo Validate tag and Value

                                        entry.Tags.Add(tagkey, tagvalue);
                                    }
                                    tempcontent = tempcontent.Substring(markerindex);
                                }
                                bib.Entries.Add(entry);
                                tempparser = tempparser.Substring(entrystring.Length, tempparser.Length - entrystring.Length);
                                break;
                        }

                        break;

                    case '%':
                        // Comment token 
                        tempparser = tempparser.Substring(1);
                        int endcommentindex = tempparser.IndexOf('%'); // Check for close
                        string comment = tempparser.Substring(0, endcommentindex);
                        bib.Comments.Add(comment);
                        tempparser = tempparser.Substring(endcommentindex + 1);
                        break;

                    case ' ':
                        //Space
                        tempparser = tempparser.Substring(1);
                        break;

                    default:
                        tempparser = string.Empty;
                        break;
                }

                if (tempparser.Length == 0) endofstring = true;
            }

            return bib;
        }

        private string GetContentBetweenOpenCloseBraces(string toprocess, char openbrace, char closebrace)
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
    }
}