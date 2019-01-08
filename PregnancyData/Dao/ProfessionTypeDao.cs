using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class ProfessionTypeDao
    {
         PregnancyEntity connect = null;
        public ProfessionTypeDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_profession_type> GetListItem()
        {
            return connect.preg_profession_type;
        }

        public preg_profession_type GetItemByID(int id)
        {
            return connect.preg_profession_type.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_profession_type> GetItemsByParams(preg_profession_type data)
		{
			IEnumerable<preg_profession_type> result = connect.preg_profession_type;
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
		public void InsertData(preg_profession_type item)
        {
            connect.preg_profession_type.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_profession_type item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(int id)
        {
            preg_profession_type item = GetItemByID(id);
            connect.preg_profession_type.Remove(item);
            connect.SaveChanges();
        }

    }
}