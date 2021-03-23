using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        //public List<CarDetailDto> GetCarDetails();
       public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null);
    }
}
