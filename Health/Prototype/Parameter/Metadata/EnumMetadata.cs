using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Prototype.Parameter.Metadata
{
    /// <summary>
    /// Типы вариантов ответов.
    /// </summary>
    public enum AnswerType
    {
        Number,     // числовой
        Text,       // текстовый
        Binary      // бинарный
    }

    /// <summary>
    /// Вариант ответа для параметра.
    /// </summary>
    [Serializable]
    public class Answer
    {
        /// <summary>
        /// Отображаемое значение.
        /// </summary>
        public object DisplayValue { get; set; }

        /// <summary>
        /// Реальное значение.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип данных реального значения параметра.
        /// </summary>
        public AnswerType AnswerType { get; set; }

        public override string ToString()
        {
            const string answer_format = "{0} ({1}, {2}, {3})";
            return string.Format(answer_format, DisplayValue, AnswerType, Value, Description);
        }
    }

    /// <summary>
    /// Вариант ответа зависящий от возраста.
    /// </summary>
    [Serializable]
    public class AgeDependsAnswer : Answer
    {
        /// <summary>
        /// Минимальный возраст.
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// Максимальный возраст.
        /// </summary>
        public int MaxAge { get; set; }

        public override string ToString()
        {
            const string answer_format = "{0}({1}, {2})";
            return string.Format(answer_format, base.ToString(), MaxAge, MinAge);
        }
    }

    /// <summary>
    /// Метаданные с несколькими фиксированными вариантами ответов.
    /// </summary>
    public class EnumMetadata<TAnswer> : IMetadata, ISerializable, IXmlSerializable
        where TAnswer : Answer
    {
        public EnumMetadata()
        {
            Answers = new List<TAnswer>();
        }

        /// <summary>
        /// Варианты ответа для параметра.
        /// </summary>
        public ICollection<TAnswer> Answers { get; set; }

        #region Implementation of ISerializable

        /// <summary>
        /// Заполняет объект <see cref="T:System.Runtime.Serialization.SerializationInfo"/> данными, необходимыми для сериализации целевого объекта.
        /// </summary>
        /// <param name="info">Объект <see cref="T:System.Runtime.Serialization.SerializationInfo"/> для заполнения данными. </param>
        /// <param name="context">Целевое местоположение сериализации (см. <see cref="T:System.Runtime.Serialization.StreamingContext"/>). </param><exception cref="T:System.Security.SecurityException">Вызывающий объект не имеет необходимого разрешения. </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var xmlSerializer = new XmlSerializer(typeof(ICollection<TAnswer>));
            var memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, Answers);
            info.AddValue("Answers", Encoding.UTF8.GetString(memoryStream.ToArray()));
        }

        #endregion

        #region Implementation of IXmlSerializable

        /// <summary>
        /// Этот метод является зарезервированным, и его не следует использовать.При реализации интерфейса IXmlSerializable этот метод должен возвращать значение null (Nothing в Visual Basic), а если необходимо указать пользовательскую схему, то вместо использования метода следует применить <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> к классу.
        /// </summary>
        /// <returns>
        /// Схема <see cref="T:System.Xml.Schema.XmlSchema"/>, в которой описывается XML-представление объекта, который создается методом <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> и используется методом <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/>.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Создает объект из его XML-представления.
        /// </summary>
        /// <param name="reader">Поток <see cref="T:System.Xml.XmlReader"/>, из которого выполняется десериализация объекта. </param>
        public void ReadXml(XmlReader reader)
        {
            string strAnswers = reader.GetAttribute("Answers");
            if (strAnswers == null)
            {
                Answers = new List<TAnswer>();
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(List<TAnswer>));
            byte[] bytes = Encoding.UTF8.GetBytes(strAnswers);
            var memoryStream = new MemoryStream(bytes);
            var answers = xmlSerializer.Deserialize(memoryStream) as ICollection<TAnswer>;
            if (answers == null)
                throw new Exception(
                    string.Format("Неверный тип вариатов ответа, ожидался {0}", typeof(List<TAnswer>)));

            Answers = answers;
        }

        /// <summary>
        /// Преобразует объект в XML-представление.
        /// </summary>
        /// <param name="writer">Поток <see cref="T:System.Xml.XmlWriter"/>, в который выполняется сериализация объекта. </param>
        public void WriteXml(XmlWriter writer)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<TAnswer>));
            var memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, Answers);
            string data = Encoding.UTF8.GetString(memoryStream.ToArray());
            writer.WriteAttributeString("Answers", data);
        }

        #endregion

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (TAnswer answer in Answers)
            {
                stringBuilder.AppendLine(answer.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
