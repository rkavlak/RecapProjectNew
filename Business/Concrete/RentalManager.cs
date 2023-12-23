using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = _rentalDal.Get(r=>r.CarId==rental.CarId&& r.ReturnDate==null);
            if (result!=null)
            {
                return new ErrorResult(Messages.RentFailed);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentAdded);
        }

        public IDataResult<Rental> CheckReturnDate(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null);
            if (result.Count>0)
            {
                return new ErrorDataResult<Rental>(Messages.UndeliveredCarRental);
            }
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.CarId == carId));
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentsListed);
        }

        public IDataResult<Rental> GetByCarId(int carId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.CarId == carId));
        }

        public IDataResult<Rental> GetByCustomerId(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.CustomerId == id));
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            var result = _rentalDal.Get((r => r.RentalId == rentalId));
            result.ReturnDate = DateTime.Now.Date;
            Update(result);
            return new SuccessDataResult<Rental>(Messages.RentListed);
        }

   
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsByCarId(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(c => c.CarId == carId), Messages.RentListed);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentUpdated);
        }
    }
}
