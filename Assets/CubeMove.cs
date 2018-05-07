using UnityEngine;
using System.Collections;
using Assets;
using System.Text;

public class CubeMove : MonoBehaviour
{
    static readonly object lockObject = new object();
    private UdpService udp;
    //private byte[] data = new byte[0];

    void Start()
    {
        //udp = new UdpService(5554);
        udp = new UdpService(5554, lockObject);
    }

    void Update()
    {
        lock (lockObject)
        {
            //var data = udp.ReceiveBroadcast();
            var data = udp.ReceiveBroadcastThread();

            var msg = Encoding.ASCII.GetString(data);
            Debug.Log(msg);
            var coord = IMUDecoder.getRotation(msg);
            this.Rotate(coord);
        }
    }

    private void Rotate(Coordinates coord)
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = coord.Y;
        temp.y = coord.Z;
        temp.z = coord.X;
        //Debug.Log("TEMP: X = " + temp.x + ", Y = " + temp.y + ", Z=" + temp.z);
        transform.rotation = Quaternion.Euler(temp);
    }

    private void translate(Coordinates coord)
    {
        //TODO 
    }
}
