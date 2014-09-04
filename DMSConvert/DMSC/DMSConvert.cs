using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace ChaoticWeg
{
    public class DMSConvert
    {
        public static void DecToDms(float dec, out float deg, out float min, out float sec)
        {
            deg = (float) Math.Floor(dec);
            min = (float) Math.Floor((dec - deg) * 60);
            sec = (float) Math.Round((((dec - deg) * 60) - min) * 60, 1);
        }

        public static void DmsToDec(out float dec, float deg, float min, float sec)
        {
            dec = RoundFloat(deg + (min / 60.0f) + (sec / 3600.0f), 4);
        }

        public static float RoundFloat(float f, int digits)
        {
            return (float) Math.Round(f, digits);
        }

        public static void Log(String message, LogType type = LogType.Log)
        {
            String msg = String.Format("[DMSConvert] {0}: {1}", Time.time.ToString("0.000"), message);
            switch (type)
            {
                case LogType.Exception:
                case LogType.Error:
                    Debug.LogError(msg);
                    break;

                case LogType.Warning:
                case LogType.Assert:
                    Debug.LogWarning(msg);
                    break;

                case LogType.Log:
                default:
                    Debug.Log(msg);
                    break;
            }
        }
    }
}
