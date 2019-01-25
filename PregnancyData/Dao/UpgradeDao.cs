using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UpgradeDao
	{
		PregnancyEntity connect = null;
		public UpgradeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_upgrade> GetListItem()
		{
			return connect.preg_upgrade;
		}

		public IQueryable<preg_upgrade> GetItemByID(int id)
		{
			return connect.preg_upgrade.Where(c => c.id == id);
		}

		public IQueryable<preg_upgrade> GetItemsByParams(preg_upgrade data)
		{
			IQueryable<preg_upgrade> result = connect.preg_upgrade;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "version" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.version) > 0);
				}
			}
			return result;
		}

		public void InsertData(preg_upgrade item)
		{
			connect.preg_upgrade.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_upgrade item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_upgrade item)
		{
			connect.preg_upgrade.Remove(item);
			connect.SaveChanges();
		}
	}
}