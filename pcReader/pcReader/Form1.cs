using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

using System.Web;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace pcReader
{
    public partial class Control : Form
    {
        string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        string UUID = null;
        string Total = null;
        string Ip = "http://www.networkmetrics.cf";
        IniFile MyIni = null;
        private static readonly HttpClient client = new HttpClient();
        const string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        PerformanceCounter cpuCounter1;
        PerformanceCounter ramCounter;
        ulong totalRam = 0;
        ulong totalHdd = 0;
        Dictionary<string, string> stream = new Dictionary<string, string>();
        public Control()
        {
            InitializeComponent();
        }
        public static List<string> GetInstalledPrograms()
        {
            var result = new List<string>();
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32));
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64));
            return result;
        }

        private static IEnumerable<string> GetInstalledProgramsFromRegistry(RegistryView registryView)
        {
            var result = new List<string>();

            using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView).OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (IsProgramVisible(subkey))
                        {
                            result.Add((string)subkey.GetValue("DisplayName"));
                        }
                    }
                }
            }

            return result;
        }

        private static bool IsProgramVisible(RegistryKey subkey)
        {
            var name = (string)subkey.GetValue("DisplayName");
            var releaseType = (string)subkey.GetValue("ReleaseType");
            //var unistallString = (string)subkey.GetValue("UninstallString");
            var systemComponent = subkey.GetValue("SystemComponent");
            var parentName = (string)subkey.GetValue("ParentDisplayName");
            var z = subkey.GetValueNames();

            var WindowsInstaller = subkey.GetValue("WindowsInstaller");


            return
                !string.IsNullOrEmpty(name)
                && string.IsNullOrEmpty(releaseType)
                && string.IsNullOrEmpty(parentName)
                && (systemComponent == null || (int)systemComponent == 0);
        }
        private void CheckFile()
        {
            var tmp = (appPath + @"\set.ini");
            if ( !File.Exists(tmp))
            {
                var z =File.Create(tmp);
                z.Close();
                Thread.Sleep(100);
                MyIni = new IniFile("set.ini");
                MyIni.Write("UUID", "");
                MyIni.Write("Total", "");
               

            }
           
        }
        public static string AttachParameters( NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();
            string str = "?";
            for (int index = 0; index < parameters.Count; ++index)
            {
                stringBuilder.Append(str + parameters.AllKeys[index] + "=" + parameters[index]);
                str = "&";
            }
            var res = stringBuilder.ToString();
            return res;
        }
        private async void CheckSec()
        {
            MyIni = new IniFile("set.ini");
            UUID = MyIni.Read("UUID");
            if (UUID == "")
            {

                var resp = await client.GetAsync("/Genel/kayitIDAl");
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var z1 = await resp.Content.ReadAsStringAsync();
                    MyIni.Write("UUID", z1);
                    UUID = z1;
                }
                

            }
            Total = MyIni.Read("Total");
            List<string> lst = new List<string>();
            lst = GetInstalledPrograms();
            lst.Sort();
            if (Total == ""|| Int32.Parse(Total) != lst.Count)
            {


                Dictionary<string, string> a = new Dictionary<string, string>();
                a.Clear();
                a.Add("kayitID", UUID);
                a.Add("programlar", string.Join(",", lst));
                a.Add("isletimSistemi", new ComputerInfo().OSFullName);
                string json = await Task.Run(() => JsonConvert.SerializeObject(a));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync("/Genel/bilgisayarGuncelle", httpContent);
                if (httpResponse.Content != null && httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MyIni.Write("Total", lst.Count.ToString());

                }
                var responseContent2 = await httpResponse.Content.ReadAsStringAsync();
         

            }
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client.BaseAddress = new Uri(Ip);
            CheckFile();
            CheckSec();
            cpuCounter1 = new PerformanceCounter("Processor", "% Idle time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            totalRam = new ComputerInfo().TotalPhysicalMemory/(1024*1024);
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                totalHdd += (ulong)(drive.TotalSize / (1024 * 1024));

            }

            this.Visible = false;
            this.Hide();
            timer1.Interval = 1000;
            timer1.Enabled = true;

            notifyIcon1.Text = "My application";
            notifyIcon1.Icon = this.Icon;

        
            notifyIcon1.ContextMenu = new ContextMenu();
            notifyIcon1.Visible = true;
            timer2.Enabled = true;
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                var bx = rk.GetValueNames();
                if (!bx.Contains("pcReader"))
                    rk.SetValue("pcReader", Application.ExecutablePath);
            }catch (Exception)
            {
                //start kayıt hatası
            }


        }

       


      
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            stream.Clear();
            stream.Add("bilgisayarID", UUID);
            stream.Add("islemciKullanimi", Math.Ceiling(100 - cpuCounter1.NextValue()).ToString());
            stream.Add("anlikKullanilanRam", ramCounter.NextValue().ToString());
            stream.Add("totalRam", totalRam.ToString());
            ulong tmp = 0;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                tmp += (ulong)(drive.TotalFreeSpace / (1024 * 1024));

            }
            stream.Add("anlikKullanilanDiskSize", (totalHdd-tmp).ToString());
            stream.Add("kullanilanTotalDiskSize", totalHdd.ToString());
            string json = JsonConvert.SerializeObject(stream);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = client.PostAsync("/Genel/stream", httpContent);





        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Hide();
            this.Visible = false;
            timer2.Enabled = false;
        }
    }
}
