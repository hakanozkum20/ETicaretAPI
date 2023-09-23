using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions.Storage.Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Infrastructure.Services.Storage.Google
{
    public class GoogleStorage : Storage,  IGoogleStorage
    {


    private readonly GoogleCredential _googleCredential;
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;



    public GoogleStorage(IConfiguration configuration)
        {
            _googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GCPStorageAuthFile"));
            _storageClient = StorageClient.Create(_googleCredential);
            _bucketName = configuration.GetValue<string>("GoogleCloudStorageBucketName");
        }


        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            await _storageClient.DeleteObjectAsync(_bucketName,$"{pathOrContainerName}/{fileName}");
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            string prefix = pathOrContainerName;
            var objects = _storageClient.ListObjects(_bucketName, prefix);
            return objects.Select(o => o.Name).ToList();

        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            string prefix = pathOrContainerName;
            var objects = _storageClient.ListObjects(_bucketName, Path.GetDirectoryName(fileName)).ToArray();
            return objects.Any(o => o.Name== $"{prefix}/{fileName}");
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            // throw new NotImplementedException();
            // using (var memoryStream = new MemoryStream())
            // {
            //     await pathOrContainerName.CopyToAsync(memoryStream);
            //     var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
            //     return dataObject.MediaLink;
            // }

            List<(string fileName, string pathOrContainerName)> datas = new ();
            foreach (IFormFile file in files)
            {

                string fileNewName = await FileRenameAsync(pathOrContainerName,file.Name,HasFile);


                await  _storageClient.UploadObjectAsync(_bucketName,$"{pathOrContainerName}/{fileNewName}",file.ContentType,file.OpenReadStream());
                datas.Add((fileNewName,$"{pathOrContainerName}/{fileNewName}"));
            }
            return datas;

            //  using (var memoryStream = new MemoryStream())
            // {
            //     await imageFile.CopyToAsync(memoryStream);
            //     var dataObject = await _storageClient.UploadObjectAsync(_bucketName, $"{pathOrContainerName}/{file.Name}", null, memoryStream);
            //     return dataObject.MediaLink;
            // }

        }
    }
}