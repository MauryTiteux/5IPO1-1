using System.Net.Http.Json;

namespace FoodSearchTutorial.Services;

public class ProductService
{
    const string BASE_SEARCH_URL = "https://fr.openfoodfacts.org/cgi/search.pl?action=process&json=true";


    HttpClient client;
    public ProductService()
    {
        client = new();
    }

    public async Task<List<Product>> GetRandomProductsAsync()
    {
        var page = new Random().Next(1, 1000);
        var url = $"{BASE_SEARCH_URL}&{GetNutriScoreFilter(0)}&page={page}&page_size=10";
        var response = await client.GetAsync(url);
        var products = await GetProductsAsync(response);
        return products;
    }


    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {
        var url = $"{BASE_SEARCH_URL}&tag_contains_0=contains&tagtype_0=categories" +
            $"&tag_0={searchTerm}&tagtype_1=label&{GetNutriScoreFilter(2)}&page_size=10";

        var reponse = await client.GetAsync(url);

        var products = await GetProductsAsync(reponse);
        return products;
    }

    private async Task<List<Product>> GetProductsAsync(HttpResponseMessage response)
    {
        List<Product> products = new();

        if (response.IsSuccessStatusCode) 
        {
            products = (await response.Content.ReadFromJsonAsync<ProductsResult>()).Products;
        }

        return products;
    }

    private string GetNutriScoreFilter(int tagId)
    {
        var nutriScore = Settings.NutriScore;

        return ("ALL" == nutriScore)
            ? string.Empty 
            : $"tagtype_{tagId}=nutrition_grades&tag_contains_{tagId}={nutriScore}";
    }

}
