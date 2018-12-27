using DynamicViewEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace DynamicViewEngine.Domain
{
	public class KawaVirtualPathProvider : VirtualPathProvider
	{ 
 		public override bool FileExists(string virtualPath)
		{
			var view = GetViewFromDatabase(virtualPath);

			if (view == null)
			{
				return base.FileExists(virtualPath);
			}
			else
			{
				return true;
			}
		}

		public override VirtualFile GetFile(string virtualPath)
		{
			var view = GetViewFromDatabase(virtualPath);

			if (view == null)
			{
				return base.GetFile(virtualPath);
			}
			else
			{
				byte[] content = ASCIIEncoding.ASCII.GetBytes(view.ViewContent);
				return new KawaVirtualFile(virtualPath, content);
			}
		}

		//public override CacheDependency GetCacheDependency(string virtualPath, Enumerable virtualPathDependencies, DateTime utcStart)
		//{

		//	var view = GetViewFromDatabase(virtualPath);

		//	if (view != null)
		//	{
		//		return null;
		//	}

		//	return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
		//}

		private View GetViewFromDatabase(string virtualPath)
		{
			virtualPath = virtualPath.Replace("~", "");

			DbContextKawa db = new DbContextKawa();
			var view = from v in db.Views
					   where v.ViewPath == virtualPath
					   select v;
			return view.SingleOrDefault();
		}
	}
}