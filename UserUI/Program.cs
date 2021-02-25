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
            //Test1();
            Test2();

        }

        private static void Test1()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Console.WriteLine("-----------CAR DETAİLS-----------");
            Console.WriteLine("BRANDNAME...COLORNAME...DAILYPRICE");
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.BrandName + " " + car.ColorName + " " + car.DailyPrice + " ");

            }
            Console.WriteLine("-----------CRUD-----------");
            Console.WriteLine("ID...BRANDID...COLORID...DAILYPRICE...MODELYEAR...DESCRİPTİON");

            //var result1 = carManager.Add(new Car { CarId = 1, BrandId = 2, ColorId = 9, DailyPrice = 50, ModelYear = "2014", Description = "Alfa Romeo" });
            //Console.WriteLine(result1.Message);

            //var result2 = carManager.Update(new Car { CarId = 1, BrandId = 2, ColorId = 3, DailyPrice = 50, ModelYear = "2000", Description = "Brodway" });
            //Console.WriteLine(result2.Message);

            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.CarId + " " + car.BrandId + " " + car.ColorId + " " + car.DailyPrice + " " + car.ModelYear + " ", car.Description + " ");
            }
            ColorManager colorManager = new ColorManager(new EfColorDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());





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


        }
        private static void Test2()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            UserManager userManager = new UserManager(new EfUserDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());



            userManager.Add(
             new User
             {

                 FirstName = "Ramazan",
                 LastName = "Kavlak",
                 Password = "12345",
                 Email = "sarjx123@gmail.com"

             });

            customerManager.Add
                (
                new Customer
                {

                    UserId = 1,
                    CompanyName = "Ar3ndy"
                });

            rentalManager.Add(

                new Rental
                {
                    CarId = 3,
                    CustomerId = 1,

                    RentDate = DateTime.Now,
                    ReturnDate = DateTime.Parse("23.02.2021")

                });

            foreach (var rental in rentalManager.GetRentalDetails().Data)
            {
                Console.WriteLine(rental.Id + " / " + rental.UserName + " / " + rental.CustomerName + " / " + rental.CarName + " / " + rental.DailyPrice + " / " + rental.RentDate + " / " + rental.ReturnDate);
            }

            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.CarId + " / " + car.CarName + " / " + car.ColorName +  " / " + car.DailyPrice );
            }


        }

    }
}
