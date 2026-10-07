using System;

namespace FnBReport.DAL.Models
{
    public interface IBaseEntity<TId>
    {
        TId Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
        bool IsDeleted { get; set; }
        bool IsActive { get; set; }
    }
}
