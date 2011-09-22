using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using Health.Core.Entities.POCO.Abstract;
using System;

namespace Health.Data.Repository.Fake
{
    public sealed class ParametersFakeRepository : CoreFakeRepository<Parameter>, IParameterRepository
    {
        public ParametersFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            #region FakeData
            Save(new Parameter
            {
                Name = "Температура",
                Value = 0,
                Id = 6,
                DefaultValue = 36.6,
                MetaData = new MetaData
                {
                    Id_cat = 6,
                    Is_var = false,
                    Age = 12,
                    Id_parent = null,
                    Obligation = true,
                    Is_childs = false,
                    period = 12,
                    Variants = null
                }
            });
            Save(
                new Parameter
                {
                    Name = "Головна боль",
                    Value = "Нет",
                    Id = 7,
                    DefaultValue = "Нет",
                    MetaData = new MetaData{
                        Id_cat = 6,
                        Is_var = true,
                        Age = 13,
                        Id_parent = null,
                        Is_childs = false,
                        Obligation = true,
                        period = 32,
                        Variants = new Variant[]{
                        new Variant{
                        Value = "Не болит",
                        Ball = 0},
                        new Variant{
                        Value = "Болит",
                        Ball = 1}}
                    }
                });
            Save(new Parameter
            {
                Name = "Характеристика здоровья ребенка при рождении",
                Value = 14,
                DefaultValue = 0,
                Id = 1,
                MetaData = new MetaData
                {
                    Id_cat = 1,
                    Is_var = false,
                    Obligation = true,
                    period = null,
                    Is_childs = true,
                    Age = 0,
                    Id_parent = null,
                    Variants = null
                }
            });

            Save(new Parameter {
                Name = "Доношенность",
                Value = 11,
                DefaultValue = 0,
                Id = 2,
                MetaData = new MetaData
                {
                    Id_cat = null,
                    Is_var = true,
                    Obligation = true,
                    period = null,
                    Is_childs = false,
                    Age = 0,
                    Id_parent = 1,
                    Variants = new Variant[]{
                        new Variant{
                            Value = "доношен",
                            Ball = 1
                        },
                        new Variant{
                            Value = "донош. - незрелый",
                            Ball = 2
                        },
                        new Variant{
                            Value = "переношенный",
                            Ball = 3
                        },
                        new Variant{
                            Value = "недоношенный",
                            Ball = 4
                        },
                        new Variant{
                            Value = "Плод",
                            Ball = 5
                        },
                    }
                }
            });

            Save(new Parameter
            {
                Name = "Масса тела",
                Value = 11,
                DefaultValue = 0,
                Id = 3,
                MetaData = new MetaData
                {
                    Id_cat = null,
                    Is_var = true,
                    Obligation = true,
                    period = null,
                    Is_childs = false,
                    Age = 0,
                    Id_parent = 1,
                    Variants = new Variant[]{
                        new Variant{
                            Value = "3000 г и более",
                            Ball = 1
                        },
                        new Variant{
                            Value = "2500 - 2999 г",
                            Ball = 2
                        },
                        new Variant{
                            Value = "2000 - 24999 г",
                            Ball = 3
                        },
                        new Variant{
                            Value = "1000 - 1999 г",
                            Ball = 4
                        },
                        new Variant{
                            Value = "менее 1000 г",
                            Ball = 5
                        },
                    }
                }
            });

            Save(new Parameter
            {
                Name = "Начало дыхания",
                Value = 11,
                DefaultValue = 0,
                Id = 4,
                MetaData = new MetaData
                {
                    Id_cat = null,
                    Is_var = true,
                    Obligation = true,
                    period = null,
                    Is_childs = false,
                    Age = 0,
                    Id_parent = 1,
                    Variants = new Variant[]{
                        new Variant{
                            Value = "установилось сразу",
                            Ball = 1
                        },
                        new Variant{
                            Value = "после отсасывания слизи",
                            Ball = 2
                        },
                        new Variant{
                            Value = "после применения мешка Амбу",
                            Ball = 3
                        },
                        new Variant{
                            Value = "интубация трахеи, кратковременная ИВЛ в родзале",
                            Ball = 4
                        },
                        new Variant{
                            Value = "интубация, постоянная ИВЛ, переведен в реанимацию",
                            Ball = 5
                        },
                    }
                }
            });

            Save(new Parameter
            {
                Name = "Баллы по Ангар",
                Value = 11,
                DefaultValue = 0,
                Id = 5,
                MetaData = new MetaData
                {
                    Id_cat = null,
                    Is_var = true,
                    Obligation = true,
                    period = null,
                    Is_childs = false,
                    Age = 0,
                    Id_parent = 1,
                    Variants = new Variant[]{
                        new Variant{
                            Value = "8-10",
                            Ball = 1
                        },
                        new Variant{
                            Value = "6-7",
                            Ball = 2
                        },
                        new Variant{
                            Value = "4-5",
                            Ball = 3
                        },
                        new Variant{
                            Value = "3",
                            Ball = 4
                        },
                        new Variant{
                            Value = "0-2",
                            Ball = 5
                        },
                    }
                }
            });
            #endregion
        }

        #region Members

        public IList<Parameter> GetAllParam()
            {
                IList<Parameter> requieredParameters;
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                           select parameter).ToList();
                if (found_parameter.Count() != 0)
                {
                    requieredParameters = found_parameter.ToList<Parameter>();
                }
                else
                {
                    requieredParameters = null;
                }
                return requieredParameters;
            }

            public Parameter GetById(int Id)
            {
                Parameter requieredParameter = new Parameter();
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                          where parameter.Id == Id
                                                          select parameter).ToList();
                if (found_parameter.Count() == 1)
                {
                    requieredParameter = found_parameter.First();
                }
                else
                {
                    requieredParameter = null;                     
                }
                return requieredParameter;
            }

            public Parameter GetByValue(string Name)
            {
                Parameter requieredParameter = new Parameter();
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                          where parameter.Name == Name
                                                          select parameter).ToList();
                if (found_parameter.Count() == 1)
                {
                    requieredParameter = found_parameter.First();
                }
                else
                {
                    requieredParameter = null;
                }
                return requieredParameter;
            }

            public bool Add(Parameter NewParam)
            {
                var res = false;
                try 
                {
                    res = Save(NewParam);
                }
                catch (System.Exception)
                {
                    res = false;
                }
                return res;
            }
            
            //Не совсем логично реализован метод редактирования.
            //Что-делать с update?
            public bool Edit(Parameter Param)
            {
                var res = false;
                try
                {
                    this.DeleteParam(Param.Id);
                    res = Save(Param);
                }
                catch (System.Exception)
                {
                    res = false;
                }
                return res;
            }
            
        /// <summary>
        /// Служебный метод
        /// 
        /// </summary>
        /// 
        /// <param name="Id"></param>
        /// <returns></returns>
        //TODO: По возможности избавиться от метода удаления параметра через его ID
        public bool DeleteParam(int Id)
            {
                Parameter requieredParameter = new Parameter();
                var res = false;
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                          where parameter.Id == Id
                                                          select parameter).ToList();
                if (found_parameter.Count() == 1)
                {
                    requieredParameter = found_parameter.First();
                    res = Delete(requieredParameter);                  
                }
                return res;
            }

            public bool DeleteParam(Parameter Param)
            {
                var res = false;
                try
                {
                    res = Delete(Param);
                }
                catch (System.Exception)
                {
                    res = false;
                }
                return res;
            }
            #endregion
    }
}