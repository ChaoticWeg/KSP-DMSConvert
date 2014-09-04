using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using KSP;
using KSP.IO;

namespace ChaoticWeg
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    class InitConfig : MonoBehaviour
    {
        void Awake()
        {
            DMSConvert.Log("Initializing config");
            DMSCConfig.Load();
        }
    }

    [KSPAddonImproved(KSPAddonImproved.Startup.RealTime)]
    class DMSCConfig : MonoBehaviour
    {
        private static ConfigNode config;
        public static ConfigNode Config
        {
            get
            {
                if (config == null)
                    Load();
                return config;
            }

            set
            {
                config = value;
            }
        }

        public static void Load()
        {
            DMSConvert.Log("Loading config");
            config = GameDatabase.Instance.GetConfigNode("DMSConvert");

            if (config == null)
                config = new ConfigNode("DMSConvert");

            Initialize();
        }

        public static void Save()
        {
            DMSConvert.Log("Saving config");
            ConfigNode savenode = new ConfigNode();
            savenode.AddNode(config);
            savenode.Save(DMSConvert.PathConfig);
        }

        static void Initialize()
        {
            if (!Config.HasValue("UseBlizzy"))
                Config.AddValue("UseBlizzy", false);
        }
    }
}
