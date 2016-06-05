


using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Ethereality.FileService
{
    public class FileHandling
    {
        public  StorageFile OpenFile()
        {
            FileOpenPicker openFile = new FileOpenPicker();
            var result = new Task<StorageFile>( ()=>openFile.PickSingleFileAsync().GetResults() );
            result.Wait();

            return result.Result;
        }

        public string[] OpenFiles()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            MultipleFileNames = openFile.FileNames;

            return MultipleFileNames;
        }

        public void CloseFile(string filename)
        {
        }

       

        public string SingleFileName { get; set; }
   


        public string[] MultipleFileNames { get; set; }
    
    }
}