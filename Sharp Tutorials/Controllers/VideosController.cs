using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace Sharp_Tutorials.Controllers
{
    public class VideosController : Controller
    {
        static public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		[HttpGet]
		public string GetMenuList()
		{
			string sqlExpression = "SELECT Id, Title FROM Video";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}

		[HttpGet]
		public string GetContent(int id)
		{
			string sqlExpression = "SELECT Id, Title, Text, VideoId FROM Video WHERE Id =" + id;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}

		[HttpGet]
		public string GetTitleByTutorialId(int id)
		{
			string sqlExpression = "SELECT Id, Title, VideoId FROM Video WHERE TutorialId =" + id;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}


	}
}