using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MyCloudProject.Common;
using System;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using Azure;
using System.Collections.Generic;

namespace MyExperiment
{
    public class AzureStorageProvider : IStorageProvider
    {
        private MyConfig config;

        public AzureStorageProvider(IConfigurationSection configSection)
        {
            config = new MyConfig();
            configSection.Bind(config);
        }

        public async Task<string> DownloadInputFile(string fileName)
        {
            var StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=teamspiralganglions1;AccountKey=DSjdlUiFRt9ckju94NlIF+1CbOr7pJVgJzfDgRABFFks6pF8awUy8YCrKSvfgRJibXQQNgsskzdy+AStxRZFZQ==;EndpointSuffix=core.windows.net ";
            await Console.Out.WriteLineAsync("i am in DownloadInputFile class");
            BlobContainerClient container = new BlobContainerClient(StorageConnectionString, fileName);
            await container.CreateIfNotExistsAsync();

            // Get a reference to a blob named "sample-file"
            BlobClient blob = container.GetBlobClient(fileName);

            //throw if not exists:
            //blob.ExistsAsync

            // return "../myinputfilexy.csv"
            throw new NotImplementedException();
        }



        /// <summary>
        /// Uploads experiment results to Azure Blob Storage.
        /// </summary>
        /// <param name="result">The experiment result to upload.</param>
        /// <returns>An asynchronous task.</returns>
        public async Task UploadExperimentResult(IExperimentResult result)
        {
            // Get the label or name of the experiment
            var experimentLabel = result.ExperimentName;

            switch (experimentLabel)
            {
                case "EncodeIntoArray":

                    // Initialize Azure Blob Service Client and Container Client
                    BlobServiceClient blobServiceClient = new BlobServiceClient(this.config.StorageConnectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("spiralganglionsencodingintoarray");

                    // Retrieve encoded data to be uploaded
                    byte[] excelData = result.excelData;

                    // Generate a unique blob name using current timestamp
                    string blobName = $"encoded_data_{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    // Upload the Excel data to the blob container
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);
                    using (MemoryStream memoryStream = new MemoryStream(excelData))
                    {
                        await blobClient.UploadAsync(memoryStream);
                    }


                    break;

                case "GetBucketIndexPeriodic":
                    BlobServiceClient blobServiceClient1 = new BlobServiceClient(this.config.StorageConnectionString);
                    BlobContainerClient containerClient1 = blobServiceClient1.GetBlobContainerClient("spiralganglionsgetbucketindexperiodic");

                    // Write encoded data to Excel file
                    byte[] excelData1 = result.excelData;

                    // Generate a unique blob name (you can customize this logic)
                    string blobName1 = $"getbucketindexperiodic_{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    // Upload the Excel data to the blob container
                    BlobClient blobClient1 = containerClient1.GetBlobClient(blobName1);
                    using (MemoryStream memoryStream = new MemoryStream(excelData1))
                    {
                        await blobClient1.UploadAsync(memoryStream);
                    }

                    break;
                case "ImagesForGetBucketIndexPeriodic":
                    //List<string> imageFilePaths = result.ImageFilePaths;

                    BlobServiceClient blobServiceClientImages = new BlobServiceClient(this.config.StorageConnectionString);
                    BlobContainerClient containerClientImages = blobServiceClientImages.GetBlobContainerClient("spiralganglionsimagesgetbucketindexperiodic");
                    //List<string> imageFilePaths = result.ImageFilePaths;

                    foreach (string imagePath in result.ImageFilePaths)
                    {
                        // Read the image file into a byte array
                        byte[] imageData;
                        using (FileStream fileStream = File.OpenRead(imagePath))
                        {
                            imageData = new byte[fileStream.Length];
                            fileStream.Read(imageData, 0, imageData.Length);
                        }

                        // Generate a unique blob name for each image
                        string blobNameImages = Path.GetFileName(imagePath);

                        // Upload the image data to the blob container
                        BlobClient blobClientImages = containerClientImages.GetBlobClient(blobNameImages);
                        using (MemoryStream memoryStream = new MemoryStream(imageData))
                        {
                            await blobClientImages.UploadAsync(memoryStream);
                        }
                    }

                    break;

                case "GetBucketIndexNonPeriodic":
                    BlobServiceClient blobServiceClient2 = new BlobServiceClient(this.config.StorageConnectionString);
                    BlobContainerClient containerClient2 = blobServiceClient2.GetBlobContainerClient("spiralganglionsgetbucketindexnonperiodic");

                    // Write encoded data to Excel file
                    byte[] excelData2 = result.excelData;

                    // Generate a unique blob name (you can customize this logic)
                    string blobName2 = $"getbucketindexnonperiodic_{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    // Upload the Excel data to the blob container
                    BlobClient blobClient2 = containerClient2.GetBlobClient(blobName2);
                    using (MemoryStream memoryStream = new MemoryStream(excelData2))
                    {
                        await blobClient2.UploadAsync(memoryStream);
                    }

                    break;


                case "ImagesForGetBucketIndexNonPeriodic":
                    //List<string> imageFilePaths = result.ImageFilePaths;

                    BlobServiceClient blobServiceClientImages2 = new BlobServiceClient(this.config.StorageConnectionString);
                    BlobContainerClient containerClientImages2 = blobServiceClientImages2.GetBlobContainerClient("spiralganglionsimagesgetbucketindexnonperiodic");
                    //List<string> imageFilePaths = result.ImageFilePaths;

                    foreach (string imagePath in result.ImageFilePaths)
                    {
                        // Read the image file into a byte array
                        byte[] imageData;
                        using (FileStream fileStream = File.OpenRead(imagePath))
                        {
                            imageData = new byte[fileStream.Length];
                            fileStream.Read(imageData, 0, imageData.Length);
                        }

                        // Generate a unique blob name for each image
                        string blobNameImages = Path.GetFileName(imagePath);

                        // Upload the image data to the blob container
                        BlobClient blobClientImages = containerClientImages2.GetBlobClient(blobNameImages);
                        using (MemoryStream memoryStream = new MemoryStream(imageData))
                        {
                            await blobClientImages.UploadAsync(memoryStream);
                        }
                    }

                    break;
            }

        }


        public Task<byte[]> UploadResultFile(string fileName, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
