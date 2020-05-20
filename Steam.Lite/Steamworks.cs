using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steam.Lite
{
    /// <summary>
    /// Wrapper for CSteamworks library.
    /// This wrapper is very simple and it forbid low level access to Steam library
    /// Instead there should be managed wrappers to hide ugly names and pointers
    /// </summary>
    public static class Steamworks
    {
        private const string NativeLibraryName = "CSteamworks";

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SteamAPI_Shutdown();

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_IsSteamRunning", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool SteamAPI_IsSteamRunning();

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool SteamAPI_RestartAppIfNecessary(int unOwnAppID);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool SteamAPI_Init();

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SteamAPI_RunCallbacks();

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SteamAPI_GetHSteamPipe();

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_GetHSteamUser", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SteamAPI_GetHSteamUser();

        // helper objects

        [DllImport(NativeLibraryName, EntryPoint = "SteamClient", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SteamClient();

        // client

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamClient_GetISteamUserStats", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ISteamClient_GetISteamUserStats(IntPtr steamClientPtr, IntPtr steamUserPtr, IntPtr steamPipePtr, UTF8StringHandle version);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamClient_GetISteamApps", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ISteamClient_GetISteamApps(IntPtr steamClientPtr, IntPtr steamUserPtr, IntPtr steamPipePtr, UTF8StringHandle version);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamClient_GetISteamFriends", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ISteamClient_GetISteamFriends(IntPtr steamClientPtr, IntPtr steamUserPtr, IntPtr steamPipePtr, UTF8StringHandle version);

        // apps

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamApps_GetCurrentGameLanguage", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ISteamApps_GetCurrentGameLanguage(IntPtr steamAppsPtr);

        [DllImport(NativeLibraryName, EntryPoint= "SteamAPI_ISteamApps_GetAvailableGameLanguages", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ISteamApps_GetAvailableGameLanguages(IntPtr steamAppsPtr);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamApps_BIsDlcInstalled", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamApps_BIsDlcInstalled(IntPtr steamAppsPtr, int appID);

        // stats

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestCurrentStats", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_RequestCurrentStats(IntPtr steamUserStatsPtr);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamUserStats_StoreStats", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_StoreStats(IntPtr steamUserStatsPtr);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetStatInt32", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_SetStatInt32(IntPtr steamUserStatsPtr, UTF8StringHandle pchName, int nData);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetStatFloat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_SetStatFloat(IntPtr steamUserStatsPtr, UTF8StringHandle pchName, float nData);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_SetAchievement(IntPtr steamUserStatsPtr, UTF8StringHandle pchName);

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_GetAchievement(IntPtr steamUserStatsPtr, UTF8StringHandle pchName, out bool pbAchieved);

        // friends

        [DllImport(NativeLibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ISteamFriends_GetPersonaName(IntPtr steamFriendsPtr);


        private static bool s_valid;
        private static bool s_inited;

        public static bool Inited
        {
            get { return s_inited; }
        }

        public static bool Valid
        {
            get { return s_valid; }
        }

        /// <summary>
        /// Initialize Steam with appId
        /// </summary>
        /// <returns>'true' if restart is pending</returns>
        /// <param name="appId">Steam App identifier</param>
        public static bool Init(int appId)
        {
            if (s_inited)
                return false;

            s_inited = true;

            Console.WriteLine("Initing steam with appId " + appId);

#if !DEBUG
            if (SteamAPI_RestartAppIfNecessary(appId))
            {
                Console.WriteLine("Restarting the game to work with Steam");
                
#if UNITY
                UnityEngine.Application.Quit();
#endif
                return true;
            }
#endif
            try
            {
                bool init = SteamAPI_Init();

                Console.WriteLine("SteamAPI_Init: " + init);
                s_valid = init;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception initing steam: " + ex);
                s_valid = false;
                return false;
            }

            Console.WriteLine("Steam is running: " + SteamAPI_IsSteamRunning());
            Console.WriteLine("Steam language set to: " + GetCurrentGameLanguage());
            return false;
        }

        /// <summary>
        /// Shutdown steam connector instance.
        /// </summary>
        public static void Shutdown()
        {
            if (!s_inited)
                return;

            s_inited = false;
            Console.WriteLine("Shutting down steam");

            SteamAPI_Shutdown();
        }

        /// <summary>
        /// Sets the achievement.
        /// </summary>
        /// <returns>Operation result</returns>
        /// <param name="name">Achievement id</param>
        public static bool SetAchievement(string name)
        {
            Console.WriteLine("Going to set achievement " + name);

            if (!s_inited || !s_valid)
                return false;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return false;
            }

            IntPtr stats = ISteamClient_GetISteamUserStats(client, IntPtr.Zero, IntPtr.Zero, null);

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam stats is null");
                return false;
            }

            bool result;

            using (var ptrName = new UTF8StringHandle(name))
                result = ISteamUserStats_SetAchievement(stats, ptrName);

            if (!result)
                Console.Error.WriteLine("Error settings achievement " + name);
            else
                ISteamUserStats_StoreStats(stats);

            return result;
        }


        public static bool GetAchievement(string name, out bool achieved)
        {
            achieved = false;

            Console.WriteLine("Going to get achievement " + name);

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return false;
            }

            IntPtr stats = ISteamClient_GetISteamUserStats(client, IntPtr.Zero, IntPtr.Zero, null);

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam stats is null");
                return false;
            }

            bool result;

            using (var ptrName = new UTF8StringHandle(name))
                result = ISteamUserStats_GetAchievement(stats, ptrName, out achieved);

            return true;
        }

        /// <summary>
        /// Sets the stat
        /// </summary>
        /// <returns>Operation result</returns>
        /// <param name="name">Achievement id</param>
        /// <param name="value">Value</param>
        public static bool SetStat(string name, int value, bool save = true)
        {
            Console.WriteLine("Going to set stat {0}, value {1}", name, value);

            if (!s_inited || !s_valid)
                return false;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return false;
            }

            IntPtr stats = ISteamClient_GetISteamUserStats(client, IntPtr.Zero, IntPtr.Zero, null);

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam stats is null");
                return false;
            }

            bool result;

            using (var ptrName = new UTF8StringHandle(name))
                result = ISteamUserStats_SetStatInt32(stats, ptrName, value);

            if (!result)
                Console.Error.WriteLine("Error settings stat " + name);
            else
                if (save)
                    ISteamUserStats_StoreStats(stats);

            return result;
        }

        /// <summary>
        /// Sets the stat
        /// </summary>
        /// <returns>Operation result</returns>
        /// <param name="name">Achievement id</param>
        /// <param name="value">Value</param>
        public static bool SetStat(string name, float value, bool save = true)
        {
            Console.WriteLine("Going to set stat {0}, value {1}", name, value);

            if (!s_inited || !s_valid)
                return false;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return false;
            }

            IntPtr stats = ISteamClient_GetISteamUserStats(client, IntPtr.Zero, IntPtr.Zero, null);

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam stats is null");
                return false;
            }

            bool result;

            using (var ptrName = new UTF8StringHandle(name))
                result = ISteamUserStats_SetStatFloat(stats, ptrName, value);

            if (!result)
                Console.Error.WriteLine("Error settings stat " + name);
            else
                if (save)
                    ISteamUserStats_StoreStats(stats);

            return result;
        }

        public static bool StoreStats()
        {
            if (!s_inited || !s_valid)
                return false;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return false;
            }

            IntPtr stats = ISteamClient_GetISteamUserStats(client, IntPtr.Zero, IntPtr.Zero, null);

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam stats is null");
                return false;
            }

            ISteamUserStats_StoreStats(stats);

            return true;
        }

        public static string GetCurrentGameLanguage()
        {
            if (!s_inited || !s_valid)
                return string.Empty;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return string.Empty;
            }

            IntPtr apps = ISteamClient_GetISteamApps(client, IntPtr.Zero, IntPtr.Zero, null);

            if (apps == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam apps is null");
                return string.Empty;
            }

            return PtrToStringUTF8(ISteamApps_GetCurrentGameLanguage(apps));
        }

        /// <summary>
        /// Checks if DLC is installed
        /// </summary>
        /// <param name="appID">DLC appId #</param>
        /// <returns></returns>
        public static bool IsDlcInstalled(int appID)
        {
            if (!s_inited || !s_valid)
                return false;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return false;
            }

            IntPtr apps = ISteamClient_GetISteamApps(client, IntPtr.Zero, IntPtr.Zero, null);

            if (apps == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam apps is null");
                return false;
            }

            return ISteamApps_BIsDlcInstalled(apps, appID);
        }


        public static string GetPersonaName()
        {
            if (!s_inited || !s_valid)
                return string.Empty;

            IntPtr client = SteamClient();

            if (client == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam client is null");
                return string.Empty;
            }

            IntPtr friends = ISteamClient_GetISteamFriends(client, IntPtr.Zero, IntPtr.Zero, null);

            if (friends == IntPtr.Zero)
            {
                Console.Error.WriteLine("Steam friends is null");
                return string.Empty;
            }

            return PtrToStringUTF8(ISteamFriends_GetPersonaName(friends));
        }

        public static void RunCallbacks()
        {
            if (!s_inited || !s_valid)
                return;

            SteamAPI_RunCallbacks();
        }

        #region private helpers

        private static string PtrToStringUTF8(IntPtr nativeUtf8)
        {
            if (nativeUtf8 == IntPtr.Zero)
                return string.Empty;

            const int DefaultBufferLength = 4;// 256;

            byte[] buffer = new byte[DefaultBufferLength];

            int len = 0;

            byte read;

            while ((read = Marshal.ReadByte(nativeUtf8, len)) != 0)
            {
                if (len >= buffer.Length)
                {
                    byte[] newBuffer = new byte[len + DefaultBufferLength];
                    Buffer.BlockCopy(buffer, 0, newBuffer, 0, buffer.Length);
                    buffer = newBuffer; // old one goes to GC
                }

                buffer[len++] = read;
            }

            return Encoding.UTF8.GetString(buffer, 0, len);
        }

        // Shamessly borrowed from Steamworks.Net
        private class UTF8StringHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
        {
            public UTF8StringHandle(string str)
                : base(true)
            {
                if (str == null)
                {
                    SetHandle(IntPtr.Zero);
                    return;
                }

                byte[] strbuf = new byte[Encoding.UTF8.GetByteCount(str) + 1];
                Encoding.UTF8.GetBytes(str, 0, str.Length, strbuf, 0);
                IntPtr buffer = Marshal.AllocHGlobal(strbuf.Length);
                Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

                SetHandle(buffer);
            }

            protected override bool ReleaseHandle()
            {
                if (!IsInvalid)
                    Marshal.FreeHGlobal(handle);
                return true;
            }
        }

        #endregion
    }
}


