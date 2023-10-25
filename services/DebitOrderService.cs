using SanlamTechnicalAssesmentWorkerService.Interfaces;
using SanlamTechnicalAssesmentWorkerService.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SanlamTechnicalAssesmentWorkerService.services
{
    public class DebitOrderService : IDebitOrderService
    {

        private readonly ILogger<DebitOrderService> _logger;

        public DebitOrderService(ILogger<DebitOrderService> logger)
        {
            _logger = logger;
        }

        public List<DebitOrder> ReadDebitOrders(string filepath)
        {
            List<DebitOrder> debitOrders = new List<DebitOrder>();

            try
            {
                _logger.LogInformation(" Reading from reading XML file");
                XDocument doc = XDocument.Load(filepath);
               
                foreach (XElement deductionElement in doc.Descendants("deduction"))
                {
                    _logger.LogInformation(" processing XML file");
                    DebitOrder debitOrder = new DebitOrder
                    {
                        AccountHolder = deductionElement.Element("accountholder").Value,
                        AccountNumber = deductionElement.Element("accountnumber").Value,
                        AccountType = deductionElement.Element("accounttype").Value,
                        BankName = deductionElement.Element("bankname").Value,
                        BranchLocation = deductionElement.Element("branch").Value,
                        DebitAmount = deductionElement.Element("amount").Value.ToString().Trim(),
                        DebitDate = DateTime.Parse(deductionElement.Element("date").Value)
                    };

                    debitOrders.Add(debitOrder);

                }
            }
            catch (Exception ex)
            {
               
                _logger.LogError("Error reading XML file: " + ex.Message);
                throw;
               // Console.WriteLine("Error reading XML file: " + ex.Message);
            }

            return debitOrders;
        }
    }
};
