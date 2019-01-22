using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
			return connect.preg_guides_type;
		}

		public preg_guides_type GetItemByID(int id)
		{
			return connect.preg_guides_type.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_guides_type> GetItemsByParams(preg_guides_type data)
		{
			IEnumerable<preg_guides_type> result = connect.preg_guides_type;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_guides_type item)
		{
			connect.preg_guides_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_guides_type item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_guides_type item)
		{
			connect.preg_guides_type.Remove(item);
			connect.SaveChanges();
		}

	}
}