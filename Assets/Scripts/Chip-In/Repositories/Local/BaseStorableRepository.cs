using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Repositories.Local
{
    public abstract class BaseStorableRepository<TDataModel> : IRestorable where TDataModel : new()
    {
        private readonly string _storableDataModelDirectoryPath;
        private readonly string _storableDataModelFilePath;

        protected BaseStorableRepository(string storagePath, string fileName)
        {
            _storableDataModelDirectoryPath = Path.Combine(Application.persistentDataPath, storagePath);
            _storableDataModelFilePath = Path.Combine(_storableDataModelDirectoryPath, fileName);
        }

        public Task SaveToLocalStorageAsync(TDataModel dataToSave)
        {
            if (!StorageDirectoryIsExists())
            {
                Directory.CreateDirectory(_storableDataModelDirectoryPath);
            }

            return FilesUtility.WriteTextToFileAsync(JsonConverterUtility.ConvertModelToJson(dataToSave), _storableDataModelFilePath);
        }

        public async Task Restore()
        {
            if (StorageDirectoryIsExists())
            {
                if (!StorageFileIsExists())
                {
                    return;
                }
            }
            else
            {
                return;
            }

            OnDataRestored(JsonConverterUtility.ConvertJsonString<TDataModel>(await FilesUtility.ReadFileTextAsync(_storableDataModelFilePath)
                .ConfigureAwait(false)));
        }

        protected abstract void OnDataRestored(TDataModel restoredData);

        private bool StorageDirectoryIsExists()
        {
            return Directory.Exists(_storableDataModelDirectoryPath);
        }

        private bool StorageFileIsExists()
        {
            return File.Exists(_storableDataModelFilePath);
        }
    }
}