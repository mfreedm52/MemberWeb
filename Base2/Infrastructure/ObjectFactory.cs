using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using System.Threading;

namespace Base2.Infrastructure
{
    public static class ObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
                new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(x =>
            {
            });


            
        }

        
        

    }
}