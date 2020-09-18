using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;

namespace RedeSocial.CrossCutting.UploadImg
{
    public static class Upload
    {
        public static string UploadFoto(Stream file, string nomeImg, string nomeContainer)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=redesocialinfnet;AccountKey=vZcfcl7NqHfcIQJXfUXZRe4U1ePNyU3D4A9mkbj4yoWNFWl/2f78zN9gDFfbg05n1tKvp6QlTTT5jRIVFtTqmg==;EndpointSuffix=core.windows.net");

            var blobService = cloudStorageAccount.CreateCloudBlobClient();

            var container = blobService.GetContainerReference(nomeContainer);
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(nomeImg);
            blob.UploadFromStream(file);
            return blob.Uri.ToString();
        }
    }
}
