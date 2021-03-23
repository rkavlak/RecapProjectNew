using Business.Abstract;
using Core.Utilities.FileHelper;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;


        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;


        }

        [HttpPost("add")]
        public IActionResult AddAsync([FromForm(Name =("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            carImage.Date = DateTime.Now;
            carImage.ImagePath = FileHelper.AddAsync(file);
            var result = _carImageService.Add(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }




        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getimagesbycarid")]
        public IActionResult GetImagesById([FromQuery(Name = ("id"))] int carId)
        {
            var result = _carImageService.GetImagesByCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var oldPath = Path.Combine(Environment.CurrentDirectory, "wwwroot",
                _carImageService.Get(carImage.Id).Data.ImagePath);
            carImage.ImagePath = FileHelper.UpdateAsync(oldPath, file);
            var result = _carImageService.Update(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int Id)
        {
            var carImage = _carImageService.Get(Id).Data;
            var oldPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", _carImageService.Get(Id).Data.ImagePath);
            FileHelper.DeleteAsync(oldPath);
            var result = _carImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
