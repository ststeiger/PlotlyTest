// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CountryImport;
//
//    var data = GettingStarted.FromJson(jsonString);
//

// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CountryImport;
//
//    var data = GettingStarted.FromJson(jsonString);
//
namespace CountryImport
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class CountryData
    {
        [JsonProperty("latMin")]
        public double LatMin { get; set; }

        [JsonProperty("longMax")]
        public double LongMax { get; set; }

        [JsonProperty("latMax")]
        public double LatMax { get; set; }

        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("longMin")]
        public double LongMin { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }


        public static List<CountryData> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<CountryData>>(json, Overpass.Converter.ReadSettings);
        }

        public static List<CountryData> FromJsonFile(string fileName)
        {
            string json = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<CountryData>>(json, Overpass.Converter.ReadSettings);
        }

        public static string ToJson(List<CountryData> self)
        {
            return JsonConvert.SerializeObject(self, Overpass.Converter.WriteSettings);
        }
    }

}
