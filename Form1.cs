using NationalInstruments;
using NationalInstruments.NetworkVariable;
using NationalInstruments.NetworkVariable.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace niProject
{
    public partial class Form1 : Form
    {
        private NetworkVariableReader<double> _reader;
        //private const string NetworkVariableLocation = @"\\localhost\OPCProcess\Temperature";
        private const string NetworkVariableLocation = @"\\localhost\National Instruments.NIOPCServers.V5\Channel1.Device1.Temp";

        public Form1()
        {
            InitializeComponent();

            ConnectOPCServer();
        }

        private void ConnectOPCServer()
        {
            _reader = new NetworkVariableReader<double>(NetworkVariableLocation);
            _reader.Connect();
            textBox1.Text = _reader.ConnectionStatus.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosedEventHandler e)
        {
            _reader.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkVariableData<double> opcData = null;

            try
            {
                opcData = _reader.ReadData();
                textBox2.Text = opcData.GetValue().ToString();
            }
            catch(TimeoutException)
            {
                MessageBox.Show("The Read has timeout.", "timeout");
                return;
            }
        }
    }
}
