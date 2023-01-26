using Infrastructure.Context;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Wrapper;
using System.Net;

namespace Infrastructure.Service;
public class ProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ProductService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper=mapper;
    }
     public async Task<Response<List<ProductDto>>> GetProduct()
    {
        try
        {
            var result = await _context.Products.ToListAsync();
            var mapped = _mapper.Map<List<ProductDto>>(result);
            return new Response<List<ProductDto>>(mapped);
        }
        catch (Exception ex)
        {

            return new Response<List<ProductDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
     public async Task<Response<AddProductDto>> AddProduct(AddProductDto product)
    {
        try
        {
            var mapped = _mapper.Map<Product>(product);
            _context.Products.Add(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddProductDto>(product);
        }
        catch (Exception ex)
        {

            return new Response<AddProductDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }
    public async Task<Response<AddProductDto>> UpdateProduct(AddProductDto product)
    {
        
             try
        {
            var result = await _context.Products.FindAsync(product.Id);
          var mapped = _mapper.Map<AddProductDto>(result);
            return new Response<AddProductDto>(mapped);
        }
        catch (Exception ex)
        {
         return new Response<AddProductDto>(HttpStatusCode.InternalServerError,new List<string>{ex.Message});
        }
    }
   public async Task<Response<ProductDto>> GetProductID(int id)
    {
        try
        {
            var result = await _context.Products.FindAsync(id);
            var mapped = _mapper.Map<ProductDto>(result);
            return new Response<ProductDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
        }

    }
   public async Task<Response<string>> DeleteProduct(int id)
    {
        var find = await _context.Products.FindAsync(id);
        if(find==null) return new Response<string>(HttpStatusCode.NotFound,new List<string>{"Not found"});
            _context.Products.Remove(find);
            _context.SaveChangesAsync();
            return new Response<string>("Sucessfully");
     
    
}
}