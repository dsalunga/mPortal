//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCMS.WebSystem.Apps.Integration
{
    using System;
    using System.Collections.Generic;
    
    public partial class MusicEntry
    {
        public int Id { get; set; }
        public int MusicId { get; set; }
        public int EntryTypeId { get; set; }
        public string FileName { get; set; }
        public string Tags { get; set; }
        public System.DateTime DateModified { get; set; }
        public int FileSize { get; set; }
    
        public virtual Music Music { get; set; }
    }
}
