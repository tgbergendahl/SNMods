using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx.Logging;

namespace KnifeDamageMod_SN
{
    public static class KnifeDamageMod_SN
    {
        [HarmonyPatch(typeof(PlayerTool))]
        public static class PlayerTool_Patch
        {
            [HarmonyPatch(nameof(PlayerTool.Awake))]
            [HarmonyPostfix]
            public static void Awake_Prefix(PlayerTool __instance) 
            {
                if (__instance.GetType() == typeof(Knife))
                {
                    float damageMultiplier = KnifeDamagePlugin.ConfigKnifeDamageMultiplier.Value;

                    Knife knife = __instance as Knife;

                    float knifeDamage = knife.damage;
                    float newKnifeDamage = knifeDamage * damageMultiplier;
                    knife.damage = newKnifeDamage;

                    KnifeDamagePlugin.logger.Log(LogLevel.Info, $"Knife damage was: {knifeDamage}, " + $"is now: {newKnifeDamage}");
                }
            }
        }
    }
}
