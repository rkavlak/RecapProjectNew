using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter =null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from car in filter == null ? context.Cars : context.Cars.Where(filter)
                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId
                             join color in context.Colors
                             on car.ColorId equals color.ColorId


                             select new CarDetailDto
                             {

                                 CarId = car.CarId,
                                 CarName = brand.BrandName,
                                 BrandId = brand.BrandId,
                                 ColorId=car.ColorId,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear=car.ModelYear,
                                 Description=car.Description,
                                ImagePath = context.CarImages.Where(x => x.CarId == car.CarId).FirstOrDefault().ImagePath





                             };
                return result.ToList();

            }
        }


    }
}
