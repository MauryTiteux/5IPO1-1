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
        // V�rifie que la m�thode ne fait rien si IsBusy est d�finie sur true,
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
        // V�rifie que la m�thode appelle correctement la m�thode GetRandomProductsAsync
        // du service de produit et ajoute les produits r�cup�r�s � la liste Products
        Assert.IsTrue(productService.GetRandomProductsAsyncCalled);
        Assert.AreEqual(productService.Products.Count, viewModel.Products.Count);
    }

}
