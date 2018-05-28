using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;

class EmployeeTCPClient{
    public static void Main() {
        TcpClient client = new TcpClient("172.16.12.102",2055);
        try{
            Stream s = client.GetStream();
            StreamReader sr = new StreamReader(s);
            StreamWriter sw = new StreamWriter(s);
            sw.AutoFlush = true;
            Console.WriteLine(sr.ReadLine());
            Console.WriteLine("Connected to 172.16.12.102");
            Thread x = new Thread(new ParameterizedThreadStart(recieve));
            x.Start(s);
            while(true){ sw.WriteLine(Console.ReadLine());}
        }finally{
            // code in finally block is guranteed 
            // to execute irrespective of 
            // whether any exception occurs or does 
            // not occur in the try block
            client.Close();
        } 
    }
/*************************************************************************************/
    public static void recieve(object s){ //must be an object then cast!
                StreamReader sr = new StreamReader((Stream) s);
             while(true) { Console.WriteLine(sr.ReadLine()); }
    }
}
