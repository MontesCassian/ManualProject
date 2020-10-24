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
        public string GetDbTitle()
		{
            string sqlExpression = "SELECT Id, MenuTitle, Title FROM Tutorial";

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                return (JsonConstructor.GetJson(reader));
			}
		}

        [HttpGet]
        public string GetDb(int id)
		{
            string sqlExpression = "SELECT * FROM Tutorial WHERE Id=" + id;

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                return (JsonConstructor.GetJson(reader));
			}
		}

        [HttpGet]
        public int DeleteDb(int id)
		{
            string sqlExpression = "DELETE FROM Tutorial WHERE Id=" + id;

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();
                return (rec);
			}
		}

        [HttpPost]
        public int AddTutorialObject(Tutorial newTut)
        {
            string sqlExpression;

            sqlExpression = "UPDATE Tutorial SET MenuTitle =" + (newTut.MenuTitle == null ? "NULL," : ("'" + newTut.MenuTitle + "',")) +
                "Title =" + (newTut.Title == null ? "NULL," : ("'" + newTut.Title + "',")) +
                "Text =" + (newTut.Text == null ? "NULL" : ("'" + newTut.Text + "'"))+
                " WHERE Id="+newTut.Id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                
                return(command.ExecuteNonQuery());
            }
        }
        [HttpGet]
        public string AddTutorialObject()
        {
            string sqlExpression = "INSERT INTO Tutorial (MenuTitle, Title, Text) VALUES (NULL, NULL, NULL)";

            string filterExpression = "DELETE FROM Tutorial WHERE MenuTitle IS NULL AND Title IS NULL AND Text IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(filterExpression, connection);
                int delRec = command.ExecuteNonQuery();

                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("SELECT * FROM Tutorial WHERE Id = IDENT_CURRENT('Tutorial')", connection);
                SqlDataReader reader = command.ExecuteReader();
                return (JsonConstructor.GetJson(reader));
            }
        }

        public string AddQuestion(Question newQuest)
		{
            string sqlExpression = "INSERT INTO Question (Text, Type, TutorialId) VALUES (" + (newQuest.Text == null ? "NULL," : ("'" + newQuest.Text + "',")) + (newQuest.Type == 0 ? "NULL," : ("'" + newQuest.Type + "',")) + (newQuest.Type == 0 ? "NULL)" : ("'" + newQuest.TutorialId + "')"));

            using(SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();

                command = new SqlCommand("SELECT * FROM Question WHERE Id = IDENT_CURRENT('Question')", connection);
                string json = JsonConstructor.GetJson(command.ExecuteReader());
                return (json);
            }
        }

        public string AddTest(Test newTest)
		{
            string sqlExpression = "INSERT INTO Test (Text, Checked, QuestionId) VALUES (" + (newTest.Text == null ? "NULL," : ("'" + newTest.Text + "',")) + (newTest.Checked == null ? "NULL," : ("'" + newTest.Checked + "',")) + (newTest.QuestionId == null ? "NULL)" : ("'" + newTest.QuestionId + "')"));
            
            using (SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();

                return ("Added");
			}
		}
    }
}