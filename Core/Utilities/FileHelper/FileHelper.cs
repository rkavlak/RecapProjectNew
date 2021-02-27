using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelper
{
    public class FileHelper
    {
        public static string AddAsync(IFormFile file)
        {
            var result =newPath(file);
            try
            {
                var sourcePath = Path.GetTempFileName();
                if (file.Length > 0)

                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                        file.CopyTo(stream);
                File.Move(sourcePath, result.newPath);
               
                  
                
            }
            catch (Exception exception)
            {
                return exception.Message;
                
            }
            return result.Path2;
        }
        public static string UpdateAsync(string sourcePath, IFormFile file)
        {
            var result = newPath(file);
            try
            {
                if (sourcePath.Length>0)
                {
                    using (var stream = new FileStream(result.newPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                File.Delete(sourcePath);
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
            return result.Path2;
        }
        public static IResult DeleteAsync(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
        public static (string newPath, string Path2) newPath(IFormFile file)
        {
            System.IO.FileInfo formfile = new System.IO.FileInfo(file.FileName);
            string fileExtension = formfile.Extension;
            var creatingUniqueFileName = Guid.NewGuid().ToString("N")
                + "_" + DateTime.Now.Month + "_"
                + DateTime.Now.Day + "_"
                + DateTime.Now.Year + fileExtension;
            string path = Environment.CurrentDirectory + @"\wwwroot\Images";
            string result = $@"{path}\{creatingUniqueFileName}";
            return (result, $@"{path}\{creatingUniqueFileName}");
        }
    }
}
