using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.AspNet.SignalR.Client;
using Scheduler.Common;

namespace Scheduler.Client
{
    class Program
    {
        static void Main(string[] args)
        {            
            //Set connection
            var connection = new HubConnection("http://localhost:8080/");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("ClientsHub");
            //connection.Headers.Add("authToken", Environment.MachineName);
            connection.Headers.Add("authToken", Guid.NewGuid().ToString());
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                connection.Headers.Add("ipAddress",
                    host.AddressList.First(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString());
            }
            
            //Start connection
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            //myHub.Invoke<string>("Send", "HELLO World ").ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error calling send: {0}",
            //                          task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine(task.Result);
            //    }
            //});

            myHub.On<CommandExecution>("executeCommand", param =>
            {
                Console.WriteLine(param);
            });

            //myHub.Invoke<string>("DoSomething", "I'm doing something!!!").Wait();


            Console.Read();
            connection.Stop();
        }
    }
}