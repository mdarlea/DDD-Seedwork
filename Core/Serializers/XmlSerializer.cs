using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Swaksoft.Core.Serializers
{
    public class XmlSerializer : ISerializer
    {
        private readonly Encoding _encoding;

        public XmlSerializer() : this(null)
        {
            OmitXmlDeclaration = false;
            XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        }

        public XmlSerializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        public string Serialize<TRequest>(TRequest request)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = string.IsNullOrEmpty(DefaultNamespace) 
                    ? new System.Xml.Serialization.XmlSerializer(typeof(TRequest))
                    : new System.Xml.Serialization.XmlSerializer(typeof(TRequest),DefaultNamespace);

                XmlWriter xmlWriter = null;
                if (OmitXmlDeclaration)
                {
                    var xmlWriterSettings = new XmlWriterSettings
                    {
                        OmitXmlDeclaration = true
                    };
                    xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
                } 
                else if (_encoding != null)
                {
                    var xmlWriterSettings = new XmlWriterSettings
                    {
                        Encoding = _encoding
                    };
                    xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
                }
               
                //stream.Position = 0;
                if (xmlWriter != null)
                {
                    var xmlNameSpace = new XmlSerializerNamespaces();
                    var key = (string.IsNullOrWhiteSpace(XsiNamespace)) ? string.Empty : "xsi";
                    xmlNameSpace.Add(key, XsiNamespace);

                    if (!string.IsNullOrEmpty(SchemaLocation))
                    {
                        xmlNameSpace.Add("schemaLocation", SchemaLocation);
                    }
                    serializer.Serialize(xmlWriter, request, xmlNameSpace);
                }
                else
                {
                    serializer.Serialize(stream, request);
                }

                stream.Seek(0, SeekOrigin.Begin);
                var streamReader = new StreamReader(stream);
                return streamReader.ReadToEnd();
            }
        }

        public TResponse Desearialize<TResponse>(Stream stream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TResponse));
            using (var reader = new StreamReader(stream))
            {
                return ((TResponse)(serializer.Deserialize(System.Xml.XmlReader.Create(reader))));           
            }
        }

        public TResponse Desearialize<TResponse>(string source)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TResponse));
            using (var reader = new StringReader(source))
            {
                return ((TResponse)(serializer.Deserialize(System.Xml.XmlReader.Create(reader))));
            }
        }

        public string ContentType
        {
            get { return "application/xml"; }
        }

        public string DefaultNamespace { get; set; }
        public string SchemaLocation { get; set; }

        public string XsiNamespace { get; set; }
        public bool OmitXmlDeclaration { get; set; }
    }
}
