namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
public class Customer
{
    public int  Id{get;set;}
      [Required(ErrorMessage = "First name should not be empty")]
    public string FirstName{get;set;}

    [Required(ErrorMessage = "LastName name should not be empty")]
    public string LastName{get;set;}

         [Required, MaxLength(100)]
    public string PhoneNumber{get;set;}
         [Required, MaxLength(100)]
    public string Email{get;set;}
    public List<Order>Orders{get;set;}
}