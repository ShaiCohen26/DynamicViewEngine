using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace DynamicViewEngine.Domain
{
	public class KawaVirtualFile : VirtualFile
	{
		private byte[] viewContent;

		public KawaVirtualFile(string virtualPath, byte[] viewContent) 
			: base(virtualPath)
		{
			this.viewContent = viewContent;
		}

		public override Stream Open()
		{
			return new MemoryStream(viewContent);
		}
	}
}