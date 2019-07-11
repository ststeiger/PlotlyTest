
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Google.Maps.Directions;
//
//    var rootNode = RootNode.FromJson(jsonString);

// namespace GeoApis
// https://app.quicktype.io/#l=cs&r=json2csharp

// https://maps.googleapis.com/maps/api/directions/json?key=key&origin=47.378141,8.540168&destination=47.551635,9.226241&transit_mode=rail&mode=transit

// https://developers.google.com/maps/documentation/directions/intro#TravelModes


namespace Google.Maps.Directions
{
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RootNode
    {
        [JsonProperty("geocoded_waypoints")]
        public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }

        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }



        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }


        public static RootNode FromJson(string json)
        {
            return JsonConvert.DeserializeObject<RootNode>(json, Converter.Settings);
        }


        internal class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
    }

    public partial class GeocodedWaypoint
    {
        [JsonProperty("geocoder_status")]
        public string GeocoderStatus { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public partial class Route
    {
        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("copyrights")]
        public string Copyrights { get; set; }

        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }

        [JsonProperty("overview_polyline")]
        public Polyline OverviewPolyline { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

        [JsonProperty("waypoint_order")]
        public List<object> WaypointOrder { get; set; }
    }

    public partial class Bounds
    {
        [JsonProperty("northeast")]
        public Northeast Northeast { get; set; }

        [JsonProperty("southwest")]
        public Northeast Southwest { get; set; }
    }

    public partial class Northeast
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public partial class Leg
    {
        [JsonProperty("arrival_time")]
        public Time ArrivalTime { get; set; }

        [JsonProperty("departure_time")]
        public Time DepartureTime { get; set; }

        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Distance Duration { get; set; }

        [JsonProperty("end_address")]
        public string EndAddress { get; set; }

        [JsonProperty("end_location")]
        public Northeast EndLocation { get; set; }

        [JsonProperty("start_address")]
        public string StartAddress { get; set; }

        [JsonProperty("start_location")]
        public Northeast StartLocation { get; set; }

        [JsonProperty("steps")]
        public List<LegStep> Steps { get; set; }

        [JsonProperty("traffic_speed_entry")]
        public List<object> TrafficSpeedEntry { get; set; }

        [JsonProperty("via_waypoint")]
        public List<object> ViaWaypoint { get; set; }
    }

    public partial class Time
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class Distance
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class LegStep
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Distance Duration { get; set; }

        [JsonProperty("end_location")]
        public Northeast EndLocation { get; set; }

        [JsonProperty("html_instructions")]
        public string HtmlInstructions { get; set; }

        [JsonProperty("polyline")]
        public Polyline Polyline { get; set; }

        [JsonProperty("start_location")]
        public Northeast StartLocation { get; set; }

        [JsonProperty("transit_details", NullValueHandling = NullValueHandling.Ignore)]
        public TransitDetails TransitDetails { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }

        [JsonProperty("steps", NullValueHandling = NullValueHandling.Ignore)]
        public List<StepStep> Steps { get; set; }
    }

    public partial class Polyline
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }

    public partial class StepStep
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Distance Duration { get; set; }

        [JsonProperty("end_location")]
        public Northeast EndLocation { get; set; }

        [JsonProperty("polyline")]
        public Polyline Polyline { get; set; }

        [JsonProperty("start_location")]
        public Northeast StartLocation { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }

        [JsonProperty("html_instructions", NullValueHandling = NullValueHandling.Ignore)]
        public string HtmlInstructions { get; set; }

        [JsonProperty("maneuver", NullValueHandling = NullValueHandling.Ignore)]
        public string Maneuver { get; set; }
    }

    public partial class TransitDetails
    {
        [JsonProperty("arrival_stop")]
        public Stop ArrivalStop { get; set; }

        [JsonProperty("arrival_time")]
        public Time ArrivalTime { get; set; }

        [JsonProperty("departure_stop")]
        public Stop DepartureStop { get; set; }

        [JsonProperty("departure_time")]
        public Time DepartureTime { get; set; }

        [JsonProperty("headsign")]
        public string Headsign { get; set; }

        [JsonProperty("line")]
        public Line Line { get; set; }

        [JsonProperty("num_stops")]
        public long NumStops { get; set; }
    }

    public partial class Stop
    {
        [JsonProperty("location")]
        public Northeast Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Line
    {
        [JsonProperty("agencies")]
        public List<Agency> Agencies { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("text_color")]
        public string TextColor { get; set; }

        [JsonProperty("vehicle")]
        public Vehicle Vehicle { get; set; }
    }

    public partial class Agency
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class Vehicle
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
