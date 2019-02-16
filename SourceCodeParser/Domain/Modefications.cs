using System.Collections.Generic;

using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain
{
    public class Modefications
    {
        public List<LineRange> ModefiedRange { get; private set; }

        public Modefications(List<LineRange> modefiedRange)
        {
            this.ModefiedRange = modefiedRange;
        }
    }
}
