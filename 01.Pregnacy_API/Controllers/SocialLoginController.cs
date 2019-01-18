using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class SocialLoginController : ApiController
	{
		public HttpResponseMessage Get()
		{
			//var client = new RestClient("https://graph.facebook.com/me?fields=id,name,email&access_token=EAALCdRYwc90BAEBKUF4SCUIzcSOvDzK61rSvjNlZBgGwzStA8GDBowkXydTr7LMk27BZBefgGeq1ysXZA9bve9BXRVu0ghh02I4mrm5ZABWfqM0FUshJ3frJcumzep9SepZCxhM8jCWZB3ZBOCFtVatwhDyCquONOsBgBTuZACejIvMsODRc4AjkvWa2OCMb1V2qf5dzjg0pQQwPbtOojj1m7GtXGc6koSB36WQbXiB7xwZDZD");
			//var request = new RestRequest(Method.GET);
			//request.AddHeader("Postman-Token", "1f4d0998-c1c5-4c59-85d4-b0a5cebc258e");
			//request.AddHeader("cache-control", "no-cache");
			//IRestResponse response = client.Execute(request);
			//var x = JsonConvert.DeserializeObject<FBInfo>(response.Content);

			var client = new RestClient("https://graph.facebook.com/");
			//var request = new RestRequest("me?fields=id,name,email&access_token=EAALCdRYwc90BAEBKUF4SCUIzcSOvDzK61rSvjNlZBgGwzStA8GDBowkXydTr7LMk27BZBefgGeq1ysXZA9bve9BXRVu0ghh02I4mrm5ZABWfqM0FUshJ3frJcumzep9SepZCxhM8jCWZB3ZBOCFtVatwhDyCquONOsBgBTuZACejIvMsODRc4AjkvWa2OCMb1V2qf5dzjg0pQQwPbtOojj1m7GtXGc6koSB36WQbXiB7xwZDZD");
			var request = new RestRequest("me",Method.GET);
			request.AddQueryParameter("fields", "id,name,email");
			request.AddQueryParameter("access_token", "EAALCdRYwc90BAEBKUF4SCUIzcSOvDzK61rSvjNlZBgGwzStA8GDBowkXydTr7LMk27BZBefgGeq1ysXZA9bve9BXRVu0ghh02I4mrm5ZABWfqM0FUshJ3frJcumzep9SepZCxhM8jCWZB3ZBOCFtVatwhDyCquONOsBgBTuZACejIvMsODRc4AjkvWa2OCMb1V2qf5dzjg0pQQwPbtOojj1m7GtXGc6koSB36WQbXiB7xwZDZD");
			var response = client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				var info = new FBInfo() { };
				info.id = JObject.Parse(response.Content)["id"].ToString();
				info.name = JObject.Parse(response.Content)["name"].ToString();
				info.email = JObject.Parse(response.Content)["email"].ToString();
				return Request.CreateResponse(HttpStatusCode.OK, info);
			}
			return Request.CreateResponse(HttpStatusCode.OK, response);
		}
	}

	public class FBInfo
	{
		public string id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
	}
}
