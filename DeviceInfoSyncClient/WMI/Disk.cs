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
        public Disk()
        {
            VolumeList = new List<DriveInfo>(DriveInfo.GetDrives());
        }

        public Int32 VolumeCount => VolumeList.Count;
        public List<DriveInfo> VolumeList { get; }
    }
}
