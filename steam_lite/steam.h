//====== Copyright © 1996-2008, Valve Corporation, All rights reserved. =======
//
// Modified by: Nomad, RunServer, 2016
// Purpose: Cleaned up and stripped version of Steam API that gives access to 
//          achievements, stats and current settings.
//
//=============================================================================

#ifndef STEAM_API_H
#define STEAM_API_H

#ifdef _WIN32
#pragma once
#pragma comment(lib, "steam_api.lib")
#endif

#if defined( _WIN32 )
#define S_API extern "C" __declspec( dllimport ) 
#else
#define S_API extern "C" 
#endif

#define S_CALLTYPE __cdecl

typedef int int32;
typedef unsigned int uint32;
typedef unsigned long long int uint64;
typedef uint32 DepotId_t;
typedef uint32 AppId_t;
typedef void * CSteamID;

class ISteamApps
{
public:
	virtual bool BIsSubscribed() = 0;
	virtual bool BIsLowViolence() = 0;
	virtual bool BIsCybercafe() = 0;
	virtual bool BIsVACBanned() = 0;
	virtual const char *GetCurrentGameLanguage() = 0;
	virtual const char *GetAvailableGameLanguages() = 0;
	virtual bool BIsSubscribedApp( AppId_t appID ) = 0;
	virtual bool BIsDlcInstalled( AppId_t appID ) = 0;
	virtual uint32 GetEarliestPurchaseUnixTime( AppId_t nAppID ) = 0;
	virtual bool BIsSubscribedFromFreeWeekend() = 0;
	virtual int GetDLCCount() = 0;
	virtual bool BGetDLCDataByIndex( int iDLC, AppId_t *pAppID, bool *pbAvailable, char *pchName, int cchNameBufferSize ) = 0;
	virtual void InstallDLC( AppId_t nAppID ) = 0;
	virtual void UninstallDLC( AppId_t nAppID ) = 0;
	virtual void RequestAppProofOfPurchaseKey( AppId_t nAppID ) = 0;
	virtual bool GetCurrentBetaName( char *pchName, int cchNameBufferSize ) = 0;
	virtual bool MarkContentCorrupt( bool bMissingFilesOnly ) = 0;
	virtual uint32 GetInstalledDepots( AppId_t appID, DepotId_t *pvecDepots, uint32 cMaxDepots ) = 0;
	virtual uint32 GetAppInstallDir( AppId_t appID, char *pchFolder, uint32 cchFolderBufferSize ) = 0;
	virtual bool BIsAppInstalled( AppId_t appID ) = 0;
	virtual CSteamID GetAppOwner() = 0;
	virtual const char *GetLaunchQueryParam( const char *pchKey ) = 0;
	virtual bool GetDlcDownloadProgress( AppId_t nAppID, uint64 *punBytesDownloaded, uint64 *punBytesTotal ) = 0; 
	virtual int GetAppBuildId() = 0;
};

class ISteamUserStats
{
public:
	virtual bool RequestCurrentStats() = 0;
	virtual bool GetStat( const char *pchName, int32 *pData ) = 0;
	virtual bool GetStat( const char *pchName, float *pData ) = 0;
	virtual bool SetStat( const char *pchName, int32 nData ) = 0;
	virtual bool SetStat( const char *pchName, float fData ) = 0;
	virtual bool UpdateAvgRateStat( const char *pchName, float flCountThisSession, double dSessionLength ) = 0;
	virtual bool GetAchievement( const char *pchName, bool *pbAchieved ) = 0;
	virtual bool SetAchievement( const char *pchName ) = 0;
	virtual bool ClearAchievement( const char *pchName ) = 0;
	virtual bool GetAchievementAndUnlockTime( const char *pchName, bool *pbAchieved, uint32 *punUnlockTime ) = 0;
	virtual bool StoreStats() = 0;
	virtual int GetAchievementIcon( const char *pchName ) = 0;
	virtual const char *GetAchievementDisplayAttribute( const char *pchName, const char *pchKey ) = 0;
	virtual bool IndicateAchievementProgress( const char *pchName, uint32 nCurProgress, uint32 nMaxProgress ) = 0;
	virtual uint32 GetNumAchievements() = 0;
	virtual const char *GetAchievementName( uint32 iAchievement ) = 0;
/*
stripped out
...*/
};

S_API bool S_CALLTYPE SteamAPI_Init();
S_API void S_CALLTYPE SteamAPI_Shutdown();
S_API bool S_CALLTYPE SteamAPI_IsSteamRunning();
S_API bool S_CALLTYPE SteamAPI_RestartAppIfNecessary( uint32 unOwnAppID );
S_API void S_CALLTYPE SteamAPI_RunCallbacks();

S_API ISteamUserStats *S_CALLTYPE SteamUserStats();
S_API ISteamApps *S_CALLTYPE SteamApps();

#endif	