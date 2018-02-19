using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyWCFService
{
    [ServiceContract(Namespace = "MyReportingService")]
    public interface IEmailReport
    {
        [OperationContract(IsOneWay=true)]
        void SendReport(string email);
    }
}