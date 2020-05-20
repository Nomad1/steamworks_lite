using System;
using System.Runtime.InteropServices;

namespace Steam.Lite
{
    /// <summary>
    /// This wrapper is very simple and it forbid low level access to Steam library
    /// Instead there should be managed wrappers to hide ugly names and pointers
    /// </summary>
    public static class Steamworks
    {
        private const string NativeLibraryName = "CSteamworks";
        
        [DllImport(NativeLibraryName, EntryPoint = "Shutdown", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SteamAPI_Shutdown();

        [DllImport(NativeLibraryName, EntryPoint = "IsSteamRunning", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool SteamAPI_IsSteamRunning();

        [DllImport(NativeLibraryName, EntryPoint = "RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool SteamAPI_RestartAppIfNecessary(int unOwnAppID);

        [DllImport(NativeLibraryName, EntryPoint = "Init", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool SteamAPI_Init();

        // helper objects

        [DllImport(NativeLibraryName, EntryPoint = "SteamClient_", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SteamClient();

        [DllImport(NativeLibraryName, EntryPoint = "SteamUserStats", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SteamUserStats();

        [DllImport(NativeLibraryName, EntryPoint = "SteamApps", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SteamApps();
        
        // apps
        [DllImport(NativeLibraryName, EntryPoint= "ISteamApps_GetCurrentGameLanguage", CallingConvention = CallingConvention.Cdecl)]
        private static extern string ISteamApps_GetCurrentGameLanguage();

        [DllImport(NativeLibraryName, EntryPoint = "ISteamApps_GetAvailableGameLanguages", CallingConvention = CallingConvention.Cdecl)]
        private static extern string ISteamApps_GetAvailableGameLanguages();

        [DllImport(NativeLibraryName, EntryPoint = "ISteamApps_BIsDlcInstalled", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamApps_BIsDlcInstalled(int appID);

        // stats
        
        [DllImport(NativeLibraryName, EntryPoint = "ISteamUserStats_RequestCurrentStats",  CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_RequestCurrentStats();
        
        [DllImport(NativeLibraryName, EntryPoint = "ISteamUserStats_StoreStats", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_StoreStats();

        [DllImport(NativeLibraryName, EntryPoint = "ISteamUserStats_SetStat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_SetStat(string pchName, int nData);

        [DllImport(NativeLibraryName, EntryPoint = "ISteamUserStats_SetAchievement", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool ISteamUserStats_SetAchievement(string pchName);
        


        public static bool Init(int appId)
        {
#if !DEBUG
            if (SteamAPI_RestartAppIfNecessary(appId))
            {
                Console.WriteLine("Restarting app with Steam");
                return false;
            }
#endif
            return SteamAPI_Init();
        }

        public static void Shutdown()
        {
            SteamAPI_Shutdown();
        }

        public static void SetAchievement(string name)
        {
            if (!ISteamUserStats_SetAchievement(name))
                Console.Error.WriteLine("Error settings achievement " + name);
            else
                ISteamUserStats_StoreStats();
        }

        public static void SetStat(string name, int value)
        {
            if (!ISteamUserStats_SetStat(name, value))
                Console.Error.WriteLine("Error settings stat " + name);
            else
                ISteamUserStats_StoreStats();
        }

        /// <summary>
        /// Gets current language selected for this game
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentGameLanguage()
        {
            return ISteamApps_GetCurrentGameLanguage();
        }

        /// <summary>
        /// Gets all available languages defined for this game
        /// </summary>
        /// <returns></returns>
        public static string GetAvailableGameLanguages()
        {
            return ISteamApps_GetAvailableGameLanguages();
        }

        /// <summary>
        /// Checks if DLC is installed
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public static bool IsDlcInstalled(int appID)
        {
            return ISteamApps_BIsDlcInstalled(appID);
        }

    }
}

