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
            string sqlExpression = "SELECT Id, MenuTitle, Title, Turn FROM Tutorial";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();
                return (rec);
            }
        }

        [HttpGet]
        public int DeleteVideo(int id)
        {
            string sqlExpression = "DELETE FROM Video WHERE Id=" + id;

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            sqlExpression = "UPDATE Tutorial SET MenuTitle =" + (newTut.MenuTitle == null ? "NULL," : ("N'" + newTut.MenuTitle + "',")) +
                "Title =" + (newTut.Title == null ? "NULL," : ("N'" + newTut.Title + "',")) +
                "Text =" + (newTut.Text == null ? "NULL," : ("N'" + newTut.Text + "',")) +
                "Turn =" + newTut.Turn + "," +
                "Hometask =" + (newTut.Hometask == null ? "NULL" : ("N'" + newTut.Hometask + "'")) +
                " WHERE Id=" + newTut.Id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                return (command.ExecuteNonQuery());
            }
        }
        [HttpGet]
        public string AddTutorialObject()
        {
            string sqlExpression = "INSERT INTO Tutorial (MenuTitle, Title, Text) VALUES (NULL, NULL, NULL)";

            string filterExpression = "DELETE FROM Tutorial WHERE MenuTitle IS NULL AND Title IS NULL AND Text IS NULL AND Hometask IS NULL";

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
        [HttpPost]
        public string AddQuestion(Question newQuest)
        {
            string sqlExpression = "INSERT INTO Question (Text, Type, TutorialId) VALUES (" + (newQuest.Text == null ? "NULL," : ("N'" + newQuest.Text + "',")) + (newQuest.Type == 0 ? "NULL," : ("N'" + newQuest.Type + "',")) + (newQuest.Type == 0 ? "NULL)" : ("N'" + newQuest.TutorialId + "')"));

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();

                command = new SqlCommand("SELECT * FROM Question WHERE Id = IDENT_CURRENT('Question')", connection);
                string json = JsonConstructor.GetJson(command.ExecuteReader());
                return (json);
            }
        }
        [HttpPost]
        public string AddTest(Test newTest)
		{
            string sqlExpression = "INSERT INTO Test (Text, Checked, QuestionId) VALUES (" + (newTest.Text == null ? "NULL," : ("N'" + newTest.Text + "',")) + (newTest.Checked == null ? "NULL," : ("N'" + newTest.Checked + "',")) + (newTest.QuestionId == null ? "NULL)" : ("N'" + newTest.QuestionId + "')"));
            
            using (SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int rec = command.ExecuteNonQuery();

                return ("Added");
			}
		}

        [HttpPost]
        public int AddVideo(Video newVideo)
		{
            string sqlExpression = "INSERT INTO Video (Title, Text, VideoId, TutorialId) VALUES (" + (newVideo.Title == null ? "NULL," : ("N'" + newVideo.Title + "',")) + (newVideo.Text == null ? "NULL," : ("N'" + newVideo.Text + "',")) + (newVideo.VideoId == null ? "NULL," : ("N'" + newVideo.VideoId + "',")) + (newVideo.TutorialId == null ? "NULL)" : ("N'" + newVideo.TutorialId + "')"));
        
            using (SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                return (command.ExecuteNonQuery());
			}
        }

        [HttpPost]
        public int UpdateVideo(Video newVideo)
        {
            string sqlExpression;

            sqlExpression = "UPDATE Video SET Text =" + (newVideo.Text == null ? "NULL," : ("N'" + newVideo.Text + "',")) +
                "Title =" + (newVideo.Title == null ? "NULL," : ("N'" + newVideo.Title + "',")) +
                "VideoId =" + (newVideo.VideoId == null ? "NULL" : ("N'" + newVideo.VideoId + "'")) +                
                " WHERE Id=" + newVideo.Id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                return (command.ExecuteNonQuery());
            }
        }

        [HttpGet]
        public int DeleteTest(int id)
        {
            string sqlExpression = "DELETE FROM Question WHERE Id=" + id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                return (command.ExecuteNonQuery());
            }
        }

    }
}