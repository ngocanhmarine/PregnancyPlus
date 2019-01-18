using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}