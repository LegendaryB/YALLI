﻿using System;

namespace YALLI.Native.Enumerations
{
    [Flags]
    internal enum ProcessAccessFlags : uint
    {
        PROCESS_ALL_ACCESS = 0x1F0FFF,
        PROCESS_CREATE_PROCESS = 0x80,
        PROCESS_TERMINATE = 0x1,
        PROCESS_CREATE_THREAD = 0x2,
        PROCESS_VM_OPERATION = 0x8,
        PROCESS_VM_READ = 0x10,
        PROCESS_SUSPEND_RESUME = 0x800,
        PROCESS_VM_WRITE = 0x20,
        PROCESS_DUP_HANDLE = 0x40,
        PROCESS_SET_QUOTA = 0x100,
        PROCESS_SET_INFORMATION = 0x200,
        PROCESS_QUERY_INFORMATION = 0x400,
        PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
        SYNCHRONIZE = 0x100000
    }
}
