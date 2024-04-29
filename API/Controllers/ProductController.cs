using Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[SwaggerTag("Products Services")]
public class ProductController : BaseController
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    /// <summary>
    ///   Get a Product By Id
    /// </summary>
    /// <returns></returns>
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await productService.Get(id);
        return Ok(result);
    }
    /// <summary>
    ///   Get All products
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await productService.GetAll();
        return Ok(result);
    }
    /// <summary>
    ///  Add a New product
    /// </summary>
    /// <returns></returns>
    [HttpPost("Add")]
    public async Task<IActionResult> Add(ProductDto model)
    {
        var result = await productService.Add(model);
        return Ok(result);
    }
    /// <summary>
    ///  Update a product
    /// </summary>
    /// <returns></returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(ProductDto model)
    {
        var result = await productService.Update(model);
        return Ok(result);
    }

    /// <summary>
    ///  Delete a product
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Update(int id)
    {
        var result = await productService.Delete(id);
        return Ok(result);
    }
}