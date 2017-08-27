using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace _2FMTester
{
    class DMA_Access
    {
        //DLL Callers. Should not be touched unless necessary.
        [DllImport("kernel32.dll", SetLastError = false)]
        public static extern Boolean ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] Byte[] lpBuffer, Int32 dwSize, out Int32 BytesRead);
        [DllImport("kernel32.dll", SetLastError = false)]
        public static extern Boolean WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] Byte[] lpBuffer, Int32 dwSize, out Int32 BytesWrite);
        [DllImport("kernel32.dll", SetLastError = false)]
        public static extern IntPtr OpenProcess(uint dwDesiredAddress, bool bInheritHandle, int dwProcessId);

        //Tells others what is what
        public static System.Diagnostics.Process PCSX2;
        public static System.IntPtr PCSX2_Iptr;
        public static System.Int32 bytesread = 0;

        //Seeks PCSX2
        public static bool SeekPCSX2()
        {
            System.Diagnostics.Process[] allProcesses = System.Diagnostics.Process.GetProcesses();
            try
            {
                System.Diagnostics.Process.GetProcessById(PCSX2.Id);
                return true;
            }
            catch
            {

                PCSX2_Iptr = (System.IntPtr)0;
                foreach (System.Diagnostics.Process IsPCSX2 in allProcesses)
                {
                    try
                    {
                        if (IsPCSX2.MainWindowTitle.Length > 0 && IsPCSX2.ProcessName.Contains("pcsx2"))
                        {
                            PCSX2 = IsPCSX2;
                            PCSX2_Iptr = OpenProcess(0x001F0FFF, true, PCSX2.Id);
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        //Read Stuff from PS2 Random Access Memory.
        public static byte[] ReadBytes(System.Int64 address, System.Int32 size)
        {
            System.Byte[] output = new System.Byte[size];
            ReadProcessMemory(PCSX2_Iptr, new System.IntPtr(address), output, output.Length, out bytesread);
            return output;
        }

        //Write Stuff to PS2 Random Access Memory.
        public static void WriteBytes(System.Int64 address, System.Byte[] data)
        {
            SeekPCSX2();
            WriteProcessMemory(PCSX2_Iptr, new System.IntPtr(address), data, data.Length, out bytesread);
        }

        //Integer Reader.
        public static System.Int32 ReadInteger(System.Int64 address)
        {
            return System.BitConverter.ToInt32(ReadBytes(address, 4), 0);
        }

        //Write Float to PCSX2 RAM
        public static void WriteFloat(int address, float value)
        {
            WriteBytes(address, BitConverter.GetBytes(value));
        }

        //Read Integer32 from PCSX2 RAM
        int ReadInt32(int address)
        {
            return BitConverter.ToInt32(ReadBytes(address, 4), 0);
        }

    }
}
