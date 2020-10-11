using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;

namespace Sharp_Tutorials.Controllers
{
	public static class JsonConstructor
	{
		public static string GetJson(SqlDataReader sqlData)
		{
			StringBuilder jsonStr = new StringBuilder();
			jsonStr.Append("[\n");
			while (sqlData.Read())
			{
				jsonStr.Append("{\n");
				for (int i = 0; i < sqlData.FieldCount; i++)
				{
					jsonStr.Append("\"" + sqlData.GetName(i) + "\":");
					jsonStr.Append("\"" + sqlData.GetValue(i) + "\"");
					jsonStr.Append(",");

					jsonStr.Append("\n");
				}
				jsonStr.Remove(jsonStr.Length - 2, 1);
				jsonStr.Append("},");
			}
			jsonStr.Remove(jsonStr.Length - 1, 1);
			jsonStr.Append("\n]");
			System.Diagnostics.Debug.WriteLine(jsonStr);

			return jsonStr.ToString();
		}
	}
}