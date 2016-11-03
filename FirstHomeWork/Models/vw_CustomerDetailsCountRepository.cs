using System;
using System.Linq;
using System.Collections.Generic;
	
namespace FirstHomeWork.Models
{   
	public  class vw_CustomerDetailsCountRepository : EFRepository<vw_CustomerDetailsCount>, Ivw_CustomerDetailsCountRepository
	{

	}

	public  interface Ivw_CustomerDetailsCountRepository : IRepository<vw_CustomerDetailsCount>
	{

	}
}