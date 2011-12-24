using System;
using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace PrototypeHM.Parameter
{
    public class ParameterFullData : QueryStatus
    {
        [DisplayName(@"Идентификатор"), Hide]
        public int ParameterId { get; set; }

        [DisplayName(@"Название")]
        public string Name { get; set; }
    }

    internal class ParameterDetail : Parameter.ParameterFullData {
        [DisplayName(@"Мета-данные параметра")]
        public IList<MetadataForParameter> Metadata { get; set; }
    }

    internal class MetadataForParameter {
        [DisplayName(@"Идентификатор"), Hide]
        public int ParameterId { get; set; }

        [DisplayName(@"Ключ-значение")]
        public string Key { get; set; }

        [DisplayName(@"Значение")]
        public object Value { get; set; }

        [DisplayName(@"Тип данных значения")]
        public ValueTypeOfMetadata ValueType { get; set; }
    }
    internal class ValueTypeOfMetadata {
        [DisplayName(@"Идентификатор"), Hide]
        public int ValueTypeId { get; set; }

        [DisplayName(@"Название")]
        public string Name { get; set; }
    }
}