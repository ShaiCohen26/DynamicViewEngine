using DynamicViewEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicViewEngine.Controllers
{
	public class HomeController : Controller
	{
		public enum ViewSource
		{
			None = 0,
			DefaultViewOnFileSystem = 1,
			CustomViewOnFileSystem = 2,
			DynamicViewFromDb = 3
		}

		public ActionResult Index()
		{
			//PopulateData();
			DbContextKawa db = new DbContextKawa();
			List<Motorcycle> motorcycles = db.Motorcycles.ToList();
			ViewData["MotorcycleListView"] = "_MotorcycleListPartial.cshtml";
			return View(motorcycles);
		}

		[HttpPost]
		public ActionResult Index(ViewSource ddlViewLocation)
		{
			string viewPartial = "_MotorcycleListPartial.cshtml";
			switch (ddlViewLocation)
			{
				case ViewSource.DefaultViewOnFileSystem:
					viewPartial = "_MotorcycleListPartial.cshtml";
					break;
				case ViewSource.CustomViewOnFileSystem:
					viewPartial = "_MotorcycleBulletListPartial.cshtml";
					break;
				case ViewSource.DynamicViewFromDb:
					break;
				default:
					break;
			}
			 //PopulateData();
			DbContextKawa db = new DbContextKawa();
			List<Motorcycle> motorcycles = db.Motorcycles.ToList();
			ViewData["MotorcycleListView"] = viewPartial;
			return View(motorcycles);
		}
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public void PopulateData()
		{
			DbContextKawa db = new DbContextKawa();
			Motorcycle motorcycle = new Motorcycle()
			{
				Model = "Ninja ZX-6R"
				, Name = "Super Sport"
			};
			db.Motorcycles.Add(motorcycle);
			motorcycle = new Motorcycle()
			{
				Model = "Ninja 1000 ABS"
				, Name = "Sport"
			};
			db.Motorcycles.Add(motorcycle);
			motorcycle = new Motorcycle()
			{
				Model = "Ninja H2 R"
				, Name = "Hyper Sport"
			};
			db.Motorcycles.Add(motorcycle);



			db.SaveChanges();
		}
	}
}