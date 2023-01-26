using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController:ControllerBase
{
        private readonly AddressService _AddressService;

        public AddressController(AddressService AddressService)
        {
                _AddressService = AddressService;
        }

        [HttpGet()]
        public  async Task<Response<List<AddressDto>>> GetAddress()
        {
                return await _AddressService.GetAddress();
        }
        
        [HttpGet("GetBYID")]
        public  async Task<Response<AddressDto>>GetById(int id){
            return await _AddressService.GetAdressId(id);
        }
        
        [HttpPost]
        public async Task<Response<AddAddressDto>> AddCustomer(AddAddressDto Address)
        {
            
                 return await  _AddressService.AddAddress(Address);
        }
          
        [HttpPut]
        public async Task<Response<AddAddressDto>>UpdateAddress(AddAddressDto address)
        {
            {
        if (ModelState.IsValid)
        { 
            return await _AddressService.AddAddress(address);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddAddressDto>(System.Net.HttpStatusCode.NotFound, errors);
        }
       
    }
        }
        [HttpDelete]
        public async Task <Response<string>>DeleteAddresss (int id)
        {
           return await _AddressService.DeleteAddress(id);
        } 
}