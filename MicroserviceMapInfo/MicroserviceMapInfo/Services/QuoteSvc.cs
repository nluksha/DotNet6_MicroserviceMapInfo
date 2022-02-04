namespace MicroserviceMapInfo.Services
{
    public class QuoteSvc
    {
        private readonly IDistanceInfoSvc distanceInfoSvc;

        public QuoteSvc(IDistanceInfoSvc distanceInfoSvc)
        {
            this.distanceInfoSvc = distanceInfoSvc;
        }

        public async Task<Quote> CreateQuote(string originCity, string destinationCity)
        {
            var distanceInfo = await distanceInfoSvc.GetDistanceAsync(originCity, destinationCity);

            var quote  = new Quote { Id = 123, ExpectedDistance = distanceInfo.Item1, ExpectedDistanceType = distanceInfo.Item2 };

            return quote;
        }
    }

    public class Quote
    {
        public int Id { get; set; }
        public int ExpectedDistance { get; set; }
        public string ExpectedDistanceType { get; set; }
    }
}
