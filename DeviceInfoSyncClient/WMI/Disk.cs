// Project: DeviceInfoSyncClient
// FileName: Disk.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using System;
using System.IO;
using System.Collections.Generic;

namespace DeviceInfoSyncClient.WMI
{
    class Disk
    {

        public Int32 VolumeCount => VolumeList.Count;
        public List<DiskInfo> VolumeList { get
            {
                var temp = new List<DriveInfo>(DriveInfo.GetDrives());
                var list = new List<DiskInfo>();
                temp.ForEach((device) =>
                {
                    list.Add(new DiskInfo(device.Name, device.DriveFormat, device.AvailableFreeSpace, device.TotalFreeSpace, device.TotalSize));
                });
                return list;
            }
        }
    }

    class DiskInfo
    {
        public string Name;
        public string DriveFormat;// = "NTFS"
        public long AvailableFreeSpace;// = 150078623744
        public long TotalFreeSpace;// = 150077030400
        public long TotalSize;// = 499242840064

        public DiskInfo(string Name, string DriveFormat, long AvailableFreeSpace, long TotalFreeSpace, long TotalSize)
        {
            this.Name = Name;
            this.DriveFormat = DriveFormat;
            this.AvailableFreeSpace = AvailableFreeSpace;
            this.TotalFreeSpace = TotalFreeSpace;
            this.TotalSize = TotalSize;
        }
    }

}
