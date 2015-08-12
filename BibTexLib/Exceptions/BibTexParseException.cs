using System;

namespace BibTexLib.Exceptions
{
    [Serializable]
    public class BibTexParseException : Exception
    {
        public BibTexParseException(string error) : base(error)
        {
        }
    }
}