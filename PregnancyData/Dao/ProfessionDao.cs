﻿using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class ProfessionDao
    {
         PregnancyEntity connect = null;
        public ProfessionDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_profession> GetListItem()
        {
            return connect.preg_professions;
        }

        public preg_profession GetItemByID(int id)
        {
            return connect.preg_professions.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_profession> GetItemsByParams(preg_profession data)
		{
			IEnumerable<preg_profession> result = connect.preg_professions;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "profession_type" && propertyValue != null)
				{
					result = result.Where(c => c.profession_type == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_profession item)
        {
            connect.preg_professions.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_profession item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(int id)
        {
            preg_profession item = GetItemByID(id);
            connect.preg_professions.Remove(item);
            connect.SaveChanges();
        }

    }
}