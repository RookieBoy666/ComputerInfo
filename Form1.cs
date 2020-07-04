using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
/// <summary>
/// 功能描述    ：Form1 
/// 创 建 者    ：ZJP
/// 创建日期    ：2020/7/1 10:48:26 
/// 最后修改者  ：ZJP
/// 最后修改日期：2020/7/1 8:48:26 
/// 版本号      ：V1.0.0.0
/// 机器名称    ：
/// </summary>
namespace ComputerInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string aaa="";
            double q = 0;
            Computer computer = new Computer();

            q = Convert.ToDouble(computer.GetTotalPhysicalMemory()) / 1024 / 1024 / 1024;
            try {
                aaa = computer.GetComputerName() + "\r\n" +
                    "产商信息:      " + computer.getCorporation() + "\r\n" +
                   "系统：" + computer.GetSystemType() + "\r\n" +
                  "磁盘: " + "固态：" + computer.getDisk() + "\r\n" +
                  "IP：" + computer.GetIPAddress() + "\r\n" +
                  "CPU序列号：" + computer.CpuID + "   " +
                  "CPU型号：" + computer.getCpuName() + "\r\n" +

                  "MAC地址:" + computer.MacAddress + "\r\n" +


                   "内存：" + q.ToString().Substring(0, 4) + "G   " +
                  computer.getPhysicalMemory() + "\r\n" +

                  "显卡：" + computer.getDisplay();
                  //  System.Windows.Forms.Application.ProductVersion.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
           
            textBox1.Text = aaa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Log.RegisterLog(textBox1.Text.ToString() + "\r\n" + "\r\n", DateTime.Now.ToString());
                Log.RegisterLog("                                             ", "                           ");
                MessageBox.Show("写入日志成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate () { new About().ShowDialog(); });
            th.Start();

        }

        //public string getVersion()
        //{
        //    var serverFileVersion = "";
        //    try
        //    {
        //        FileVersionInfo fileVerInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ComputerInfo);
        //        //版本号显示为“主版本号.次版本号.内部版本号.专用部件号”。
        //        serverFileVersion = String.Format("{0}.{1}.{2}.{3}", fileVerInfo.FileMajorPart, fileVerInfo.FileMinorPart, fileVerInfo.FileBuildPart, fileVerInfo.FilePrivatePart);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());   
        //    }
        //    return serverFileVersion;

        //}
    }
}
