using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.UseCases.Adapters;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.File;

namespace BlogApp.Infrastructure
{
    public class AzureFilesDataPersister : IDataPersister
    {
        private static readonly StorageCredentials StorageCredentials =
            new StorageCredentials(Constants.Account, Constants.KeyValue);

        private static readonly CloudStorageAccount Account =
            new CloudStorageAccount(StorageCredentials, true);

        private static readonly CloudFileClient Client = Account.CreateCloudFileClient();
        private static readonly CloudFileShare Share = Client.GetShareReference(Constants.Share);
        private static readonly CloudFileDirectory Directory = Share.GetRootDirectoryReference();

        public void PersistData(IBlogPostData data)
        {
            var file = Directory.GetFileReference(data.Title);
            var dataAsString = data.AsString();
            file.UploadText(dataAsString);
        }

        public IBlogPostData GetData(string title)
        {
            var file = Directory.GetFileReference(title);
            var text = file.DownloadText();
            var lines = text.Split('\n');
            if (lines.Length != 2) return null;
            var result = new BlogPostData(lines[0], lines[1]);
            return result;
        }
    }
}