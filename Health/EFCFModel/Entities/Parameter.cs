using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Parameters"), DisplayName("Параметр")]
    public abstract class Parameter : IIdentity
    {
        protected Parameter()
        {
            Patients = new List<Patient>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [Required, DisplayName("Имя")]
        public string Name { get; set; }

        [NotDisplay, DisplayName("Значение по-умолчанию"), ByteType("ValueType")]
        public byte[] DefaultValue { get; set; }

        [NotDisplay, DisplayName("Пациенты")]
        public virtual ICollection<Patient> Patients { get; set; }

        [NotDisplay, DisplayName("Заполненные параметры")]
        public virtual ICollection<ParameterStorage> ParametersStorages { get; set; }

        [NotMapped, NotDisplay]
        public abstract Type ValueType { get; }

        public override string ToString()
        {
            return Name;
        }
    }

    [ScaffoldTable(true), DisplayName("Булевый параметр")]
    public class BoolParameter : Parameter
    {
        [NotDisplay]
        public override Type ValueType { get { return typeof (bool); } }
    }

    [ScaffoldTable(true), DisplayName("Целый параметр")]
    public class IntegerParameter : Parameter
    {
        [DisplayName("Минимальное значение"), Required]
        public int MinValue { get; set; }
        [DisplayName("Максимальное значение"), Required]
        public int MaxValue { get; set; }

        [NotDisplay]
        public override Type ValueType { get { return typeof (int); } }
    }

    [ScaffoldTable(true), DisplayName("Дробный параметр")]
    public class DoubleParameter : Parameter
    {
        [DisplayName("Минимальное значение"), Required]
        public double MinValue { get; set; }
        [DisplayName("Максимальное значение"), Required]
        public double MaxValue { get; set; }

        [NotDisplay]
        public override Type ValueType { get { return typeof (double); } }
    }

    [ScaffoldTable(true), DisplayName("Строковый параметр")]
    public class StringParameter : Parameter
    {
        [DisplayName("Максимальная длина"), Required]
        public int MaxLength { get; set; }
        [DisplayName("Минимальная длина"), Required]
        public int MinLength { get; set; }

        [NotDisplay]
        public override Type ValueType { get { return typeof (string); } }
    }

    [ScaffoldTable(true), DisplayName("Параметр-дата")]
    public class DateTimeParameter : Parameter
    {
        [DisplayName("Минимальная дата"), Required]
        public DateTime MinDate { get; set; }
        [DisplayName("Максимальная дата"), Required]
        public DateTime MaxDate { get; set; }

        [NotDisplay]
        public override Type ValueType { get { return typeof(DateTime); } }
    }

    public class ListParameter : Parameter
    {
        private readonly ObservableCollection<string> _collection;

        public ListParameter()
        {
            _collection = new ObservableCollection<string>();
            _collection.CollectionChanged += CllectionChanged;
        }

        private void CllectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        #region Overrides of Parameter

        public override Type ValueType { get { return typeof (ObservableCollection<string>); } }

        #endregion
    }
}