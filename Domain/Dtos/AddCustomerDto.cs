namespace Domain.Dtos;
using System.ComponentModel.DataAnnotations;
public class AddCustomerDto
{
    public int Id{get;set;}
     [Required(ErrorMessage = "First name should not be empty")]
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string PhoneNumber{get;set;}
    public string Email{get;set;}
}