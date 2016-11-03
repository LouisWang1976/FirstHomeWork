using System;
using System.Linq;
using System.Collections.Generic;
	
namespace FirstHomeWork.Models
{   
	public  class 客戶分類Repository : EFRepository<客戶分類>, I客戶分類Repository
	{
        public override IQueryable<客戶分類> All()
        {
            return base.All();
        }
        public 客戶分類 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶分類Repository : IRepository<客戶分類>
	{

	}
}