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

namespace TCPServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnSend.Enabled = false;
            server = new SimpleTcpServer(textBox1.Text);
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataReceived;

        }
        SimpleTcpServer server;

        /*@brief btnSend_Click, Send button function.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                if(!string.IsNullOrEmpty(textBox3.Text) && lstClientIP.SelectedItem != null)
                {
                    server.Send(lstClientIP.SelectedItem.ToString(), textBox3.Text);
                    tBoxMessage.Text += $"Server: {textBox3.Text}{Environment.NewLine}";
                    textBox3.Text = String.Empty;
                }
            }
        }

        /*@brief Events_DataReceived, Function works when data received from client.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Received event data
         * 
         * @retval None
         */
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                tBoxMessage.Text += $"{e.IpPort}:{Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });
                
        }

        /*@brief Events_ClientDisconnected, Function works when server disconnect from client.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                tBoxMessage.Text += $"{e.IpPort} disconnected.{Environment.NewLine}";
                lstClientIP.Items.Remove(e.IpPort);
            });
            
            
        }

        /*@brief Events_ClientConnected, Function works when server connects to the client.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tBoxMessage.Text += $"{e.IpPort} connected.{Environment.NewLine}";
                lstClientIP.Items.Add(e.IpPort);
            });
            
        }

        /*@brief btnStart_Click_1, Start button function.
         * 
         * @param sender = Contains reference of the object
         * @param e      = Event data
         * 
         * @retval None
         */
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            server.Start();
            tBoxMessage.Text += $"Starting ...{Environment.NewLine}";
            btnStart.Enabled = false;
            btnSend.Enabled = true;
        }
    }
}
