using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LGMonitorControl
{
    public class ActiveAppInfo
    {
        public string WindowTitle { get; set; }
        public string ExeName { get; set; }
    }

    public static class WindowScanner
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

        public static ActiveAppInfo GetActiveWindowInfo()
        {
            var info = new ActiveAppInfo();
            IntPtr handle = GetForegroundWindow();

            if (handle == IntPtr.Zero)
                return info;

            // 1. Get Active Window Title
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                info.WindowTitle = Buff.ToString();
            }

            // 2. Get Active Process Executable Name Safely
            GetWindowThreadProcessId(handle, out uint pid);
            if (pid > 0)
            {
                IntPtr hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, pid);
                if (hProcess != IntPtr.Zero)
                {
                    uint size = 1024;
                    StringBuilder exePathBuilder = new StringBuilder((int)size);
                    if (QueryFullProcessImageName(hProcess, 0, exePathBuilder, ref size))
                    {
                        info.ExeName = Path.GetFileName(exePathBuilder.ToString());
                    }
                    CloseHandle(hProcess);
                }
            }

            return info;
        }

        // Kept for backwards compatibility just in case the author referenced it elsewhere
        public static string GetActiveWindowTitle()
        {
            return GetActiveWindowInfo().WindowTitle;
        }
    }
}
