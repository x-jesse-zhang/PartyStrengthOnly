using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Clan;
using HarmonyLib;

namespace PartyStrengthOnly
{
    [HarmonyPatch(typeof(Clan), nameof(UpdateStrength))]
    internal class ClanUpdateStrengthPatch
    {
        static bool Prefix()
        {
            this.TotalStrength = 0f;
            foreach (WarPartyComponent warPartyComponent in this._warPartyComponentsCache)
            {
                this.TotalStrength += warPartyComponent.MobileParty.Party.TotalStrength;
            }

            return false;
        }
    }
}
