using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

using SourceCodeParser.Domain.SourceCodeParser;
namespace SourceCodeParser.Infrastructure
{
    public class ParseSettingLoader
    {
        private static readonly DataContractJsonSerializerSettings SerializeSettings = 
            new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            };

        public Domain.SourceCodeParser.SourceCodeParser.Setting  Load(string path)
        {
            Domain.SourceCodeParser.SourceCodeParser.Setting setting;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractJsonSerializer(typeof(Domain.SourceCodeParser.SourceCodeParser.Setting));
                setting = serializer.ReadObject(fs) as Domain.SourceCodeParser.SourceCodeParser.Setting;
            }
            return setting;
        }

        public void Save(string path, Domain.SourceCodeParser.SourceCodeParser.Setting setting)
        {
            using (var stream = File.Create(path))
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true, "  "))
            {
                var serializer = new DataContractJsonSerializer(typeof(Domain.SourceCodeParser.SourceCodeParser.Setting), SerializeSettings);
                serializer.WriteObject(writer, setting);
                writer.Flush();
            }

        }
    }
}
