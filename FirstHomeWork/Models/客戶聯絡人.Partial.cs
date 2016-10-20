namespace FirstHomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CustomerEntities db = new CustomerEntities();
            客戶聯絡人 l_email =db.客戶聯絡人
                .Where(p=>p.Email==Email )
                .Where(p => p.客戶Id != 客戶Id)
                .FirstOrDefault();
            if (l_email!=null)
            {
                yield return new ValidationResult("email不能與其他人重複",
                    new string[] { "email" });
            }
            yield break;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        [Required]
        [Display(Name = "作廢")]
        public bool IsDeleted { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
