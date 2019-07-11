
namespace OSM.Geocoder
{


    public class BoundingBoxes
    {

        private Nominatim m_data;


        public BoundingBoxes(Nominatim nom)
        {
            this.m_data = nom;
        } // End Constructor 


        protected void InitBoundingBox()
        {
            if (this.m_data.Boundingbox == null)
            {
                this.m_data.Boundingbox = new System.Collections.Generic.List<decimal>();
            } // End if (this.m_data.Boundingbox == null) 

            for (int i = this.m_data.Boundingbox.Count; i < 4; ++i)
            {
                this.m_data.Boundingbox.Add(0);
            } // Next i 

        } // End Sub InitBoundingBox 



        public decimal? MinimumLatitude
        {
            get
            {
                if (this.m_data.Boundingbox != null && this.m_data.Boundingbox.Count > 0)
                    return this.m_data.Boundingbox[0];

                return null;
            }
            set
            {
                InitBoundingBox();
                this.m_data.Boundingbox[0] = value.Value;
            }
        } // End Property MinimumLatitude 


        public decimal? MaximumLatitude
        {
            get
            {
                if (this.m_data.Boundingbox != null && this.m_data.Boundingbox.Count > 1)
                    return this.m_data.Boundingbox[1];

                return null;
            }
            set
            {
                InitBoundingBox();
                this.m_data.Boundingbox[1] = value.Value;
            }
        } // End Property MaximumLatitude  


        public decimal? MinimumLongitude
        {
            get
            {
                if (this.m_data.Boundingbox != null && this.m_data.Boundingbox.Count > 2)
                    return this.m_data.Boundingbox[2];

                return null;
            }
            set
            {
                InitBoundingBox();
                this.m_data.Boundingbox[2] = value.Value;
            }
        } // End Property MinimumLongitude 


        public decimal? MaximumLongitude
        {
            get
            {
                if (this.m_data.Boundingbox != null && this.m_data.Boundingbox.Count > 3)
                    return this.m_data.Boundingbox[3];

                return null;
            }
            set
            {
                InitBoundingBox();
                this.m_data.Boundingbox[3] = value.Value;
            }
        } // End Property MaximumLongitude

    } // End Class BoundingBoxes 


    // https://wiki.openstreetmap.org/wiki/Nominatim
    public partial class Nominatim
    {
        [Newtonsoft.Json.JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [Newtonsoft.Json.JsonProperty("licence")]
        public string Licence { get; set; }

        [Newtonsoft.Json.JsonProperty("osm_type")]
        public string OsmType { get; set; }

        [Newtonsoft.Json.JsonProperty("osm_id")]
        public string OsmId { get; set; }

        [Newtonsoft.Json.JsonProperty("boundingbox")]
        public System.Collections.Generic.List<decimal> Boundingbox { get; set; }


        [Newtonsoft.Json.JsonIgnore()]
        public BoundingBoxes Bounds 
        {
            get
            {
                // this.Boundingbox[0] // minLat
                // this.Boundingbox[1] // maxLat
                // this.Boundingbox[2] // minLng
                // this.Boundingbox[3] // maxLng
                return new BoundingBoxes(this);
            }
        } // End Property Bounds 


        [Newtonsoft.Json.JsonProperty("lat")]
        public decimal Lat { get; set; }

        [Newtonsoft.Json.JsonProperty("lon")]
        public decimal Lon { get; set; }

        [Newtonsoft.Json.JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [Newtonsoft.Json.JsonProperty("class")]
        public string Class { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("importance")]
        public double Importance { get; set; }

        [Newtonsoft.Json.JsonProperty("icon")]
        public string Icon { get; set; }


        public static System.Collections.Generic.List<Nominatim> FromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<
                System.Collections.Generic.List<Nominatim>
                >(json, Converter.Settings);
        }


        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Converter.Settings);
        }


        internal class Converter
        {
            public static readonly Newtonsoft.Json.JsonSerializerSettings Settings =
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.Ignore,
                    DateParseHandling = Newtonsoft.Json.DateParseHandling.None,
                    Converters = {
                        new Newtonsoft.Json.Converters.IsoDateTimeConverter
                        {
                            DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal
                        }
                    }
                };
        } // End Class Converter 


    } // End Class 


} // End Namespace 

