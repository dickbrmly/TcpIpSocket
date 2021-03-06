using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

class Communicator{
    
    static TcpListener listener;
    /*************************************************************************************/
    public static void Main(){
		IPAddress localaddr = IPAddress.Parse("172.16.12.102");
        listener = new TcpListener(localaddr,2055);
        listener.Start();
        Console.WriteLine("Server mounted, listening to port 2055");
        connector();
    }
     /*************************************************************************************/
    public static void Service(){
				Socket soc = listener.AcceptSocket();
                Console.WriteLine("Connected: {0}", soc.RemoteEndPoint);
            try{
                Stream s = new NetworkStream(soc); 

                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true; // enable automatic flushing
                sw.WriteLine("{0} Connect.  ID: ");

                Thread x = new Thread(new ParameterizedThreadStart(recieve));
                x.Start(s);

                while(true){ sw.WriteLine(Console.ReadLine());}
            }catch(Exception e){
				Console.WriteLine(e.Message);
            }
    }
    /*************************************************************************************/
    public static void connector(){
        Thread t = new Thread(new ThreadStart(Service));
        t.Start();
    }
    /*************************************************************************************/
    public static void recieve(object s){ // Must be an object then cast! 
                                          // Synchronous read / write
                StreamReader sr = new StreamReader((Stream) s);
             while(true) { Console.WriteLine(sr.ReadLine());}
    }
}
