using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PregnancyData.Entity;
using RestSharp;
using PregnancyData.Dao;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

//[Serializable]
public static class SysMethod
{
	public static string MD5Hash(string input)
	{
		StringBuilder hash = new StringBuilder();
		MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
		byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

		for (int i = 0; i < bytes.Length; i++)
		{
			hash.Append(bytes[i].ToString("x2"));
		}
		return hash.ToString();
	}

	public static bool DeepEquals(this object obj, object another)
	{

		if (ReferenceEquals(obj, another))
		{
			return true;
		};
		if ((obj == null) || (another == null)) return false;
		if (obj.GetType() != another.GetType()) return false;

		var result = true;
		foreach (var property in obj.GetType().GetProperties())
		{
			var objValue = property.GetValue(obj);
			var anotherValue = property.GetValue(another);
			if (objValue != null && anotherValue != null)
			{
				Type chkType = objValue.GetType();
				if (!chkType.IsClass && chkType != typeof(int))
				{
					if (objValue.GetHashCode() != anotherValue.GetHashCode())
					{ result = false; }
				}
				else if (chkType == typeof(int))
				{
					if (objValue.ToString() != anotherValue.ToString())
					{ result = false; }
				}
			}
			else if (objValue != null || anotherValue != null)
			{
				result = false;
			}
		}
		return result;
	}

	public static bool createAccountNop(preg_user user)
	{
		try
		{
			UserDao dao = new UserDao();
			//user = dao.GetUserByID(user.id).FirstOrDefault();
			var client = new RestClient("http://1.55.17.233:6868/");
			var request = new RestRequest("api/customers", Method.POST);
			request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NTA2NjE1MDAsImV4cCI6MTg2NjAyMTUwMCwiaXNzIjoiaHR0cDovLzEuNTUuMTcuMjMzOjY4NjgiLCJhdWQiOlsiaHR0cDovLzEuNTUuMTcuMjMzOjY4NjgvcmVzb3VyY2VzIiwibm9wX2FwaSJdLCJjbGllbnRfaWQiOiIyOGZkNmQ2ZS1kMDcwLTQ3OTQtYTM5NC05YTE0ZmQ5ZTQ1YzYiLCJzdWIiOiIyOGZkNmQ2ZS1kMDcwLTQ3OTQtYTM5NC05YTE0ZmQ5ZTQ1YzYiLCJhdXRoX3RpbWUiOjE1NTA2NjE0OTksImlkcCI6ImxvY2FsIiwic2NvcGUiOlsibm9wX2FwaSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXX0.QJN2fosQbGYJE7ZwhHS1d9Y2_cgISMQs9B-gdG38i3NLVC-xEtDOoltw-_1tcpq5b2zSAYKynnMGdw-R5fWdPuj47iArUK6zoNGyrxj9HuAp9YmGTzXnRgI8sGbdLUzB4OS1__FKuN5lT8vfQtiCi1wr94ypj_RK9ECRSYokkwBzQfgAivkl-bIm2WKrSLhA3V7dcLdB9q6CDtUnIMqagRD6CdNDyG2pqUWQg8srCg5HwfCXY1x8-OwWwjFDjEapUPRCPFiWArcr6m23kBtY11SqhK5izRsONDYe2f5lOtr_yroJgmt5vc-e__e9N2vkj2GORAxfvWJntRI7viZjGQ");

			string defaultPassword = "qwertyuiop";
			if (user.uid != null)
			{
				if (user.social_type_id == (int)SysConst.SocialTypes.facebook)
				{
					request.AddParameter("undefined", "{\"customer\":{\"password\":\"" + defaultPassword + "\",\"email\":\"facebook_" + user.uid + "\",\"role_ids\":[3]}}", ParameterType.RequestBody);
				}
				else if (user.social_type_id == (int)SysConst.SocialTypes.google)
				{
					request.AddParameter("undefined", "{\"customer\":{\"password\":\"" + defaultPassword + "\",\"email\":\"google_" + user.uid + "\",\"role_ids\":[3]}}", ParameterType.RequestBody);
				}
			}
			else
			{
				request.AddParameter("undefined", "{\"customer\":{\"password\":\"" + defaultPassword + "\",\"email\":\"" + user.phone + "\",\"role_ids\":[3]}}", ParameterType.RequestBody);
			}

			var response = client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				var content = JObject.Parse(response.Content.ToString());
				int newId = Convert.ToInt32(JObject.Parse(response.Content)["customers"][0]["id"]);

				int id;
				if (content["customers"][0] != null)
				{
					id = Convert.ToInt32(content["customers"][0]["id"]);
					user.nopcustomer_id = id;
					dao.UpdateData(user);
				}
			}
			return true;
		}
		catch (Exception)
		{
			return false;
		}

	}
}