using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01.Pregnacy_API.Social.Models
{
	public class GoogleUserInfo
	{
		public string sub { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string picture { get; set; }
		public string given_name { get; set; }
		public string family_name { get; set; }
	}
}