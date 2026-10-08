using System;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using System.Data;

namespace FnBReport.DAL.Repositories
{
    public class MonthlySaleRepository : EfBaseRepository<MonthlySale, Guid>, IMonthlySaleRepository
    {
        public MonthlySaleRepository(POSReportContext context) : base(context)
        {
        }
    }
}
