using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Health.Core.API;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Configuration.Providers
{
    /// <summary>
    /// Базовый класс для получения метаданных из сериализованных данных.
    /// </summary>
    public abstract class SerializerMetadataConfigurationProvider : MetadataConfigurationProvider
    {
        /// <summary>
        /// DI ядро.
        /// </summary>
        private readonly IDIKernel _diKernel;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="di_kernel">DI ядро.</param>
        protected SerializerMetadataConfigurationProvider(IDIKernel di_kernel)
        {
            _diKernel = di_kernel;
        }        

        #region Serialization

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