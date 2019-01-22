using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class GenderDao
	{
		PregnancyEntity connect = null;
		public GenderDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_gender> GetListItem()
		{
			return connect.preg_gender;
		}

		public preg_gender GetItemByID(int id)
		{
			return connect.preg_gender.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_gender> GetItemsByParams(preg_gender data)
		{
			IEnumerable<preg_gender> result = connect.preg_gender;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "gender" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.gender) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_gender item)
		{
			connect.preg_gender.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_gender item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_gender item)
		{

			connect.preg_gender.Remove(item);
			connect.SaveChanges();
		}

	}
}