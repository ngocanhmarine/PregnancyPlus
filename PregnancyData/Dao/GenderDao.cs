using PregnancyData.Entity;
using System;
using System.Collections.Generic;
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
			return connect.preg_genders;
		}

		public preg_gender GetItemByID(int id)
		{
			return connect.preg_genders.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_gender> GetItemsByParams(preg_gender data)
		{
			IEnumerable<preg_gender> result = connect.preg_genders;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "gender" && propertyValue != null)
				{
					result = result.Where(c => c.gender == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_gender item)
		{
			connect.preg_genders.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_gender item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_gender item)
		{

            connect.preg_genders.Remove(item);
			connect.SaveChanges();
		}

	}
}