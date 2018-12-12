using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class SysConst
{
	#region Values
	public static TimeSpan AccessTokenExpiredTimeSpan = TimeSpan.FromHours(4);
	public enum UserType
	{
		admin = 0, dev, user
	}
	public enum UserStatus
	{
		noactive=0,active
	}
	#endregion

	#region Error Messages
	public static string DATA_NOT_FOUND = "Data not found.";
	public static string DATA_EMPTY = "Data empty.";
	public static string ID_MUST_INTEGER = "ID must be an integer.";
	public static string PHONE_EXIST = "Phone already exist.";
	public static string PHONE_PASSWORD_NOT_NULL = "Phone and password cannot be null.";
	public static string DATA_INSERT_SUCCESS = "Data insert succeed.";
	public static string DATA_INSERT_FAIL = "Data insert failed.";
	public static string DATA_UPDATE_SUCCESS = "Data update succeed.";
	public static string DATA_UPDATE_FAIL = "Data update failed.";
	public static string DATA_DELETE_SUCCESS = "Data delete succeed.";
	public static string DATA_DELETE_FAIL = "Data delete failed.";
	public static string LOGIN_FAILED = "Provided username & password is incorrect.";
	#endregion
}
