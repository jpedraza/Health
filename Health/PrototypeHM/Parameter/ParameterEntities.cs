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

        [DisplayName(@"Назв. параметра"), SimpleOrCompoundModel(IsSimple=true)]
        public string Name { get; set; }

        [DisplayName(@"Знач. по умолч."), SimpleOrCompoundModel(IsSimple = true)]
        public string DefaultValue { get; set; }

        public bool IsValid()
        {
            return checkEnitiesValid.checkValid(this.GetType(), (object)this);
        }
    }

    public class ParameterDetail : ParameterBaseData {        
                
        [DisplayName(@"Мета-данные параметра"), SimpleOrCompoundModel(IsSimple=false), MultiSelectEditMode(typeof(OperationsContext<MetadataForParameter>), "ParameterId", TypeMappingEnum.ManyToOne)]
        public IList<MetadataForParameter> Metadata { get; set; }
                
    }

    public class MetadataForParameter:IHealthParameterContext, IIdentity {
        [DisplayName(@"Идентификатор"), Hide]
        public int ParameterId { get; set; }

        [DisplayName(@"Ключ-значение"), SimpleOrCompoundModel(IsSimple = true)]
        public string Key { get; set; }

        [DisplayName(@"Значение"), SimpleOrCompoundModel(IsSimple = true)]
        public object Value { get; set; }

        [DisplayName(@"Тип данных значения"), SimpleOrCompoundModel(IsSimple = false), SingleSelectEditMode(typeof(OperationsContext<ValueTypeOfMetadata>), "ValueTypeId", TypeMappingEnum.OneToMany)]
        public ValueTypeOfMetadata ValueType { get; set; }

        public bool IsValid()
        {
            return checkEnitiesValid.checkValid(this.GetType(), (object)this);
        }

        [NotDisplay, NotEdit]
        public int Id { get { return ParameterId; } set { ParameterId = value; } }
    }
    public class ValueTypeOfMetadata:IHealthParameterContext, IIdentity {
        [DisplayName(@"Идентификатор"), Hide]
        public int ValueTypeId { get; set; }

        [DisplayName(@"Название типа"), SimpleOrCompoundModel(IsSimple = true)]
        public string Name { get; set; }

        public bool IsValid()
        {
            return checkEnitiesValid.checkValid(this.GetType(), (object)this);
        }

        [NotDisplay, NotEdit]
        public int Id { get { return ValueTypeId; } set { ValueTypeId = value; } }
    }
}