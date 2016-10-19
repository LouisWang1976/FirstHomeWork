namespace FirstHomeWork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(sp_helpdiagramdefinition_ResultMetaData))]
    public partial class sp_helpdiagramdefinition_Result
    {
    }
    
    public partial class sp_helpdiagramdefinition_ResultMetaData
    {
        public Nullable<int> version { get; set; }
        public byte[] definition { get; set; }
    }
}
