using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Q4
{
    class Program
    {
        static void Main(string[] args)
        {
            myInterfaceClient myPxy = new myInterfaceClient(); //Create the proxy for the service

            Console.Write("Enter Upper: ");     //Ask for upper limit of possible numbers
            int upper = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Lower: ");     
            int lower = Convert.ToInt32(Console.ReadLine()); //get user input for lower limit of numbers
            int SecretNumber = myPxy.SecretNumber(lower, upper);
            Console.WriteLine("A secret number has been generated"); //Indicate that number is generated
            Console.Write("Enter a guess: ");
            int userNum = Convert.ToInt32(Console.ReadLine()); //User inputted guess

            while(userNum != SecretNumber) //Goes until user has entered correct number
            {
                Console.WriteLine(myPxy.checkNumber(userNum, SecretNumber)); //Uses checknumber service from proxy
                Console.Write("Enter another guess: ");
                userNum = Convert.ToInt32(Console.ReadLine()); //Changes UserNumber

            }

            Console.WriteLine(myPxy.checkNumber(userNum, SecretNumber)); //To get final string for completed
            myPxy.Close(); //Close the proxy
            Console.WriteLine("press enter to close the client."); 
        }
    }
}
