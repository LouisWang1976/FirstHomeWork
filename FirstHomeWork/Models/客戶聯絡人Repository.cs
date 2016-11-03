using System;
using System.Linq;
using System.Collections.Generic;
	
namespace FirstHomeWork.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }
        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶聯絡人> GetAllDataOrderById(string p_Name, string p_Title, int page = 1)
        {
            var IQContact = this.All();
            if (!string.IsNullOrEmpty(p_Name))
            {
                IQContact = IQContact.Where(p => p.姓名.Contains(p_Name));
            }
            if (!string.IsNullOrEmpty(p_Title))
            {
                IQContact = IQContact.Where(p => p.職稱.Contains(p_Title));
            }
            IQContact = IQContact.OrderByDescending(p => p.Id);
            return IQContact;
        }
        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDeleted = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}