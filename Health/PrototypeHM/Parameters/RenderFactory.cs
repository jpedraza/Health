using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities;
using Prototype.DI;

namespace Prototype.Parameters
{
    public class RenderFactory : IDIInjected
    {
        private readonly IDictionary<Type, Type> _binds;

        public RenderFactory(IDIKernel diKernel)
        {
            DIKernel = diKernel;
            _binds = new Dictionary<Type, Type>();
        }

        public IDIKernel DIKernel { get; set; }

        internal void Bind<TP, TR>()
            where TP : Parameter
            where TR : IRenderer
        {
            _binds.Add(typeof(TP), typeof(TR));
        }

        internal IRenderer Renderer(Type parameterType)
        {
            Type rendererType = _binds.FirstOrDefault(b => b.Key == parameterType).Value;
            if (rendererType == null)
                throw new Exception(string.Format("Для типа {0} не зарегистрирован отрисовщик", parameterType.Name));

            var renderer = DIKernel.Get(rendererType) as IRenderer;

            if (renderer == null)
                throw new Exception(string.Format("Невозможно создать отрисовщик {0} для параметра {1}",
                                                  rendererType.Name, parameterType.Name));
            return renderer;
        }
    }
}
