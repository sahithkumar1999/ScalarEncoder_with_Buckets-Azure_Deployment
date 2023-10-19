using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NeoCortexApi.Encoders;
using NeoCortexApi.Entities;
using NeoCortexEntities.NeuroVisualizer;
using Newtonsoft.Json.Linq;
using System.Buffers.Text;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Xml.Linq;
using NeoCortexApi.Utility;
using System.Drawing;
using SkiaSharp;
using static SkiaSharp.SKPath;
using static SkiaSharp.SKImageFilter;
using Org.BouncyCastle.Ocsp;
using static System.Net.Mime.MediaTypeNames;
using Org.BouncyCastle.Crypto;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using LearningFoundation;
using Org.BouncyCastle.Utilities;
using NeoCortex;
using OfficeOpenXml;
using static System.Formats.Asn1.AsnWriter;
using static Azure.Core.HttpHeader;

namespace MyExperiment
{
    /// <summary>
    /// This class implements the ML experiment that will run in the cloud. This is refactored code from my SE project.
    /// </summary>
    public class Experiment : IExperiment
    {
        private IStorageProvider storageProvider;

        private ILogger logger;

        private MyConfig config;

        public Experiment(IConfigurationSection configSection, IStorageProvider storageProvider, ILogger log)
        {
            this.storageProvider = storageProvider;
            this.logger = log;

            config = new MyConfig();
            configSection.Bind(config);
        }
        /// <summary>
        /// Runs an experiment with Scalar Encoder.
        /// It takes various input parameters, including file names, configuration values, and flags. 
        /// Depending on the inputFile parameter's value, it performs different actions, such as encoding data into arrays, 
        /// retrieving bucket indices, or handling image files.The method's primary purpose is to execute specific experiments 
        /// or data processing tasks based on the provided input and configuration
        /// </summary>
        /// <param name="inputFile">Name of the input file or experiment type.</param>
        /// <param name="W">Width of the Scalar Encoder.</param>
        /// <param name="N">Number of buckets or unique values the encoder can represent.</param>
        /// <param name="Radius">Radius parameter (affects bucket selection).</param>
        /// <param name="MinVal">Minimum value for encoding.</param>
        /// <param name="MaxVal">Maximum value for encoding.</param>
        /// <param name="Periodic">Indicates if encoding should be periodic.</param>
        /// <param name="NameID">An identifier for the experiment.</param>
        /// <param name="ClipInput">Indicates if input values should be clipped to the specified range.</param>
        /// <returns>Task representing the experiment result.</returns>
        public Task<IExperimentResult> Run(string inputFile, int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            // TODO read file

            // YOU START HERE WITH YOUR SE EXPERIMENT!!!!

            // Create an experiment result object.
            ExperimentResult res = new ExperimentResult(this.config.GroupId, null);

            // Record the start time of the experiment.
            res.StartTimeUtc = DateTime.UtcNow;

            Dictionary<double, string> encodedData;
            switch (inputFile)
            {
                case "EncodeIntoArray":
                    // Call a method for encoding into an array.
                    encodedData = ScalarEncoder_EncodeIntoArray_PrealloactedBoolArray_EncodesCorrectly1(W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);

                    // Configure Excel package license context.
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Create a list to store encoded data.
                    List<Tuple<int, string>> encodedDataList = new List<Tuple<int, string>>();

                    // Create an experiment result.
                    ExperimentResult result = new ExperimentResult("damir", "0")
                    {
                        Timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                        Accuracy = (float)0.5
                    };
                    foreach (var entry in encodedData)
                    {
                        res.Input = (int)entry.Key;
                        string encodedValue = entry.Value;
                        encodedDataList.Add(Tuple.Create(res.Input, encodedValue));


                    }
                    // Convert the encodedDataList to a formatted string and store it in res.Encoded_Array.
                    res.Encoded_Array = string.Join(", ", encodedDataList.Select(tuple => $"Input: {tuple.Item1}, Encoded Value: {tuple.Item2}"));

                    //Console.WriteLine(res.Encoded_Array);

                    // Now you have encodedDataList with pairs of (input, encodedValue)
                    res.excelData = WriteEncodedDataToExcel(encodedDataList, W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);
                    res.ExperimentName = "EncodeIntoArray";

                    break;

                case "GetBucketIndexPeriodic":
                    // Call a method to get bucket indices for periodic encoding.
                    List<Tuple<decimal, int?>> valueAndBucketIndexList = ScalarEncodingGetBucketIndexPeriodic(W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);

                    // Configure Excel package license context.
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Write data to Excel.
                    res.excelData = WriteDataToExcel_GetBucketIndex(valueAndBucketIndexList, W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);

                    // Set the experiment name.
                    res.ExperimentName = "GetBucketIndexPeriodic";

                    break;

                case "ImagesForGetBucketIndexPeriodic":
                    res.ExperimentName = "ImagesForGetBucketIndexPeriodic";
                    res.ImageFilePaths = ImagesForScalarEncodingGetBucketIndexPeriodic(W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);

                    break;

                case "GetBucketIndexNonPeriodic":
                    List<Tuple<decimal, int?>> valueAndBucketIndexList1 = ScalarEncodingGetBucketIndexNonPeriodic(W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Write data to Excel
                    res.excelData = WriteDataToExcel_GetBucketIndex(valueAndBucketIndexList1, W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);
                    res.ExperimentName = "GetBucketIndexNonPeriodic";

                    break;

                case "ImagesForGetBucketIndexNonPeriodic":
                    res.ExperimentName = "ImagesForGetBucketIndexNonPeriodic";
                    res.ImageFilePaths = ImagesForScalarEncodingGetBucketIndexNonPeriodic(W, N, Radius, MinVal, MaxVal, Periodic, NameID, ClipInput);

                    break;

            }

            // Run your experiment code here.

            return Task.FromResult<IExperimentResult>(res); // TODO...

        }

        /// <summary>
        /// This method writes a list of value and bucket index pairs to an Excel file.
        /// </summary>
        /// <param name="valueAndBucketIndexList">A list of tuples containing decimal values and their corresponding bucket indexes.</param>
        /// <returns>A byte array representing the Excel file content.</returns>
        public byte[] WriteDataToExcel_GetBucketIndex(List<Tuple<decimal, int?>> valueAndBucketIndexList, int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Add headers to the Excel worksheet
                worksheet.Cells[1, 1].Value = "W";
                worksheet.Cells[1, 2].Value = "N";
                worksheet.Cells[1, 3].Value = "Radius";
                worksheet.Cells[1, 4].Value = "MinVal";
                worksheet.Cells[1, 5].Value = "MaxVal";
                worksheet.Cells[1, 6].Value = "Periodic";
                worksheet.Cells[1, 7].Value = "NameID";
                worksheet.Cells[1, 8].Value = "ClipInput";
                worksheet.Cells[1, 9].Value = "Value";
                worksheet.Cells[1, 10].Value = "BucketIndex";

                // Fill data in the Excel worksheet
                for (int i = 0; i < valueAndBucketIndexList.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = W;
                    worksheet.Cells[i + 2, 2].Value = N;
                    worksheet.Cells[i + 2, 3].Value = Radius;
                    worksheet.Cells[i + 2, 4].Value = MinVal;
                    worksheet.Cells[i + 2, 5].Value = MaxVal;
                    worksheet.Cells[i + 2, 6].Value = Periodic;
                    worksheet.Cells[i + 2, 7].Value = NameID;
                    worksheet.Cells[i + 2, 8].Value = ClipInput;
                    worksheet.Cells[i + 2, 9].Value = valueAndBucketIndexList[i].Item1;
                    worksheet.Cells[i + 2, 10].Value = valueAndBucketIndexList[i].Item2;
                }

                // Auto fit columns to adjust column widths based on content
                worksheet.Cells.AutoFitColumns();

                // Save the Excel package to a memory stream
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    // Convert the saved Excel data in the memory stream to a byte array and return it
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// This method writes a list of value and bucket index pairs to an Excel file.
        /// </summary>
        /// <param name="valueAndBucketIndexList">A list of tuples containing decimal values and their corresponding bucket indexes.</param>
        /// <returns>A byte array representing the Excel file content.</returns>
        public byte[] WriteEncodedDataToExcel(List<Tuple<int, string>> encodedDataList, int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            using (var package = new ExcelPackage())
            {
                // Add a worksheet named "Encoded Data"
                var worksheet = package.Workbook.Worksheets.Add("Encoded Data");

                // Add headers to the first row of the worksheet
                worksheet.Cells[1, 1].Value = "W";
                worksheet.Cells[1, 2].Value = "N";
                worksheet.Cells[1, 3].Value = "Radius";
                worksheet.Cells[1, 4].Value = "MinVal";
                worksheet.Cells[1, 5].Value = "MaxVal";
                worksheet.Cells[1, 6].Value = "Periodic";
                worksheet.Cells[1, 7].Value = "NameID";
                worksheet.Cells[1, 8].Value = "ClipInput";
                worksheet.Cells[1, 9].Value = "Input";
                worksheet.Cells[1, 10].Value = "Encoded Value";

                // Fill data
                for (int i = 0; i < encodedDataList.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = W;
                    worksheet.Cells[i + 2, 2].Value = N;
                    worksheet.Cells[i + 2, 3].Value = Radius;
                    worksheet.Cells[i + 2, 4].Value = MinVal;
                    worksheet.Cells[i + 2, 5].Value = MaxVal;
                    worksheet.Cells[i + 2, 6].Value = Periodic;
                    worksheet.Cells[i + 2, 7].Value = NameID;
                    worksheet.Cells[i + 2, 8].Value = ClipInput;
                    worksheet.Cells[i + 2, 9].Value = encodedDataList[i].Item1;
                    worksheet.Cells[i + 2, 10].Value = encodedDataList[i].Item2;
                }

                // Automatically adjust column widths to fit content
                worksheet.Cells.AutoFitColumns();

                // Save the Excel package to a memory stream
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Continuously listens for messages in an Azure Storage Queue and processes them.
        /// </summary>
        /// <param name="cancelToken">A cancellation token to stop the listener.</param>
        /// <returns>An asynchronous task representing the listener operation.</returns>
        public async Task RunQueueListener(CancellationToken cancelToken)
        {
            // Create a connection to the Azure Storage Queue

            QueueClient queueClient = new QueueClient(this.config.StorageConnectionString, this.config.Queue);

            // Continuously listen for messages in the queue until cancellation is requested
            while (cancelToken.IsCancellationRequested == false)
            {
                // Receive a message from the queue, if available
                QueueMessage message = await queueClient.ReceiveMessageAsync();

                if (message != null)
                {
                    try
                    {
                        // Decode the message content from the queue
                        string msgTxt = Encoding.UTF8.GetString(message.Body.ToArray());

                        // Log that a message has been received
                        this.logger?.LogInformation($"Received the message {msgTxt}");

                        // Deserialize the message into an ExperimentRequestMessage
                        ExerimentRequestMessage request = JsonSerializer.Deserialize<ExerimentRequestMessage>(msgTxt);

                        // Extract parameters from the received message
                        var inputFile = request.InputFile;
                        var W = request.W;
                        var N = request.N;
                        var Radius = request.Radius;
                        var MinVal = request.MinVal;
                        var MaxVal = request.MaxVal;
                        var Periodic = request.Periodic;
                        var NameID = request.NameID;
                        var ClipInput = request.ClipInput;

                        // Start the Scalar Encoder (SE) project code
                        // This is where your SE project logic begins (between steps 4 and 5).
                        IExperimentResult result = await this.Run(inputFile, W, (int)N, (double)Radius, (double)MinVal, (double)MaxVal, (bool)Periodic, (string)NameID, (bool)ClipInput);

                        // Serialize and upload the experiment result (Step 5)                     
                        await storageProvider.UploadExperimentResult(result);

                        // Delete the processed message from the queue
                        await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur during processing
                        this.logger?.LogError(ex, "TODO...");
                    }
                }
                else
                {
                    // If the queue is empty, wait for a short duration before checking again
                    await Task.Delay(500);
                    logger?.LogTrace("Queue empty...");
                }
            }

            // Log a message when the cancellation token is triggered
            this.logger?.LogInformation("Cancel pressed. Exiting the listener loop.");
        }


        #region Private Methods
        /// <summary>
        /// Encodes a range of scalar values into a dictionary of binary representations using a scalar encoder.
        /// </summary>
        /// <param name="W">Width of the scalar encoder.</param>
        /// <param name="N">Number of bits in the encoded representation.</param>
        /// <param name="Radius">Radius for value normalization.</param>
        /// <param name="MinVal">Minimum input value.</param>
        /// <param name="MaxVal">Maximum input value.</param>
        /// <param name="Periodic">Indicates whether encoding is periodic.</param>
        /// <param name="NameID">A unique name or identifier for this encoding.</param>
        /// <param name="ClipInput">Indicates whether to clip input values to the specified range.</param>
        /// <returns>A dictionary mapping input values to their binary representations.</returns>
        public Dictionary<double, string> ScalarEncoder_EncodeIntoArray_PrealloactedBoolArray_EncodesCorrectly1(int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {

            NewScalarEncoder encoder = new NewScalarEncoder(new Dictionary<string, object>()
            {
                { "W", W},
                { "N", N},
                { "Radius", Radius},
                { "MinVal", MinVal},
                { "MaxVal", MaxVal },
                { "Periodic", Periodic},
                { "Name", NameID},
                { "ClipInput", ClipInput},
            });
            // Create a dictionary to store the data
            Dictionary<double, string> data = new Dictionary<double, string>();

            // Act & Assert
            for (double input = 0; input <= MaxVal; input += 1)
            {
                bool[] encodedArray = new bool[N];

                // Encode the input value into the pre-allocated array
                encoder.EncodeIntoArray(input, encodedArray);

                data[input] = string.Join("", encodedArray.Select(b => b ? "1" : "0"));
            }
            return data;
        }

        /// <summary>
        /// Computes bucket indices for periodic scalar encoding.
        /// </summary>
        /// <param name="W">The width of the encoder.</param>
        /// <param name="N">The number of bits in the encoder.</param>
        /// <param name="Radius">The radius parameter for encoding.</param>
        /// <param name="MinVal">The minimum value for encoding.</param>
        /// <param name="MaxVal">The maximum value for encoding.</param>
        /// <param name="Periodic">A flag indicating periodic encoding.</param>
        /// <param name="NameID">A unique identifier for the encoding.</param>
        /// <param name="ClipInput">A flag to clip input values within the specified range.</param>
        /// <returns>A list of tuples containing decimal values and their corresponding bucket indices.</returns>
        public List<Tuple<decimal, int?>> ScalarEncodingGetBucketIndexPeriodic(int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            string outFolder = nameof(ScalarEncodingGetBucketIndexPeriodic);

            Directory.CreateDirectory(outFolder);

            DateTime now = DateTime.Now;
            List<Tuple<decimal, int?>> valueAndBucketIndexList = new List<Tuple<decimal, int?>>();

            NewScalarEncoder encoder = new NewScalarEncoder(new Dictionary<string, object>()
            {
                { "W", W},
                { "N", N},
                { "Radius", Radius},
                { "MinVal", MinVal},
                { "MaxVal", MaxVal },
                { "Periodic", Periodic},
                { "Name", NameID},
                { "ClipInput", ClipInput},
            });

            // Loop through a range of decimal values and encode each value to a bitmap.
            for (decimal i = 0.0M; i < (long)encoder.MaxVal; i += 0.1M)
            {
                // Encode the value using ScalarEncoder.
                var result = encoder.Encode(i);

                // Get the corresponding bucket index for the value.
                int? bucketIndex = encoder.GetBucketIndex(i);

                // Convert the result into a 2D array and transpose it.
                int[,] twoDimenArray = ArrayUtils.Make2DArray<int>(result, (int)Math.Sqrt(result.Length), (int)Math.Sqrt(result.Length));
                var twoDimArray = ArrayUtils.Transpose(twoDimenArray);

                // Store the values in the list
                valueAndBucketIndexList.Add(Tuple.Create(i, bucketIndex));

            }

            return valueAndBucketIndexList;
        }


        /// <summary>
        /// Generates and saves bitmap images for scalar values encoded using ScalarEncoder with periodic encoding.
        /// </summary>
        /// <param name="W">Width parameter for the encoder.</param>
        /// <param name="N">Number of bits for encoding.</param>
        /// <param name="Radius">Radius parameter for encoding.</param>
        /// <param name="MinVal">Minimum scalar value to encode.</param>
        /// <param name="MaxVal">Maximum scalar value to encode.</param>
        /// <param name="Periodic">Flag indicating periodic encoding.</param>
        /// <param name="NameID">Identifier for the encoding configuration.</param>
        /// <param name="ClipInput">Flag indicating whether input values should be clipped.</param>
        /// <returns>A list of file paths to the generated bitmap images.</returns>
        public List<string> ImagesForScalarEncodingGetBucketIndexPeriodic(int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            string outFolder = nameof(ScalarEncodingGetBucketIndexPeriodic);

            Directory.CreateDirectory(outFolder);

            DateTime now = DateTime.Now;

            NewScalarEncoder encoder = new NewScalarEncoder(new Dictionary<string, object>()
            {
                { "W", W},
                { "N", N},
                { "Radius", Radius},
                { "MinVal", MinVal},
                { "MaxVal", MaxVal },
                { "Periodic", Periodic},
                { "Name", NameID},
                { "ClipInput", ClipInput},
            });
            List<string> imageFilePaths = new List<string>();
            // Loop through a range of decimal values and encode each value to a bitmap.
            for (decimal i = 0.0M; i < (long)encoder.MaxVal; i += 0.1M)
            {
                // Encode the value using ScalarEncoder.
                var result = encoder.Encode(i);

                // Get the corresponding bucket index for the value.
                int? bucketIndex = encoder.GetBucketIndex(i);


                // Convert the result into a 2D array and transpose it.
                int[,] twoDimenArray = ArrayUtils.Make2DArray<int>(result, (int)Math.Sqrt(result.Length), (int)Math.Sqrt(result.Length));
                var twoDimArray = ArrayUtils.Transpose(twoDimenArray);


                // Save the generated bitmap to the output folder with the corresponding text.
                NeoCortexUtils.DrawBitmap(twoDimArray, 1024, 1024, $"{outFolder}\\{i}.png", Color.Gray, Color.Green, text: $"v:{i}  /b:{bucketIndex}");
                string imagePath = $"{outFolder}\\{i}.png";
                imageFilePaths.Add(imagePath);
            }

            return imageFilePaths;
        }


        /// <summary>
        /// Retrieves bucket indices for non-periodic scalar encoding based on the provided configuration parameters.
        /// </summary>
        /// <param name="W">Width of the encoding</param>
        /// <param name="N">Number of active bits in the encoding</param>
        /// <param name="Radius">Radius value for encoding</param>
        /// <param name="MinVal">Minimum value for encoding</param>
        /// <param name="MaxVal">Maximum value for encoding</param>
        /// <param name="Periodic">Indicates whether encoding is periodic</param>
        /// <param name="NameID">Name or identifier for the encoding</param>
        /// <param name="ClipInput">Specifies whether to clip input values within the encoding range</param>
        /// <returns>A list of tuples containing decimal values and their corresponding bucket indices</returns>
        public List<Tuple<decimal, int?>> ScalarEncodingGetBucketIndexNonPeriodic(int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            // Create a directory to save the bitmap output.
            string outFolder = nameof(ScalarEncodingGetBucketIndexNonPeriodic);

            Directory.CreateDirectory(outFolder);

            DateTime now = DateTime.Now;
            List<Tuple<decimal, int?>> valueAndBucketIndexList1 = new List<Tuple<decimal, int?>>();
            // Create a new ScalarEncoder with the given configuration.
            NewScalarEncoder encoder = new NewScalarEncoder(new Dictionary<string, object>()
            {
                { "W", W},
                { "N", N},
                { "Radius", Radius},
                { "MinVal", MinVal},
                { "MaxVal", MaxVal },
                { "Periodic", Periodic},
                { "Name", NameID},
                { "ClipInput", ClipInput},
            });

            // Iterate through a range of numbers and encode them using the ScalarEncoder.
            for (decimal i = 0.0M; i < (long)encoder.MaxVal; i += 0.1M)
            {
                // Encode the number and get the corresponding bucket index.
                var result = encoder.Encode(i);

                int? bucketIndex = encoder.GetBucketIndex(i);
                //Console.WriteLine($"S: {bucketIndex}");


                // Convert the encoded result into a 2D array and transpose it.
                int[,] twoDimenArray = ArrayUtils.Make2DArray<int>(result, (int)Math.Sqrt(result.Length), (int)Math.Sqrt(result.Length));
                var twoDimArray = ArrayUtils.Transpose(twoDimenArray);


                // Store the values in the list
                valueAndBucketIndexList1.Add(Tuple.Create(i, bucketIndex));
            }
            return valueAndBucketIndexList1;
        }


        /// <summary>
        /// Generates images depicting the encoding and bucket index for non-periodic scalar data using a ScalarEncoder.
        /// </summary>
        /// <param name="W">The number of bits in each bucket.</param>
        /// <param name="N">The total number of buckets.</param>
        /// <param name="Radius">The encoding radius for scalar values.</param>
        /// <param name="MinVal">The minimum scalar value to encode.</param>
        /// <param name="MaxVal">The maximum scalar value to encode.</param>
        /// <param name="Periodic">Specifies if the encoding is periodic (circular) or not.</param>
        /// <param name="NameID">A name or identifier for this encoding experiment.</param>
        /// <param name="ClipInput">Indicates whether input values should be clipped within the specified range.</param>
        /// <returns>A list of file paths to the generated image files.</returns>
        public List<string> ImagesForScalarEncodingGetBucketIndexNonPeriodic(int W, int N, double Radius, double MinVal, double MaxVal, bool Periodic, string NameID, bool ClipInput)
        {
            string outFolder = nameof(ScalarEncodingGetBucketIndexNonPeriodic);

            Directory.CreateDirectory(outFolder);

            DateTime now = DateTime.Now;
            List<string> imageFilePaths = new List<string>();

            // Create a new ScalarEncoder with the given configuration.
            NewScalarEncoder encoder = new NewScalarEncoder(new Dictionary<string, object>()
            {
                { "W", W},
                { "N", N},
                { "Radius", Radius},
                { "MinVal", MinVal},
                { "MaxVal", MaxVal },
                { "Periodic", Periodic},
                { "Name", NameID},
                { "ClipInput", ClipInput},
            });

            // Iterate through a range of numbers and encode them using the ScalarEncoder.
            for (decimal i = 0.0M; i < (long)encoder.MaxVal; i += 0.1M)
            {
                // Encode the number and get the corresponding bucket index.
                var result = encoder.Encode(i);

                int? bucketIndex = encoder.GetBucketIndex(i);


                // Convert the encoded result into a 2D array and transpose it.
                int[,] twoDimenArray = ArrayUtils.Make2DArray<int>(result, (int)Math.Sqrt(result.Length), (int)Math.Sqrt(result.Length));
                var twoDimArray = ArrayUtils.Transpose(twoDimenArray);

                //Draw a bitmap of the encoded result with the corresponding bucket index and save it to the output folder.
                NeoCortexUtils.DrawBitmap(twoDimArray, 1024, 1024, $"{outFolder}\\{i}.png", Color.Gray, Color.Green, text: $"v:{i} /b:{bucketIndex}");


                string imagePath = $"{outFolder}\\{i}.png";
                imageFilePaths.Add(imagePath);
            }
            return imageFilePaths;
        }


        public Task<IExperimentResult> Run(string inputFile)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
