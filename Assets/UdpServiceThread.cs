using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Assets
{
    public class UdpServiceThread : IUdpService
    {
        public UdpClient Client { get; set; }
        private int port;
        private object lockObject;
        private byte[] data = new byte[0];
        Thread thread;

        public UdpServiceThread(int port, object lockObject)
        {
            try
            {
                this.port = port;
                this.lockObject = lockObject;
                thread = new Thread(new ThreadStart(ThreadMethod));
                thread.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public byte[] ReceiveBroadcast()
        {
            return data;
        }

        private void ThreadMethod()
        {
            Client = new UdpClient(this.port);
            byte[] receiveBytes = new byte[0];

            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Broadcast, this.port);

                //if (udp.Available < 1)
                //{
                //    print("Me Trabe al leer el puerto");
                //    thread.Abort();
                //    return;
                //}

                receiveBytes = Client.Receive(ref RemoteIpEndPoint);

                /*lock object to make sure there data is 
                 *not being accessed from multiple threads at thesame time*/
                lock (lockObject)
                {
                    data = receiveBytes;
                }
            }
        }
    }
}
