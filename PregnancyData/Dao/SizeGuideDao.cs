using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class SizeGuideDao
    {
         PregnancyEntity connect = null;
         public SizeGuideDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_size_guide> GetListItem()
        {
            return connect.preg_size_guides;
        }

        public preg_size_guide GetItemByID(int id)
        {
            return connect.preg_size_guides.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_size_guide> GetItemsByParams(preg_size_guide data)
		{
			IEnumerable<preg_size_guide> result = connect.preg_size_guides;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "image" && propertyValue != null)
				{
					result = result.Where(c => c.image == propertyValue.ToString());
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_size_guide item)
        {
            connect.preg_size_guides.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_size_guide item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(int id)
        {
            preg_size_guide item = GetItemByID(id);
            connect.preg_size_guides.Remove(item);
            connect.SaveChanges();
        }

    }
}