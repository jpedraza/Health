using System;
using System.Reflection;

namespace Model
{
    public enum RelationType
    {
        OneToMany,
        ManyToMany,
        ManyToOne
    }

    public class Relation
    {
        /// <summary>
        /// Для генерик-типов вернет тип первого генерик аргумента.
        /// </summary>
        public Type FromType { get; set; }

        public PropertyInfo FromProperty { get; set; }

        /// <summary>
        /// Для генерик-типов вернет тип первого генерик аргумента.
        /// </summary>
        public Type ToType { get; set; }

        public PropertyInfo ToProperty { get; set; }
        public RelationType RelationType { get; set; }
    }
}
