using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public interface IUdpService
{
    byte[] ReceiveBroadcast();
}

