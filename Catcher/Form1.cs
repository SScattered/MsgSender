using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Catcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient clientME = new TcpClient(ip.Text, Convert.ToInt32(port.Text));
                StreamReader readerMSG = new StreamReader(clientME.GetStream());
                textBox1.Text += readerMSG.ReadLine();
                readerMSG.Close();
                clientME.Close();

                Properties.Settings.Default.ip = ip.Text;
                Properties.Settings.Default.port = Convert.ToInt32(port.Text);
                Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
                MessageBox.Show("It seems either the server is down or you have no net connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ip.Text = Properties.Settings.Default.ip;
            port.Text = Properties.Settings.Default.port.ToString();
        }
    }
}
