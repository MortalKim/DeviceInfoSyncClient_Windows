﻿// Project: DeviceInfoSyncClient
// FileName: CPU.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using System;
using DeviceInfoSyncClient.Define;

namespace DeviceInfoSyncClient.WMI
{
    public class CPU
    {
        public CPU()
        {
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

        public String Name { get; private set; }

        public Int32 CurrentClock { get; private set; }

        public Double Voltage { get; private set; }

        public Int32 L2CacheSize { get; private set; }

        public Int32 L3CacheSize { get; private set; }

        public Int32 CoreCount { get; private set; }

        public Int32 ThreadCount { get; private set; }
    }
}
