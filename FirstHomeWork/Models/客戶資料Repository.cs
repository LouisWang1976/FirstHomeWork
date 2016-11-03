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
            客戶聯絡人Repository ContactRepo = RepositoryHelper.Get客戶聯絡人Repository();
            var l_ListContact = ContactRepo.All().Where(p => p.客戶Id == entity.Id).ToList();
            if (l_ListContact != null)
            {
                foreach (客戶聯絡人 t_Contact in l_ListContact)
                {
                    t_Contact.IsDeleted = true;
                }
            }
            客戶銀行資訊Repository AccountRepo = RepositoryHelper.Get客戶銀行資訊Repository();
            var l_ListAccount = AccountRepo.Where(p => p.客戶Id == entity.Id).ToList();
            if (l_ListAccount != null)
            {
                foreach (客戶銀行資訊 t_Account in l_ListAccount)
                {
                    t_Account.IsDeleted = true;
                }
            }
            entity.IsDeleted = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}