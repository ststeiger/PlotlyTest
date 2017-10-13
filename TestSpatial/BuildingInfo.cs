
// https://quicktype.io/?l=cs&r=json2csharp
namespace Overpass.Building
{


    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class BuildingInfo
    {
        [JsonProperty("generator")]
        public string Generator { get; set; }

        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }

        [JsonProperty("osm3s")]
        public Osm3s Osm3s { get; set; }

        [JsonProperty("version")]
        public double Version { get; set; }
    }

    public partial class Element
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("changeset")]
        public long Changeset { get; set; }

        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("geometry")]
        public List<Geometry> Geometry { get; set; }

        [JsonProperty("tags")]
        public Tags Tags { get; set; }

        [JsonProperty("nodes")]
        public List<long> Nodes { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }

    public partial class Bounds
    {
        [JsonProperty("maxlon")]
        public double Maxlon { get; set; }

        [JsonProperty("maxlat")]
        public double Maxlat { get; set; }

        [JsonProperty("minlat")]
        public double Minlat { get; set; }

        [JsonProperty("minlon")]
        public double Minlon { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lon")]
        public decimal Longitude { get; set; }
    }

    public partial class Tags
    {
        [JsonProperty("addr:street")]
        public string AddrStreet { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("addr:housenumber")]
        public string AddrHousenumber { get; set; }

        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }
    }

    public partial class Osm3s
    {
        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("timestamp_osm_base")]
        public string TimestampOsmBase { get; set; }
    }



    public partial class BuildingInfo
    {

        public static BuildingInfo FromJson(string json)
        {
            if (json == null)
                return null;

            return JsonConvert.DeserializeObject<BuildingInfo>(json, Converter.ReadSettings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.WriteSettings);
        }

    }


}
