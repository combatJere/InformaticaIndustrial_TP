using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public static class IMUDecoder
    {
        public static Coordinates getRotation(string data)
        {
            Coordinates coordenadas = new Coordinates();

            if (data.Split(',').Length > 1)
            {
                //var X = double.Parse(returnData.Split(',')[2]) * 0.5;
                //var Y = double.Parse(returnData.Split(',')[3]) * 0.5;
                //var Z = double.Parse(returnData.Split(',')[4]) * 0.5;

                //Debug.Log("COORD SANTI: X = " + X + ", Y = " + Y + ", Z=" + Z);

                coordenadas.X = (float)double.Parse(data.Split(',')[2]) * 9;
                coordenadas.Y = (float)double.Parse(data.Split(',')[3]) * 9;
                coordenadas.Z = (float)double.Parse(data.Split(',')[4]) * 9;

                //[6] Gyroscope x
                //[7] gyro y
                //[8] gyro z

                //[10] mag x
                //[11] mag y
                //[12] mag z
            }
            return coordenadas;
        }

        public static Coordinates getTranslation(string data)
        {
            Coordinates coordenadas = new Coordinates();

            //TODO implementar

            return coordenadas;
        }
    }
}
