using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RentACarContext context=new RentACarContext())
            {
                {


                    var result = from rt in filter == null ? context.Rentals : context.Rentals.Where(filter)
                                 join cr in context.Cars on rt.CarId equals cr.CarId
                                 join cst in context.Customers on rt.CustomerId equals cst.CustomerId
                                 join usr in context.Users on cst.UserId equals usr.Id
                                 join brd in context.Brands on cr.BrandId equals brd.BrandId
                                 join clr in context.Colors on cr.ColorId equals clr.ColorId
                                 select new RentalDetailDto
                                 {
                                     Id = rt.RentalId,
                                     CustomerName = cst.CompanyName,
                                     CarName = brd.BrandName,
                                     DailyPrice=cr.DailyPrice,
                                     UserName=usr.FirstName + usr.LastName,
                                     CustomerId=rt.CustomerId,
                                     CarId = rt.CarId,
                                     RentDate = rt.RentDate,
                                     ReturnDate = rt.ReturnDate
                                 };
                    return result.ToList();

                }
            }
        }
    }
}
