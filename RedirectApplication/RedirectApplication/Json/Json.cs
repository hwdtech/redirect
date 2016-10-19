using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RedirectApplication.Json
{
    public class JsonWorking
    {
        public void Main(string tree)
        {
            var reader = new JsonTextReader(new StringReader(tree));
            PostJson content = JsonSerializer.CreateDefault().Deserialize<PostJson>(reader);
        }
    }

    class TreeNodeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.Load(reader);
            var target = new List<ITreeNode>();


            if (array.HasValues)
            {
                foreach (var child in array.Children())
                {
                    var node = CreateNode(child);

                    serializer.Populate(child.CreateReader(), node);
                    target.Add(node);
                }
            }
            return target;
        }

        private ITreeNode CreateNode(JToken obj)
        {
            var type = (string)obj["name"];

            switch (type)
            {
                case "Composite":
                    return new Composite();
                case "ByBrowser":
                    return new ByBrowser();
                case "ByLanguage":
                    return new ByLanguage();
                case "ByCountry":
                    return new ByCountry();
                case "ByIp":
                    return new ByIp();
                case "ByOS":
                    return new ByOS();
                case "ByDevice":
                    return new ByDevice();
                case "ByDate":
                    return new ByDate();
                default:
                    throw new NotSupportedException();

            }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

    class PostJson
    {
        [JsonConverter(typeof(TreeNodeConverter))]
        public string TargetUrl { get; set; }
        [JsonConverter(typeof(TreeNodeConverter))]
        public List<ITreeNode> Conditions { get; set; }
    }

    class DbJson
    {
        [JsonConverter(typeof(TreeNodeConverter))]
        public List<ITreeNode> Conditions { get; set; }
    }

    interface ITreeNode
    {
        string Name { get; set; }
    }

    class Composite : ITreeNode
    {
        public string Name { get; set; }

        [JsonConverter(typeof(TreeNodeConverter))]
        public List<ITreeNode> Rules { get; set; }

        public string url { get; set; }
    }

    class ByBrowser : ITreeNode
    {
        public string Name { get; set; }

        public string Browser { get; set; }

        public string Url { get; set; }
    }
    class ByLanguage : ITreeNode
    {
        public string Name { get; set; }

        public string Language { get; set; }

        public string Url { get; set; }
    }
    class ByCountry : ITreeNode
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string Url { get; set; }
    }
    class ByIp : ITreeNode
    {
        public string Name { get; set; }

        public string[] Ip { get; set; }

        public string Url { get; set; }
    }
    class ByOS : ITreeNode
    {
        public string Name { get; set; }

        public string OS { get; set; }

        public string Url { get; set; }
    }
    class ByDevice : ITreeNode
    {
        public string Name { get; set; }

        public string Device { get; set; }

        public string Url { get; set; }
    }
    class ByDate : ITreeNode
    {
        public string Name { get; set; }

        public string Date { get; set; }

        public string Url { get; set; }
    }
}
