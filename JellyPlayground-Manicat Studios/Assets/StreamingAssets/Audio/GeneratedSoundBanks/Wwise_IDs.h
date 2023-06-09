/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_ACCELERATION = 794457826U;
        static const AkUniqueID PLAY_BERRYCOLLECTED = 2151728835U;
        static const AkUniqueID PLAY_BREAK = 3908484415U;
        static const AkUniqueID PLAY_CLICK = 311910498U;
        static const AkUniqueID PLAY_COLLISION = 1553219250U;
        static const AkUniqueID PLAY_COUNTDOWN1 = 2669705572U;
        static const AkUniqueID PLAY_COUNTDOWN2 = 2669705575U;
        static const AkUniqueID PLAY_COUNTDOWN3 = 2669705574U;
        static const AkUniqueID PLAY_COUNTDOWN4 = 2669705569U;
        static const AkUniqueID PLAY_CROUCH = 3989731860U;
        static const AkUniqueID PLAY_JUMP = 3689126666U;
        static const AkUniqueID PLAY_MUSICMAIN = 3170845728U;
        static const AkUniqueID PLAY_SLIDE = 3715669161U;
        static const AkUniqueID STOP_ACCELERATION = 3178709332U;
        static const AkUniqueID STOP_MUSICMAIN = 1408954334U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace MENUSTATE
        {
            static const AkUniqueID GROUP = 1548586727U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID MENUPAUSE = 56378966U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MENUSTATE

        namespace MUSIC_STATES
        {
            static const AkUniqueID GROUP = 1690668539U;

            namespace STATE
            {
                static const AkUniqueID END = 529726532U;
                static const AkUniqueID LOADING = 3573931707U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PAUSEMENUMUSIC = 2478971905U;
                static const AkUniqueID SEC1 = 585140289U;
                static const AkUniqueID SEC2 = 585140290U;
                static const AkUniqueID TITLEMENUMUSIC = 508769081U;
            } // namespace STATE
        } // namespace MUSIC_STATES

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID ACCELERATION_PARAMETER = 53139121U;
        static const AkUniqueID MUSICVOLUME = 2346531308U;
        static const AkUniqueID SFXVOLUME = 988953028U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MUSICBANK = 3017428748U;
        static const AkUniqueID SFXBANK = 3017475316U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID INGAME = 984691642U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MENU = 2607556080U;
        static const AkUniqueID MUSICBUS = 2886307548U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID UI = 1551306167U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
