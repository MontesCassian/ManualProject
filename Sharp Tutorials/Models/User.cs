using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace Sharp_Tutorials.Models
{
	public class User
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }
		[Column]
		public string Login { get; set; }
		[Column]
		public string Password { get; set; }
		[Column]
		public int Group { get; set; }
	}
}