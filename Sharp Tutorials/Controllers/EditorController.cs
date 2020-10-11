using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Configuration;
using Sharp_Tutorials.Models;

namespace Sharp_Tutorials.Controllers
{
    public class EditorController : Controller
    {
        static public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        public string GetBd()
		{
            string sqlExpression = "SELECT * FROM Tutorial";

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                return (JsonConstructor.GetJson(reader));
			}
		}

        [HttpPost]
        public int AddTutorialObject(Tutorial newTut)
        {
            if(newTut.MenuTitle==null || newTut.Title==null || newTut.Text == null)
			{
                return -1;
			}

            string sqlExpression = "INSERT INTO Tutorial (MenuTitle, Title, Text) VALUES ('" + newTut.MenuTitle + "','" + newTut.Title + "','" + newTut.Text + "')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();
                return rec;
            }

        }
    }
}