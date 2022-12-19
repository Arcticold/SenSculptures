using Database;
using FUtility;
using HarmonyLib;
using MoreSmallSculptures.FUtilityArt;
using static MoreSmallSculptures.STRINGS.BUILDINGS.PREFABS.MARBLESCULPTURE;
using static STRINGS.BUILDINGS.PREFABS;

namespace MoreSmallSculptures.Patches
{
    public class ArtableStagesPatch
    {
        //private const string ANIM_FILE = "mss_sculptures_kanim";
        private const string TARGET_DEF = MarbleSculptureConfig.ID;

        [HarmonyPatch(typeof(ArtableStages), MethodType.Constructor, typeof(ResourceSet))]
        public class TargetType_Ctor_Patch
        {
            public static void Postfix(ArtableStages __instance)
            {
                ArtHelper.GetDefaultDecors(__instance, SculptureConfig.ID, out var greatDecor, out var okayDecor, out var uglyDecor);

                __instance.Add(CreateGreatStage("aura", greatDecor, AURA.NAME, AURA.DESCRIPTION));
                __instance.Add(CreateGreatStage("nikita", greatDecor, NIKITAVARB.NAME, NIKITAVARB.DESCRIPTION));

                ArtHelper.MoveStages(
                    __instance.GetPrefabStages(TARGET_DEF),
                    Mod.Settings.MoveSculptures,
                    uglyDecor,
                    okayDecor,
                    greatDecor);
            }

            private static ArtableStage CreateStage(string stageId, int decor, string name, bool cheer, ArtableStatusItem statusItem, string description)
            {
                var id = $"{TARGET_DEF}_{stageId}";
                Log.Debuglog("ADDED STAGE " + id);
                Mod.myOverrides.Add(id);

                return new ArtableStage(
                    id,
                    name,
                    description,
                    PermitRarity.Universal,
                    stageId + "_kanim",
                    stageId,
                    decor,
                    cheer,
                    statusItem,
                    TARGET_DEF);
            }

            private static ArtableStage CreateGreatStage(string stageId, int decor, string name, string description)
            {
                return CreateStage(
                    stageId,
                    decor,
                    name,
                    true,
                    Db.Get().ArtableStatuses.LookingGreat,
                    description);
            }

            private static ArtableStage CreateBadStage(string stageId, int decor, string name, string description)
            {
                return CreateStage(
                    stageId,
                    decor,
                    name,
                    false,
                    Db.Get().ArtableStatuses.LookingUgly,
                    description);
            }

            private static ArtableStage CreateOkayStage(string stageId, int decor, string name, string description)
            {
                return CreateStage(
                    stageId,
                    decor,
                    name,
                    false,
                    Db.Get().ArtableStatuses.LookingOkay,
                    description);
            }
        }
    }
}