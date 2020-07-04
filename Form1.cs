using System;
using System.Diagnostics;
using System.Windows.Forms;

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

                   "显卡：" + computer.getDisplay()+
                    System.Windows.Forms.Application.ProductVersion.ToString();
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
