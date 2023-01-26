using FrontEndService.Models;
using FrontEndService.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndService.Controllers;


public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService = null)
    {
        _logger = logger;
        _productService = productService;
    }

    #region index
    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> productList = new();
        var response = await _productService.GetAllProductsAsync<ResponseDto>();
        if (response != null && response.IsSuccess)
        {
            productList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
        return View(productList);
    }
    #endregion

    #region create
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.CreateProductAsync<ResponseDto>(product);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(product);
    }
    #endregion

    #region edit
    public async Task<IActionResult> ProductEdit(int id)
    {

        var response = await _productService.GetProductByIdAsync<ResponseDto>(id);
        if (response != null && response.IsSuccess)
        {
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(productDto);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProductAsync<ResponseDto>(product);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(product);
    }
    #endregion

    #region delete
    public async Task<IActionResult> ProductDelete(int id)
    {

        var response = await _productService.GetProductByIdAsync<ResponseDto>(id);
        if (response != null && response.IsSuccess)
        {
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(productDto);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductDelete(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.DeleteProductAsync<ResponseDto>(product.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(product);
    }
    #endregion




}
