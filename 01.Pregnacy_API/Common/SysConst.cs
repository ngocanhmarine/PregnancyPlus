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
		admin = 0, dev, user, social
	}
	public enum UserStatus
	{
		noactive = 0, active
	}
	public enum SocialTypes
	{
		facebook = 1, google
	}
	public static string[] imgOnlyExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp" };
	public static string[] imgHtmlExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp", ".html" };
	#endregion

	#region Error Messages
	public static string ADMIN_DONT_HAVE_PROFILE = "You are admin. You dont have a profile.";
	public static string DATA_NOT_FOUND = "Data not found.";
	public static string DATA_NOT_EMPTY = "Data must not be empty.";
	public static string DATA_EXIST = "Data already exist.";
	public static string ID_MUST_INTEGER = "ID must be an integer.";
	public static string ITEM_ID_NOT_EXIST = "Item with ID={0} not exist.";
	public static string EMAIL_EXIST = "Email already exist.";
	public static string EMAIL_PASSWORD_NOT_NULL = "Email and password cannot be null.";
	public static string DATA_INSERT_SUCCESS = "Data insert succeed.";
	public static string DATA_INSERT_FAIL = "Data insert failed.";
	public static string DATA_UPDATE_SUCCESS = "Data update succeed.";
	public static string DATA_UPDATE_FAIL = "Data update failed.";
	public static string DATA_DELETE_SUCCESS = "Data delete succeed.";
	public static string DATA_DELETE_FAIL = "Data delete failed.";
	public static string LOGIN_FAILED = "Provided username & password is incorrect.";
	public static string LOGIN_SOCIAL_FAILED = "Provided access token is invalid.";
	public static string USER_CREATED = "Your user account has been created. ID = {0}.";
	public static string INVALID_FILE_TYPE = "Invalid file type.";
	public static string FILE_NOT_EXIST = "File with path {0} not exist.";
	public static string FILE_EXIST = "File with name {0} already exist.";
	#endregion
}
