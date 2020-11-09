using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace Sharp_Tutorials.Models
{
	public class Video
	{
		[Column(IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id { get; set; }
		[Column]
		public string Text { get; set; }
		[Column]
		public string Title { get; set; }
		[Column]
		public string VideoId { get; set; }
		[Column]
		public int TutorialId { get; set; }
	}
}