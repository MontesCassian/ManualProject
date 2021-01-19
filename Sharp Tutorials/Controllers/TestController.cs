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
            string sqlExpression = "sp_SelectQuestionByTutorialId";

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

        public string GetTest(int id)
		{
            string sqlExpression = "sp_SelectTestByQuestionId";

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