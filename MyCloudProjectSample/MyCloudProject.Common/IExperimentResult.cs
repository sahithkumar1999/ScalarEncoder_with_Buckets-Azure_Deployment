
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCloudProject.Common
{
    public interface IExperimentResult
    {
        //private List<string> imageFilePaths { get; set; }

        string ExperimentId { get; set; }

        string ExperimentName { get; set; }

        //string ExperimentName { get; set; }

        string Description { get; set; }

        //List<string> ImageFilePaths { get; set; }

        public List<string> ImageFilePaths { get; set; }

        DateTime? StartTimeUtc { get; set; }

        DateTime? EndTimeUtc { get; set; }

        public int Input { get; set; }
        public string Encoded_Array { get; set; }
        IEnumerable<string> encodedData { get; }
        public byte[] excelData { get; set; }
        public class EncodedData
        {
            public int Input { get; set; }
            public string Encoded_Array { get; set; }
        }

        //List<string> imageFilePaths { get; set; }

        //public List<string> imageFilePaths { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    }

}
