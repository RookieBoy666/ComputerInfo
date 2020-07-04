using System;
using System.Management;

namespace ComputerInfo
{
    /// <summary>
    /// 功能描述    ：info  
    /// 创 建 者    ：ZJP
    /// 创建日期    ：20/07/03 20:36:24 
    /// 最后修改者  ：ZJP
    /// 最后修改日期：20/07/03 20:36:24 
    /// 版本号      ：V1.0.0.0
    /// 机器名称    ：
    /// </summary>

    public class Computer
    {
        public string CpuID;
        public string MacAddress;
        public string DiskID;
        public string IpAddress;
        public string LoginUserName;
        public string ComputerName;
        public string SystemType;
        public string TotalPhysicalMemory; //单位：M  
        private static Computer _instance;
        public static Computer Instance()
        {
            if (_instance == null)
                _instance = new Computer();
            return _instance;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public Computer()
        {
            CpuID = GetCpuID();
            MacAddress = GetMacAddress();
            DiskID = GetDiskID();
            IpAddress = GetIPAddress();
            LoginUserName = GetUserName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }
        public string getCorporation()
        {
            string Corporation = "";
            string Manufacturer = "";
            ManagementClass mc2 = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc3 = mc2.GetInstances();

            if (moc3.Count != 0)
            {
                foreach (ManagementObject mo in mc2.GetInstances())
                {
                    Corporation = mo["Manufacturer"].ToString();///////////制造厂商
                }
            }

            foreach (ManagementObject m in moc3)
            {
                Manufacturer = m["model"].ToString();
            }
            return Corporation + "  " + Manufacturer;////机器型号

        }

        public string getPhysicalMemory()
        {
            string PhysicalMemory = "";
            ManagementClass m = new ManagementClass("Win32_PhysicalMemory");//内存条
            ManagementObjectCollection mn = m.GetInstances();
            PhysicalMemory = "物理内存条数量：" + mn.Count.ToString() + "  ";
            double capacity = 0.0;
            int count = 0;
            foreach (ManagementObject mo1 in mn)
            {
                count++;
                capacity = ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
                PhysicalMemory += "第" + count.ToString() + "张内存条大小：" + capacity.ToString() + "G   ";
            }
            mn.Dispose();

            return PhysicalMemory;
        }

        public string getDisplay()
        {
            string DisplayName = "";
            ManagementClass m = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection mn = m.GetInstances();
            DisplayName = "显卡数量：" + mn.Count.ToString() + "  ";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_VideoController");//Win32_VideoController 显卡
            int count = 0;
            foreach (ManagementObject mo in mos.Get())
            {
                count++;
                DisplayName += "第" + count.ToString() + "张显卡名称：" + mo["Name"].ToString() + "   ";
            }
            mn.Dispose();
            m.Dispose();


            return DisplayName;
        }

        public string getDisk()
        {
            string DiskDrive = "硬盘为：";
            ManagementClass m = new ManagementClass("win32_DiskDrive");//硬盘
            ManagementObjectCollection mn = m.GetInstances();
            double capacity = 0.0;
            foreach (ManagementObject mo1 in mn)
            {
                capacity += Int64.Parse(mo1.Properties["Size"].Value.ToString()) / 1024 / 1024 / 1024;
            }
            mn.Dispose();

            string HDSN = "";
            ManagementClass cimobject = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDSN = (string)mo.Properties["Model"].Value;
            }
            ManagementClass mc1 = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc2 = mc1.GetInstances();
            string HDID = "";
            foreach (ManagementObject mo in moc2)
            {
                HDID = mo.Properties["SerialNumber"].Value.ToString().Trim();
                break;
            }




            return HDSN + " " + HDID + "    " + DiskDrive + capacity.ToString();
        }


        public string getCpuName()
        {
            string CPUName = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");//Win32_Processor  CPU处理器
            foreach (ManagementObject mo in mos.Get())
            {
                CPUName = mo["Name"].ToString();
            }
            mos.Dispose();

            return CPUName;
        }
        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        /// <returns></returns>
        public string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码  
                string cpuInfo = "";//cpu序列号  
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取网卡硬件地址 
        /// </summary>
        /// <returns></returns>
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址  
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public string GetIPAddress()
        {
            try
            {
                //获取IP地址  
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString();  
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns></returns>
        string GetDiskID()
        {
            try
            {
                //获取硬盘ID  
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        /// <summary>  
        /// 操作系统的登录用户名  
        /// </summary>  
        /// <returns></returns>  
        public string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["UserName"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }


        /// <summary>  
        /// PC类型  
        /// </summary>  
        /// <returns></returns>  
        public string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["SystemType"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        /// <summary>  
        /// 物理内存  
        /// </summary>  
        /// <returns></returns>  
        public string GetTotalPhysicalMemory()
        {
            try
            {

                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["TotalPhysicalMemory"].ToString();

                }
                moc = null;
                mc = null;

                //st = aa.ToString();
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
        /// <summary>  
        ///    电脑名称
        /// </summary>  
        /// <returns></returns>  
        public string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }


    }
}
