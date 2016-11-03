using System;
using System.Linq;
using System.Collections.Generic;
	
namespace FirstHomeWork.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶資料> GetAllDataOrderById(string p_Name,int? p_Class, int page = 1)
        {
            var IQCustpmers = this.All();
            if (!string.IsNullOrEmpty(p_Name))
            {
                IQCustpmers = IQCustpmers.Where(p => p.客戶名稱.Contains(p_Name));
            }
            if (p_Class.HasValue)
            {
                IQCustpmers = IQCustpmers.Where(p => p.Classification == p_Class);
            }
            IQCustpmers = IQCustpmers.OrderByDescending(p => p.Id);
            return IQCustpmers;
        }
        public override void Delete(客戶資料 entity)
        {
            entity.IsDeleted = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}