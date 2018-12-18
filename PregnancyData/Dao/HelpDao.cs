using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class HelpDao
    {
         PregnancyEntity connect = null;
         public HelpDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_help> GetListItem()
        {
            return connect.preg_help;
        }

        public preg_help GetItemByID(int id)
        {
            return connect.preg_help.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_help> GetItemsByParams(preg_help data)
		{
			IEnumerable<preg_help> result = connect.preg_help;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "help_category_id" && propertyValue != null)
				{
					result = result.Where(c => c.help_category_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "image" && propertyValue != null)
				{
					result = result.Where(c => c.image == propertyValue.ToString());
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_help item)
        {
            connect.preg_help.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_help item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(preg_help item)
        {
            connect.preg_help.Remove(item);
            connect.SaveChanges();
        }

    }
}