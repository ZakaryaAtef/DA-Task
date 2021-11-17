using System;
using DATask.Droid;
using System.IO;
using System.Threading.Tasks;
[assembly: Xamarin.Forms.Dependency(typeof(SaveAndLoad))]
namespace DATask.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            try
            {
                filename = "DATask" + filename;
                var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("/files","");
                //var documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;
                var filePath = Path.Combine(documentsPath, filename);
                //System.IO.File.WriteAllText(filePath, text);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false))
                {
                    sw.Write(text);
                    sw.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }
        public string LoadText(string filename)
        {
            filename = "DATask" + filename;
            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("/files", "");
            //var documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;
            var filePath = Path.Combine(documentsPath, filename);
            string text = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                text = sr.ReadToEnd();
                sr.Close();
            }

            return text;//System.IO.File.ReadAllText(filePath);
        }

        void ISaveAndLoad.SaveTextAsync(string filename, string text)
        {
            throw new NotImplementedException();
        }

        Task<string> ISaveAndLoad.LoadTextAsync(string filename)
        {
            throw new NotImplementedException();
        }
    }
}