// Project: DeviceInfoSyncClient
// FileName: Untill.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using Microsoft.Win32;
using System;

namespace DeviceInfoSyncClient.Define
{
    public class Untill
    {
        public static string ReadRegistry(RegistryKey key, String root, String subkey)
        {

            key = key.OpenSubKey(root, false);

            if (key == null) return "null";

            var value = key.GetValue(subkey);
            if (value != null)
                return Convert.ToString(value);
            else
                return "null";
        }
    }
}
