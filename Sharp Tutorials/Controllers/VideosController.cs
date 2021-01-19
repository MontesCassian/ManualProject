﻿using System;
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
			string sqlExpression = "sp_SelectVideosTitle";

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
			string sqlExpression = "sp_SelectVideoById";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				SqlParameter idParam = new SqlParameter
				{
					ParameterName = "@id",
					Value = id
				};

				command.Parameters.Add(idParam);
				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}

		[HttpGet]
		public string GetTitleByTutorialId(int id)
		{
			string sqlExpression = "sp_SelectVideoTitleByTutorialId";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);

				command.CommandType = System.Data.CommandType.StoredProcedure;

				SqlParameter idParam = new SqlParameter
				{
					ParameterName = "@id",
					Value = id
				};

				command.Parameters.Add(idParam);

				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}


	}
}