using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
namespace SourceCodeParser.Domain.SourceCodeParser
{
    [DataContract]
    public class Setting
    {
        [DataContract]
        public class FunctionCheckerInfo
        {
            [DataMember]
            public string FunctionCheckerName{ get; set; }
            [DataMember]
            public List<string> Args{ get; set; }
        }

        [DataMember]
        public List<string> TargetExtensions{ get; set; }
        [DataMember]
        public List<string> CommentAndIgnoreRegexp { get; set; }
        [DataMember]
        public List<string> FunctionRegexp { get; set; }
        [DataMember]
        public string FunctionBeginMarker { get; set; }
        [DataMember]
        public string FunctionEndMarker { get; set; }
        [DataMember]
        public List<FunctionCheckerInfo> FunctionCheckerInfos { get; set; }

        private static List<Type> allFunctionCheckerTypes;

        static Setting()
        {
            allFunctionCheckerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.IsClass && a.GetInterface(typeof(IFunctionChecker).Name) != null).ToList();
        }

        public List<IFunctionChecker> CreateFunctionCheckers()
        {
            if (FunctionCheckerInfos == null)
                return new List<IFunctionChecker>();

            var checkers = new List<IFunctionChecker>();
            foreach(var checkerInfo in FunctionCheckerInfos)
            {
                var checkerType = allFunctionCheckerTypes.FirstOrDefault(t => t.Name == checkerInfo.FunctionCheckerName);
                if (checkerType == null)
                    throw new FormatException(checkerInfo.FunctionCheckerName + "は存在しません.");

                var checker = Activator.CreateInstance(checkerType, checkerInfo.Args.Select(x => (object)x).ToArray()) as IFunctionChecker;
                checkers.Add(checker);
            }
            return checkers;
        }

        public bool IsParsable(string path)
        {
            var extension = System.IO.Path.GetExtension(path);
            return TargetExtensions.Any(e => extension.Equals(e, StringComparison.OrdinalIgnoreCase));
        }

    }
}
