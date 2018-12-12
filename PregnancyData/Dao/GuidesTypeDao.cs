using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class GuidesTypeDao
	{
		PregnancyEntity connect = null;
		public GuidesTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_guides_type> GetListItem()
		{
			return connect.preg_guides_types;
		}

		public preg_guides_type GetItemByID(int id)
		{
			return connect.preg_guides_types.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_guides_type> GetItemsByParams(preg_guides_type data)
		{
			IEnumerable<preg_guides_type> result = connect.preg_guides_types;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => c.type == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_guides_type item)
		{
			connect.preg_guides_types.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_guides_type item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_guides_type item)
		{
			
			connect.preg_guides_types.Remove(item);
			connect.SaveChanges();
		}

	}
}