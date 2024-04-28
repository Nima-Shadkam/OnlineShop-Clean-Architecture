using Infrastructure.Common;
using Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ProductService : IProductService
{
    private readonly OnlineShopDbContext dbContext;

    private readonly ILogger logger;


    public ProductService(OnlineShopDbContext dbContext, ILogger<ProductService> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;

    }

    public async Task<CustomActionResult<ProductDto>> Add(ProductDto model)
    {
        var result = new CustomActionResult<ProductDto>();
        var product = new Product
        {
            ProductName = model.ProductName,
            Price = model.Price,
        };

        //dbContext.Products.Add(product);
        await dbContext.AddAsync(product);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Add New Product : {product.Id}");

        model.Id = product.Id;
        result.Data = model;
        result.IsSuccess = true;
        result.Message = "The operation was Successful";

        return result;
    }

    public async Task<CustomActionResult<bool>> Delete(int id)
    {
        var result = new CustomActionResult<bool>();

        var item = new Product { Id = id };
        dbContext.Remove(item);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Remove Product : {item.ProductName + " - " + item.Id }");
        result.IsSuccess = true;    
        result.Message = "The operation was Successful";

        return result;


    }

    public async Task<CustomActionResult<ProductDto>> Get(int id)
    {
        var result = new CustomActionResult<ProductDto>();

        var product = await dbContext.Products.FindAsync(id);
        var model = new ProductDto
        {
            Id = product.Id,
            Price = product.Price,
            ProductName = product.ProductName,
            PriceWithComma = product.Price.ToString("###.###"),
        };

        result.Data = model;    
        result.IsSuccess = true;    
        return result;
    }

    public async Task<CustomActionResult<List<ProductDto>>> GetAll()
    {
        var result = new CustomActionResult<List<ProductDto>>();

        result.Data = await dbContext.Products.Select(product => new ProductDto
        {
            Id = product.Id,
            Price = product.Price,
            ProductName = product.ProductName,
            PriceWithComma = product.Price.ToString("###.###"),
        }).ToListAsync();

        result.IsSuccess = true;

        return result;
    }

    public async Task<CustomActionResult<bool>> Update(ProductDto model)
    {
        var result = new CustomActionResult<bool>();

        var data = await dbContext.Products.FindAsync( model.Id);
        data.ProductName = model.ProductName;   
        data.Price = model.Price;
        logger.LogInformation($"Update Product : {data.Id}");

        result.Message = "The operation was Successful";
        result.IsSuccess=true;
        return result;
    }
}