using System;
using System.Collections.Generic;
using System.Text;

namespace MyCloudProject.Common
{
    /// <summary>
    /// Defines the contract for the message request that will run your experiment.
    /// </summary>
    public interface IExerimentRequestMessage
    {
        string ExperimentId { get; set; }

        string InputFile { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        int W { get; set; }

        int N { get; set; }

        double Radius { get; set; }

        double MinVal { get; set; }

        double MaxVal { get; set; }

        bool Periodic { get; set; }

        bool ClipInput { get; set; }

        string NameID { get; set; }

    }
}
