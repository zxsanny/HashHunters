using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace HashHunters.Extensions
{
    public static class SecureStringExtensions
    {
        public static byte[] ToBytes(this SecureString s, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var chars = new Char[s.Length];
            
            //For free resources after call
            var handleChars = GCHandle.Alloc(chars, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(Marshal.SecureStringToBSTR(s), (char[])handleChars.Target, 0, s.Length);
                return encoding.GetBytes(chars);
            }
            finally
            {
                Array.Clear(chars, 0, chars.Length);
                handleChars.Free();
            }
        }
        public static string ToInsecureString(this SecureString s)
        {
            var ptr = Marshal.SecureStringToBSTR(s);
            try { return Marshal.PtrToStringBSTR(ptr); }
            finally { Marshal.ZeroFreeBSTR(ptr); }
        }

        public static SecureString ToSecureString(this string input)
        {
            var secureStr = new SecureString();
            foreach (char c in input)
            {
                secureStr.AppendChar(c);
            }
            secureStr.MakeReadOnly();
            return secureStr;
        }

        public static SecureString ToSecureString(this byte[] bytes, Encoding encoding) =>
            encoding.GetString(bytes).ToSecureString();
    }
}
