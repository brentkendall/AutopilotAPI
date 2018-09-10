using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace AutopilotAPI
{
    class Program
    {
        // This sample calls the PQ (test) environment, not the LIVE environment
        static string AutpilotBaseURL = "https://api.microsoftoem.info/ComputerBuildReport/royd/v1/autopilot";

        static void Main(string[] args)
        {
            AutoPilotSearch();
            AutoPilotValidate();
            AutoPilotRegister();
            Console.ReadLine();
        }

        private static void AutoPilotSearch()
        {
            Console.WriteLine($"======================================================================================================================");
            var BatchID = "48065c9d-e3f6-41fc-b461-c546320dbefe:0de59b18-c850-4a92-b7d8-8fde5ba8f838";  // Enter a valid Batch ID - this is a bogus example only
            Console.WriteLine($"AutoPilot Search request for BatchID : {BatchID}");
            var uri = $"{AutpilotBaseURL}/?BatchID={BatchID}";
            var requestObj = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            requestObj = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            requestObj.ContentType = "application/json";
            requestObj.Method = "GET";
            var cert = GetCertificate();
            requestObj.ClientCertificates.Add(cert);
            try
            {
                var responseObj = (HttpWebResponse)requestObj.GetResponse();
                StreamReader reader = new StreamReader(responseObj.GetResponseStream());
                String ResponseData = reader.ReadToEnd();
                Console.WriteLine($"AutoPilot Search response for BatchID : {BatchID} {Environment.NewLine} {ResponseData}");
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"======================================================================================================================");
        }

        private static void AutoPilotValidate()
        {
            Console.WriteLine($"======================================================================================================================");
            Console.WriteLine($"AutoPilot validate");
            var uri = $"{AutpilotBaseURL}/validate";
            // Enter valid values for submitData below - this is an example with bogus values only
            var submitData = "{'PurchaseOrderId': 'Pilotsub1','GroupTag': 'tag1','TenantID': '0de59b18-c850-4a92-b7d8-8fde5ba8f838','TenantDomain': 'SampleTenantName.com',  'SoldToCustomerID': '0000130368',  'ReceivedFromCustomerID': '0000130368',  'Devices': [{'ProductKey': '345457677' }]}";
            var requestObj = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            requestObj.Method = "POST";
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(submitData);
            requestObj.ContentLength = bytes.Length;
            requestObj.ContentType = "application/json";
            requestObj.ClientCertificates.Add(GetCertificate());
            try
            {
                Stream objRequestStream = requestObj.GetRequestStream();
                objRequestStream.Write(bytes, 0, bytes.Length);
                objRequestStream.Close();
                var responseObj = (HttpWebResponse)requestObj.GetResponse();
                StreamReader reader = new StreamReader(responseObj.GetResponseStream());
                Console.WriteLine(responseObj.StatusCode);
                Console.WriteLine(reader.ReadToEnd());
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"======================================================================================================================");
        }

        private static void AutoPilotRegister()
        {
            Console.WriteLine($"======================================================================================================================");
            Console.WriteLine($"AutoPilot Register");
            var uri = $"{AutpilotBaseURL}";
            // Enter valid values for submitData below - this is an example with bogus values only
            var submitData = "{'PurchaseOrderId': 'Pilotsub1','GroupTag': 'tag1','TenantID': '0de59b18-c850-4a92-b7d8-8fde5ba8f838','TenantDomain': 'SampleTenantName.com',  'SoldToCustomerID': '0000130368',  'ReceivedFromCustomerID': '0000130368',  'Devices': [{'ProductKey': '345457677' }]}";
            var requestObj = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            requestObj.Method = "POST";
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(submitData);
            requestObj.ContentLength = bytes.Length;
            requestObj.ContentType = "application/json";
            requestObj.ClientCertificates.Add(GetCertificate());
            try
            {
                Stream objRequestStream = requestObj.GetRequestStream();
                objRequestStream.Write(bytes, 0, bytes.Length);
                objRequestStream.Close();
                var responseObj = (HttpWebResponse)requestObj.GetResponse();
                StreamReader reader = new StreamReader(responseObj.GetResponseStream());
                Console.WriteLine(responseObj.StatusCode);
                Console.WriteLine(reader.ReadToEnd());
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"======================================================================================================================");
        }

        private static X509Certificate GetCertificate()
        {
            // Ensure you have a valid certificate to call this API
            var serialNumber = "560050872b5daa9f97bce34b3400000050872b";
            var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.OpenExistingOnly);
            var cert = certStore.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, true);
            if (cert.Count == 0)
                throw new Exception("cert not found");

            return cert[0];
        }
    }
}
