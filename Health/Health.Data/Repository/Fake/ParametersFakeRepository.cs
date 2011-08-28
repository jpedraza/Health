using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Data.Repository.Fake
{
    public sealed class ParametersFakeRepository : CoreFakeRepository<Parameter>, IParameterRepository
    {
        public ParametersFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            #region FakeData
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
                    period = 0,
                    Is_childs = true,
                    Age = 0,
                    Id_parent = 0,
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
                    Id_cat = 0,
                    Is_var = true,
                    Obligation = true,
                    period = 0,
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
                    Id_cat = 0,
                    Is_var = true,
                    Obligation = true,
                    period = 0,
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
                    Id_cat = 0,
                    Is_var = true,
                    Obligation = true,
                    period = 0,
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
                    Id_cat = 0,
                    Is_var = true,
                    Obligation = true,
                    period = 0,
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

            public Parameter[] GetAllParam()
            {
                Parameter[] requieredParameters;
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                           select parameter).ToList();
                if (found_parameter.Count() != 0)
                {
                    requieredParameters = new Parameter[found_parameter.Count()];
                    for (int i = 0; i < found_parameter.Count(); i++)
                    {
                        requieredParameters[i] = found_parameter.ElementAt(i);
                    }
                }
                else
                {
                    requieredParameters = null;
                }
                return requieredParameters;
            }

            public Parameter GetById(int Id)
            {
                Parameter requieredParameter = default(Parameter);
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

            public Parameter GetByValue(string value)
            {
                Parameter requieredParameter = default(Parameter);
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                          where parameter.Name == value
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
                    this.Delete(Param.Id);
                    res = Save(Param);
                }
                catch (System.Exception)
                {
                    res = false;
                }
                return res;
            }
            
            //Этот метод удаления параметра через его Id необходим для нормальной реализации метода редактирования. По возможности от него избавится.
            public bool Delete(int Id)
            {
                Parameter requieredParameter = default(Parameter);
                var res = false;
                IEnumerable<Parameter> found_parameter = (from parameter in _entities
                                                          where parameter.Id == Id
                                                          select parameter).ToList();
                if (found_parameter.Count() == 1)
                {
                    requieredParameter = found_parameter.First();
                    res = Delete(requieredParameter);                  
                }
                else
                {
                    res = false;
                }
                return res;
            }

            public bool Delete(Parameter Param)
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