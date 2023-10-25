using SanlamTechnicalAssesmentWorkerService.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SanlamTechnicalAssesmentWorkerService.services
{
    public class DebitOrderServiceOLD
    {
        public static List<DebitOrder> ReadDebitOrders()
        {
            var debitOrders = new List<DebitOrder>();

            using (var reader = XmlReader.Create("debitorders.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "deduction":
                                var debitOrder = new DebitOrder();
                                while (reader.Read())
                                {
                                    if (reader.NodeType == XmlNodeType.EndElement)
                                    {
                                        break;
                                    }

                                    if (reader.IsStartElement())
                                    {
                                        switch (reader.Name)
                                        {
                                            case "accountholder":
                                                debitOrder.AccountHolder = reader.ReadElementContentAsString();
                                                break;
                                            case "accountnumber":
                                               // debitOrder.AccountNumber = reader.ReadElementContentAsString();
                                                break;
                                            case "accounttype":
                                                debitOrder.AccountType = reader.ReadElementContentAsString();
                                                break;
                                            case "bankname":
                                                debitOrder.BankName = reader.ReadElementContentAsString();
                                                break;
                                            case "branch":
                                                debitOrder.BranchLocation = reader.ReadElementContentAsString();
                                                break;
                                            case "amount":
                                              //  debitOrder.DebitAmount = decimal.Parse(reader.ReadElementContentAsString());
                                                break;
                                            case "date":
                                              //  debitOrder.DebitDate = DateTime.ParseExact(reader.ReadElementContentAsString(), "MM/dd/yyyy", null);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            debitOrders.AddRange(debitOrders);

            if (debitOrders is not null && debitOrders.Count > 0)
            {
                return debitOrders;
            }
            return debitOrders;
        }
    }
};
