// Project: DeviceInfoSyncClient
// FileName: CPU.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using System;
using System.Diagnostics;
using DeviceInfoSyncClient.Define;

namespace DeviceInfoSyncClient.WMI
{
    public class CPU
    {
        private readonly PerformanceCounter cpuCounter;

        public CPU()
        {
            cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };

            var iter = WMIQuery.WMIExecQuery(WMIQuery.CPU.Query).GetEnumerator();
            while (iter.MoveNext())
            {
                var wmi = iter.Current;
                Name = wmi[WMIQuery.CPU.Name].ToString().Trim();
                CurrentClock = Convert.ToInt32(wmi[WMIQuery.CPU.MaxClock]);
                Voltage = Convert.ToDouble(wmi[WMIQuery.CPU.Voltage]) / 10;
                L2CacheSize = Convert.ToInt32(wmi[WMIQuery.CPU.L2CacheSize]);
                L3CacheSize = Convert.ToInt32(wmi[WMIQuery.CPU.L3CacheSize]);
                CoreCount = Convert.ToInt32(wmi[WMIQuery.CPU.NumberOfCores]);
                ThreadCount = Convert.ToInt32(wmi[WMIQuery.CPU.ThreadCount]);
            }
        }
        public Int32 Usage
        {
            get
            {
                Double percent = cpuCounter.NextValue();
                return Convert.ToInt32(percent);
            }
        }

        public String Name { get; private set; }

        public Int32 CurrentClock { get; private set; }

        public Double Voltage { get; private set; }

        public Int32 L2CacheSize { get; private set; }

        public Int32 L3CacheSize { get; private set; }

        public Int32 CoreCount { get; private set; }

        public Int32 ThreadCount { get; private set; }
    }
}
