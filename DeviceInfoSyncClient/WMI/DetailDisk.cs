// Project: DeviceInfoSyncClient
// FileName: DetailDisk.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using System.Linq;
using System.Management;
using DeviceInfoSyncClient.Define;

namespace DeviceInfoSyncClient.WMI
{
    class DetailDisk
    {
        public DetailDisk()
        {   
            /*DiskPartitions = WMIQuery.WMIExecQuery(WMIQuery.Disk.LogicalToPartitionQuery)
                .GroupBy(
                    p => int.Parse(Regex.Match(p["Antecedent"].ToString(), @"(?<=Disk #)\d+").Value), // physical disk index
                    p => Regex.Match(p["Dependent"].ToString(), "(?<=DeviceID=\").*?(?=\")").Value)   // device id (Volume label)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToArray());

            LogicalDisks = WMIQuery.WMIExecQuery(WMIQuery.Disk.LogicalDiskQuery)
                .ToDictionary(
                    d => d["DeviceID"].ToString(), // device id
                    d => d);*/
            
            DiskDrives = WMIQuery.WMIExecQuery(WMIQuery.Disk.DiskDriveQuery)
                .OrderBy(d => d["Index"].ToString()) // disk index
                .ToArray();
        }

        public ManagementBaseObject[] DiskDrives { get; }
        /*public Dictionary<int, string[]> DiskPartitions { get; }
        public Dictionary<string, ManagementBaseObject> LogicalDisks { get; }*/
    }
}
