using System;
using System.Threading;
using System.Windows.Forms;

namespace ArduinoComExample
{
    public partial class Form1 : Form
    {

        string[] ports = System.IO.Ports.SerialPort.GetPortNames();


        public Form1()
        {
            InitializeComponent();

            FormClosing += Form1_FormClosing;
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void connectionButton_Click(object sender, EventArgs e)
        {
            if (connectionButton.Text == "Connect")
            {
                connectionButton.Text = "Stop Connection";
                connectionStateLabel.Text = "Connection Status: " + comPortComboBox.Text + ", " + baudRateComboBox.Text + ", Connected";
                serialPort1.PortName = comPortComboBox.Text;
                serialPort1.BaudRate = Convert.ToInt32(baudRateComboBox.Text);
                serialPort1.Open();
            }
            else {
                connectionButton.Text = "Connect";
                connectionStateLabel.Text = "Connection Status: No connection";
                serialPort1.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in ports) {
                comPortComboBox.Items.Add(port);
            }

            // Add Baud Rates to ComboBox
            baudRateComboBox.Items.Add("300");
            baudRateComboBox.Items.Add("600");
            baudRateComboBox.Items.Add("1200");
            baudRateComboBox.Items.Add("2400");
            baudRateComboBox.Items.Add("4800");
            baudRateComboBox.Items.Add("9600");
            baudRateComboBox.Items.Add("19200");
            baudRateComboBox.Items.Add("57600");
            baudRateComboBox.Items.Add("115200");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (serialPort1.IsOpen == true) {
                serialPort1.Close();
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            recivedDataRichTextBox.Clear();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write(dataTextBox.Text + "\n");
            }
            else {
                MessageBox.Show("You cannot send data without a connection!");
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(30);

            try
            {
                string data = serialPort1.ReadLine(); // Read data from buffer

                if (recivedDataRichTextBox.InvokeRequired)
                {
                    recivedDataRichTextBox.Invoke(new MethodInvoker(delegate { recivedDataRichTextBox.Text += data + "\r\n"; }));
                }
            }
            catch {
                MessageBox.Show("There was a problem when disconnecting the connection.");
            }

            
            
        }
    }
}
