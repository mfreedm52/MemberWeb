using System;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace Platform.Infrastructure
{
	public class ControllerConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.CanBeCastTo(typeof (Controller)) && !type.IsAbstract)
			{
				registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
			}
		}

        public void ScanTypes(TypeSet types, Registry registry)
        {
            throw new NotImplementedException();
        }
    }
}