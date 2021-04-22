using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COL_CS381
{
    
    class TextFile
    {
        

        public static string writeReportOnServer(string text, string path)
        {

            string fileName = (getLastReportNumber(path) + 1).ToString() + ".txt";

            string fullPath = Path.Combine(path, fileName);

            File.WriteAllText(fullPath, text);

            return fullPath;
        }

        public static string getReportPath()
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Reports"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Reports"));
            return Path.Combine(Directory.GetCurrentDirectory(), "Reports");
        }

        public static void writeReportFailed(string path, string text)
        {
            
            string date = DateTime.Now.ToString().Replace('/','_').Replace(':','_');

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            try
            {
                File.WriteAllText(Path.Combine(path, date + ".txt"), text);
            }
            catch
            {
                MessageBox.Show("Problemi di salvataggio di nel report falliti");
            }
        }

        public static int getLastReportNumber(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

            List<int> filesNumber = new List<int>();
            
            if(Files.Length < 1)
            {
                return 0;
            }

            foreach(FileInfo f in Files.ToArray<FileInfo>())
            {
                filesNumber.Add(int.Parse(f.ToString().Split('.')[0]));
            }

            int last = filesNumber.Max();

            return last;


            //string str = ""
            //foreach (FileInfo file in Files)
            //{
            //    str = str + ", " + file.Name;
            //}


        }
    }



}
