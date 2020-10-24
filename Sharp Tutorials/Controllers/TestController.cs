using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Sharp_Tutorials.Models;

namespace Sharp_Tutorials.Controllers
{
    public class TestController : Controller
    {
        static public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
       
        [HttpGet]
        public string GetQuestion(int id)
		{
            string sqlExpression = "SELECT * FROM Question WHERE TutorialId = "+id;

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                return (JsonConstructor.GetJson(reader));

			}
		}

        public string GetTest(int id)
		{
            string sqlExpression = "SELECT * FROM Test WHERE QuestionId=" + id;

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                return (JsonConstructor.GetJson(reader));
			}
		}
    }
}