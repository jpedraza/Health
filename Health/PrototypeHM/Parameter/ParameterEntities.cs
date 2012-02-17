using System;
using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;

namespace PrototypeHM.Parameter
{
    public class ParameterBaseData : QueryStatus, IIdentity, IHealthParameterContext
    {
        [DisplayName(@"Идентификатор"), Hide]
        public int ParameterId { get; set; }        

        [NotDisplay, NotEdit]
        public int Id { get { return ParameterId; } set { ParameterId = value; } }

        [DisplayName(@"Название параметра"), SimpleOrCompoundModel(IsSimple=true)]
        public string Name { get; set; }

        [DisplayName(@"Значение по умолчанию"), SimpleOrCompoundModel(IsSimple = true)]
        public byte[] DefaultValue { get; set; }

        public bool IsValid()
        {
            return checkEnitiesValid.checkValid(this.GetType(), (object)this);
        }

        //public ParameterBaseData():base()
        //{
        //    Name = string.Empty;
        //    DefaultValue = string.Empty;
        //}
    }

    public class ParameterDetail : ParameterBaseData {        
                
        [DisplayName(@"Мета-данные параметра"), SimpleOrCompoundModel(IsSimple=false), MultiSelectEditMode(typeof(OperationsContext<MetadataForParameter>), "ParameterId", TypeMappingEnum.ManyToOne)]
        public IList<MetadataForParameter> Metadata { get; set; }

        //public ParameterDetail() : base()
        //{
        //    Metadata = new MetadataForParameter[0].ToList();
            
        //}
    }

    public class MetadataForParameter:IHealthParameterContext, IIdentity {
        [DisplayName(@"Id параметра")]
        public int ParameterId { get; set; }

        public string ParameterName { get; set; }

        [DisplayName(@"Ключ-значение"), SimpleOrCompoundModel(IsSimple = true)]
        public string Key { get; set; }

        [DisplayName(@"Значение"), SimpleOrCompoundModel(IsSimple = true)]
        public byte[] Value { get; set; }

        //[DisplayName(@"Тип данных значения"), SingleSelectEditMode(typeof(OperationsContext<ValueTypeOfMetadata>), "Name", TypeMappingEnum.OneToMany)]
        //public ValueTypeOfMetadata ValueType { get; set; }

        public string ValueTypeId { get; set; }

        public string ValueTypeName { get; set; }

        public bool IsValid()
        {
            return checkEnitiesValid.checkValid(this.GetType(), (object)this);
        }

        public MetadataForParameter()
        {
            foreach (var pI in this.GetType().GetProperties())
            {
                if (pI.PropertyType == typeof(string))
                {
                    pI.SetValue(this, string.Empty, null);
                }

                if (pI.PropertyType == typeof(DateTime))
                {
                    pI.SetValue(this, DateTime.Today, null);
                }
            }
        }

        [NotDisplay, NotEdit]
        public int Id { get { return ParameterId; } set { ParameterId = value; } }
    }
    public class ValueTypeOfMetadata:QueryStatus, IIdentity {
        [DisplayName(@"Идентификатор"), Hide]
        public int ValueTypeId { get; set; }

        [DisplayName(@"Название типа"), SimpleOrCompoundModel(IsSimple = true)]
        public string Name { get; set; }


        [NotDisplay, NotEdit]
        public int Id { get { return ValueTypeId; } set { ValueTypeId = value; } }

        public ValueTypeOfMetadata()
            : base()
        {
            Name = string.Empty;
            
        }
    }
}