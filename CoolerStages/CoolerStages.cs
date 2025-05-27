using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace CoolerStages
{
    [BepInPlugin("com.Nuxlar.CoolerStages", "CoolerStages", "2.0.0")]

    public class CoolerStages : BaseUnityPlugin
    {
        // Shroud Assets
        private static readonly PostProcessProfile shroudProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneArenaCleared.asset").WaitForCompletion();
        private static readonly Material shroudTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion();
        private static readonly Material shroudTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion();
        private static readonly Material shroudDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCrystal.mat").WaitForCompletion();
        private static readonly Material shroudDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCoralPlatformOrange.mat").WaitForCompletion();
        private static readonly Material shroudDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_TempleDecal2.mat").WaitForCompletion();
        private static readonly Material shroudSkyMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaSky.mat").WaitForCompletion();
        private static readonly Material shroudTreeLeaves = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidGrass.mat").WaitForCompletion();
        private static readonly Material shroudTreeFrond = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCoral.mat").WaitForCompletion();
        private static readonly GameObject shroudPlainsGrass1 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/VoidTallGrass.prefab").WaitForCompletion();
        private static readonly GameObject shroudPlainsGrass2 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/VoidTallGrassPair.prefab").WaitForCompletion();
        private static readonly GameObject shroudPlainsGrass3 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/VoidTallGrassTrio.prefab").WaitForCompletion();
        private static readonly GameObject[] shroudGrassArr = new GameObject[3] { shroudPlainsGrass1, shroudPlainsGrass2, shroudPlainsGrass3 };
        private static readonly GameObject shroudTreeReplacement = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidCamp/VoidCampKelp.prefab").WaitForCompletion();

        // RoR2/Base/Common/TrimSheets/matTrimsheetPurpleStoneSkymeadow.mat
        // RoR2/Base/intro/matColonyShipStandard.mat
        // RoR2/Base/Common/TrimSheets/matTrimSheetAlien3Wires.mat
        // RoR2/Base/Common/TrimSheets/matTrimSheetAlien1Lichen.mat
        private static readonly PostProcessProfile desolateProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneMoon.asset").WaitForCompletion();
        private static readonly Material desolateTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonTerrain.mat").WaitForCompletion();
        private static readonly Material desolateTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonTerrainDistant.mat").WaitForCompletion();
        private static readonly Material desolateDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetPurpleStoneSkymeadow.mat").WaitForCompletion();
        private static readonly Material desolateDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetAlien1Lichen.mat").WaitForCompletion();
        private static readonly Material desolateDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_TempleDecal2.mat").WaitForCompletion();
        private static readonly Material desolateSkyMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion();
        private static readonly Material desolateSkyMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matAWSkySphere.mat").WaitForCompletion();
        private static readonly Material desolateSkyMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matAWSkySphereSparks.mat").WaitForCompletion();
        private static readonly Material desolateGrassMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/LeavesInfiniteTower_0_LOD0.mat").WaitForCompletion();
        private static readonly Material desolateLeaves = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/FacingLeaves_3.mat").WaitForCompletion();
        private static readonly Material desolateFrond = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Fronds_1.mat").WaitForCompletion();
        private static readonly Material desolateBranch = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Branches_0.mat").WaitForCompletion();
        private static readonly Material desolateBranch2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Branches_0.mat").WaitForCompletion();
        private static readonly Material grassMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Branches_0.mat").WaitForCompletion();
        // RoR2/DLC1/itskymeadow/LeavesInfiniteTower_0_LOD0.mat
        private static readonly GameObject desolatePlainsGrass1 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/itskymeadow/spmSMGrassInfiniteTower.spm").WaitForCompletion();
        private static readonly GameObject desolatePlainsGrass2 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/itskymeadow/spmSMGrassInfiniteTower.spm").WaitForCompletion();
        private static readonly GameObject desolatePlainsGrass3 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/itskymeadow/spmSMGrassInfiniteTower.spm").WaitForCompletion();
        private static readonly GameObject[] desolateGrassArr = new GameObject[3] { desolatePlainsGrass1, desolatePlainsGrass2, desolatePlainsGrass3 };

        private static readonly GameObject locusSkybox = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion();

        private System.Random rng;
        private static readonly string[] whitelistedMaps = new string[] {
      "lakes",
      "snowyforest",
      "blackbeach",
      "blackbeach2",
      "golemplains",
      "golemplains2",
      "goolake",
      "foggyswamp",
      "ancientloft",
      "frozenwall",
      "wispgraveyard",
      "sulfurpools",
      "shipgraveyard",
      "rootjungle",
      "dampcavesimple",
      "skymeadow",
      "moon2"
};

        public static ConfigEntry<bool> enableWinter;
        public static ConfigEntry<bool> enableFantasy;
        public static ConfigEntry<bool> enableAuburn;
        public static ConfigEntry<bool> enableAfternoon;
        public static ConfigEntry<bool> enableDrowned;
        public static ConfigEntry<bool> enableDreary;
        private static ConfigFile CSConfig { get; set; }


        public void Awake()
        {
            CSConfig = new ConfigFile(Paths.ConfigPath + "\\com.Nuxlar.CoolerStages.cfg", true);
            enableWinter = CSConfig.Bind<bool>("General", "Enable Winter Profile", true, "White and Foggy.");
            enableFantasy = CSConfig.Bind<bool>("General", "Enable Fanstasy Profile", true, "Pink");
            enableAuburn = CSConfig.Bind<bool>("General", "Enable Auburn Profile", true, "Red-ish Brown.");
            enableAfternoon = CSConfig.Bind<bool>("General", "Enable Afternoon Profile", true, "Bright, Sunny Blue Skies.");
            enableDrowned = CSConfig.Bind<bool>("General", "Enable Drowned Profile", true, "Soft Purple/Blue.");
            enableDreary = CSConfig.Bind<bool>("General", "Enable Dreary Profile", true, "Dark, Midnight Blue.");

            rng = new System.Random();

            On.RoR2.SceneDirector.Start += SceneDirector_Start;
        }
        private static void SetDesolateAmbience()
        {
            GameObject.Find("Directional Light (SUN)").SetActive(false);
            GameObject.Find("Reflection Probe").SetActive(false);
            GameObject probe2 = GameObject.Find("Reflection Probe");
            if (probe2)
                probe2.SetActive(false);
            GameObject origAmb = GameObject.Find("PP + Amb");
            GameObject weather = GameObject.Find("Weather, Golemplains");
            if (origAmb)
                origAmb.SetActive(false);
            if (weather)
                weather.SetActive(false);
            GameObject pp = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/golemplains/Weather, Golemplains.prefab").WaitForCompletion());
            GameObject amb = pp.transform.GetChild(2).gameObject;
            PostProcessVolume ppv = amb.GetComponent<PostProcessVolume>();
            ppv.sharedProfile = desolateProfile;
            ppv.priority = 9999f;
            SetAmbientLight ambientLight = amb.GetComponent<SetAmbientLight>();
            ambientLight.skyboxMaterial = desolateSkyMat2;
            //ambientLight.ambientSkyColor = new Color(0.7f, 0.7f, 1, 1); 
            ambientLight.ambientIntensity = 0.5f;
            ambientLight.ApplyLighting();

            Light sunLight = pp.transform.GetChild(1).GetComponent<Light>();
            sunLight.color = Color.grey;
            sunLight.intensity = 1f;

            Transform skybox = Object.Instantiate(locusSkybox.transform.GetChild(6).GetChild(0), pp.transform);
            skybox.gameObject.GetComponent<MeshRenderer>().sharedMaterials = new Material[1] { desolateSkyMat3 };
        }
        private static void SetShroudAmbience()
        {
            GameObject.Find("Directional Light (SUN)").SetActive(false);
            GameObject.Find("Reflection Probe").SetActive(false);
            GameObject origAmb = GameObject.Find("PP + Amb");
            GameObject weather = GameObject.Find("Weather, Golemplains");
            if (origAmb)
                origAmb.SetActive(false);
            if (weather)
                weather.SetActive(false);

            GameObject pp = Object.Instantiate(locusSkybox);
            pp.transform.Rotate(new Vector3(180, 0, 0));

            GameObject skyboxKelp = pp.transform.GetChild(1).gameObject;
            skyboxKelp.SetActive(true);
            skyboxKelp.transform.Rotate(new Vector3(180, 0, 0));
            skyboxKelp.transform.localPosition = new Vector3(634f, 500f, 787f);

            pp.transform.GetChild(5).gameObject.SetActive(false);
            pp.transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[1] { shroudSkyMat };
            pp.transform.GetChild(6).GetChild(2).gameObject.SetActive(false);

            GameObject amb = pp.transform.GetChild(0).GetChild(0).gameObject;
            PostProcessVolume ppv = amb.GetComponent<PostProcessVolume>();
            ppv.sharedProfile = shroudProfile;
            ppv.priority = 9999f;
            SetAmbientLight ambientLight = amb.GetComponent<SetAmbientLight>();
            //ambientLight.ambientSkyColor = new Color(0.7f, 0.7f, 1, 1); 
            ambientLight.ambientIntensity = 1.4f;
            ambientLight.ApplyLighting();

            Light sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color(0.833f, 0.745f, 1f, 1);
        }

        private void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            int seed = (int)(RoR2.Run.instance.GetStartTimeUtc().Ticks ^ (long)(RoR2.Run.instance.stageClearCount << 16));
            rng = new System.Random(seed);

            string sceneName = SceneManager.GetActiveScene().name;
            SceneInfo currentScene = SceneInfo.instance;
            if (currentScene && whitelistedMaps.Contains(sceneName))
            {
                PostProcessVolume volume = currentScene.GetComponent<PostProcessVolume>();
                if (!volume || !volume.isActiveAndEnabled)
                {
                    GameObject alt = GameObject.Find("PP + Amb");
                    if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                        alt = GameObject.Find("PP, Global");
                    if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                        alt = GameObject.Find("GlobalPostProcessVolume, Base");
                    if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                        alt = GameObject.Find("PP+Amb");
                    if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                        alt = GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("SandOvercast")?.gameObject;
                    if (alt)
                        volume = alt.GetComponent<PostProcessVolume>();
                    if (sceneName == "moon2")
                    {
                        volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                        volume.profile.AddSettings<RampFog>();

                        volume.enabled = true;
                        volume.isGlobal = true;
                        volume.priority = 9999f;
                    }
                }
                if (volume)
                {
                    volume.enabled = false;

                    /* int idx = rng.Next(themeMaterials1.Count);
                     Material[] themeMaterials = themeMaterials1[idx];
                     int idx2 = rng.Next(themeMaterials2.Count);
                     Material[] themeMaterialsAlt = themeMaterials2[idx2];


                     int idx7 = rng.Next(profiles.Count);
                     PostProcessProfile testProfile = profiles[idx7];

                     */
                    switch (sceneName)
                    {
                        case "lakes":
                            // Stage1.Falls(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3Alt, mainLight.color);
                            break;
                        case "blackbeach":
                            SetDesolateAmbience();
                            // (Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material stubs, Material treeBase, Material branch, Material leaves)
                            // Stage1.Roost1(shroudTerrainMat, shroudTerrainMat2, shroudDetailMat, shroudDetailMat2, shroudDetailMat3, shroudDetailMat3, shroudDetailMat3, shroudTreeLeaves, shroudTreeReplacement);
                            Stage1.Roost1(desolateTerrainMat, desolateTerrainMat2, desolateDetailMat, desolateDetailMat2, desolateBranch, desolateBranch2, desolateLeaves);
                            break;
                        case "blackbeach2":
                            SetDesolateAmbience();
                            Stage1.Roost2(desolateTerrainMat, desolateTerrainMat2, desolateDetailMat, desolateDetailMat2, desolateBranch, desolateBranch2, desolateLeaves);
                            // Stage1.Roost2(shroudTerrainMat, shroudTerrainMat2, shroudDetailMat, shroudDetailMat2, shroudDetailMat3, shroudDetailMat3, shroudDetailMat3, shroudTreeLeaves, shroudTreeReplacement);
                            // Stage1.Roost2(testTerrainMat, testDetailMat, testDetailMat2);
                            break;
                        case "golemplains":
                            // Stage1.Plains(testTerrainMat, testDetailMat, testDetailMat2);
                            break;
                        case "golemplains2":
                            //  Stage1.Plains(testTerrainMat, testDetailMat, testDetailMat2);
                            break;
                        case "snowyforest":
                            //  Stage1.Forest(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                            break;
                        case "goolake":
                            //  Stage2.Aqueduct(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                            break;
                        case "ancientloft":
                            GameObject deepFog = GameObject.Find("DeepFog");
                            if (deepFog)
                                deepFog.SetActive(false);
                            //  Stage2.Aphelian(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                            break;
                        case "foggyswamp":
                            // Stage2.Wetland(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3Alt);
                            break;
                        case "frozenwall":
                            //   Stage3.RPD(testTerrainMat, testDetailMat, testDetailMat2);
                            break;
                        case "wispgraveyard":
                            //   Stage3.Acres(testTerrainMat, testDetailMat, testDetailMat2);
                            break;
                        case "sulfurpools":
                            GameObject.Find("SPCavePP").SetActive(false);
                            GameObject.Find("CameraRelative").transform.GetChild(1).gameObject.SetActive(false);
                            //  Stage3.Pools(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt);
                            break;
                        case "rootjungle":
                            //   Stage4.Grove(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                            break;
                        case "shipgraveyard":
                            //    Stage4.Sirens(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt);
                            break;
                        case "dampcavesimple":
                            GameObject camera = GameObject.Find("Main Camera(Clone)");
                            if (camera)
                                camera.transform.GetChild(0).GetComponent<PostProcessLayer>().breakBeforeColorGrading = true;
                            // AbyssalLighting(lightColor);
                            if (GameObject.Find("DCPPInTunnels"))
                                GameObject.Find("DCPPInTunnels").SetActive(false);
                            break;
                        case "skymeadow":
                            // Stage5.SkyMeadow(testTerrainMat, testDetailMat, testDetailMat3, testDetailMat2, mainLight.color);
                            break;
                        case "moon2":
                            Transform es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
                            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
                            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
                            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;
                            /*
                            RampFog fog = volume.profile.GetSetting<RampFog>();
                            fog.fogIntensity.value = testFog.fogIntensity.value;
                            fog.fogPower.value = testFog.fogPower.value;
                            fog.fogZero.value = testFog.fogZero.value;
                            fog.fogOne.value = testFog.fogOne.value;
                            fog.fogColorStart.value = testFog.fogColorStart.value;
                            fog.fogColorMid.value = testFog.fogColorMid.value;
                            fog.fogColorEnd.value = testFog.fogColorEnd.value;
                            fog.skyboxStrength.value = testFog.skyboxStrength.value;

                            PostProcessVolume bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
                            bruh.weight = 0.28f;
                            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
                            // 0.1138 0.1086 0.15 1
                            // 0.1012 0.1091 0.1226 1
                            bruh2.overrideAmbientColor = new Color(0.4138f, 0.4086f, 0.45f, 1);
                            bruh2.overrideDirectionalColor = new Color(0.4012f, 0.4091f, 0.4226f, 1);
                                Stage6.Moon(testTerrainMat, testDetailMat3, testDetailMat2, testDetailMat);
                            */
                            break;
                    }
                }
            }
            orig(self);
        }

        public static void AbyssalLighting(Color color)
        {
            Light[] lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                GameObject lightBase = light.gameObject;
                if (lightBase != null)
                {
                    Transform lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (light.gameObject.transform.parent.gameObject.name.Equals("DCCoralPropMediumActive"))
                        {
                            light.color = color;
                            var lightLP = light.transform.localPosition;
                            lightLP.z = 4;
                        }
                        else if (light.gameObject.transform.parent.gameObject.name.Contains("DCCrystalCluster Variant"))
                        {
                            light.color = color;
                            light.intensity *= 1.5f;
                        }
                        else if (light.gameObject.transform.parent.gameObject.name.Contains("DCCrystalLarge"))
                            light.color = color;
                    }
                    if (light.gameObject.name.Equals("CrystalLight")) light.color = color;
                }
            }
        }
    }
}