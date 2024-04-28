using Infrastructure.Common;
using Infrastructure.Dto;

public interface IProductService
{
   // ??
    Task<CustomActionResult<List<ProductDto>>> GetAll();
    Task<CustomActionResult<ProductDto>> Get(int id);
    Task<CustomActionResult<ProductDto>> Add(ProductDto model);
    Task<CustomActionResult<bool>> Update(ProductDto model);
    Task<CustomActionResult<bool>> Delete(int id);

}