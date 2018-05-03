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

        cubemove = cube.GetComponent<CubeMove>();
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();

        //Client = new UdpClient(5554);

        //try
        //{
        //    print("JERE");
        //    Client.BeginReceive(new AsyncCallback(recv), null);
        //}
        //catch (Exception e)
        //{
        //    print(e.ToString());
        //}

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

        print(precessData);
        //if (precessData)
        //{
        /*lock object to make sure there data is 
         *not being accessed from multiple threads at thesame time*/
        lock (lockObject)
        {
            //precessData = false;
            //cube.SendMessage("Move");
            //Vector3 moveVector = new Vector3(coord.X, coord.Y, coord.Z);
            //transform.Rotate(moveVector);

            Vector3 temp = transform.rotation.eulerAngles;
        temp.x = coord.Y;
        temp.y = coord.Z;
        temp.z = coord.X;
        Debug.Log("TEMP: X = " + temp.x + ", Y = " + temp.y + ", Z=" + temp.z);
        transform.rotation = Quaternion.Euler(temp);
        // or
        //cubemove.Move();

        //Process received data
        Debug.Log("Received: " + returnData);

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
        while (true)
        {
            print("JERE_0");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 5554);
            print("JERE_1");
            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);
            print("JERE_2");
            /*lock object to make sure there data is 
            *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                returnData = Encoding.ASCII.GetString(receiveBytes);

                //Debug.Log(returnData);
                if (returnData == "1\n")
                {
                    //Done, notify the Update function
                    precessData = true;
                }

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
                }
                Debug.Log("COORD: X = " + coord.X + ", Y = " + coord.Y + ", Z=" + coord.Z);
            }
        }
    }
}