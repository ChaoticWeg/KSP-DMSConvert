using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using KSP;
using KSP.IO;

namespace KspDmsConvert
{
    /// <summary>The type of conversion to be done (DecToDMS or DMSToDec)</summary>
    public enum ConvertType
    {
        DecToDMS,   // decimal to DMS
        DMSToDec    // dms to decimal
    }

    /// <summary>A class containing a few utilities</summary>
    class Utilities
    {
        /// <summary>
        /// Convert either decimal to DMS or vice versa.
        /// Variables are passed by reference, so any changes will directly
        /// modify the variables that are passed as arguments.
        /// </summary>
        /// <param name="type">the type of conversion to be done</param>
        /// <param name="dec">a reference to the float variable for decimal degrees</param>
        /// <param name="deg">a reference to the float variable for DMS degrees</param>
        /// <param name="min">a reference to the float variable for DMS minutes</param>
        /// <param name="sec">a reference to the float variable for DMS seconds</param>
        public static void Convert(ConvertType type, ref float dec, ref float deg, ref float min, ref float sec)
        {
            switch (type)
            {
                case ConvertType.DecToDMS:
                    DecToDms(dec, ref deg, ref min, ref sec);
                    return;
                case ConvertType.DMSToDec:
                    DmsToDec(ref dec, deg, min, sec);
                    return;
                default:
                    DmsConvert.LogError("Unknown ConvertType: " + type.ToString());
                    return;
            }
        }

        /// <summary>
        /// Convert DMS to decimal degrees
        /// </summary>
        /// <param name="dec">a reference to the float variable for decimal degrees</param>
        /// <param name="deg">DMS degrees</param>
        /// <param name="min">DMS minutes</param>
        /// <param name="sec">DMS seconds</param>
        private static void DmsToDec(ref float dec, float deg, float min, float sec)
        {
            dec = deg + (min / 60f) + (sec / 3600f);
        }

        /// <summary>
        /// Convert decimal degrees to DMS
        /// </summary>
        /// <param name="dec">decimal degrees</param>
        /// <param name="deg">a reference to the float variable for DMS degrees</param>
        /// <param name="min">a reference to the float variable for DMS minutes</param>
        /// <param name="sec">a reference to the float variable for DMS seconds</param>
        private static void DecToDms(float dec, ref float deg, ref float min, ref float sec)
        {
            deg = (float) Math.Floor(dec);

            float remainder = (dec - deg) * 60f;
            min = (float) Math.Floor(remainder);
            sec = (float) Math.Round((remainder - min) * 60f, 3);
        }

        public class Config
        {
            /*            Paths            */
            public static readonly String CFG_PATH_REL      // path of the config file, relative to KSP root
                = "GameData/DmsConvert/config.cfg";
            public static readonly String CFG_PATH_ABS      // absolute path of the config file
                = KSPUtil.ApplicationRootPath.Replace("\\", "/") + CFG_PATH_REL;

            /*          Key names          */
            public static readonly String NODE_NAME = "DmsConvert";
            public static readonly String KEY_GUI_X = "gui_x";
            public static readonly String KEY_GUI_Y = "gui_y";
            public static readonly String KEY_GUI_RENDER = "gui_render";

            /*       Default values        */
            public static readonly float DEF_GUI_X = (Screen.width / 2) - 100;
            public static readonly float DEF_GUI_Y = (Screen.height / 2) - 100;
            public static readonly bool DEF_GUI_RENDER = false;

            /*           Methods           */

            public static ConfigNode DefaultNode()
            {
                ConfigNode node = new ConfigNode(NODE_NAME);
                node.name = NODE_NAME;

                node.AddValue(KEY_GUI_X, DEF_GUI_X);
                node.AddValue(KEY_GUI_Y, DEF_GUI_Y);
                node.AddValue(KEY_GUI_RENDER, DEF_GUI_RENDER);

                return node;
            }

            public static ConfigNode LoadConfig()
            {
                ConfigNode node = ConfigNode.Load(CFG_PATH_ABS);

                // if there is no config file, save the default one
                if (node == null)
                {
                    SaveConfig(DefaultNode());
                    return LoadConfig();
                }

                return node;
            }

            public static void SaveConfig(ConfigNode node)
            {
                node.Save(CFG_PATH_ABS);
            }
        }
    }
}
