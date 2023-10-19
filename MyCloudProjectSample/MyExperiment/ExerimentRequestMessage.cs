using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExperiment
{
    internal class ExerimentRequestMessage : IExerimentRequestMessage
    {
        public string ExperimentId { get; set; }

        public string InputFile { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Radius { get; set; }

        public int W { get; set; }

        public int N { get; set; }

        public double MinVal { get; set; }

        public double MaxVal { get; set; }

        public bool Periodic { get; set; }

        public string NameID { get; set; }

        public bool ClipInput { get; set; }

    }
}


/*
 
 {
    "ExperimentId": "sasa",
    "InputFile":"sasss",

}
 
 */ 