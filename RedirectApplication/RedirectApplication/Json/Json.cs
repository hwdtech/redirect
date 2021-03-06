﻿using System;
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
                return TargetDeserialization(reader);
            }
            return ConditionDeserialization(reader, serializer);
        }

        public object TargetDeserialization(JsonReader reader)
        {
            return reader.Value;
        }

        public object ConditionDeserialization(JsonReader reader, JsonSerializer serializer)
        {
            JArray array = null;
            try
            {
                array = JArray.Load(reader);
                return array.Children().Select(child =>
                {
                    ITreeNode node = null;
                        node = CreateNode(child);
                        if (node == null) throw new Exception("Node is empty");
                        serializer.Populate(child.CreateReader(), node);
                        return node;
                }).ToList();
            }
            catch
            {
            }
            return array;
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
            ITreeNode buf = null;
            try
            {
                buf = RulesType[type]();
                return buf;
            }
            catch
            { }
            return buf;
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}
