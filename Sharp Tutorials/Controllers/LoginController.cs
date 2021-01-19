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
    public class LoginController : Controller
    {
        static public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int Login(User user)
		{
            string sqlExpression = "sp_SelectClientByLogin";
            SqlDataReader reader;

            using (SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				SqlParameter loginParam = new SqlParameter
				{
					ParameterName = "@login",
					Value = user.Login
				};

				command.Parameters.Add(loginParam);
				reader = command.ExecuteReader();

				if (reader.Read())
				{
					string password = reader.GetString(2);
					if (user.Password == password)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
				else
				{
					return -1;
				}
			}
			
		}
    }
}