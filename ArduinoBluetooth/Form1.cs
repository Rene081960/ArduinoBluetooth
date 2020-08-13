using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoBluetooth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitializeSerialPort();

            OpenSerialPort();

        }

        private void InitializeSerialPort()
        {
            serialPort1.PortName = "COM9";
            serialPort1.BaudRate = 9600;
        }

        public void OpenSerialPort()
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Datareceived);
        }

        string RxString = string.Empty;
        private void Datareceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                RxString = sp.ReadLine();
                this.Invoke(new EventHandler(DisplayText));
                RxString = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayText(object sender, EventArgs e)
        {
            string[] lijst = RxString.Split('|');
            label1.Text = lijst[0];
            label2.Text = lijst[1];
            label3.Text = lijst[2];
            label4.Text = lijst[3];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }
    }
}
