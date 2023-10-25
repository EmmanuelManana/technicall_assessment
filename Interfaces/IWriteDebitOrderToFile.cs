using SanlamTechnicalAssesmentWorkerService.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanlamTechnicalAssesmentWorkerService.Interfaces
{
    interface IWriteDebitOrderToFile
    {
        public void WriteDebitOrdersToFile(List<DebitOrder> debitOrders, string filePath);
    }
}
