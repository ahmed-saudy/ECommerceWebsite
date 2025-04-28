using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace ECommerceWebsite
{


    static class ConstantSettings
    {

        //public static string MainSavingPathHTML = @"/img/bg/";
        //public static string MainSavingPathCSharp = @"wwwroot/img/bg/"; 

        public static string MainSavingPathHTML = @"\Images\";
        
        public static string MainSavingPathCSharp = @"wwwroot/Images/";


    }

    public static class GlobalMethods
    {
        /// <summary>
        /// Save the file to the images folder
        /// </summary>
        /// <param name="ImageName"></param>
        /// <param name="ImageFile"></param>
        public static void SaveNewImage(string? ImageName, IFormFile? ImageFile)
        {
            if (ImageName != null && ImageFile != null)
            {
                string ImagePath = ConstantSettings.MainSavingPathCSharp + ImageName;
                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
            }
        }
        /// <summary>
        /// delete the old image using imageName
        /// </summary>
        /// <param name="ImageName"></param>
        public static void DeleteOldImage(string? ImageName)
        {
            if (ImageName != null)
            {
                string oldImagePath = ConstantSettings.MainSavingPathCSharp + ImageName;
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
        }

        /// <summary>
        /// return the name as GUID with the file extension
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        public static string ReturnNewGUIDNamewithFileExtension(IFormFile? File)
        {
            return Guid.NewGuid().ToString() + System.IO.Path.GetExtension(File.FileName);

        }




    }
}
