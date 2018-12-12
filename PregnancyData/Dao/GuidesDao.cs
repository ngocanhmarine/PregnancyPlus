using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class GuidesDao
	{
		PregnancyEntity connect = null;
		public GuidesDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_guides> GetListItem()
		{
			return connect.preg_guidess;
		}

		public preg_guides GetItemByID(int id)
		{
			return connect.preg_guidess.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_guides> GetItemsByParams(preg_guides data)
		{
			IEnumerable<preg_guides> result = connect.preg_guidess;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "page_id" && propertyValue != null)
				{
					result = result.Where(c => c.page_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "guides_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.guides_type_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_guides item)
		{
			connect.preg_guidess.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_guides item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_guides item)
		{
			
			connect.preg_guidess.Remove(item);
			connect.SaveChanges();
		}
		public string resultReturn(preg_guides data)
		{
			string result = "{";

			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "preg_guides_type")
				{

				}
				else if (propertyName == "preg_page")
				{

				}
				else
				{
					result += @"""" + propertyName + @""":""" + propertyValue.ToString() + @""",";
				}
			}
			result += "}";
			return result;
		}
	}
}