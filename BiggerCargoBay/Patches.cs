using Database;
using Harmony;
using PeterHan.PLib;
using PeterHan.PLib.Options;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BiggerCargoBay
{
    public class Patches
    {
        public static class Mod_OnLoad
        {
            public static string ModPath;
            public static string ModDirectory;
            public static void OnLoad()
            {
                ModPath = Assembly.GetExecutingAssembly().Location;
                Debug.Log("Initialling LightyMod At: " + ModPath);
                ModDirectory = new FileInfo(ModPath).DirectoryName;
                if (!File.Exists(Path.Combine(ModDirectory, "config.json")))
                {
                    Debug.Log("Configuration file not exists.");
                    POptions.WriteSettings<BiggerCargoBayConfiguration>(new BiggerCargoBayConfiguration());
                    Debug.Log("Creating default configuration.");
                }
                PUtil.InitLibrary();
                POptions.RegisterOptions(typeof(BiggerCargoBayConfiguration));
            }
        }

        [HarmonyPatch(typeof(CargoBayConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class CargoBayConfigPatch
        {
            public static void Prefix()
            {
                //
            }

            public static void Postfix(ref GameObject go)
            {
                CargoBay cargoBay = go.AddOrGet<CargoBay>();
                cargoBay.storage.capacityKg = (float)BiggerCargoBayConfiguration.Instance.CargoBayCapacity;
            }
        }

        [HarmonyPatch(typeof(LiquidCargoBayConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidCargoBayConfigPatch
        {
            public static void Prefix()
            {
                //
            }

            public static void Postfix(ref GameObject go)
            {
                CargoBay cargoBay = go.AddOrGet<CargoBay>();
                cargoBay.storage.capacityKg = (float)BiggerCargoBayConfiguration.Instance.LiquidCargoBayCapacity;
            }
        }

        [HarmonyPatch(typeof(GasCargoBayConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasCargoBayConfigPatch
        {
            public static void Prefix()
            {
                //
            }

            public static void Postfix(ref GameObject go)
            {
                CargoBay cargoBay = go.AddOrGet<CargoBay>();
                cargoBay.storage.capacityKg = (float)BiggerCargoBayConfiguration.Instance.GasCargoBayCapacity;
            }
        }

        [HarmonyPatch(typeof(SpaceDestination))]
        [HarmonyPatch("UpdateRemainingResources")]
        public class SpaceDestinationMassPatch
        {
            public static bool Prefix(CargoBay bay)
            {
                return !BiggerCargoBayConfiguration.Instance.ResetPlanetMass;
            }

            public static void Postfix(SpaceDestination __instance, CargoBay bay)
            {
                if (!BiggerCargoBayConfiguration.Instance.ResetPlanetMass) return;
                var traverse = Traverse.Create(__instance);
                SpaceDestinationType destinationType = __instance.GetDestinationType();
                traverse.SetField("availableMass", destinationType.maxiumMass - __instance.CurrentMass);
            }
        }

        /*
        [HarmonyPatch(typeof(SpaceDestination))]
        [HarmonyPatch("GetMissionResourceResult")]
        public class SpaceDestinationResourcePatch
        {
            public static bool Prefix(float totalCargoSpace,
                bool solids = true,
                bool liquids = true,
                bool gasses = true)
            {
                return !BiggerCargoBayConfiguration.Instance.DontChangeRemainMass;
            }

            public static void Postfix(
                SpaceDestination __instance,
                ref Dictionary<SimHashes, float> __result,
                float totalCargoSpace,
                bool solids = true,
                bool liquids = true,
                bool gasses = true)
            {
                if (!BiggerCargoBayConfiguration.Instance.DontChangeRemainMass) return;
                Dictionary<SimHashes, float> dictionary = new Dictionary<SimHashes, float>();
                float num1 = 0.0f;
                foreach (KeyValuePair<SimHashes, float> recoverableElement in __instance.recoverableElements)
                {
                    if (ElementLoader.FindElementByHash(recoverableElement.Key).IsSolid & solids || ElementLoader.FindElementByHash(recoverableElement.Key).IsLiquid & liquids || ElementLoader.FindElementByHash(recoverableElement.Key).IsGas & gasses)
                        num1 += __instance.GetResourceValue(recoverableElement.Key, recoverableElement.Value);
                }
                float num2 = totalCargoSpace;
                foreach (KeyValuePair<SimHashes, float> recoverableElement in __instance.recoverableElements)
                {
                    if (ElementLoader.FindElementByHash(recoverableElement.Key).IsSolid & solids || ElementLoader.FindElementByHash(recoverableElement.Key).IsLiquid & liquids || ElementLoader.FindElementByHash(recoverableElement.Key).IsGas & gasses)
                    {
                        float num3 = num2 * (__instance.GetResourceValue(recoverableElement.Key, recoverableElement.Value) / num1);
                        dictionary.Add(recoverableElement.Key, 1000f);
                    }
                }
                __result = dictionary;
            }
        }
        */
    }
}
