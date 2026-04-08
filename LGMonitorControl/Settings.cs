using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System;
using Newtonsoft.Json.Converters;

namespace LGMonitorControl
{
    public class Settings
    {
        private Settings() { }
        private static Settings instance = null;
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public LG.GameMode.Modes DefaultMode { get; set; }
        public bool StartMinimized { get; set; }

       
        public int WindowWidth { get; set; } = 0;
        public int WindowHeight { get; set; } = 0;
        public int WindowX { get; set; } = -10000; // Default out-of-bounds to know if it's the first run
        public int WindowY { get; set; } = -10000;
        public string LastSortColumn { get; set; } = string.Empty;
        public System.Windows.Forms.SortOrder LastSortOrder { get; set; } = System.Windows.Forms.SortOrder.None;


        public SortableBindingList<ApplicationData> Applications { get; set; } = new SortableBindingList<ApplicationData>();


        public void Save()
        {
            Console.WriteLine("saved settings");
            try
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Settings.json"), JsonConvert.SerializeObject(Instance));
            }
            catch
            {

            }

        }
        public void Load()
        {
            try
            {
                Instance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Settings.json")));
            }
            catch
            {

            }
        }

        public void RegisterInStartup(bool isChecked)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("LGMonitorControl", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("LGMonitorControl", false);
            }
        }

        public bool GetAutostartState()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            return registryKey.GetValue("LGMonitorControl") != null;
        }
    }

        public class ApplicationData
    {
        public string WindowName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LG.GameMode.Modes GameMode { get; set; }

        public ApplicationData(string windowName, LG.GameMode.Modes gameMode)
        {
            WindowName = windowName;
            GameMode = gameMode;
        }
    }
}
