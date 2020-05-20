#ifdef WIN32
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#endif

#include <stdio.h>
#include <stdlib.h>

#include "steam.h"

int main (int argc, char *argv[])
{
	printf("Starting test Steam app\n");

	// if your game is not in Steam yet you need to disable this code and use steam_appid.txt file with app id
#ifndef DEBUG
        if (SteamAPI_RestartAppIfNecessary(464230))
	{
		printf("Restarting app to launch with Steam\n");
	        return 0;
	}
#endif

	// this call checks Steam DLLs and looks for running steam client
	bool inited = SteamAPI_Init();

	if (!inited)
	{
		printf("Steam not inited");
		return 0;
	}

	// just sanity check, Steam should be already running at this point
	printf("Steam running: %s\n", SteamAPI_IsSteamRunning() ? "true" : "false");

	// ISteamApps is responsible for current language, settings, DLC checks, etc.
	ISteamApps * apps = SteamApps();
	printf("Apps address: %#08x\n", apps);

	// get language name. It should be one of following: danish, dutch, english, finnish, french, german, italian, koreana,
	// norwegian, polish, portuguese, russian, schinese, spanish, swedish, tchinese, ukrainian
	printf("Steam language: %s\n", apps->GetCurrentGameLanguage());

	// achievements and stats are accessed with ISteamUserStats
	ISteamUserStats * stats = SteamUserStats();
	printf("Stats address: %#08x\n", stats);

	// only needed it you want to retrieve old stats/achs
	stats->RequestCurrentStats();

	// that is what you need to add achievement to user profile
	bool achSet = stats->SetAchievement( "ach_stage_green" );
	printf("Achievement set: %s\n", achSet ? "true" : "false");

	// adding stats
	bool statSet = stats->SetStat( "complete_3_star", 50 );
	printf("Stat set: %s\n", statSet ? "true" : "false");

	// saving data
	stats->StoreStats();

	// cleaning up
	SteamAPI_Shutdown();

	return 0;
}

