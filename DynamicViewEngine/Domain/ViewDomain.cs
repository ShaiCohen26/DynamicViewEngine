using System;
using System.Linq;
using DynamicViewEngine.Data;

namespace DynamicViewEngine.Domain
{
	public class ViewDomain
	{
		public static View Retrieve(string viewPath)
		{
			return new DynamicViewEngineEntities().Views.Where(v => String.Equals(v.ViewPath, viewPath, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
		}
	}
}