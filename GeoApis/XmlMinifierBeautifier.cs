
namespace GeoApis
{


    public class XmlMinifierBeautifier
    {


        public static void Prettify(string source, string destination)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.XmlResolver = null;
            doc.Load(source);

            System.Xml.XmlWriterSettings settings = 
                new System.Xml.XmlWriterSettings
                {
                    Encoding = System.Text.Encoding.UTF8,
                    Indent = true,
                    IndentChars = "    "
                };

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(destination, settings))
            {
                doc.Save(writer);
            }

        }

        public static void Minify(string source, string destination)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.XmlResolver = null;
            doc.PreserveWhitespace = false;
            doc.Load(source);

            System.Xml.XmlWriterSettings settings =
                new System.Xml.XmlWriterSettings
                {
                    Encoding = System.Text.Encoding.UTF8,
                    Indent = false
                };

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(destination, settings))
            {
                doc.Save(writer);
            }
        }



    }


}
