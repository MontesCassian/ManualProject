using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Sharp_Tutorials.Models;

namespace Sharp_Tutorials.Controllers
{
	public class IndexController : Controller
	{
		static public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		public ActionResult Content()
		{

			return View();
		}

	}
}