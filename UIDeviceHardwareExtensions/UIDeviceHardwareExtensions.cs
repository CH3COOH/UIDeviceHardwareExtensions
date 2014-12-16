using System;
using System.Runtime.InteropServices;
using UIKit;

namespace UIKit
{
    public static class UIDeviceHardwareExtensions
    {
        public const string HardwareProperty = "hw.machine";

        [DllImport ("libc", CallingConvention = CallingConvention.Cdecl)]
        static internal extern int sysctlbyname([MarshalAs(UnmanagedType.LPStr)] string property,
            IntPtr output, IntPtr oldLen, IntPtr newp, uint newlen);

        public static string GetPlatform(this UIDevice device)
        {
            return GetPlatform();
        }

        public static string GetPlatform()
        {
            var deviceVersion = string.Empty;

            var pLength = IntPtr.Zero;
            var pString = IntPtr.Zero;
            try
            {
                pLength = Marshal.AllocHGlobal(sizeof(int));
                sysctlbyname(UIDeviceHardwareExtensions.HardwareProperty, IntPtr.Zero, pLength, IntPtr.Zero, 0);
                var length = Marshal.ReadInt32(pLength);
                if (length <= 0)
                {
                    return string.Empty;
                }

                pString = Marshal.AllocHGlobal(length);
                sysctlbyname(UIDeviceHardwareExtensions.HardwareProperty, pString, pLength, IntPtr.Zero, 0);

                deviceVersion = Marshal.PtrToStringAnsi(pString);
            }
            finally
            {
                if (pLength != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pLength);
                }
                if (pString != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pString);
                }
            }

            return deviceVersion;
        }

        // reference: https://github.com/fahrulazmi/UIDeviceHardware/blob/master/UIDeviceHardware.m
        public static string GetPlatformName(this UIDevice device)
        {
            var platform = GetPlatform();

            if (platform == "iPhone1,1")    return "iPhone 1G";
            if (platform == "iPhone1,2")    return "iPhone 3G";
            if (platform == "iPhone2,1")    return "iPhone 3GS";
            if (platform == "iPhone3,1")    return "iPhone 4 (GSM)";
            if (platform == "iPhone3,3")    return "iPhone 4 (CDMA)";
            if (platform == "iPhone4,1")    return "iPhone 4S";
            if (platform == "iPhone5,1")    return "iPhone 5 (GSM)";
            if (platform == "iPhone5,2")    return "iPhone 5 (CDMA)";
            if (platform == "iPhone5,3")    return "iPhone 5C";
            if (platform == "iPhone5,4")    return "iPhone 5C";
            if (platform == "iPhone6,1")    return "iPhone 5S";
            if (platform == "iPhone6,2")    return "iPhone 5S";
            if (platform == "iPhone7,1")    return "iPhone 6 Plus";
            if (platform == "iPhone7,2")    return "iPhone 6";

            if (platform == "iPod1,1")      return "iPod Touch 1G";
            if (platform == "iPod2,1")      return "iPod Touch 2G";
            if (platform == "iPod3,1")      return "iPod Touch 3G";
            if (platform == "iPod4,1")      return "iPod Touch 4G";
            if (platform == "iPod5,1")      return "iPod Touch 5G";

            if (platform == "iPad1,1")      return "iPad";
            if (platform == "iPad2,1")      return "iPad 2 (WiFi)";
            if (platform == "iPad2,2")      return "iPad 2 (GSM)";
            if (platform == "iPad2,3")      return "iPad 2 (CDMA)";
            if (platform == "iPad2,5")      return "iPad Mini (WiFi)";
            if (platform == "iPad2,6")      return "iPad Mini (GSM)";
            if (platform == "iPad2,7")      return "iPad Mini (CDMA)";
            if (platform == "iPad3,1")      return "iPad 3 (WiFi)";
            if (platform == "iPad3,2")      return "iPad 3 (CDMA)";
            if (platform == "iPad3,3")      return "iPad 3 (GSM)";
            if (platform == "iPad3,4")      return "iPad 4 (WiFi)";
            if (platform == "iPad3,5")      return "iPad 4 (GSM)";
            if (platform == "iPad3,6")      return "iPad 4 (CDMA)";

            if (platform == "iPad4,1")      return "iPad Air (WiFi)";
            if (platform == "iPad4,2")      return "iPad Air (GSM)";
            if (platform == "iPad4,3")      return "iPad Air (CDMA)";
            if (platform == "iPad5,3")      return "iPad Air 2 (WiFi)";
            if (platform == "iPad5,4")      return "iPad Air 2 (CDMA)";

            if (platform == "iPad4,4")      return "iPad Mini Retina (WiFi)";
            if (platform == "iPad4,5")      return "iPad Mini Retina (CDMA)";
            if (platform == "iPad4,7")      return "iPad Mini 3 (WiFi)";
            if (platform == "iPad4,8")      return "iPad Mini 3 (CDMA)";
            if (platform == "iPad4,9")      return "iPad Mini 3 (CDMA)";

            if (platform == "i386")         return device.Model;
            if (platform == "x86_64")       return device.Model;

            return platform;
        }
    }
}

