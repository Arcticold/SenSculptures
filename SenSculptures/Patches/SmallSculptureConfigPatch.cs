﻿using HarmonyLib;
using MoreSmallSculptures.FUtilityArt.Components;
using UnityEngine;

namespace MoreSmallSculptures.Patches
{
    public class SmallSculptureConfigPatch
    {
        [HarmonyPatch(typeof(SmallSculptureConfig), "DoPostConfigureComplete")]
        public class SmallSculptureConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddComponent<ArtOverride>().extraStages = Mod.myOverrides;
                go.AddComponent<ArtOverrideRestorer>().fallback = "Default";
                go.GetComponent<KPrefabID>().prefabSpawnFn += g => Mod.artRestorers.Add(g.GetComponent<ArtOverrideRestorer>());
            }
        }
    }
}