using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Web;

namespace Sharp_Tutorials.Models
{
	[Table(Name = "Tutorial")]
	public class Tutorial
	{

		[Column(IsPrimaryKey=true, IsDbGenerated = true)]
		public int Id { get; set; }
		[Column]
		public string MenuTitle { get; set; }
		[Column]
		public string Title { get; set; }
		[Column]
		public string Text { get; set; }
		[Column]
		public string Hometask { get; set; }
		[Column]
		public int Turn { get; set; }

	}
}