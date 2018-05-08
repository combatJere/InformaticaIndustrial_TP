using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Assets
{
    public class UdpServiceOnDemand : IUdpService
    {
        public UdpClient Client { get; set; }
        private int port;

        public UdpServiceOnDemand(int port)
        {
            try
            {
                this.port = port;
                this.Client = new UdpClient(port);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public byte[] ReceiveBroadcast()
        {
            byte[] data = new byte[0];
            try
            {
                IPEndPoint server = new IPEndPoint(IPAddress.Broadcast, this.port);
                data = this.Client.Receive(ref server);
            }
            catch
            {
                Client.Close();
                Debug.WriteLine("FALLO");
            }

            return data;
        }
    }
}
