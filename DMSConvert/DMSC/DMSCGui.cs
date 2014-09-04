using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using KSP;

namespace ChaoticWeg
{
    [KSPAddonImproved(KSPAddonImproved.Startup.RealTime)]
    class DMSCGui : MonoBehaviour
    {
        private bool show = true;
        private Rect windowPos = new Rect(50, 50, 50, 50);

        private String txtDecimal = "0", txtDegrees = "0", txtMinutes = "0", txtSeconds = "0";
        private float dec = 0f, deg = 0f, min = 0f, sec = 0f;
        private bool btnDecToDms = false, btnDmsToDec = false, btnShowOptions = false;

        public void Awake()
        {
            DMSConvert.Log("Awake, updating text fields");
            UpdateTextFields(false);
        }

        public void OnGUI()
        {
            GUI.skin = HighLogic.Skin;
            if (show)
                windowPos = GUILayout.Window("DMSConvert".GetHashCode(), windowPos, debugWindow, "DMS/Decimal Conversion", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (btnDmsToDec)
            {
                ParseAllFloats();
                DMSConvert.DmsToDec(out dec, deg, min, sec);
                UpdateTextFields(true);
            }

            if (btnDecToDms)
            {
                ParseAllFloats();
                DMSConvert.DecToDms(dec, out deg, out min, out sec);
                UpdateTextFields(true);
            }

            if (btnShowOptions)
            {
                DMSCOptions.Draw = true;
            }
        }

        void debugWindow(int windowID)
        {
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();

            // decimal
            GUILayout.BeginVertical();
            GUILayout.Label("Decimal", GUILayout.Width(75f));
            txtDecimal = GUILayout.TextField(txtDecimal, GUILayout.Width(75f));
            GUILayout.EndVertical();

            GUILayout.Space(10f);

            // button pane
            GUILayout.BeginVertical();
            btnDecToDms = GUILayout.Button("-->", GUILayout.Width(50f));
            GUILayout.FlexibleSpace();
            btnDmsToDec = GUILayout.Button("<--", GUILayout.Width(50f));
            GUILayout.EndVertical();

            GUILayout.Space(10f);

            // dms side
            GUILayout.BeginVertical();
            GUILayout.Label("D", GUILayout.Width(50f));
            txtDegrees = GUILayout.TextField(txtDegrees, GUILayout.Width(50f));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("M", GUILayout.Width(50f));
            txtMinutes = GUILayout.TextField(txtMinutes, GUILayout.Width(50f));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("S", GUILayout.Width(50f));
            txtSeconds = GUILayout.TextField(txtSeconds, GUILayout.Width(50f));
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();

            btnShowOptions = GUILayout.Button("Options...");
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        public bool AreFieldsStale()
        {
            return !txtDecimal.Equals(dec.ToString())
                || !txtDegrees.Equals(deg.ToString())
                || !txtMinutes.Equals(min.ToString())
                || !txtSeconds.Equals(sec.ToString());
        }

        public void UpdateTextFields(bool log)
        {
            if (!AreFieldsStale())
                return;

            if (log)
                DMSConvert.Log("Updating text fields");

            txtDecimal = dec.ToString();
            txtDegrees = deg.ToString();
            txtMinutes = min.ToString();
            txtSeconds = sec.ToString();
        }

        public void ParseAllFloats()
        {
            float.TryParse(txtDecimal, out dec);
            float.TryParse(txtDegrees, out deg);
            float.TryParse(txtMinutes, out min);
            float.TryParse(txtSeconds, out sec);
        }
    }
}
