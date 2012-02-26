using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Parameters"), DisplayName("Параметр")]
    public class Parameter : IIdentity
    {
        public Parameter()
        {
            Patients = new List<Patient>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [Required, DisplayName("Имя")]
        public string Name { get; set; }

        [NotDisplay, DisplayName("Значение по-умолчанию")]
        public byte[] DefaultValue { get; set; }

        [NotDisplay, DisplayName("Пациенты")]
        public virtual ICollection<Patient> Patients { get; set; }

        [NotDisplay, DisplayName("Заполненные параметры")]
        public virtual ICollection<ParameterStorage> ParametersStorages { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [ScaffoldTable(true), DisplayName("Булевый параметр")]
    public class BoolParameter : Parameter
    {
        public readonly Type ValueType = typeof(bool);
    }

    [ScaffoldTable(true), DisplayName("Целый параметр")]
    public class IntegerParameter : Parameter
    {
        [DisplayName("Минимальное значение"), Required]
        public int MinValue { get; set; }
        [DisplayName("Максимальное значение"), Required]
        public int MaxValue { get; set; }

        public readonly Type ValueType = typeof(int);
    }

    [ScaffoldTable(true), DisplayName("Дробный параметр")]
    public class DoubleParameter : Parameter
    {
        [DisplayName("Минимальное значение"), Required]
        public double MinValue { get; set; }
        [DisplayName("Максимальное значение"), Required]
        public double MaxValue { get; set; }

        public readonly Type ValueType = typeof(double);
    }

    [ScaffoldTable(true), DisplayName("Строковый параметр")]
    public class StringParameter : Parameter
    {
        [DisplayName("Максимальная длина"), Required]
        public int MaxLength { get; set; }
        [DisplayName("Минимальная длина"), Required]
        public int MinLength { get; set; }
        [DisplayName("Regex паттерн")]
        public string Pattern { get; set; }

        public readonly Type ValueType = typeof(string);
    }
}