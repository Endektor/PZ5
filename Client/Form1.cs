using Shared;
using System;
using System.Runtime.Remoting;
using System.Windows.Forms;
using static Shared.SomeServer;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SomeServer objectSomeServer;
        SomeServer objectSomeServer2;
        AnotherServer objectAnotherServer;

        private void Form1_Load(object sender, EventArgs e)
        {
            RemotingConfiguration.Configure("client.exe.config", false);

            objectSomeServer = new SomeServer();
            objectAnotherServer = new AnotherServer();

            label3.Text = objectSomeServer?.Status;
            label4.Text = objectAnotherServer?.Status;
            
            objectSomeServer.RemoteEvent += new RemoteEventHandler(objectSomeServer.MultiplyBy42);
            objectSomeServer.RemoteEvent += new RemoteEventHandler(objectSomeServer.Add42);
            objectSomeServer.RemoteEvent += new RemoteEventHandler(s => ShowNum(s, textBox1));

             
            objectSomeServer2 = new SomeServer();
            objectSomeServer2.RemoteEvent += new RemoteEventHandler(objectSomeServer2.MultiplyBy1337);
            objectSomeServer2.RemoteEvent += new RemoteEventHandler(s => ShowNum(s, textBox2));
        }

        public void ShowNum(RemoteEventArg arg, TextBox textBox)
        {
            textBox.Text = arg.num.ToString();
        }

        private void buttonGetData_Click(object sender, EventArgs e)
        {
            objectSomeServer?.RaiseSomeClassEvent();
            objectSomeServer2?.RaiseSomeClassEvent();
        }

        private void buttonDispose_Click(object sender, EventArgs e)
        {
            objectSomeServer.RemoteEvent -= new RemoteEventHandler(objectSomeServer.MultiplyBy42);
            objectSomeServer.RemoteEvent -= new RemoteEventHandler(objectSomeServer.Add42);
            objectSomeServer.RemoteEvent -= new RemoteEventHandler(s => ShowNum(s, textBox1));
            objectSomeServer2.RemoteEvent -= new RemoteEventHandler(objectSomeServer2.MultiplyBy1337);
            objectSomeServer2.RemoteEvent -= new RemoteEventHandler(s => ShowNum(s, textBox2));

            objectSomeServer = null;
            objectSomeServer2 = null;
            label3.Text = "Inactive";
            label4.Text = "Inactive";

            GC.Collect();
        }

        private void buttonClearData_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
