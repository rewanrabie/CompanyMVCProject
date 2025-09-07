using Elfie.Serialization;

namespace MVC_3.Helpers
{
    public static class DocumentSetting
    {
        //1.UpLoad
        public static string UpLoad(IFormFile File, string foldername)
        {
            // 1. Get Loaction Of File 

            //  string folderpath= $"C:\\Users\\center arwa\\source\\repos\\MVC 3\\MVC 3\\wwwroot\\Files\\{foldername}";

            //  string folderpath = Directory.GetCurrentDirectory() + $"\\wwwroot\\Files\\{foldername}";

            // string folderpath = Path.Combine(Directory.GetCurrentDirectory() , $"\\wwwroot\\Files\\{foldername}");
            /* string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername);

             if (!Directory.Exists(folderpath))
             {
                 Directory.CreateDirectory(folderpath);
             }

             // 2.Get File Name And Make it Unique

             string fileName = $"{Guid.NewGuid()}{File.FileName}";

             // 3.Get File Path : FolderPath + FileName

             string filepath = Path.Combine(folderpath,fileName);

             // 4.File Steam : Data Ber Sec

            using var FileSteam = new FileStream (filepath, FileMode.Create);
             File.CopyTo(FileSteam);
             return fileName;
         }

         //2.Delete

         public static void Delete(string fileName,string foldername)
         {
             string filepath = Path.Combine(Directory.GetCurrentDirectory(), $"\\wwwroot\\Files\\{foldername}",fileName);

             if(File.Exists(filepath))
             File.Delete(filepath);*/

            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername);

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            string fileExt = Path.GetExtension(File.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExt}";
            string filepath = Path.Combine(folderpath, fileName);

            using var fileStream = new FileStream(filepath, FileMode.Create);
            File.CopyTo(fileStream);

            return fileName;
        }

        public static void Delete(string fileName, string foldername)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername, fileName);

            if (File.Exists(filepath))
                File.Delete(filepath);
        }
    }
}
