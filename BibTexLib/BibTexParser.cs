using BibTexLib.Exceptions;
using BibTexLib.Model;
using System;
using System.Text.RegularExpressions;

namespace BibTexLib
{
    public class BibTexParser
    {
        public BibTexParser()
        {
        }

        /// <summary>
        /// Function to detect content between matching braces 
        /// </summary>
        /// <param name="toprocess">
        /// This is the string that should be processed 
        /// </param>
        /// <param name="openbrace">
        /// This is the character that signifies an open brace 
        /// </param>
        /// <param name="closebrace">
        /// This is the character that signifies a close brace 
        /// </param>
        /// <returns>
        /// </returns>
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

                                ProcessEntry(entityname, entry, entitycontents);

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

        /// <summary>
        /// This is the process 
        /// </summary>
        /// <param name="openindex">
        /// </param>
        /// <param name="entityname">
        /// </param>
        /// <param name="entry">
        /// </param>
        /// <param name="entitycontents">
        /// </param>
        private void ProcessEntry(string entityname, Entry entry, string entitycontents)
        {
            if (entitycontents.Equals(string.Empty)) throw new BibTexParseException(string.Format("Entry malformed, no contents for entry found for entry {0}", entityname));
            bool internalKeySet = false;
            // Process all tags and validate based on entity type 
            int index = 0;
            int previousentityindex = 0;
            int braces = 0;
            while (index < entitycontents.Length)
            {
                //Check if we have a valid entity, ends in ',' or end of the string
                if (entitycontents[index].Equals(',') && braces == 0)
                {
                    // Handle entity 
                    string entity = entitycontents.Substring(previousentityindex, index - previousentityindex);
                    entity = entity.Trim(); // Clear wrapped whitespace
                    if (entity.Contains("="))
                    {
                        //Key Value Pair
                        var kv = entity.Split(new char[] { '=' }, 2);
                        string key = kv[0].Trim();
                        string value = kv[1].Trim();
                        if (!value.Trim().Equals(string.Empty))
                        {
                            //Dont add the key if it has no data
                            if (value.Contains("{"))
                            {
                                value = GetContentBetweenOpenCloseBraces(value, '{', '}').Trim();
                            }

                            entry.Tags.Add(key, value);
                        }
                    }
                    else
                    {
                        //Internal Key
                        if (!internalKeySet && !entity.Equals(string.Empty))
                        {
                            entry.InternalKey = entity;
                            internalKeySet = true;
                        }
                    }

                    previousentityindex = index + 1;
                }

                if (entitycontents[index].Equals('{')) braces++;
                if (entitycontents[index].Equals('}')) braces--;

                index++;
            }
        }
    }
}