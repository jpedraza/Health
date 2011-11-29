using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Prototype.Parameter.Metadata;

namespace Prototype.Parameter
{
    /// <summary>
    /// Управление параметрами.
    /// </summary>
    internal class ParameterFactory
    {
        internal string Serialize(IMetadata metadata)
        {
            Type[] types = metadata.GetType().GetGenericArguments();
            var xmlSerializer = new XmlSerializer(metadata.GetType(), types);
            var memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, metadata);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        internal object Deserialize(string data, Type dataType)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            var xmlSerializer = new XmlSerializer(dataType);
            var memoryStream = new MemoryStream(bytes);
            object obj = Convert.ChangeType(xmlSerializer.Deserialize(memoryStream), dataType);
            if (obj == null)
                throw new Exception("Десериализация не удалась.");
            return obj;
        }
    }
}
