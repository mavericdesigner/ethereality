


using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace Ethereality.FileService
{
    public class FileHandling
    {
        public  Task<string> OpenFile()
        {
            FileOpenPicker openFile = new FileOpenPicker();
            var result = openFile.PickSingleFileAsync();


            return result;
        }

        public string[] OpenFiles()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            FileNamesISIS = openFile.FileNames;

            return FileNamesISIS;
        }

        public void CloseFile(string filename)
        {
        }

        private string _FileNameISIS;

        public string FileNameISIS
        {
            get { return _FileNameISIS; }
            set
            {
                _FileNameISIS = value;
                RaisePropertyChanged<string>(() => this.FileNameISIS);
            }
        }

        private string[] _FileNamesISIS;

        public string[] FileNamesISIS
        {
            get { return _FileNamesISIS; }
            set
            {
                _FileNamesISIS = value;
                RaisePropertyChanged<string[]>(() => this.FileNamesISIS);
            }
        }
    }
}