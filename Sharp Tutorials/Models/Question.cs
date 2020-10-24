using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace Sharp_Tutorials.Models
{
	public class Question
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }
		[Column]
		public string Text { get; set; }
		[Column]
		public int Type { get; set; }//1-- radio, 2--check
		[Column]
		public int TutorialId { get; set; }

	}
}