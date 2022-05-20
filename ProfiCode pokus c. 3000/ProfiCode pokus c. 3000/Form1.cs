using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace ProfiCode_pokus_c._3000
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string NameHost(string Address)
        {
            try
            {

                string name = Dns.GetHostEntry(Address).HostName.ToString();
                return name;

            }
            catch 
            {
                return null;
            }
        }
        static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
            }
            return pingable;
        }
        List<IP_Info> info = new List<IP_Info>();

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            IP_Info[] IPS = new IP_Info[256];
            int ip_start, ip_end;
            ip_start = int.Parse(textBox4.Text);
            ip_end = int.Parse(textBox5.Text);
            Parallel.For(ip_start, ip_end, i =>
            {
                info.Add(new IP_Info
                {
                    ID = i,
                    IP = "192.168.2." + i.ToString(),
                    status = PingHost("192.168.2." + i.ToString()),
                    Name = NameHost("192.168.2."+i.ToString())
                });
            });
            dataGridView1.DataSource = info;
            dataGridView1.Visible = true;
        }
        class IP_Info
        {
            public int ID { get; set; }
            public string IP { get; set; }
            public string Name { get; set; }
            public bool status { get; set; }

        }

    }

}
