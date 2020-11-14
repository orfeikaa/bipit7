using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatClient.ServiceChat;

namespace ChatClient
{
    public partial class Form1 : Form, IService1Callback
    {
        public bool isConnected = false;
        Service1Client client;
        int ID;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();

            }
            else
            {
                ConnectUser();
            }
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new Service1Client(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(textBox1.Text);
                textBox1.Enabled = false;
                isConnected = true;
                button1.Text = "Disconnect";

                client = new Service1Client(new System.ServiceModel.InstanceContext(this));
                string[] soobshenia = client.Istoria_list();
                listBox1.Items.Clear();
                for (int i = 0; i < soobshenia.Length; i++)
                {
                    listBox1.Items.Add(soobshenia[i]);
                }
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                textBox1.Enabled = true;
                isConnected = false;
                button1.Text = "Connect";
            }
        }

        public void MsgCallBack(string messag)
        {
            listBox1.Items.Add(messag);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectUser();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(textBox2.Text, ID);
                    textBox2.Text = string.Empty; 
                }
            }
        }
    }
}
