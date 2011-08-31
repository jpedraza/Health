using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace Health.Site.Models.Configuration.Providers
{
    public class BinaryMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
        #region Implementation of IMetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <returns>Результат.</returns>
        public bool IsHaveMetadata(Type model_type, string property_name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить метаданные для свойства модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства.</returns>
        public ModelMetadataPropertyConfiguration GetMetadata(Type model_type, string property_name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сериализует метаданные в строку.
        /// </summary>
        /// <param name="model_metadata_configuration">Метаданные.</param>
        /// <returns>Сериализованная строка метаданных.</returns>
        public static string Serialize(ModelMetadataConfiguration model_metadata_configuration)
        {
            var memory_stream = new MemoryStream();
            var binary_serializer = new BinaryFormatter();
            binary_serializer.Serialize(memory_stream, model_metadata_configuration);
            return Encoding.Default.GetString(memory_stream.ToArray());
        }

        /// <summary>
        /// Десериализует метаданные изстроки.
        /// </summary>
        /// <param name="data">Строка для десериализации.</param>
        /// <returns>Метаданные модели.</returns>
        public static ModelMetadataConfiguration Deserialize(string data)
        {
            byte[] byte_data = Encoding.Default.GetBytes(data);
            var memory_stream = new MemoryStream(byte_data);
            var binary_serializer = new BinaryFormatter();
            object obj = binary_serializer.Deserialize(memory_stream);
            if (obj is ModelMetadataConfiguration)
            {
                return obj as ModelMetadataConfiguration;
            }
            throw new Exception("Неверный формат данных для десериализации.");
        }

        #endregion
    }
}