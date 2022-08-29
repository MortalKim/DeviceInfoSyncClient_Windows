using DeviceInfoSyncClient.WMI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceInfoSyncClient.Helpers
{
    class SystemInfoHelper
    {
        private static SystemInfoHelper? _instance = null;

        /// <summary>
        /// 静态实例
        /// </summary>
        public static SystemInfoHelper Instance
        {
            get
            {
                lock ("SystemInfo")
                {
                    if (_instance == null)
                    {
                        _instance = new SystemInfoHelper();
                    }
                    return _instance;
                }
            }
        }

        private SystemInfoHelper() { }


        public delegate void SystemInfoDelegate();

        Thread UpdateThread;
        float UpdateTime;
        SystemInfoDelegate TheSystemInfoDelegate;

        //Static info, no need to update.
        public Bios Bios = new Bios();
        public OS OS = new OS();
        public GPU GPU = new GPU();
        //can update by get now
        public RAM RAM = new RAM();
        [JsonIgnore]
        public Disk Disk = new Disk();
        public CPU CPU = new CPU();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UpdateTime">By second</param>
        /// <param name="SystemInfoDelegate">new delegate function, will override old delegate. If need add delegate, use AddDelegate</param>
        public void Start(float UpdateTime, SystemInfoDelegate SystemInfoDelegate)
        {
            this.UpdateTime = UpdateTime;
            TheSystemInfoDelegate = SystemInfoDelegate;
            UpdateThread?.Interrupt();
            UpdateThread = new Thread(UpdateInfo);
            UpdateThread.Start();
        }

        public void UpdateTimer(float NewUpdateTime)
        {
            UpdateTime = NewUpdateTime;
        }

        public void AddDelegate(SystemInfoDelegate SystemInfoDelegate)
        {
            TheSystemInfoDelegate += SystemInfoDelegate;
        }

        public void RemoveDelegate(SystemInfoDelegate SystemInfoDelegate)
        {
            if(TheSystemInfoDelegate != null)
            {
                TheSystemInfoDelegate -= SystemInfoDelegate;
            }
        }

        public void Stop()
        {
            UpdateThread.Interrupt();
        }

        void UpdateInfo()
        {
            while (true)
            {
                try
                {
                    TheSystemInfoDelegate.Invoke();
                    Thread.Sleep((int)(UpdateTime*1000));
                }
                catch (ThreadInterruptedException e)
                {
                    Debug.Print("Thread Quit");
                    break;
                }
            }
        }
    }
}
