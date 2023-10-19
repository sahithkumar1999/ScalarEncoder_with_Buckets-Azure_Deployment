# ML22/23-4 Scalar Encoder with Buckets-Team SpiralGanglions - Azure Cloud Implementation

## Table of Contents

1. [Introduction for SDR](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/edit/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#1-introduction-for-sdr)
2. [Description (Scalarencoder Connect with SDR)](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#2-description-scalarencoder-connect-with-sdr)
3. [Software Engineering Project](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#3-software-engineering-project)
4. [Cloud Project](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#4-cloud-project)
5. [Overview](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#5-overview)
6. [Methods](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#6-methods)
7. [Private Methods](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#7-private-methods)
8. [Observations](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#8-observations)
9. [Execute the experiment](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#9-execute-the-experiment)
10. [References](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/Documentation/Experiment%20Specification%20-%20Team_SpiralGanglions.md#11-references)



## 1. Introduction for SDR

Sparse dispersed Representation (SDR) is a neurobiologically inspired approach of expressing data in binary format, with the primary purpose of effectively storing information in a dispersed and sparse manner. SDR is frequently linked to artificial intelligence and the science of neural networks, notably in areas like memory, pattern recognition, and sensory processing. The cochlea, for instance, is a specialized device that converts the amplitudes and frequencies of outside sounds into a small group of active neurons. The inner hair cells in a row that respond to various frequencies make up the fundamental mechanism for this process [Fig 1].The hair cells stimulate the neurons, which then convey the signal to the brain when a certain frequency of sound is detected. In this way, a group of neurons are triggered to create a sparse distributed representation of the sound. 

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/145749c4-afd9-48ca-aeec-6474aa24580b)


Fig 1 Cochlear hair cells excite a group of neurons based on the frequency of the sound.

Data in SDR is represented using binary values, which are commonly 0s and 1s. SDR is different from typical binary representations in that it is sparse, meaning that only a tiny portion of the bits are active (set to 1) while the remainder are inactive (set to 0). SDRs have a large number of bits—2,000 on average—but only a tiny percentage, about 2%, are actually used[Fig 2]. Contrary to conventional representations, where individual bits lack value, each bit has a semantic meaning.

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/f5290e7b-ca5d-42ca-8d7d-9b36a5a7d5a0)

Distributed Encoding: SDR makes use of distributed representations, in which each bit of data is dispersed among numerous bits rather than being stored in a single unit as in one-hot encoding. As a result, various combinations of active and inactive bits can represent various types of information. Sparse distributed representations are used to create sequence memory, which is similar to how the brain's dendrites and synapses function during sequence learning [Fig.3]. 

Sparsity: The SDR's sparsity is one of its fundamental characteristics. Only a small portion of the bits in a typical SDR are active, while the bulk are idle. This sparsity is crucial because it facilitates effective generalization, computing, and storage [Fig.4].

Overlap: By having overlapping groups of active bits, SDRs can express ideas that are comparable or connected. Due to the fact that common characteristics across many data points may be identified in the overlap of active bits, this trait enables robust pattern identification and similarity detection. By modeling the anatomy of the brain, sequence memory may be produced[Fig 5]. According to the presentation, the dendritic segments in the brain serve as coincidence detectors, enabling cells to foresee and communicate with other active cells.
![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/4eab45d7-e038-482f-934d-314885b5e8f6)

The Sparse Distributed Representation (SDR) uses "buckets" to describe a series of "on" bit arrays. These buckets are employed to represent particular scalar value ranges.

## 2. Description (Scalarencoder Connect with SDR):

In HTM (Hierarchical Temporal Memory) systems, buckets play a crucial role in scalar encoding by discretizing scalar values into distinct sets of "on" bits within Sparse Distributed Representations (SDRs). The size of a bucket is determined by the consecutive activation of bits in the SDR, with larger buckets offering coarser granularity and smaller SDRs, while smaller buckets provide finer granularity with larger SDRs. Some scalar encoders, like the Random Distributed Scalar Encoder (RDS), dynamically create buckets for new scalar data, adapting to different data ranges without fixed parameters for minimum and maximum values. Semantic similarity between encoded values is assessed by the overlap of associated buckets, with values sharing more "on" bits considered semantically similar, while those with minimal overlap are considered distinct. Various parameters, including bucket size, affect semantic similarity, with smaller buckets resulting in higher similarity for closely spaced values and larger buckets yielding lower similarity for values farther apart.

In cloud-based solutions, scalar encoding is applied to efficiently process large datasets. Data ingestion initiates the process, with cloud-based services distributing encoding tasks across multiple nodes or containers for parallel processing. This approach accelerates encoding for massive datasets and offers scalability and elasticity by automatically allocating or deallocating computing resources based on workload fluctuations. Performance optimization, utilizing efficient data structures and algorithms, is essential, and often achieved through optimized libraries and frameworks. Overall, encoding input data into SDRs in the cloud involves ingestion, parallel encoding, performance optimization, fault tolerance, storage management, and event-driven processing. Leveraging cloud scalability and elasticity, these solutions accommodate diverse data sources and support various applications, from sensor data processing to natural language understanding and recommendation systems

## 3. Software Engineering Project

Software Engineering project [Repository](https://github.com/sahithkumar1999/neocortexapi_Team_SpiralGanglion/tree/test-cases)



### 3.1 Program Links:
  Link for [Scalar Encoder](https://github.com/sahithkumar1999/neocortexapi_Team_SpiralGanglion/blob/test-cases/source/NeoCortexApi/Encoders/ScalarEncoder.cs)
  
  Link for [EncoderBaseClass](https://github.com/sahithkumar1999/neocortexapi_Team_SpiralGanglion/blob/test-cases/source/NeoCortexApi/Encoders/EncoderBase.cs )
  
  Link for [Test Cases]( https://github.com/sahithkumar1999/neocortexapi_Team_SpiralGanglion/blob/test-cases/source/UnitTestsProject/EncoderTests/ScalarEncoderTests.cs)

## 4. Cloud Project

Program links:

[MyCloudProject](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/tree/Team_SpiralGanglions/Source/MyCloudProjectSample).

[Program.cs](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/MyCloudProject/Program.cs).

[Experiment.cs](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/MyExperiment/Experiment.cs).

[AzureStorageProvider](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/Team_SpiralGanglions/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs).

[Run](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L79)

[WriteDataToExcel_GetBucketIndex](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L179C23-L179C54)

[WriteEncodedDataToExcel](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L230C23-L230C46)

[RunQueueListener](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L281)

[ScalarEncoder_EncodeIntoArray_PrealloactedBoolArray_EncodesCorrectly1](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L359)

[ScalarEncodingGetBucketIndexPeriodic](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L401)

[ImagesForScalarEncodingGetBucketIndexPeriodic](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L456)

[ScalarEncodingGetBucketIndexNonPeriodic](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L513)

[ImagesForScalarEncodingGetBucketIndexNonPeriodic](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L569)


## 5. Overview
The Experiment class implements an ML experiment that runs in the cloud. It includes methods for encoding data, processing it, and generating reports. Additionally, it provides functionality for listening to an Azure Storage Queue for experiment requests and uploading results to storage.

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/612b4221-0336-4090-8fae-9a2817d68f60)


## 6. Methods
### 6.1 Run
The Run method is responsible for executing the ML experiment based on the input file provided as a parameter. It supports various experiment scenarios based on the input file's name. The method returns an IExperimentResult.[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L79)

### 6.2 RunQueueListener
The RunQueueListener method is designed to listen for experiment requests from an Azure Storage Queue. It continuously checks for messages in the queue, processes them, and uploads the experiment results back to storage. It runs until a cancellation is requested.[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L281)

### 6.3 WriteDataToExcel_GetBucketIndex
This method generates an Excel report for the results of the ScalarEncodingGetBucketIndex-Periodic and ScalarEncodingGetBucketIndexNonPeriodic experiments. It takes a list of tuples containing value and bucket index pairs and returns the report as a byte array.[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L179)

### 6.4 WriteEncodedDataToExcel
This method generates an Excel report for the results of the ScalarEncoder_EncodeIntoArray_PrealloactedBoolArray_EncodesCorrectly1 experiment. It takes a list of tuples containing input values and their encoded values and returns the report as a byte array.[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L230)


## 7. Private Methods:

The following private methods are used within the class for specific tasks and experiments:

### 7.1. EncodeIntoArray:

It initializes variables related to data encoding, sets a license context for using the EPPlus li-brary, creates a list of encoded data tuples, and constructs an ExperimentResult object with specific attributes. It then iterates over the encoded data, populates the list, and converts it into a formatted string. Finally, it calls a function to write the encoded data to an Excel file and sets the ExperimentName to "EncodeIntoArray."[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L359)

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/6c4787a0-fedf-463a-9473-ac33945fb8a8)


#### 7.1.1 Code:
```csharp
case "EncodeIntoArray":
    double minValue = 0;
    double maxValue = 100;
    int numBits = 1024;
    double period = maxValue - minValue;
    encodedData = ScalarEncod-er_EncodeIntoArray_PrealloactedBoolArray_EncodesCorrectly1(minValue, maxValue, num-Bits, period);
    int index = 0; 
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    List<Tuple<int, string>> encodedDataList = new List<Tuple<int, string>>();
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

        
    res.Encoded_Array = string.Join(", ", encodedDataList.Select(tuple => $"Input: {tuple.Item1}, Encoded Value: {tuple.Item2}"));

    Console.WriteLine(res.Encoded_Array);
    res.excelData = WriteEncodedDataToExcel(encodedDataList);
    res.ExperimentName = "EncodeIntoArray";
```

#### 7.1.2 AzureProvider:
[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs#L55)
```csharp
case "EncodeIntoArray":

     BlobServiceClient blobServiceClient = new BlobServiceCli-ent(this.config.StorageConnectionString);
     BlobContainerClient containerClient = blobServiceCli-ent.GetBlobContainerClient("encodingintoarray");

     // Write encoded data to Excel file
     byte[] excelData = result.excelData;

     // Generate a unique blob name (you can customize this logic)
     string blobName = $"encod-ed_data_{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.xlsx";

     // Upload the Excel data to the blob container
     BlobClient blobClient = containerClient.GetBlobClient(blobName);
     using (MemoryStream memoryStream = new MemoryStream(excelData))
     {
         await blobClient.UploadAsync(memoryStream);
     }

```
#### 7.1.3 TriggerQueueMessage:

```csharp
{
  "ExperimentId": "2023",
  "InputFile": "EncodeIntoArray",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": false,
  "ClipInput": false
}

```
#### 7.1.4 Output:
![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/44e61dd8-ff48-42a3-a1a1-65c4f4f2e7fa)


### 7.2. ScalarEncodingGetBucketIndexPeriodic:

The code obtains a list of tuples containing decimal values and corresponding bucket indices through the function ScalarEncodingGetBucketIndexPeriodic(). It then sets the license context for EPPlus, writes the data to an Excel file using a function called WriteDataToExcel_GetBucketIndex, and assigns the ExperimentName as "GetBucketIndexPeriodic."[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L401)


![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/43af9f89-ee34-4b17-856d-3dcf1cca9ffb)


#### 7.2.1 Code:
```csharp
case "GetBucketIndexPeriodic":
    List<Tuple<decimal, int?>> valueAndBucketIndexList = ScalarEncodingGetBucketIndex-Periodic ();
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    res.excelData = WriteDataToExcel_GetBucketIndex(valueAndBucketIndexList);
    res.ExperimentName = "GetBucketIndexPeriodic";
```

#### 7.2.2 AzureProvider:
[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs#L77)
```csharp
case "GetBucketIndexPeriodic":
    BlobServiceClient blobServiceClient1 = new BlobServiceClient(this.config.StorageConnectionString);
    BlobContainerClient containerClient1 = blobServiceClient1.GetBlobContainerClient("getbucketindexperiodic");

    // Write encoded data to Excel file
    byte[] excelData1 = result.excelData;

    // Generate a unique blob name (you can customize this logic)
    string blobName1 = $"getbucketindexnonperiodic_{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.xlsx";

    // Upload the Excel data to the blob container
    BlobClient blobClient1 = containerClient1.GetBlobClient(blobName1);
    using (MemoryStream memoryStream = new MemoryStream(excelData1))
    {
        await blobClient1.UploadAsync(memoryStream);
    }
```

#### 7.2.3 TriggerQueueMessage:

```csharp
{
  "ExperimentId": "2023",
  "InputFile": "GetBucketIndexPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": true,
  "ClipInput": false
}

```

#### 7.2.4 Output:
![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/bd39bcf4-da38-45fa-b3bc-baecdec2d111)


### 7.3. ImagesForScalarEncodingGetBucketIndexPeriodic:

The code sets the ExperimentName to "ImagesForGetBucketIndexPeriodic" and assigns a list of image file paths obtained from the function ImagesForScalarEncodingGetBucketIndexPeriodic() to the ImageFilePaths property of the 'res' object.[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L456)

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/9892d8f7-498d-4fd9-a895-b5ee5c58f77c)

#### 7.3.1 Code:
```csharp
case "ImagesForGetBucketIndexPeriodic":
     res.ExperimentName = "ImagesForGetBucketIndexPeriodic";
     res.ImageFilePaths = ImagesForScalarEncodingGetBucketIndexPeriodic();
```

#### 7.3.2 AzureProvider:
[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs#L95)
```csharp
case "ImagesForGetBucketIndexPeriodic":
    //List<string> imageFilePaths = result.ImageFilePaths;

    BlobServiceClient blobServiceClientImages = new BlobServiceClient(this.config.StorageConnectionString);
    BlobContainerClient containerClientImages = blobServiceClientImages.GetBlobContainerClient("imagesgetbucketindexperiodic");
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

```
#### 7.3.3 TriggerQueueMessage:

```csharp
{
  "ExperimentId": "2023",
  "InputFile": "ImagesForGetBucketIndexPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": true,
  "ClipInput": false
}
```
#### 7.3.4 Output:
![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/0141bc82-ee95-48a4-84c8-b62a51317866)


### 7.4. ScalarEncodingGetBucketIndexNonPeriodic:

The code retrieves a list of tuples containing decimal values and corresponding bucket indices using the function ScalarEncodingGetBucketIndexNonPeriodic(). It then sets the license context for EPPlus, writes the data to an Excel file using the function WriteDataToExcel_GetBucketIndex, and assigns the ExperimentName as "GetBucketIndexNonPeriodic." [Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L513)

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/d2ab79a1-1303-4e52-88ae-7a1021b5a4c6)


#### 7.4.1 Code:
```csharp
case "GetBucketIndexNonPeriodic":
    List<Tuple<decimal, int?>> valueAndBucketIndexList1 = ScalarEncodingGetBucketIn-dexNonPeriodic();
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    
    res.excelData = WriteDataToExcel_GetBucketIndex(valueAndBucketIndexList1);
    res.ExperimentName = "GetBucketIndexNonPeriodic";
```

#### 7.4.2 AzureProvider:
[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs#L125)
```csharp
case "GetBucketIndexNonPeriodic":
    BlobServiceClient blobServiceClient2 = new BlobServiceClient(this.config.StorageConnectionString);
    BlobContainerClient containerClient2 = blobServiceClient2.GetBlobContainerClient("getbucketindexnonperiodic");

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
```

#### 7.4.3 TriggerQueueMessage:

```csharp
{
  "ExperimentId": "2023",
  "InputFile": "GetBucketIndexNonPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": false,
  "ClipInput": false
}
```

#### 7.4.4 Output:
![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/4145ad4a-b246-4035-8824-a4bf40e5610b)


### 7.5. ImagesForScalarEncodingGetBucketIndexNonPeriodic:

The code sets the ExperimentName to "ImagesForGetBucketIndexNonPeriodic" and assigns a list of image file paths obtained from the function ImagesForScalarEncodingGetBucketIndexNonPeriodic() to the ImageFilePaths property of the 'res' object.[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/Experiment.cs#L569)

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/488bb9a2-b3bf-46da-b88c-79cf24efa415)

#### 7.5.1 Code:
```csharp
case "ImagesForGetBucketIndexNonPeriodic":
    res.ExperimentName = "ImagesForGetBucketIndexNonPeriodic";
    res.ImageFilePaths = ImagesForScalarEncodingGetBucketIndexNonPeriodic();

```

#### 7.5.2 AzureProvider:
[Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/cb0b2f0fd9805cac5cdfc81871f9111cc0cc0999/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs#L145)
```csharp
 case "ImagesForGetBucketIndexNonPeriodic":
     //List<string> imageFilePaths = result.ImageFilePaths;

     BlobServiceClient blobServiceClientImages2 = new BlobServiceClient(this.config.StorageConnectionString);
     BlobContainerClient containerClientImages2 = blobServiceClientImages2.GetBlobContainerClient("imagesgetbucketindexnonperiodic");
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
```

#### 7.5.3 TriggerQueuemessage

```csharp
ImagesForGetBucketIndexNonPeriodic
{
  "ExperimentId": "2023",
  "InputFile": "ImagesForGetBucketIndexNonPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": false,
  "ClipInput": false
}
```

#### 7.5.4 Output:
![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/64829519/b38b0b9e-5637-41e4-b14f-09696aa66a5d)


## 8. Observations:

### 8.1. Changes in W and N Fields:

TriggerMessage Case "A" and "B":

| Field         | Value         | Value         |
|---------------|---------------|---------------|
| ExperimentId  | 2023          | 2023          |
| InputFile     | GetBucketIndexPeriodic | GetBucketIndexPeriodic |
| Name          | sample        | sample        |
| NameID        | scalar        | scalar        |
| W             | 21            | 31            |
| N             | 1024          | 512           |
| Radius        | -1.0          | -1.0          |
| MinVal        | 0.0           | 0.0           |
| MaxVal        | 100.0         | 100.0         |
| Periodic      | true          | true          |
| ClipInput     | false         | false         |

#### 8.1.1 Data Case for Case "A":

| Value | BucketIndex | Value | BucketIndex | Value | BucketIndex | Value | BucketIndex |
|-------|-------------|-------|-------------|-------|-------------|-------|-------------|
| 0     | 0           | 0.1   | 1           | 0.2   | 2           | 0.3   | 3           |
| 0.4   | 4           | 0.5   | 5           | 0.6   | 6           | 0.7   | 7           |
| 0.8   | 8           | 0.9   | 9           | 1     | 10          | 1.1   | 11          |
| 1.2   | 12          | 1.3   | 13          | 1.4   | 14          | 1.5   | 15          |
| 1.6   | 16          | 1.7   | 17          | 1.8   | 18          | 1.9   | 19          |
| 2     | 20          | 2.1   | 21          | 2.2   | 22          | 2.3   | 23          |
| 2.4   | 24          | 2.5   | 25          | 2.6   | 26          | 2.7   | 27          |
| 2.8   | 28          | 2.9   | 29          |


#### 8.1.2 Results:

Images of Case "A":

<table>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/5f57a9c0-f59f-4850-90a8-6c6adf1844af" alt="Image 1"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/8ba3e030-75a0-4fad-8c50-192283484fb1" alt="Image 2"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/ce53fe7f-72e9-49e4-ba0e-83d507874044" alt="Image 3"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/3cc7c404-e78f-49be-9dae-f6de0228b13f" alt="Image 4"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/85072c30-07a5-40b9-a0a4-1d14add88ad3" alt="Image 5"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/18e0b8e5-ce0b-49cc-a081-070fd83a6465" alt="Image 6"></td>
  </tr>
</table>

#### 8.1.3 Summary Case "A" (W = 21, N = 1024):

In this case, you have a smaller "W" value (21) compared to the total number of buckets ("N" = 1024).
The input values are continuously mapped to bucket indices with each bucket representing a range of values.
As you can see, each increment of 0.1 in the input value corresponds to an increment of 1 in the bucket index. This means that the buckets are finely divided, and each bucket represents a narrow range of values.

#### 8.1.4 Excel Data Case "B":

| Value | BucketIndex | Value | BucketIndex | Value | BucketIndex | Value | BucketIndex |
|-------|-------------|-------|-------------|-------|-------------|-------|-------------|
| 0     | 0           | 0.1   | 1           | 0.2   | 2           | 0.3   | 3           |
| 0.4   | 4           | 0.5   | 5           | 0.6   | 6           | 0.7   | 7           |
| 0.8   | 8           | 0.9   | 9           | 1     | 10          | 1.1   | 11          |
| 1.2   | 12          | 1.3   | 13          | 1.4   | 14          | 1.5   | 15          |
| 1.6   | 16          | 1.7   | 17          | 1.8   | 18          | 1.9   | 19          |
| 2     | 20          | 2.1   | 21          | 2.2   | 22          | 2.3   | 23          |
| 2.4   | 24          | 2.5   | 25          | 2.6   | 26          | 2.7   | 27          |
| 2.8   | 28          | 2.9   | 29          |

#### 8.1.5 Results:

Images of Case "B":

<table>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/822b8931-5dea-4447-93e9-0d4b45cdc310" alt="Image 1"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/bc86989a-b142-4d25-af79-780085731cac" alt="Image 2"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/673dfa7e-fed9-4877-a88e-b2a7b76bf619" alt="Image 3"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/ff9846c2-03be-4631-ac30-1d71a27c99da" alt="Image 4"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/b57d9f66-ffae-4c03-a46a-46afd3100d54" alt="Image 5"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/7ded5c5f-817f-4d24-9d6b-64251af76093" alt="Image 6"></td>
  </tr>
</table>


#### 8.1.6 Summary Case of "B" (W = 31, N = 512):

In this case, you have a larger "W" value (31) compared to the total number of buckets ("N" = 512).
The input values are still continuously mapped to bucket indices, but because "W" is larger, each bucket represents a wider range of values.
As you can see, each increment of 0.1 in the input value corresponds to an increment of 2 in the bucket index. This means that the buckets are coarser, and each bucket represents a broader range of values.

In summary, the choice of "W" and "N" values in your scalar encoder determines how finely or coarsely the input values are divided into buckets. A smaller "W" value results in finer divisions, while a larger "W" value results in coarser divisions. The choice depends on your specific application and the level of granularity you need when encoding input values.


### 8.2. Changes in Radius, MinVal, and MaxVal


#### 8.2.1 TriggerMessage Case "A1" and "B1":

| Field        | value             | value             |
|--------------|-------------------|-------------------|
| ExperimentId | 2023              | 2023              |
| InputFile    | GetBucketIndexPeriodic  | ImagesForGetBucketIndexPeriodic |
| Name         | sample            | sample            |
| NameID       | scalar            | scalar            |
| W            | 21                | 21                |
| N            | 1024              | 1024              |
| Radius       | -1.0              | 0.5               |
| MinVal       | 0.0               | -50.0             |
| MaxVal       | 100.0             | 50.0              |
| Periodic     | true              | true              |
| ClipInput    | false             | false             |

#### 8.2.2 Results:

Excel Data of Case "A1":

| Value | BucketIndex | Value | BucketIndex | Value | BucketIndex | Value | BucketIndex |
|-------|-------------|-------|-------------|-------|-------------|-------|-------------|
| 0     | 0           | 0.1   | 1           | 0.2   | 2           | 0.3   | 3           |
| 0.4   | 4           | 0.5   | 5           | 0.6   | 6           | 0.7   | 7           |
| 0.8   | 8           | 0.9   | 9           | 1     | 10          | 1.1   | 11          |
| 1.2   | 12          | 1.3   | 13          | 1.4   | 14          | 1.5   | 15          |
| 1.6   | 16          | 1.7   | 17          | 1.8   | 18          | 1.9   | 19          |
| 2     | 20          | 2.1   | 21          | 2.2   | 22          | 2.3   | 23          |
| 2.4   | 24          | 2.5   | 25          | 2.6   | 26          | 2.7   | 27          |
| 2.8   | 28          | 2.9   | 29          |


#### 8.2.3 Images of Case "A1":

<table>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/5f57a9c0-f59f-4850-90a8-6c6adf1844af" alt="Image 1"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/8ba3e030-75a0-4fad-8c50-192283484fb1" alt="Image 2"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/ce53fe7f-72e9-49e4-ba0e-83d507874044" alt="Image 3"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/3cc7c404-e78f-49be-9dae-f6de0228b13f" alt="Image 4"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/85072c30-07a5-40b9-a0a4-1d14add88ad3" alt="Image 5"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/18e0b8e5-ce0b-49cc-a081-070fd83a6465" alt="Image 6"></td>
  </tr>
</table>

#### 8.2.4 Summary Case "A1" (Radius = -1.0, MinVal = 0.0, MaxVal = 100.0):

In this case, you have a "Radius" of -1.0, which means that there's no specific radius limitation.
"MinVal" is set to 0.0, and "MaxVal" is set to 100.0, indicating that the input values span from 0.0 to 100.0.
As you can see, the input values are continuously mapped to bucket indices. Each increment of 0.1 in the input value corresponds to an increment of 1 in the bucket index.
The mapping spans the entire range of values from 0 to 100.

#### 8.2.5 Results:

Excel Data "B1":

| Value | BucketIndex | Value | BucketIndex | Value | BucketIndex | Value | BucketIndex |
|-------|-------------|-------|-------------|-------|-------------|-------|-------------|
| 0     | 0           | 0.1   | 0           | 0.2   | 1           | 0.3   | 1           |
| 0.4   | 2           | 0.5   | 2           | 0.6   | 3           | 0.7   | 3           |
| 0.8   | 4           | 0.9   | 4           | 1     | 5           | 1.1   | 5           |
| 1.2   | 6           | 1.3   | 6           | 1.4   | 7           | 1.5   | 7           |
| 1.6   | 8           | 1.7   | 8           | 1.8   | 9           | 1.9   | 9           |
| 2     | 10          | 2.1   | 10          | 2.2   | 11          | 2.3   | 11          |
| 2.4   | 12          | 2.5   | 12          | 2.6   | 13          | 2.7   | 13          |
| 2.8   | 14          | 2.9   | 14          |

#### 8.2.6 Images of Case "B1" :

<table>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/7d701105-103c-48cf-ae9c-25690d62c1c0" alt="Image 1"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/135cf81a-0771-4f1f-a443-138b83032014" alt="Image 2"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/c18a1e8b-8492-4482-8334-4f180181462d" alt="Image 3"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/023ca36b-a69b-473f-9f0a-0b7e77a4275b" alt="Image 4"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/5b8a3193-411f-47b2-b005-543e562cd89e" alt="Image 5"></td>
  </tr>
</table>

#### 8.2.7 Summary Case "B1" (Radius = 0.5, MinVal = -50.0, MaxVal = 50.0):

In this case, you've introduced a "Radius" of 0.5, which means that the encoder will consider a radius of 0.5 around each value.
"MinVal" is set to -50.0, and "MaxVal" is set to 50.0, indicating that the input values span from -50.0 to 50.0.
The results show that the input values are mapped to bucket indices differently. The bucket indices start at 512, indicating that the encoder is treating the range from -50.0 to 50.0 as a continuous cycle.
Each increment of 0.1 in the input value corresponds to an increment of 1 in the bucket index within this cyclic range.


In summary, the changes in "Radius," "MinVal," and "MaxVal" have affected how the scalar encoder maps input values to bucket indices. Case "A1" results in a continuous mapping over the entire specified range, while Case B1 results in a cyclic mapping within the specified range with a defined radius. The choice of these parameters depends on the specific requirements of your application and how you want to encode and represent the input values.

### 8.3. Repetition of BucketValues

#### 8.3.1 TriggerMessage:

| Field         | Value         | 
|---------------|---------------|
| ExperimentI   | 2023          |
| InputFile     | GetBucketIndexPeriodic |
| Name          | sample        |
| NameID        | scalar        |
| W             | 31            |
| N             | 512           |
| Radius        | -1.0          |
| MinVal        | 0.0           |
| MaxVal        | 100.0         |
| Periodic      | true          |
| ClipInput     | false         |


#### 8.3.2 Excel Data :

| Value | BucketIndex | Value | BucketIndex | Value | BucketIndex | Value | BucketIndex |
|-------|-------------|-------|-------------|-------|-------------|-------|-------------|
| 0     | 0           | 0.1   | 0           | 0.2   | 1           | 0.3   | 1           |
| 0.4   | 2           | 0.5   | 2           | 0.6   | 3           | 0.7   | 3           |
| 0.8   | 4           | 0.9   | 4           | 1     | 5           | 1.1   | 5           |
| 1.2   | 6           | 1.3   | 6           | 1.4   | 7           | 1.5   | 7           |
| 1.6   | 8           | 1.7   | 8           | 1.8   | 9           | 1.9   | 9           |
| 2     | 10          | 2.1   | 10          | 2.2   | 11          | 2.3   | 11          |
| 2.4   | 12          | 2.5   | 12          | 2.6   | 13          | 2.7   | 13          |
| 2.8   | 14          | 2.9   | 14          |

#### 8.3.3 Images:


<table>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/fee93a39-00ed-4d2d-a5a9-a30fad12e5f3" alt="Image 1"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/8e54f69f-cb4d-461d-ad97-b36ca7587b5e" alt="Image 2"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/f58c4b3d-cfd5-48bd-940d-59f011b8e6f6" alt="Image 3"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/b197e570-0fd5-49ac-8b71-05c0c71d14af" alt="Image 4"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/cd529e69-9848-4f4b-842f-79c6722aae94" alt="Image 5"></td>
    <td><img src="https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/759b7425-b902-4e6b-acc9-01e96942de2c" alt="Image 6"></td>
  </tr>
</table>


#### 8.3.4 Summary:

In the case you've provided, where two values have the same bucket index, it means that these values are considered equivalent or indistinguishable by the scalar encoder. This is a result of the parameters you've set, particularly the "W" (width) and "N" (number of buckets) values.

Here's a breakdown of the case:

"W" is set to 31: This means you have 31 buckets available for encoding your input values.
"N" is set to 512: This is the total number of unique values the encoder can represent.

Given these parameters, the encoder has to map a wide range of values (from 0.0 to 100.0) into a relatively small number of buckets (31). This mapping results in some values being mapped to the same bucket because the resolution is limited by the number of buckets available.

For example, in your results:

0 and 0.1 are both mapped to bucket 0.
0.2 and 0.3 are both mapped to bucket 1.
0.4 and 0.5 are both mapped to bucket 2.

This behavior is a trade-off between the width of the buckets and the number of buckets available. If you need more fine-grained discrimination between values, you can increase the number of buckets ("N"), but this will also increase the number of buckets that share the same value.

In summary, when two values have the same bucket index in this context, it means that the encoder considers them to be very similar within the specified parameters and resolution of the encoding. Adjusting the "W" and "N" values can help control the granularity of the encoding and potentially reduce the number of equivalent bucket mappings if needed.

## 9. Execute the experiment:

```csharp
docker pull ccprojectc.azurecr.io/teamspiralganglions_myclouldproject:latest
```
### 9.1 Image In Azure:

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/77dd2108-9948-4729-a35d-b3aa13b60619)

### 9.2 Container Instances:

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/f84f8d86-5d4a-4bae-8516-1902a33a6713)

### 9.3 TriggerMessages:

```csharp
EncodeIntoArray
{
  "ExperimentId": "2023",
  "InputFile": "EncodeIntoArray",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": false,
  "ClipInput": false
}

GetBucketIndexPeriodic
{
  "ExperimentId": "2023",
  "InputFile": "GetBucketIndexPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": true,
  "ClipInput": false
}

ImagesForGetBucketIndexPeriodic
{
  "ExperimentId": "2023",
  "InputFile": "ImagesForGetBucketIndexPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": true,
  "ClipInput": false
}

GetBucketIndexNonPeriodic
{
  "ExperimentId": "2023",
  "InputFile": "GetBucketIndexNonPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": false,
  "ClipInput": false
}

ImagesForGetBucketIndexNonPeriodic
{
  "ExperimentId": "2023",
  "InputFile": "ImagesForGetBucketIndexNonPeriodic",
  "Name": "sample",
  "NameID": "scalar",
  "W": 21,
  "N": 1024,
  "Radius": -1.0,
  "MinVal": 0.0,
  "MaxVal": 100.0,
  "Periodic": false,
  "ClipInput": false
}
```
### 9.4 Resource group of Team:

![MicrosoftTeams-image (3)](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/8574f8b8-fc1d-4983-915c-c3164bbe352f)

### 9.5 Output in Azure:

![MicrosoftTeams-image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/44403216-80b5-466f-845b-ec512392eb92)

### 9.6 Storage Browser:

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/b949c30e-756c-4b3f-89fb-8dacefd1eb70)

### 9.7 trigger-Queue:

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/f0085328-e316-4f49-84ce-e530ee7c3ea0)

### 9.8 Blob Container Outputs:

![image](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/assets/116576484/27ab2c19-a373-4e3c-9c71-dd4a3212903c)

## 10. References
1.	[nupic.docs](https://nupic.docs.numenta.org/)
2.	[Cloud architecture](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2022-2023/blob/master/Lessons/CloudComputing/Cloud%20Project%20Architecture.pptx)
3.	Links: 
[Numenta](http://nupic.docs.numenta.org/1.0.3/api/algorithms/encoders.html#scalar-encoders)
(https://www.youtube.com/watch?v=V3Yqtpytif0)
4.	[Scalar Encoders PDF](https://arxiv.org/ftp/arxiv/papers/1602/1602.05925.pdf)]
5.	VideoLinks: [scalar encoder](https://www.youtube.com/watch?v=V3Yqtpytif0&t=82s) , [SDR](https://www.youtube.com/watch?v=HLuRQKzYbb8&list=PL3yXMgtrZmDrotym3fyz_Oi06ZzFzpA_P)
