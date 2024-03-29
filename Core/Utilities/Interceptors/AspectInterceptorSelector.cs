﻿using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new PerformanceAspect(50));//Herhangi bir metod 5 saniyeden fazla çalışırsa uyarı ver. (performans yönetimi)
            /*classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); */ //otomatik olarak sistemdeki herşeyi logla

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
