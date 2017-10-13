
namespace Google.API.Geocode
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class GeoCodedAddress
    {
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("partial_match")]
        public bool PartialMatch { get; set; }

        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public partial class AddressComponent
    {
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public Bounds Viewport { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lng")]
        public decimal Longitude { get; set; }
    }

    public partial class Bounds
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }
    

    public partial class GeoCodedAddress
    {
        public static GeoCodedAddress FromJson(string json)
        {
            return JsonConvert.DeserializeObject<GeoCodedAddress>(json, Overpass.Converter.ReadSettings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Overpass.Converter.WriteSettings);
        }

    }

}
