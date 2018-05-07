using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class UDPManager : MonoBehaviour
{
    //public UdpService service;

    static UdpClient udp;
    Thread thread;

    public GameObject cube;
    public CubeMove cubemove;

    static readonly object lockObject = new object();
    Coordinates coord = new Coordinates();
    string returnData = "";
    bool precessData = false;

    UdpClient Client;

    void Start()
    {
        //this.service = new UdpService(5554);
        //udp = new UdpClient(5554);

        //cubemove = cube.GetComponent<CubeMove>();
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        

    }

    //CallBack
    //private void recv(IAsyncResult res)
    //{
    //    print("JERE_2");
    //    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 5554);
    //    byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);

    //    //Process codes

    //    print(Encoding.UTF8.GetString(received));
    //    Client.BeginReceive(new AsyncCallback(recv), null);
    //}

    void Update()
    {
        //var coordenadas = this.service.ReceiveBroadcast();
        //if (coordenadas.Raw == "MOVE")
        //{
        //    print("JERE10");
        //    print(coordenadas.X +", " + coordenadas.Y +", " + coordenadas.Z);
        //}

        //print(precessData);
        //if (precessData)
        //{
            /*lock object to make sure there data is 
             *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                precessData = false;

                Vector3 temp = transform.rotation.eulerAngles;
                temp.x = coord.Y;
                temp.y = coord.Z;
                temp.z = coord.X;
                //Debug.Log("TEMP: X = " + temp.x + ", Y = " + temp.y + ", Z=" + temp.z);
                transform.rotation = Quaternion.Euler(temp);

                ////Reset it for next read(OPTIONAL)
                returnData = "";
            }
        //}
    }

    void OnDestroy()
    {
        thread.Abort();
        Debug.Log("DETENIDO");
    }

    private void ThreadMethod()
    {
        print("HOLAAAAAAAAAA");
        udp = new UdpClient(5554);
        byte[] receiveBytes =  new byte[0];

        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 5554);
            
            //if (udp.Available < 1)
            //{
            //    print("Me Trabe al leer el puerto");
            //    thread.Abort();
            //    return;
            //}
            
            receiveBytes = udp.Receive(ref RemoteIpEndPoint);
            
            /*lock object to make sure there data is 
             *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                returnData = Encoding.ASCII.GetString(receiveBytes);
                print(returnData);
                
                //Debug.Log(returnData);

                if (returnData.Split(',').Length > 1)
                {
                    //var X = double.Parse(returnData.Split(',')[2]) * 0.5;
                    //var Y = double.Parse(returnData.Split(',')[3]) * 0.5;
                    //var Z = double.Parse(returnData.Split(',')[4]) * 0.5;

                    //Debug.Log("COORD SANTI: X = " + X + ", Y = " + Y + ", Z=" + Z);

                    coord.X = (float)double.Parse(returnData.Split(',')[2]) * 9;
                    coord.Y = (float)double.Parse(returnData.Split(',')[3]) * 9;
                    coord.Z = (float)double.Parse(returnData.Split(',')[4]) * 9;

                    //[6] Gyroscope x
                    //[7] gyro y
                    //[8] gyro z

                    //[10] mag x
                    //[11] mag y
                    //[12] mag z
                }
                Debug.Log("COORD: X = " + coord.X + ", Y = " + coord.Y + ", Z=" + coord.Z);

                if (returnData == "1\n")
                {
                    //Done, notify the Update function
                    precessData = true;
                }
            }
        }
    }
}