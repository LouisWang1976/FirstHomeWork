using System;
using System.Linq;
using System.Collections.Generic;
	
namespace FirstHomeWork.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶銀行資訊> GetAllDataOrderById(string p_Name, int page = 1)
        {
            var IQAccount = this.All();
            if (!string.IsNullOrEmpty(p_Name))
            {
                IQAccount = IQAccount.Where(p => p.帳戶名稱.Contains(p_Name));
            }
            IQAccount = IQAccount.OrderByDescending(p => p.Id);
            return IQAccount;
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.IsDeleted = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}