using CoreFlowAspire.Web.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreFlowAspire.Web.Pages
{
    public partial class SampleDataModel : PageModel
    {

        [BindProperty(SupportsGet = false)]
        public string Name { get; set; } = string.Empty;
        public List<SampleData> Items { get; set; } = new();

        private readonly GraphQLHttpClient _client;

        public SampleDataModel()
        {
            _client = new GraphQLHttpClient("http://localhost:5568/graphql", new NewtonsoftJsonSerializer());
        }

        public async Task OnGetAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                {
                    sampleData {
                        id
                        name
                        createdAt
                    }
                }"
            };

            var response = await _client.SendQueryAsync<SampleDataQueryResponse>(query);
            Items = response.Data.SampleData;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError(string.Empty, "Name is required.");
                // โหลดข้อมูลใหม่
                await OnGetAsync();
                return Page(); // ใช้ Page() แทน RedirectToPage() เพื่อรักษา ModelState
            }
            var mutation = new GraphQLRequest
            {
                Query = @"
            mutation($name: String!) {
                addSampleData(name: $name)
            }",
                Variables = new { name = Name }
            };

            var response = await _client.SendMutationAsync<GraphQLResponse<dynamic>>(mutation);

            if (response.Errors != null)
            {
                foreach (var err in response.Errors)
                {
                    Console.WriteLine("❌ GraphQL Error: " + err.Message);
                }
            }

            return RedirectToPage();
        }
    }
}
