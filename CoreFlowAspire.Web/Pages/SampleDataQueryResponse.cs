using CoreFlowAspire.Web.Models;

namespace CoreFlowAspire.Web.Pages
{
    public partial class SampleDataModel
    {
        public class SampleDataQueryResponse
        {
            public List<SampleData> SampleData { get; set; } = new();
        }
    }
}
