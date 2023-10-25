using SanlamTechnicalAssesmentWorkerService.Interfaces;
using SanlamTechnicalAssesmentWorkerService.models;
using System.Diagnostics;
using System.Text;

namespace SanlamTechnicalAssesmentWorkerService.services
{
    public class WriteDebitOrderToFile : IWriteDebitOrderToFile
    {
        private readonly ILogger<WriteDebitOrderToFile> _logger;
        public WriteDebitOrderToFile(ILogger<WriteDebitOrderToFile> logger) {
            _logger = logger;
        }

        public void WriteDebitOrdersToFile(List<DebitOrder> debitOrders, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    string bankName = debitOrders.FirstOrDefault().BankName.Trim().ToUpper();

                   /* var _debitOrders = new List<string>();
                    foreach(var debitOrder in debitOrders)
                    {
                        _debitOrders.Add(debitOrder.DebitAmount);
                    }

                    //  var debitAmounts = ConvertStringsToIntegers(_debitOrders);
                    //   string headerRecord = $"{bankName,-16}{debitOrders.Count():D3}{debitOrders.Sum(order => Int32.Parse(order.DebitAmount)):D10}";
                   */
                    string headerRecord = $"{bankName,-16}{debitOrders.Count():D3}";
                 
                    writer.WriteLine($"{bankName} ");
                    foreach (var order in debitOrders)
                    {
                        string initials = order.AccountHolder.Split(' ').First()[0].ToString();
                        string surname = order.AccountHolder.Split(' ').Last().Replace(" ", "").PadRight(15);
                        var accountType = getAccountAbbreviation(order.AccountType);
                        var debitAmount = ConvertToDesiredFormat(order.DebitAmount);
                        var debitDate = ConvertDateToCustomFormat(order.DebitDate);
                        writer.WriteLine($"{initials}{surname}{order.AccountNumber}  {accountType} {order.BranchLocation}  {debitAmount}{debitDate}");
                       
                    }
                }

                
                _logger.LogInformation("Debit orders have been written to the file successfully.");
            }
            catch (Exception ex)
            {
              
                _logger.LogInformation($"An error occurred: {ex.Message}");
                throw ex;

            }
        }

        public string getAccountAbbreviation(string  accountType) { 
     
            if (accountType.ToString().Trim().Contains("cheque"))
            {
                return "CC";
            }else if (accountType.ToString().Trim().Contains("savings"))
            {
                return "SAV";
            }
            else if (accountType.ToString().Trim().Contains("credit"))
            {
                return "CR";
            }
            return "OTH";
        }


        public static string ConvertToDesiredFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string is null or empty");
            }

            decimal number;
            string sanitizedInput = input.Replace('.', ',');

            if (!decimal.TryParse(sanitizedInput, out number))
            {
                throw new ArgumentException("Input string is not a valid decimal number");
            }

            // If the number is less than 100000, prepend leading zeros
            string numberAsString = number.ToString("F2");
            while (numberAsString.Length < 7)
            {
                numberAsString = "00" + numberAsString;
            }

            return numberAsString.Replace(",", "");
        }

        public string ConvertDateToCustomFormat(DateTime date)
        {
            // Get the year, month, and day components from the date
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            string customFormat = $"{year}{month:D2}{day:D2}";

            return customFormat;
        }

        public static List<int> ConvertStringsToIntegers(List<string> stringList)
        {
            List<int> intList = new List<int>();

            foreach (string str in stringList)
            {
                if (int.TryParse(str, out int parsedValue))
                {
                    intList.Add(Int32.Parse(parsedValue.ToString().Split(".")[0]));
                }
                else
                {
                    // Handle parsing errors if necessary
                    Console.WriteLine($"Failed to parse element: {str}");
                }
            }

            return intList;
        }

    }
   
}
