namespace FirstHomeWork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(sp_helpdiagrams_ResultMetaData))]
    public partial class sp_helpdiagrams_Result
    {
    }
    
    public partial class sp_helpdiagrams_ResultMetaData
    {
        
        [StringLength(128, ErrorMessage="欄位長度不得大於 128 個字元")]
        public string Database { get; set; }
        
        [StringLength(128, ErrorMessage="欄位長度不得大於 128 個字元")]
        [Required]
        public string Name { get; set; }
        [Required]
        public int ID { get; set; }
        
        [StringLength(128, ErrorMessage="欄位長度不得大於 128 個字元")]
        public string Owner { get; set; }
        [Required]
        public int OwnerID { get; set; }
    }
}
