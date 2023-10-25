using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanlamTechnicalAssesmentWorkerService.models
{
    public class DebitOrder
    {
        public string? AccountHolder { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? BankName { get; set; }
        public string? BranchLocation { get; set; }
        public string? DebitAmount { get; set; }
        public DateTime DebitDate { get; set; }

    }
}
