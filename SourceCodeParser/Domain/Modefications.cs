using System.Collections.Generic;
using System.Linq;
using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain
{
    public class Modifications
    {
        public List<LineRange> ModefiedRange { get; private set; }

        public Modifications(List<LineRange> modefiedRange)
        {
            this.ModefiedRange = modefiedRange;
        }

        public bool IsModified(LineRange range)
        {
            return ModefiedRange.Any(r => r.Overlap(range));
        }
    }
}
