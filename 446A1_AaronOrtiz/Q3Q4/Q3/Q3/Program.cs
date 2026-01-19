using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Q3
{
    [ServiceContract]
    public interface myInterface //Client side Interface
    {
        [OperationContract]
        int SecretNumber(int lower, int upper);
        [OperationContract]
        string checkNumber(int userNum, int SecretNum);
    }

    public class myService : myInterface //use interface
    {
        public int SecretNumber(int lower, int upper) //Code to generate secrete number (given)
        {
            DateTime currentDate = DateTime.Now;
            int seed = (int)currentDate.Ticks;
            Random random = new Random(seed);
            int sNumber = random.Next(lower, upper);
            return sNumber;
        }

        public string checkNumber(int userNum, int SecretNum) //Code to check the number (given)
        {
            if (userNum == SecretNum)
                return "correct";
            else
            if (userNum > SecretNum)
                return "too big";
            else return "too small";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8000/Service"); //Create Uri for local host usage

            ServiceHost selfHost = new ServiceHost(typeof(myService), baseAddress); //Self hosting code

            try
            {
                selfHost.AddServiceEndpoint(typeof(myInterface), new WSHttpBinding(), "myService"); //Apply the service to an endpoint for future use
                System.ServiceModel.Description.ServiceMetadataBehavior smb = new System.ServiceModel.Description.ServiceMetadataBehavior { };
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb); //Add the metadata to the Host

                selfHost.Open(); //Open Host
                Console.WriteLine("myService is ready to take requests. Please create a clieant to call my service.");
                Console.WriteLine("if you want to quit this service press enter");
                Console.ReadLine();

                selfHost.Close();//Close host
            }
            catch
            {
                Console.WriteLine("An exception occurred: (0)");
                selfHost.Abort(); //Abort when error occurs
            }
        }
    }
}
