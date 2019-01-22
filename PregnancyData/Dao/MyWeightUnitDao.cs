using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyWeightUnitDao
	{
		PregnancyEntity connect = null;
		public MyWeightUnitDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_weight_unit> GetListItem()
		{
			return connect.preg_my_weight_unit;
		}

		public preg_my_weight_unit GetItemByID(int id)
		{
			return connect.preg_my_weight_unit.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_my_weight_unit> GetItemsByParams(preg_my_weight_unit data)
		{
			IEnumerable<preg_my_weight_unit> result = connect.preg_my_weight_unit;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "unit" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.unit) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_my_weight_unit item)
		{
			connect.preg_my_weight_unit.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_weight_unit item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_my_weight_unit item = GetItemByID(id);
			connect.preg_my_weight_unit.Remove(item);
			connect.SaveChanges();
		}

	}
}