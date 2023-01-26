using Infrastructure.Context;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Wrapper;
using System.Net;

namespace Infrastructure.Service;
public class OrderService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public OrderService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<OrderDto>>> GetOrder()
    {
        try
        {
            var result = await _context.Orders.ToListAsync();
            var mapped = _mapper.Map<List<OrderDto>>(result);
            return new Response<List<OrderDto>>(mapped);
        }
        catch (Exception ex)
        {

            return new Response<List<OrderDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<AddOrderDto>> AddOrder(AddOrderDto order)
    {
        try
        {
            var mapped = _mapper.Map<Order>(order);
            _context.Orders.Add(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddOrderDto>(order);
        }
        catch (Exception ex)
        {

            return new Response<AddOrderDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }
    public async Task<Response<AddOrderDto>> UpdateOrder(AddOrderDto order)
    {
       
             try
        {
            var result = await _context.Orders.FindAsync(order.Id);
          var mapped = _mapper.Map<AddOrderDto>(result);
            return new Response<AddOrderDto>(mapped);
        }
        catch (Exception ex)
        {
         return new Response<AddOrderDto>(HttpStatusCode.InternalServerError,new List<string>{ex.Message});
        }
    }
    public async Task<Response<OrderDto>> GetOrderID(int id)
    {
        try
        {
            var result = await _context.Orders.FindAsync(id);
            var mapped = _mapper.Map<OrderDto>(result);
            return new Response<OrderDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
        }

    }
    public async Task<Response<string>> DeleteOrdr(int id)
    {
      
            var find = await _context.Orders.FindAsync(id);
            if(find==null) return new Response<string>(HttpStatusCode.NotFound,new List<string>{"Not found"});
            _context.Orders.Remove(find);
            _context.SaveChangesAsync();
            return new Response<string>("OK");
    }
}
