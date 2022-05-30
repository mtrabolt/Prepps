using Baseline;
using Microsoft.AspNetCore.Mvc;
using Prepps.Products;
using Prepps.Products.Commands;
using Prepps.Products.Queries;

namespace Prepps.Api.Controllers.Products;

[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGetProducts _getProducts;
    private readonly IStoreProduct _storeProduct;
    private readonly IUpdateProduct _updateProduct;
    private readonly IDeleteProduct _deleteProduct;

    public ProductsController(IGetProducts getProducts, 
        IStoreProduct storeProduct,
        IUpdateProduct updateProduct,
        IDeleteProduct deleteProduct)
    {
        _getProducts = getProducts;
        _storeProduct = storeProduct;
        _updateProduct = updateProduct;
        _deleteProduct = deleteProduct;
    }

    [HttpGet]
    public ActionResult<ICollection<ProductReadModel>> GetProducts() =>
        Ok(_getProducts.Execute().Select(ProductReadModel.FromProduct).ToList());

    [HttpPost]
    public async Task<IActionResult> StoreProduct([FromBody] ProductWriteModel productWriteModel)
    {
        if (productWriteModel.ExpiresAt.ToDateTime() < DateTime.UtcNow)
        {
            return BadRequest("Item cannot expire in the past");
        }
        
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = productWriteModel.Name,
            ExpiresAt = DateOnly.FromDateTime(productWriteModel.ExpiresAt.ToDateTime()),
        };

        await _storeProduct.Execute(product);

        return Ok(product);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateProduct(Guid id, ProductWriteModel model)
    {
        var expiresAt = DateOnly.FromDateTime(model.ExpiresAt.ToDateTime());
        
        if (expiresAt.ToDateTime(TimeOnly.MinValue) < DateTime.UtcNow)
        {
            return BadRequest("Item cannot expire in the past");
        }
        
        await _updateProduct.Execute(id, model.Name, expiresAt);

        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        if (Guid.Empty == id) return BadRequest();
        
        await _deleteProduct.Execute(id);

        return Ok();
    }
}