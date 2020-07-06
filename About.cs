using System;
using System.Windows.Forms;
/// <summary>
/// 功能描述    ：About
/// 创 建 者    ：ZJP
/// 创建日期    ：2020/7/4 18:24:26 
/// 最后修改者  ：ZJP
/// 最后修改日期：2020/7/4 18:24:26 
/// 版本号      ：V1.0.0.0
/// 机器名称    ：
/// </summary>
namespace ComputerInfo
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

 

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/RookieBoy666"); 
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
      
            System.Diagnostics.Process.Start("explorer.exe", "  https://github.com/RookieBoy666/ComputerInfo");
        }
    }
}
