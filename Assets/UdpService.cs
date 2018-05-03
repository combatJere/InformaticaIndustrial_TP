using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

    public class UdpService
    {
        public UdpClient Client { get; set; }
        private int port;
        public UdpService(int port)
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

        public Coordinates ReceiveBroadcast()
        {
        var coord = new Coordinates();
        try
        {

            IPEndPoint server = new IPEndPoint(IPAddress.Broadcast, this.port);
            Debug.WriteLine("PASO_1");
            byte[] packet = this.Client.Receive(ref server);
            Debug.WriteLine("PASO_2");

            var msg = Encoding.ASCII.GetString(packet);
            if (msg.Split(',').Length > 1)
            {
                coord.X = (float)Math.Round(double.Parse(msg.Split(',')[2].Replace(".", ",")));
                coord.Y = (float)Math.Round(double.Parse(msg.Split(',')[3].Replace(".", ",")));
                coord.Z = (float)Math.Round(double.Parse(msg.Split(',')[4].Replace(".", ",")));
            }
            else
            {
                coord.X = 10;
                coord.Y = 10;
            }

        }
        catch
        {
            Client.Close();
            Debug.WriteLine("FALLO");
            throw new Exception();
        }

            return coord;
        }
    }

