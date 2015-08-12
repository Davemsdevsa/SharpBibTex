using BibTexLib;
using BibTexLib.Model;
using Microsoft.Office.Tools.Ribbon;
using System.Linq;
using System.Windows.Forms;

namespace SharpBibTex
{
    public partial class BibTexRibbon
    {
        private void BibTexRibbon_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void cmdButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                ProcessBib(Clipboard.GetText());
            }
        }

        private void ProcessBib(string bib)
        {
            //Validate
            try
            {
                var retbib = new BibTexParser().Parse(bib);

                //Process
                string xml = string.Empty;
                string tag = string.Empty;
                string sourceType = string.Empty;
                string title = string.Empty;
                string authors = string.Empty;

                string year = string.Empty;
                string city = string.Empty;
                string publisher = string.Empty;
                string volume = string.Empty;
                string pages = string.Empty;
                string doi = string.Empty;
                string issue = string.Empty;
                string people = string.Empty;
                string journal = string.Empty;
                string masid = string.Empty;

                foreach (Entry entry in retbib.Entries)
                {
                    sourceType = entry.EntryType;

                    if (entry.Tags.ContainsKey("title"))
                    {
                        title = entry.Tags["title"];
                    }

                    if (entry.Tags.ContainsKey("year"))
                    {
                        year = entry.Tags["year"];
                    }

                    if (entry.Tags.ContainsKey("city"))
                    {
                        year = entry.Tags["city"];
                    }

                    if (entry.Tags.ContainsKey("publisher"))
                    {
                        year = entry.Tags["publisher"];
                    }

                    if (entry.Tags.ContainsKey("journal"))
                    {
                        journal = entry.Tags["journal"];
                    }

                    if (entry.Tags.ContainsKey("volume"))
                    {
                        volume = entry.Tags["volume"];
                    }

                    if (entry.Tags.ContainsKey("issue"))
                    {
                        issue = entry.Tags["issue"];
                    }

                    if (entry.Tags.ContainsKey("doi"))
                    {
                        doi = entry.Tags["doi"];
                    }

                    if (entry.Tags.ContainsKey("pages"))
                    {
                        pages = entry.Tags["pages"];
                    }

                    string authorsxml = string.Empty;
                    string authortag = string.Empty;

                    if (entry.Tags.ContainsKey("author"))
                    {
                        authors = entry.Tags["author"];

                        var authorslist = authors.Replace("and", ";").Split(';');
                        bool isFirst = true;
                        foreach (string author in authorslist)
                        {
                            var authorfs = author.Trim().Split(' ');
                            if (isFirst)
                            {
                                string temp = authorfs.Last().Trim().PadRight(3, '0');
                                authortag = temp.Substring(0, 3);
                                isFirst = false;
                            }
                            authorsxml += "<b:Person>" +
                                       "<b:Last>" + authorfs.Last() + "</b:Last>" +
                                       "<b:First>" + authorfs.First() + "</b:First>" +
                                   "</b:Person>";
                        }
                    }

                    tag = authortag + Globals.ThisAddIn.Application.ActiveDocument.Bibliography.GenerateUniqueTag();
                    string guid = System.Guid.NewGuid().ToString();
                    xml =
                    "<b:Source>" +
                        "<b:Tag>" + tag + "</b:Tag>" +
                        "<b:SourceType>" + sourceType + "</b:SourceType>" +
                        "<b:Guid>" + guid + "</b:Guid>" +
                        "<b:LUID>0</b:LUID>" +
                        "<b:Author><b:NameList>" +
                            authorsxml +
                        "</b:NameList></b:Author>" +
                        "<b:Title>" + title + "</b:Title>" +
                        "<b:Year>" + year + "</b:Year>" +
                        "<b:City>" + city + "</b:City>" +
                        "<b:Journal>" + journal + "</b:Journal>" +
                        "<b:Volume>" + volume + "</b:Volume>" +
                        "<b:Issue>" + issue + "</b:Issue>" +
                        "<b:Pages>" + pages + "</b:Pages>" +
                        "<b:DOI>" + doi + "</b:DOI>" +
                        "<b:Publisher>" + publisher + "</b:Publisher>"+
                    "</b:Source>";

                    Globals.ThisAddIn.Application.ActiveDocument.Bibliography.Sources.Add(xml);
                }
            }
            catch
            {
            }
        }

        private void cmdFromFile_Click(object sender, RibbonControlEventArgs e)
        {
            var temp = Globals.ThisAddIn.Application.Bibliography.Sources[1];
            var xml = temp.XML;
        }
    }
}