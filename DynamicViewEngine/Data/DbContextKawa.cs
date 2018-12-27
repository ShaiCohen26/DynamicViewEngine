using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DynamicViewEngine.Data
{
	public class DbContextKawa : DbContext
	{
		public DbSet<View> Views { get; set; }
		public DbSet<Motorcycle> Motorcycles { get; set; }
	}
}