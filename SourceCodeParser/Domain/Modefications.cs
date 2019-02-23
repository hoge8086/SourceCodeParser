using System.Collections.Generic;
using System.Linq;
using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain
{
    public class Modifications
    {
        public List<LineRange> RangeList { get; private set; }

        public Modifications(List<LineRange> rangeList)
        {
            this.RangeList = rangeList;
        }

        public bool IsModified(LineRange range)
        {
            return RangeList.Any(r => r.Overlap(range));
        }
    }
}
