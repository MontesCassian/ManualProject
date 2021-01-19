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
        public string GetTutorialsTitle()
        {
            string sqlExpression = "sp_SelectTutorialsTitle";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                return (JsonConstructor.GetJson(reader));
            }
        }

        [HttpGet]
        public string GetTutorialById(int id)
        {
            string sqlExpression = "sp_SelectTutorialById";

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
        public int DeleteTutorial(int id)
        {
            string sqlExpression = "sp_DeleteFromTutorial";

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

                int rec = command.ExecuteNonQuery();
                return (rec);
            }
        }

        [HttpGet]
        public int DeleteVideo(int id)
        {
            string sqlExpression = "sp_DeleteFromVideo";

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

                int rec = command.ExecuteNonQuery();
                return (rec);
            }
        }

        [HttpPost]
        public int UpdateTutorial(Tutorial newTut)
        {
            string sqlExpression = "sp_UpdateTutorial";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (newTut.Id != null)
                {
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = newTut.Id
                    };
                    command.Parameters.Add(idParam);
                }

                if (newTut.MenuTitle != null)
                {
                    SqlParameter menuTitleParam = new SqlParameter
                    {
                        ParameterName = "@MenuTitle",
                        Value = newTut.MenuTitle
                    };
                    command.Parameters.Add(menuTitleParam);
                }

                if (newTut.Title != null)
                {
                    SqlParameter titleParam = new SqlParameter
                    {
                        ParameterName = "@Title",
                        Value = newTut.Title
                    };
                    command.Parameters.Add(titleParam);
                }

                if (newTut.Text != null)
                {
                    SqlParameter textParam = new SqlParameter
                    {
                        ParameterName = "@Text",
                        Value = newTut.Text
                    };
                    command.Parameters.Add(textParam);
                }

                if (newTut.Hometask != null)
                {
                    SqlParameter hometaskParam = new SqlParameter
                    {
                        ParameterName = "@Hometask",
                        Value = newTut.Hometask
                    };
                    command.Parameters.Add(hometaskParam);
                }


                SqlParameter turnParam = new SqlParameter
                {
                    ParameterName = "@Turn",
                    Value = newTut.Turn
                };
                command.Parameters.Add(turnParam);

                return (command.ExecuteNonQuery());
            }
        }
        [HttpGet]
        public string AddEmptyTutorial()
        {
            string sqlExpression = "sp_CreateEmptyTutorial";

            string filterExpression = "sp_DeleteEmptyFromTutorial";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(filterExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                int delRec = command.ExecuteNonQuery();

                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                string result = JsonConstructor.GetJson(reader);
                reader.Close();

                transaction.Commit();

                return (result);
            }
        }
        [HttpPost]
        public string AddQuestion(Question newQuest)
        {
            string sqlExpression = "sp_CreateQuestion";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (newQuest.Text != null)
                {
                    SqlParameter textParam = new SqlParameter
                    {
                        ParameterName = "@Text",
                        Value = newQuest.Text
                    };
                    command.Parameters.Add(textParam);
                }

                SqlParameter typeParam = new SqlParameter
                {
                    ParameterName = "@Type",
                    Value = newQuest.Type
                };
                command.Parameters.Add(typeParam);

                SqlParameter tutorialIdParam = new SqlParameter
                {
                    ParameterName = "@TutorialId",
                    Value = newQuest.TutorialId
                };
                command.Parameters.Add(tutorialIdParam);

                string json = JsonConstructor.GetJson(command.ExecuteReader());
                return (json);
            }
        }
        [HttpPost]
        public string AddTest(Test newTest)
		{
            string sqlExpression = "sp_CreateTest";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (newTest.Text != null)
                {
                    SqlParameter textParam = new SqlParameter
                    {
                        ParameterName = "@Text",
                        Value = newTest.Text
                    };
                    command.Parameters.Add(textParam);
                }

                SqlParameter checkedParam = new SqlParameter
                {
                    ParameterName = "@Checked",
                    Value = newTest.Checked
                };
                command.Parameters.Add(checkedParam);

                SqlParameter questionIdParam = new SqlParameter
                {
                    ParameterName = "@QuestionId",
                    Value = newTest.QuestionId
                };
                command.Parameters.Add(questionIdParam);

                int rec = command.ExecuteNonQuery();

                return ("Added");
			}
		}

        [HttpPost]
        public int AddVideo(Video newVideo)
		{
            string sqlExpression = "sp_CreateVideo";
            using (SqlConnection connection = new SqlConnection(connectionString))
			{
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (newVideo.Title != null)
                {
                    SqlParameter titleParam = new SqlParameter
                    {
                        ParameterName = "@Title",
                        Value = newVideo.Title
                    };
                    command.Parameters.Add(titleParam);
                }

                if (newVideo.Text != null)
                {
                    SqlParameter textParam = new SqlParameter
                    {
                        ParameterName = "@Text",
                        Value = newVideo.Text
                    };
                    command.Parameters.Add(textParam);
                }

                if (newVideo.VideoId != null)
                {
                    SqlParameter videoIdParam = new SqlParameter
                    {
                        ParameterName = "@VideoId",
                        Value = newVideo.VideoId
                    };
                    command.Parameters.Add(videoIdParam);
                }

                SqlParameter tutorialIdParam = new SqlParameter
                {
                    ParameterName = "@TutorialId",
                    Value = newVideo.TutorialId
                };
                command.Parameters.Add(tutorialIdParam);

                return (command.ExecuteNonQuery());
			}
        }

        [HttpPost]
        public int UpdateVideo(Video newVideo)
        {
            string sqlExpression;

            sqlExpression = "sp_UpdateVideo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = newVideo.Id
                };
                command.Parameters.Add(idParam);

                if (newVideo.Title != null)
                {
                    SqlParameter titleParam = new SqlParameter
                    {
                        ParameterName = "@Title",
                        Value = newVideo.Title
                    };
                    command.Parameters.Add(titleParam);
                }

                if (newVideo.Text != null)
                {
                    SqlParameter textParam = new SqlParameter
                    {
                        ParameterName = "@Text",
                        Value = newVideo.Text
                    };
                    command.Parameters.Add(textParam);
                }

                if (newVideo.VideoId != null)
                {
                    SqlParameter videoIdParam = new SqlParameter
                    {
                        ParameterName = "@VideoId",
                        Value = newVideo.VideoId
                    };
                    command.Parameters.Add(videoIdParam);
                }

                return (command.ExecuteNonQuery());
            }
        }

        [HttpGet]
        public int DeleteTest(int id)
        {
            string sqlExpression = "sp_DeleteFromTest";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                command.Parameters.Add(idParam);

                return (command.ExecuteNonQuery());
            }
        }

    }
}