using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace HashHunters.MinerMonitor.RigClient
{
    public class ApplicationLoader
    {
        #region Constants, Structures, Enums

        public const int TOKEN_DUPLICATE = 0x0002;
        public const uint MAXIMUM_ALLOWED = 0x2000000;
        public const int CREATE_NEW_CONSOLE = 0x00000010;

        public const int IDLE_PRIORITY_CLASS = 0x40;
        public const int NORMAL_PRIORITY_CLASS = 0x20;
        public const int HIGH_PRIORITY_CLASS = 0x80;
        public const int REALTIME_PRIORITY_CLASS = 0x100;


        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int Length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
        {
            public int cb;
            public String lpReserved;
            public String lpDesktop;
            public String lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public enum TokenType
        {
            TokenPrimary = 1,
            TokenImpersonation = 2
        }

        public enum SecurityImpersonationLevel
        {
            SecurityAnonymous = 0,
            SecurityIdentification = 1,
            SecurityImpersonation = 2,
            SecurityDelegation = 3,
        }

        #endregion

        #region Win32 API Imports

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hSnapshot);

        [DllImport("kernel32.dll")]
        static extern uint WTSGetActiveConsoleSessionId();

        [DllImport("advapi32.dll", EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool CreateProcessAsUser(IntPtr hToken, String lpApplicationName, String lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandle, int dwCreationFlags, IntPtr lpEnvironment,
            String lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        static extern bool ProcessIdToSessionId(uint dwProcessId, ref uint pSessionId);

        [DllImport("advapi32.dll", EntryPoint = "DuplicateTokenEx")]
        public extern static bool DuplicateTokenEx(IntPtr ExistingTokenHandle, uint dwDesiredAccess,
            ref SECURITY_ATTRIBUTES lpThreadAttributes, int TokenType,
            int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("advapi32", SetLastError = true), SuppressUnmanagedCodeSecurity]
        static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

        #endregion

        public static bool StartProcess(string appName)
        {
            var dwSessionId = Convert.ToInt32(WTSGetActiveConsoleSessionId());
            var winLogonPId = Convert.ToUInt32(Process.GetProcessesByName("winlogon").FirstOrDefault(p => p.SessionId == dwSessionId)?.Id);

            var hUserTokenDup = IntPtr.Zero;
            var hPToken = IntPtr.Zero;

            var hProcess = OpenProcess(MAXIMUM_ALLOWED, false, winLogonPId);
            if (!OpenProcessToken(hProcess, TOKEN_DUPLICATE, ref hPToken))
            {
                CloseHandle(hProcess);
                return false;
            }

            var sa = new SECURITY_ATTRIBUTES();
            sa.Length = Marshal.SizeOf(sa);

            if (!DuplicateTokenEx(hPToken, MAXIMUM_ALLOWED, ref sa, (int)SecurityImpersonationLevel.SecurityIdentification, (int)TokenType.TokenPrimary, ref hUserTokenDup))
            {
                CloseHandle(hProcess);
                CloseHandle(hPToken);
                return false;
            }

            var si = new STARTUPINFO();
            si.cb = Marshal.SizeOf(si);

            // interactive window station parameter; basically this indicates that the process created can display a GUI on the desktop
            si.lpDesktop = @"winsta0\default";

            PROCESS_INFORMATION procInfo;
            var result = CreateProcessAsUser(hUserTokenDup,        // client's access token
                                            null,                   // file to execute
                                            appName,        // command line
                                            ref sa,                 // pointer to process SECURITY_ATTRIBUTES
                                            ref sa,                 // pointer to thread SECURITY_ATTRIBUTES
                                            false,                  // handles are not inheritable
                                            NORMAL_PRIORITY_CLASS | CREATE_NEW_CONSOLE,        // creation flags
                                            IntPtr.Zero,            // pointer to new environment block 
                                            null,                   // name of current directory 
                                            ref si,                 // pointer to STARTUPINFO structure
                                            out procInfo            // receives information about new process
                                            );
            CloseHandle(hProcess);
            CloseHandle(hPToken);
            CloseHandle(hUserTokenDup);

            return result;
        }
    }
}
