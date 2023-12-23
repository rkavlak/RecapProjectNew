using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IColorService _colorService;
        public CarManager(ICarDal carDal, IColorService colorService)
        {
            _carDal = carDal;
            _colorService = colorService;
        }

        //[SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {

            ValidationTool.Validate(new CarValidator(), car);
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }


        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<CarDetailDto>> GetAll()
        {
            if (DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarListed);
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.CarId == id));
        }
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p => p.BrandId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorId(int id)
        {

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p => p.ColorId == id));
        }
        public IDataResult<List<CarDetailDto>> GetCarDetails(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p => p.CarId == id), Messages.CarListed);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            _carDal.Add(car);
            if (car.BrandId == 1)
            {
                throw new Exception("error");
            }


            car.Description = "Its a transactional test";
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

     
    }
}
