using Harmony;
using PeterHan.PLib;
using PeterHan.PLib.Options;
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
    }
}
