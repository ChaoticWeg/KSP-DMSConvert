using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using KSP;
using KSP.IO;

namespace DMSConvert
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class CheckConfigs : MonoBehaviour
    {
        void Start()
        {
            DMSConvert.LogEntry("Initializing config file");
            Utilities.Config.LoadConfig();
        }
    }

    [KSPAddonImproved(KSPAddonImproved.Startup.RealTime)]
    public class DMSConvert : MonoBehaviour
    {
        /// <summary>Logs a message in the Alt+F2 debug log</summary>
        /// <param name="message">The message to be logged</param>
        public static void LogEntry(String message)
        {
            Debug.Log("DmsConvert [" + Time.time.ToString("0.0000") + "] : " + message);
        }

        /// <summary>Logs an error in the Alt+F2 debug log</summary>
        /// <param name="error">The error message to be logged</param>
        public static void LogError(String error)
        {
            Debug.LogError("DmsConvert [" + Time.time.ToString("0.0000") + "] : " + error);
        }

        /*               Configuration                */

        ConfigNode config = Utilities.Config.LoadConfig();

        /*                GUI stuffs                  */

        Rect _gui = new Rect() { };     // the gui itself
        GUILayoutOption[] optDms =      // layout options for the DMS text field
            new GUILayoutOption[] { GUILayout.Width(50f) };
        GUILayoutOption[] optDecimal =  // layout options for the decimal text field
            new GUILayoutOption[] { GUILayout.Width(75f) };
        GUILayoutOption[] optButtons =  // layout options for the buttons
            new GUILayoutOption[] { GUILayout.Width(75f) };

        // static fields: persist through instance creation and destruction
        static float
            _x = 0.0f,                  // gui x position
            _y = 0.0f,                  // gui y position
            dec = 0.0f,                 // decimal value
            deg = 0.0f,                 // degrees value
            min = 0.0f,                 // minutes value
            sec = 0.0f;                 // seconds value
        static bool renderGUI = false;  // whether to render the gui

        // instance fields: are created and destroyed along with each instance
        bool
            updatedXY = false,          // whether gui X or Y was updated in this tick
            btnDecToDms,                // button: decimal -> dms
            btnDmsToDec;                // button: dms -> decimal
        String
            fmtFloats = "0.000",        // ToString formats for floats
            txtDecimal = "dec",         // decimal text box
            txtDeg = "deg",             // degrees text box
            txtMin = "min",             // minutes text box
            txtSec = "sec";             // seconds text box
        float
            posUpdateInterval = 5.0f,   // how often to update the GUI position fields
            lastPosUpdate = 0.0f;       // the last time the GUI pos fields were updated

        /*            Configuration methods           */

        public void ReadFieldsFromConfig()
        {
            if (config == null)
                config = Utilities.Config.LoadConfig();

            float.TryParse(config.GetValue(Utilities.Config.KEY_GUI_X), out _x);
            float.TryParse(config.GetValue(Utilities.Config.KEY_GUI_Y), out _y);
            bool.TryParse(config.GetValue(Utilities.Config.KEY_GUI_RENDER), out renderGUI);
        }

        public void SaveFieldsToConfig()
        {
            if (config == null)
                config = Utilities.Config.LoadConfig();

            config.SetValue(Utilities.Config.KEY_GUI_X, _x.ToString());
            config.SetValue(Utilities.Config.KEY_GUI_Y, _y.ToString());
            config.SetValue(Utilities.Config.KEY_GUI_RENDER, renderGUI.ToString());

            Utilities.Config.SaveConfig(config);
        }

        /*        GUI-drawing-related methods         */
        /* (OnGUI is under "methods called by Unity") */

        void WindowGUI(int windowID)
        {
            GUILayout.BeginVertical();          // outermost layer
            GUILayout.Space(10f);               // a little padding

            GUILayout.BeginHorizontal();        // contains the conversion elements

            GUILayout.BeginVertical();          // Decimal side
            GUILayout.Label("Decimal", optDecimal);
            txtDecimal = GUILayout.TextField(txtDecimal, optDecimal);
            GUILayout.EndVertical();            // end Decimal side

            GUILayout.Space(10f);               // a little padding

            GUILayout.BeginVertical();          // BUTTON PANEL :D
            btnDecToDms = GUILayout.Button("-->", optDms);
            btnDmsToDec = GUILayout.Button("<--", optDms);
            GUILayout.EndVertical();            // end button panel :(

            GUILayout.Space(10f);               // a little padding

            GUILayout.BeginHorizontal();        // BEGIN DMS SIDE

            GUILayout.BeginVertical();          // DMS side - DEGREES
            GUILayout.Label("D", optDms);
            txtDeg = GUILayout.TextField(txtDeg, optDms);
            GUILayout.EndVertical();            // end DEGREES

            GUILayout.BeginVertical();          // DMS side - MINUTES
            GUILayout.Label("M", optDms);
            txtMin = GUILayout.TextField(txtMin, optDms);
            GUILayout.EndVertical();            // end MINUTES

            GUILayout.BeginVertical();          // DMS side - SECONDS
            GUILayout.Label("S", optDms);
            txtSec = GUILayout.TextField(txtSec, optDms);
            GUILayout.EndVertical();            // end SECONDS

            GUILayout.EndHorizontal();          // END DMS SIDE
            GUILayout.EndHorizontal();          // end conversion element container

            GUILayout.Space(10f);               // a little padding
            GUILayout.Label("Press Alt+U to toggle this interface");
            GUILayout.EndVertical();            // end outermost layer

            // Allow user to drag the window (important)
            GUI.DragWindow();
        }

        void DrawGUI()
        {
            GUI.skin = HighLogic.Skin;
            _gui = GUILayout.Window(1, _gui, WindowGUI, "DMS Conversion");
        }

        /*    Methods regarding the global fields     */

        bool HaveFieldsBeenChanged()
        {
            return !(txtDecimal.Equals(dec.ToString(fmtFloats))
                && txtDeg.Equals(deg.ToString())
                && txtMin.Equals(min.ToString())
                && txtSec.Equals(sec.ToString(fmtFloats)));
        }

        void UpdateAllFields()
        {
            txtDecimal = dec.ToString(fmtFloats);
            txtDeg = deg.ToString();
            txtMin = min.ToString();
            txtSec = sec.ToString(fmtFloats);
        }

        bool ParseAllFields()
        {
            try
            {
                dec = float.Parse(txtDecimal);
                deg = float.Parse(txtDeg);
                min = float.Parse(txtMin);
                sec = float.Parse(txtSec);

                // if it falls through completely without error
                return true;
            } catch (FormatException ex)  // if the input doesn't look like a float
            {
                LogError("Invalid formatting: " + ex.Message);
                return false;
            } catch (Exception ex)        // OverflowException, ArgumentNullException
            {
                Debug.LogException(ex);
                return false;
            }
        }

        void UpdateGUIPosition()
        {
            if (_x != _gui.x)
            {
                if (!updatedXY)
                    updatedXY = true;
                _x = _gui.x;
            }

            if (_y != _gui.y)
            {
                _y = _gui.y;
                if (!updatedXY)
                    updatedXY = true;
            }

            if (updatedXY)
            {
                LogEntry("Updated one or more GUI position fields");
                updatedXY = false;
            }

            lastPosUpdate = Time.time;
        }

        /*           Methods called by Unity          */

        void Awake()
        {
            LogEntry("Hello, world! I am awake");
            ReadFieldsFromConfig();
        }

        void Start()
        {
            if (renderGUI)
                RenderingManager.AddToPostDrawQueue(3, new Callback(DrawGUI));

            _gui.x = _x;
            _gui.y = _y;

            UpdateAllFields();
        }

        void Update()
        {
            // toggle GUI visibility on key combo
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.U))
            {
                if (!renderGUI)
                    RenderingManager.AddToPostDrawQueue(3, new Callback(DrawGUI));
                else
                    RenderingManager.RemoveFromPostDrawQueue(3, new Callback(DrawGUI));
                renderGUI = !renderGUI;
            }

            // update the gui position fields if we haven't done so in a while
            float timeSinceGUIPosUpdate = Time.time - lastPosUpdate;
            if (timeSinceGUIPosUpdate > posUpdateInterval)
                UpdateGUIPosition();
        }

        void OnDestroy()
        {
            LogEntry("Destroyed! Bye-bye");
            SaveFieldsToConfig();
        }

        void OnGUI()
        {
            if (!renderGUI)
                return;

            if (btnDecToDms)
            {
                if (!HaveFieldsBeenChanged())
                    return;

                if (!ParseAllFields())
                    return;

                Utilities.Convert(ConvertType.DecToDMS, ref dec, ref deg, ref min, ref sec);
                UpdateAllFields();
            }

            if (btnDmsToDec)
            {
                if (!HaveFieldsBeenChanged())
                    return;

                if (!ParseAllFields())
                    return;

                Utilities.Convert(ConvertType.DMSToDec, ref dec, ref deg, ref min, ref sec);
                UpdateAllFields();
            }
        }
    }
}
