using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Steamworks.RunServer
{
    public static class Steamworks
    {
        internal const string NativeLibraryName = "CSteamworks";
        
        [DllImport(NativeLibraryName, EntryPoint = "Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SteamAPI_Shutdown();

        [DllImport(NativeLibraryName, EntryPoint = "IsSteamRunning", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SteamAPI_IsSteamRunning();

        [DllImport(NativeLibraryName, EntryPoint = "RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SteamAPI_RestartAppIfNecessary(int unOwnAppID);

        [DllImport(NativeLibraryName, EntryPoint = "Init", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SteamAPI_Init();

        // helper objects

        [DllImport(NativeLibraryName, EntryPoint = "SteamClient_", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamClient();

        [DllImport(NativeLibraryName, EntryPoint = "SteamUserStats", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamUserStats();

        [DllImport(NativeLibraryName, EntryPoint = "SteamApps", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamApps();
        
        // apps
        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern string ISteamApps_GetCurrentGameLanguage();

        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ISteamApps_GetAvailableGameLanguages();

        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ISteamApps_BIsDlcInstalled(int appID);

        // stats
        
        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ISteamUserStats_RequestCurrentStats();
        
        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ISteamUserStats_StoreStats();

        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ISteamUserStats_SetStat(string pchName, int nData);

        [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ISteamUserStats_SetAchievement(string pchName);
        
        
        public static void Init(int appId)
        {
#if !DEBUG
            //Steamworks.SteamAPI_RestartAppIfNecessary(appId);
#endif
            Steamworks.SteamAPI_Init();
        }

        public static void Shutdown()
        {
            Steamworks.SteamAPI_Shutdown();
        }

        public static void SetAchievement(string name)
        {
            if (!Steamworks.ISteamUserStats_SetAchievement(name))
                Console.WriteLine("Error settings achievement " + name);
            else
                Steamworks.ISteamUserStats_StoreStats();
        }

        public static void SetStat(string name, int value)
        {
            if (!Steamworks.ISteamUserStats_SetStat(name, value))
                Console.WriteLine("Error settings stat " + name);
            else
                Steamworks.ISteamUserStats_StoreStats();
        }

        public static string GetLanguage()
        {
            return Steamworks.ISteamApps_GetCurrentGameLanguage();
        }
    }
}

