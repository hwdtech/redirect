using System.Collections.Generic;
using Newtonsoft.Json;
using RedirectApplication.Json;

namespace RedirectApplication.Models
{
    class PostJson
    {
        [JsonConverter(typeof(TreeNodeConverter))]
        public string TargetUrl { get; set; }

        [JsonConverter(typeof(TreeNodeConverter))]
        public List<ITreeNode> Conditions { get; set; }
    }

    public class DbJson
    {
        [JsonConverter(typeof(TreeNodeConverter))]
        public List<ITreeNode> Conditions { get; set; }

        public string SerialDBJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public interface ITreeNode
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
