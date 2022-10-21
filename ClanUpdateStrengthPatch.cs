using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;

namespace PartyStrengthOnly
{

    [HarmonyPatch(typeof(Clan), "UpdateStrength")]
    internal class ClanUpdateStrengthPatch
    {

        static AccessTools.FieldRef<Clan, float> totalStrength = AccessTools.FieldRefAccess<Clan, float>("<TotalStrength>k__BackingField");

        static void Postfix(Clan __instance)
        {
                float fiefStrength = 0f;

                foreach (Town town in __instance.Fiefs)
                {
                    if (town.GarrisonParty != null)
                    {
                        fiefStrength += town.GarrisonParty.Party.TotalStrength;
                    }
                }

                float expectedStrength = __instance.TotalStrength - fiefStrength;

                totalStrength(__instance) = expectedStrength;
        }
    }
}
