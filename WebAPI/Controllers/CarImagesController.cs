using Business.Abstract;
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
        IWebHostEnvironment _environment;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _environment = webHostEnvironment;

        }

        [HttpPost("add")]
        public async Task<string> Add([FromForm] FileUpload files)
        {
            System.IO.FileInfo fromform = new System.IO.FileInfo(files.files.FileName);
            string fileExtension = fromform.Extension;
            var result = Guid.NewGuid().ToString("N") + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_"+ DateTime.Now.Year + fileExtension;

            try
            {
                if (files.files.Length > 0)
                {
                    string path = _environment.WebRootPath + "\\Uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + result))
                    {

                        files.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Uploads\\" + files.files.FileName;

                    }
     

                }


                else
                {
                    return "Upload unsuccessful";
                }
             }

            
            catch (Exception exception)
            {

                return exception.Message;
            }
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
        [HttpPost("update")]
        public IActionResult Update(CarImage carImage)
        {
            var result = _carImageService.Update(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImage)
        {
            var result = _carImageService.Update(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
