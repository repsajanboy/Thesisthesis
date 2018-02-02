﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanHalfThesis.Utils
{
    internal static class NativeTimeMethod
    {
        [DllImport("api-ms-win-core-sysinfo-l1-2-1.dll")]
        internal static extern void GetLocalTime(out SYSTEMTIME lpLocalTime);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEMTIME
    {
        [MarshalAs(UnmanagedType.U2)]
        internal short Year;


        [MarshalAs(UnmanagedType.U2)]
        internal short Month;


        [MarshalAs(UnmanagedType.U2)]
        internal short DayOfWeek;


        [MarshalAs(UnmanagedType.U2)]
        internal short Day;


        [MarshalAs(UnmanagedType.U2)]
        internal short Hour;


        [MarshalAs(UnmanagedType.U2)]
        internal short Minute;


        [MarshalAs(UnmanagedType.U2)]
        internal short Second;


        [MarshalAs(UnmanagedType.U2)]
        internal short Milliseconds;

        internal DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day, Hour, Minute, Second);
        }
    }
}
