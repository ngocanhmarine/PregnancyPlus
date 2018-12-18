using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class CountryDao
	{
		PregnancyEntity connect = null;
		public CountryDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_country> GetListItem()
		{
			return connect.preg_country;
		}

		public preg_country GetItemByID(int id)
		{
			return connect.preg_country.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_country> GetItemsByParams(preg_country data)
		{
			IEnumerable<preg_country> result = connect.preg_country;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => c.name == propertyValue.ToString());
				}
			}
			return result;
		}

		public void InsertData(preg_country item)
		{
			connect.preg_country.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_country item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_country item)
		{

            connect.preg_country.Remove(item);
			connect.SaveChanges();
		}

	}
}