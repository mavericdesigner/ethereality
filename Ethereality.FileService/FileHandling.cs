using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Ethereality.FileService
{
    public class FileHandling
    {
        public async Task<StorageFile> OpenSingleFile()
        {
            FileOpenPicker openFile = new FileOpenPicker();
            Task<StorageFile> pickfileTask = new Task<StorageFile>(() => openFile.PickSingleFileAsync().GetResults());
            StorageFile result = await pickfileTask;

            return result;
        }

        public async Task<List<StorageFile>> OpenMultiFiles()
        {
            FileOpenPicker openFile = new FileOpenPicker();
            Task<IReadOnlyList<StorageFile>> pickMultiFileTask = new Task<IReadOnlyList<StorageFile>>(() => openFile.PickMultipleFilesAsync().GetResults());
            IReadOnlyList<StorageFile> result = await pickMultiFileTask;

            return result.ToList();
        }

        public void CloseFile(string filename)
        {
        }

        public string SingleFileName { get; set; }

        public string[] MultipleFileNames { get; set; }
    }
}