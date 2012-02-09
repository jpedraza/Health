using System;
using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PrototypeHM.Parameter
{
    public class ParameterBaseData : QueryStatus, IIdentity
    {
        [DisplayName(@"Идентификатор"), Hide]
        public int ParameterId { get; set; }        

        [NotDisplay, NotEdit]
        public int Id { get { return ParameterId; } set { ParameterId = value; } }

        [DisplayName(@"Назв. параметра")]
        public string Name { get; set; }

        [DisplayName(@"Знач. по умолч.")]
        public string DefaultValue { get; set; }

    }

    public class ParameterDetail : ParameterBaseData {        
                
        [DisplayName(@"Мета-данные параметра"), DinamicCollectionModel(typeof(MetadataForParameter))]
        public IList<MetadataForParameter> Metadata { get; set; }

        public ParameterDetail()
        {
            //this.Metadata = new List<MetadataForParameter>();
        }

        public void AddRow()
        {
            this.Metadata.Add(new MetadataForParameter());
        }
    }

    public class MetadataForParameter {
        [DisplayName(@"Идентификатор"), Hide]
        public int ParameterId { get; set; }

        [DisplayName(@"Ключ-значение")]
        public string Key { get; set; }

        [DisplayName(@"Значение")]
        public object Value { get; set; }

        [DisplayName(@"Тип данных значения")]
        public ValueTypeOfMetadata ValueType { get; set; }
    }
    public class ValueTypeOfMetadata {
        [DisplayName(@"Идентификатор"), Hide]
        public int ValueTypeId { get; set; }

        [DisplayName(@"Название типа")]
        public string Name { get; set; }
    }
}