using DynamicViewEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicViewEngine.Domain
{
	public class MotorcycleDomain
	{
		public static List<Motorcycle> Retrieve()
		{
			return new DynamicViewEngineEntities().Motorcycles.ToList();
		}
	}
}