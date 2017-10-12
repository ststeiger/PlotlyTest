
using Newtonsoft.Json;


namespace Overpass
{


    public class Converter
    {


        public static readonly JsonSerializerSettings ReadSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };


        public static readonly JsonSerializerSettings WriteSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = ReadSettings.MetadataPropertyHandling,
            DateParseHandling = ReadSettings.DateParseHandling,
            Formatting =  Formatting.Indented
        };


    }

}
