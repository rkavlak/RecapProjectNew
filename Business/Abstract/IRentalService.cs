using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int rentalId);
        IDataResult<List<RentalDetailDto>> GetRentalsByCarId(int carId);
        IDataResult<Rental> GetByCarId(int carId);
        IDataResult<Rental> GetByCustomerId(int id);
        IDataResult<Rental> CheckReturnDate(int carId);
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        
    }
}
