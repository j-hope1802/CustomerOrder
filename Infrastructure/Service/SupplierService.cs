using Infrastructure.Context;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Wrapper;
using System.Net;
namespace Infrastructure.Service;
public class SupplierService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public SupplierService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper=mapper;
    }
    public async Task<Response<List<SupplierDto>>> GetSupplier()
    {
        try
        {
            var result = await _context.Suppliers.ToListAsync();
            var mapped = _mapper.Map<List<SupplierDto>>(result);
            return new Response<List<SupplierDto>>(mapped);
        }
        catch (Exception ex)
        {

            return new Response<List<SupplierDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }
        public async Task<Response<AddSupplierDto>> AddSuplier(AddSupplierDto sup)
    {
        try
        {
            var mapped = _mapper.Map<Supplier>(sup);
            _context.Suppliers.Add(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddSupplierDto>(sup);
        }
        catch (Exception ex)
        {

            return new Response<AddSupplierDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }
    public async Task<Response<AddSupplierDto>> UpdateSupplier(AddSupplierDto supplier)
    {
       
             try
        {
            var result = await _context.Suppliers.FindAsync(supplier.Id);
          var mapped = _mapper.Map<AddSupplierDto>(result);
            return new Response<AddSupplierDto>(mapped);
        }
        catch (Exception ex)
        {
         return new Response<AddSupplierDto>(HttpStatusCode.InternalServerError,new List<string>{ex.Message});
        }
       
    }
    public async Task<Response<SupplierDto>> GetSupplierID(int id)
    {
        try
        {
            var result = await _context.Suppliers.FindAsync(id);
            var mapped = _mapper.Map<SupplierDto>(result);
            return new Response<SupplierDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<SupplierDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
        }

    }
    public async Task<Response<string>> DeleteSupplier(int id)
    {
            var find = await _context.Suppliers.FindAsync(id);
            if (find==null) return new Response<string>(HttpStatusCode.NotFound,new List<string>{"Not found"});
            _context.Suppliers.Remove(find);
            _context.SaveChangesAsync();
            return new Response<string>("Sucessfully");
    }
}