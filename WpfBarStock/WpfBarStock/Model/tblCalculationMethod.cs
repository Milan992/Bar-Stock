//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfBarStock.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCalculationMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCalculationMethod()
        {
            this.tblArticles = new HashSet<tblArticle>();
        }
    
        public int CalculationMethodID { get; set; }
        public string CalculationMethodName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblArticle> tblArticles { get; set; }
    }
}
