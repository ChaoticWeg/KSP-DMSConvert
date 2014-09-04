using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace ChaoticWeg
{
    [KSPAddonImproved(KSPAddonImproved.Startup.RealTime)]
    class DMSCOptions : MonoBehaviour
    {
        public static ConfigNode Config = null;

        private Rect windowPos = new Rect(100, 100, 50, 50);
        private static bool draw = false;
        public static bool Draw
        {
            get { return draw; }
            set { draw = value; }
        }

        private bool
            UseBlizzy,
            btnSaveAndClose,
            btnDiscardAndClose;

        public void Awake()
        {
            Config = DMSCConfig.Config;
            LoadOptions();
        }

        private void LoadOptions()
        {
            if (Config == null)
                Config = DMSCConfig.Config;

            if (Config.HasValue("UseBlizzy"))
                bool.TryParse(Config.GetValue("UseBlizzy"), out UseBlizzy);
            else
            {
                UseBlizzy = false;
                Config.AddValue("UseBlizzy", UseBlizzy);
                DMSCConfig.Save();
            }
        }

        public void OnGUI()
        {
            if (draw)
                windowPos = GUILayout.Window("DMSCOptions".GetHashCode(), windowPos, drawGUI, "DMSConvert Options",
                    GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            else
                return;

            if (btnSaveAndClose)
            {
                Config.SetValue("UseBlizzy", UseBlizzy.ToString());

                DMSCConfig.Save();
                draw = false;
            }

            if (btnDiscardAndClose)
                draw = false;
        }

        private void drawGUI(int windowID)
        {
            GUI.skin = HighLogic.Skin;

            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            UseBlizzy = GUILayout.Toggle(UseBlizzy, "Use Blizzy toolbar");
            GUILayout.Space(20f);

            GUILayout.BeginHorizontal();
            btnSaveAndClose = GUILayout.Button("Save & Close");
            GUILayout.FlexibleSpace();
            btnDiscardAndClose = GUILayout.Button("Discard & Close");
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }
    }
}
