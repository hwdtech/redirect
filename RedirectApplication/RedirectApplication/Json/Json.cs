using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedirectApplication.Models;

namespace RedirectApplication.Json
{
    class TreeNodeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Path == "TargetUrl")
            {
                return reader.Value;
            }
            var array = JArray.Load(reader);
            return array.Children().Select(child =>
            {
                var node = CreateNode(child);
                serializer.Populate(child.CreateReader(), node);
                return node;
            }).ToList();
        }

        Dictionary<string, Func<ITreeNode>> RulesType = new Dictionary<string, Func<ITreeNode>>
        {
            { "Composite", () => new Composite() },
            { "ByBrowser", () => new ByBrowser()},
            { "ByLanguage", () => new ByLanguage()},
            { "ByCountry", () => new ByCountry()},
            { "ByIP", () => new ByIp()},
            { "ByOS", () => new ByOS()},
            { "ByDevice", () => new ByDevice()},
            { "ByDate", () => new ByDate()}
        };

        public ITreeNode CreateNode(JToken obj)
        {
            var type = (string)obj["Name"];
            try
            {
                return RulesType[type]();
            }
            catch (FormatException)
            {
                throw new NotSupportedException();
            }            
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}
