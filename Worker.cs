using SanlamTechnicalAssesmentWorkerService.services;

namespace SanlamTechnicalAssesmentWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DebitOrderService _debitOrderService;
        private readonly WriteDebitOrderToFile _writeDebitOrderToFile;

        public Worker(ILogger<Worker> logger, DebitOrderService debitOrderService, WriteDebitOrderToFile writeDebitOrderToFile)
        {
            _logger = logger;
            _debitOrderService = debitOrderService;
            _writeDebitOrderToFile = writeDebitOrderToFile;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var xmlFilePath = "debitorders.xml";

            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var debitOrders = _debitOrderService.ReadDebitOrders(xmlFilePath);
                var bankList = new List<string>();

                if (debitOrders != null)
                {
                    foreach( var debitOrder in debitOrders)
                    {
                        if (!bankList.Contains(debitOrder.BankName)){
                            bankList.Add(debitOrder.BankName);
                        }
                    }

                    if (bankList.Count > 0)
                    {
                        foreach ( var bankname in bankList)
                        {
                            // filter by bank name
                            var debitOrderByBank = debitOrders.FindAll(x => x.BankName == bankname);
                            if (debitOrderByBank != null)
                            {
                                _writeDebitOrderToFile.WriteDebitOrdersToFile(debitOrderByBank, bankname + ".txt");
                            }
                           
                        }
                    }
                }
                await Task.Delay(1000, stoppingToken);
                // Call this method to exit the application
                Environment.Exit(0);
            }

           
        }
    }
}