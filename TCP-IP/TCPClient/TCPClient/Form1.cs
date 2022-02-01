using SimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            client = new(textBox1.Text);
            client.Events.Connected += Events_ClientConnected;
            client.Events.Disconnected += Events_ClientDisconnected;
            client.Events.DataReceived += Events_DataReceived;
            
            btnSend.Enabled = false;
        }

        SimpleTcpClient client;

        /*@brief Events_DataReceived, Function works when data received from server.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                textBox2.Text += $"Server : {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });
            
        }

        /*@brief Events_ClientDisconnected, Function works when client disconnect to the server.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                textBox2.Text += $"Server disconnected {Environment.NewLine}";
            });
            
        }

        /*@brief Events_ClientConnected, Function works when client connect to the server.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                textBox2.Text += $"Server connected {Environment.NewLine}";
            });
            
        }

        /*@brief btnConnect_Click, Connect button function.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try 
            {
                client.Connect();
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*@brief btnSend_Click, Send button function.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    client.Send(textBox3.Text);
                    textBox2.Text += $"Me: {textBox3.Text}{Environment.NewLine}";
                    textBox3.Text = string.Empty;
                }
            }
        }

    }
}
