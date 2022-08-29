// Project: DeviceInfoSyncClient
// FileName: RAM.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using System;
using System.Diagnostics;
using System.Management;
using DeviceInfoSyncClient.Define;

namespace DeviceInfoSyncClient.WMI
{
    class RAM
    {
        private long m_PhysicalMemory = 0;   //物理内存 

        public RAM()
        {
            var iter = WMIQuery.WMIExecQuery(WMIQuery.RAM.Query).GetEnumerator();
            while (iter.MoveNext())
            {
                var wmi = iter.Current;
                Speed = wmi[WMIQuery.RAM.Speed].ToString();
                Voltage = wmi[WMIQuery.RAM.ConfiguredVoltage].ToString().Insert(1, ".");
                Manufacturer = wmi[WMIQuery.RAM.Manufacturer].ToString();
            }

            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    m_PhysicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString());
                }
            }

            //double available = MemoryAvailable / 1024.0 / 1024.0 / 1014.0;
            //double used = (PhysicalMemory - MemoryAvailable) / 1024.0 / 1024.0 / 1024.0;
            //double physicalMemory = PhysicalMemory / 1024.0 / 1024.0 / 1024.0;
            //Debug.Print("MemoryAvailable：" + String.Format("{0:f2}GB", available));
            //Debug.Print("PhysicalMemory：" + String.Format("{0:f2}GB", physicalMemory));
            //Debug.Print("Pre：" + String.Format("{0:f2}/{1:f2}GB({2:f0}%)", used, physicalMemory, (PhysicalMemory - MemoryAvailable) * 100.0 / PhysicalMemory));
        }

        public long AvailablePhysicalSize => MemoryAvailable;

        //public UInt64 AvailableVirtualSize => hardwareInfo.MemoryStatus.AvailableVirtual;

        public String Speed { get; private set; }

        public String Voltage { get; private set; }

        public String Manufacturer { get; private set; }

        public long used { get
            {
                return (PhysicalMemory - MemoryAvailable);
            } 
        }

        public double usage { get
            {
                return used * 100.0 / PhysicalMemory;
            }
        }

        #region MemoryAvailable

        ///  
        /// Get MemoryAvailable
        ///  
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory"); 
                //foreach (ManagementObject mo in mos.Get()) 
                //{ 
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString()); 
                //} 
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                    }
                }
                return availablebytes;
            }
        }

        #endregion

        #region PhysicalMemory

        ///  
        /// get PhysicalMemory
        ///  
        public long PhysicalMemory
        {
            get
            {
                return m_PhysicalMemory;
            }
        }

        #endregion 

    }

}
