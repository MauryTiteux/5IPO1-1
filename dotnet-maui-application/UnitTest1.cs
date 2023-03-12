using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

[TestClass]
public class ProductsViewModelTests
{
    [TestMethod]
    public async Task GetRandomProductsAsync_WhenBusy_ReturnsImmediately()
    {
        // Arrange
        var viewModel = new ProductsViewModel();
        viewModel.IsBusy = true;

        // Act
        await viewModel.GetRandomProductsAsync();

        // Assert
        // Vérifie que la méthode ne fait rien si IsBusy est définie sur true,
        // en s'assurant que la liste de produits reste vide
        Assert.AreEqual(0, viewModel.Products.Count);
    }

    [TestMethod]
    public async Task GetRandomProductsAsync_RetrievesProductsFromService()
    {
        // Arrange
        var viewModel = new ProductsViewModel();
        var productService = new ProductServiceMock();
        viewModel.productService = productService;

        // Act
        await viewModel.GetRandomProductsAsync();

        // Assert
        // Vérifie que la méthode appelle correctement la méthode GetRandomProductsAsync
        // du service de produit et ajoute les produits récupérés à la liste Products
        Assert.IsTrue(productService.GetRandomProductsAsyncCalled);
        Assert.AreEqual(productService.Products.Count, viewModel.Products.Count);
    }

}
