using System;
using System.Windows.Forms;
using System.Threading;

namespace Learning.Thread.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"************btnSync_click同步方法 start {Thread.Current.Manage}***********");
            Thread
        }

        private void DoSomeThingLong()
        {
            
        }
    }
}
