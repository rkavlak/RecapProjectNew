using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarContext context=new RentACarContext())
            {
                {
                    var result = from car in context.Cars
                                 join rental in context.Rentals
                                 on car.CarId equals rental.CarId
                                 join customer in context.Customers
                                 on rental.CustomerId equals customer.UserId
                                 join user in context.Users
                                 on customer.UserId equals user.Id
                                 select new RentalDetailDto
                                 {
                                     Id = rental.CarId,
                                     CarId = car.CarId,
                                     CarName = car.Description,
                                     DailyPrice = car.DailyPrice,
                                     CustomerName = customer.CompanyName,
                                     RentDate = rental.RentDate,
                                     ReturnDate = rental.ReturnDate,
                                     UserName = user.FirstName + " " + user.LastName,




                                 };
                    return result.ToList();

                }
            }
        }
    }
}
