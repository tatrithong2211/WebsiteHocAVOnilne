//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteHocAVOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Learning
    {
        public int ID_BaiHoc { get; set; }
        public string TenBaiHoc { get; set; }
        public string Link { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> ID_Category { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
