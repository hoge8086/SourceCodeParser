using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeParser.Domain
{
    public interface IOutputModifiedFunctions
    {
        void Write(List<FunctionSummary> functions);
    }
}
