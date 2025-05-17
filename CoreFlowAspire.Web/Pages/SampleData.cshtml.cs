using CoreFlowAspire.Web.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreFlowAspire.Web.Pages
{
    public class SampleDataModel : PageModel
    {
        public List<SampleData> Items { get; set; } = new();

        [BindProperty]
        public string Name { get; set; } = string.Empty;

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

            try
            {
                var response = await _client.SendQueryAsync<SampleDataQueryResponse>(query);
                Items = response.Data.SampleData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Query Error: " + ex.Message);
                ModelState.AddModelError("", "Failed to load sample data.");
            }
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("➡️ Name input: " + Name);

            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError("", "Name is required.");
                await OnGetAsync(); // reload existing data
                return Page();
            }

            var mutation = new GraphQLRequest
            {
                Query = @"
                mutation($name: String!) {
                    addSampleData(name: $name)
                }",
                Variables = new { name = Name }
            };

            try
            {
                var response = await _client.SendMutationAsync<AddSampleDataResponse>(mutation);
                Console.WriteLine("✅ Mutation Success");
            }

            catch (GraphQLHttpRequestException gqlEx)
            {
                Console.WriteLine($"❌ GraphQL Error: {gqlEx.Message}");

                if (gqlEx.Content != null)
                {
                    using (var reader = new System.IO.StreamReader(gqlEx.Content))
                    {
                        var content = await reader.ReadToEndAsync();
                        Console.WriteLine($"📄 Response Content: {content}");
                    }
                }

                ModelState.AddModelError(string.Empty, "Failed to add data. See console for details.");
            }
            return RedirectToPage(); // reload page after success
        }

        public class SampleDataQueryResponse
        {
            public List<SampleData> SampleData { get; set; } = new();
        }
        public class AddSampleDataResponse
        {
            public string AddSampleData { get; set; } = string.Empty;
        }
    }
}