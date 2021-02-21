using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());




            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.CarId + " " + car.BrandId + " " + car.ColorId + " " + car.DailyPrice + " " + car.Description);
            }
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorId + " " + color.ColorName);
                Console.WriteLine(colorManager.GetAll().Message);
            }
            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.BrandId + " " + brand.BrandName);
            }
            Console.WriteLine("----------");
            foreach(var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.CarName + " " + car.BrandName + " " + car.ColorName + " " + car.DailyPrice);
            }


        }
    }
}
