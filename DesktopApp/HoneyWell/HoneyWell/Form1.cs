using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using NativeWifi;
using System.Text;
using System.Net.Http;

namespace HoneyWell
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
        }

        //start button click handler
        private void button1_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            NetworkChange.NetworkAddressChanged += RegisterReceiver;
            update();
            post(GetDeviceMacAddress(), GetAccessPoint());
        }

        private void RegisterReceiver(object sender, EventArgs e)
        {
            string DeviceMac = GetDeviceMacAddress();
            string apMac = GetAccessPoint();
            //Send it to server from here
            setDeviceMac(DeviceMac, apMac);
            post(DeviceMac, apMac);
        }

        private void setDeviceMac(string deviceMac, string apMac)
        {
            string text = deviceMac;
            if(MacAddress.InvokeRequired)
            {
                this.Invoke(new SetTextCallBack(setDeviceMac), new object[] {deviceMac, apMac});
            }
            else
            {
                MacAddress.Text = text;
                ApAddress.Text = apMac;
            }
        }

        private delegate void SetTextCallBack(string dm, string ap);

        private void Stop_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            ApAddress.Text = "";
            MacAddress.Text = "";
            NetworkChange.NetworkAddressChanged -= RegisterReceiver;
        }

        private string GetDeviceMacAddress()
        {
            string mac = string.Empty;
            foreach(NetworkInterface Interface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (Interface.OperationalStatus == OperationalStatus.Up)
                {
                    mac = Interface.GetPhysicalAddress().ToString();
                    return mac;
                }
                    //MessageBox.Show(Interface.NetworkInterfaceType.ToString());
            }
            return mac;
        }

        private string GetAccessPoint()
        {
            try
            {
                WlanClient wlanClient = new WlanClient();
                foreach (WlanClient.WlanInterface wlanInterface in wlanClient.Interfaces)
                {
                    string ssid = GetStringForSSID(wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid);
                    byte[] macAddr = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Bssid;
                    var macAddrLen = (uint)macAddr.Length;
                    var str = new string[(int)macAddrLen];
                    for (int i = 0; i < macAddrLen; i++)
                    {
                        str[i] = macAddr[i].ToString("x2");
                    }
                    string mac = string.Join(":", str);
                    
                    return mac;
                }
            }
            catch(Exception e)
            {

            }
            return string.Empty;
        }

        private static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        private void update()
        {
            string mac = GetAccessPoint();
            ApAddress.Text = mac;
            MacAddress.Text = GetDeviceMacAddress();
        }

        private void post(string Mac, string Ap)
        {
            HttpClient client = new HttpClient();
            string server = "http://122.167.240.91:3000/api/location/laptop/mac/" + Mac;
            StringContent content = new StringContent("{\"mac\":\"" + Ap +"\"}",Encoding.UTF8,"application/json");
            client.PutAsync(server, content);
            Console.WriteLine("In post");
        }
    }
}
