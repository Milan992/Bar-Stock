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
    
    public partial class tblArticle
    {
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public Nullable<int> Price { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Amount { get; set; }
        public int NewAmount { get; set; }
        public Nullable<int> ProcuredAmount { get; set; }
        public Nullable<int> AmountSold { get; set; }
        public int CalculationMethodID { get; set; }
    
        public virtual tblCalculationMethod tblCalculationMethod { get; set; }
    }
}
