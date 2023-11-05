using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using System.Linq;

namespace CoolerStages
{
  [BepInPlugin("com.Nuxlar.CoolerStages", "CoolerStages", "1.0.0")]

  public class CoolerStages : BaseUnityPlugin
  {
    private static readonly PostProcessProfile danProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneEclipseStandard.asset").WaitForCompletion();
    private static readonly PostProcessProfile droughtProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneWispGraveyard.asset").WaitForCompletion();
    private static readonly PostProcessProfile voidProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/DLC1/Common/Void/ppSceneVoidStage.asset").WaitForCompletion();

    private static readonly Material nightTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/wispgraveyard/matWPTerrain.mat").WaitForCompletion();
    private static readonly Material nightTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/wispgraveyard/matWPTerrainRocky.mat").WaitForCompletion();
    private static readonly Material nightDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_CircleArchwayGreen.mat").WaitForCompletion();
    private static readonly Material nightDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardTempleWhiteGrassy.mat").WaitForCompletion();
    private static readonly Material nightDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/wispgraveyard/matTempleObelisk.mat").WaitForCompletion();
    private static readonly Material nightDetailMat2Alt = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardTempleWhite.mat").WaitForCompletion();

    private static readonly Material danTerrain = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion();
    private static readonly Material danDetail = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat").WaitForCompletion();
    private static readonly Material danDetail2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetAlien1BossEmission.mat").WaitForCompletion();
    private static readonly Material danDetail3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidTrim.mat").WaitForCompletion();
    // 
    private static readonly Material ruinTerrain = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TerrainInfiniteTower.mat").WaitForCompletion();
    private static readonly Material ruinDetail = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_BoulderInfiniteTower.mat").WaitForCompletion();
    private static readonly Material ruinDetail2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TempleProjectedInfiniteTower.mat").WaitForCompletion();
    private static readonly Material ruinDetail3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion();
    public static ConfigEntry<bool> shouldRollVanilla;
    private static ConfigFile CSConfig { get; set; }
    private static readonly string[] whitelistedMaps = new string[] {
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
      "skymeadow"
};
    public void Awake()
    {
      CSConfig = new ConfigFile(Paths.ConfigPath + "\\com.Nuxlar.CoolerStages.cfg", true);
      shouldRollVanilla = CSConfig.Bind<bool>("General", "Include Vanilla", true, "Add vanilla stage materials/post processing to the pool");
      On.RoR2.SceneDirector.Start += SceneDirector_Start;
    }

    private void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
    {
      string sceneName = SceneManager.GetActiveScene().name;
      SceneInfo currentScene = SceneInfo.instance;
      if (currentScene)
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
        }
        if (volume && whitelistedMaps.Contains(sceneName))
        {
          float rng = UnityEngine.Random.value;
          float chance = 0.33f;
          float chance2 = 0.66f;
          float chance3 = 0.75f;
          if (shouldRollVanilla.Value && rng > chance3)
          {
            orig(self);
            return;
          }
          volume.priority = -1;
          if (rng < chance)
          {
            Skybox.Stasis(sceneName, droughtProfile);
            Destroy(volume);
          }
          else if (rng > chance && rng < chance2)
          {
            Skybox.Night(sceneName, danProfile);
            Destroy(volume);
          }
          else
          {
            Skybox.Void(sceneName, voidProfile);
            Destroy(volume);
          }
          switch (sceneName)
          {
            case "blackbeach":
              if (rng < chance)
                Stage1.Roost1(nightTerrainMat, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage1.Roost1(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage1.Roost1(danTerrain, danDetail, danDetail2);
              break;
            case "blackbeach2":
              if (rng < chance)
                Stage1.Roost2(nightTerrainMat, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage1.Roost2(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage1.Roost2(danTerrain, danDetail, danDetail2);
              break;
            case "golemplains":
              if (rng < chance)
                Stage1.Plains(nightTerrainMat, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage1.Plains(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage1.Plains(danTerrain, danDetail, danDetail2);
              break;
            case "golemplains2":
              if (rng < chance)
                Stage1.Plains(nightTerrainMat, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage1.Plains(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage1.Plains(danTerrain, danDetail, danDetail2);
              break;
            case "snowyforest":
              GameObject.Find("Treecards").SetActive(false);
              if (rng < chance)
                Stage1.Forest(nightTerrainMat, nightDetailMat, nightDetailMat2, nightDetailMat3);
              else if (rng > chance && rng < chance2)
                Stage1.Forest(ruinTerrain, ruinDetail, ruinDetail2, ruinDetail3);
              else
                Stage1.Forest(danTerrain, danDetail, danDetail2, danDetail3);
              break;
            case "goolake":
              if (rng < chance)
                Stage2.Aqueduct(nightTerrainMat, nightDetailMat, nightDetailMat2, nightDetailMat3);
              else if (rng > chance && rng < chance2)
                Stage2.Aqueduct(ruinTerrain, ruinDetail, ruinDetail2, ruinDetail3);
              else
                Stage2.Aqueduct(danTerrain, danDetail, danDetail2, danDetail3);
              break;
            case "ancientloft":
              GameObject.Find("Sun").SetActive(false);
              if (rng < chance)
                Stage2.Aphelian(nightTerrainMat2, nightDetailMat3, nightDetailMat2, nightDetailMat);
              else if (rng > chance && rng < chance2)
                Stage2.Aphelian(ruinTerrain, ruinDetail3, ruinDetail2, ruinDetail);
              else
                Stage2.Aphelian(danTerrain, danDetail3, danDetail2, danDetail);
              break;
            case "foggyswamp":
              if (rng < chance)
                Stage2.Wetland(nightTerrainMat2, nightDetailMat, nightDetailMat2, nightDetailMat3);
              else if (rng > chance && rng < chance2)
                Stage2.Wetland(ruinTerrain, ruinDetail, ruinDetail2, ruinDetail3);
              else
                Stage2.Wetland(danTerrain, danDetail, danDetail2, danDetail3);
              break;
            case "frozenwall":
              if (rng < chance)
                Stage3.RPD(nightTerrainMat, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage3.RPD(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage3.RPD(danTerrain, danDetail, danDetail2);
              break;
            case "wispgraveyard":
              GameObject.Find("SunHolder").SetActive(false);
              if (rng < chance)
                Stage3.Acres(nightTerrainMat, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage3.Acres(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage3.Acres(danTerrain, danDetail, danDetail2);
              break;
            case "sulfurpools":
              if (rng < chance)
                Stage3.Pools(nightTerrainMat2, nightDetailMat, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage3.Pools(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage3.Pools(danTerrain, danDetail, danDetail2);
              break;
            case "rootjungle":
              if (rng < chance)
                Stage4.Grove(nightTerrainMat2, nightDetailMat, nightDetailMat2, nightDetailMat3);
              else if (rng > chance && rng < chance2)
                Stage4.Grove(ruinTerrain, ruinDetail, ruinDetail2, ruinDetail3);
              else
                Stage4.Grove(danTerrain, danDetail, danDetail2, danDetail3);
              break;
            case "shipgraveyard":
              if (rng < chance)
                Stage4.Sirens(nightTerrainMat2, nightDetailMat, nightDetailMat2Alt);
              else if (rng > chance && rng < chance2)
                Stage4.Sirens(ruinTerrain, ruinDetail, ruinDetail2);
              else
                Stage4.Sirens(danTerrain, danDetail, danDetail2);
              break;
            case "dampcavesimple":
              if (rng < chance)
                Stage4.Abyssal(nightTerrainMat2, nightDetailMat, nightDetailMat3, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage4.Abyssal(ruinTerrain, ruinDetail, ruinDetail3, ruinDetail2);
              else
                Stage4.Abyssal(danTerrain, danDetail, danDetail3, danDetail2);
              GameObject.Find("CEILING").SetActive(false);
              Destroy(GameObject.Find("DCGiantChainVariationWithCrystal (1)").GetComponent<MeshRenderer>());
              Destroy(GameObject.Find("DCGiantChainVariationWithCrystal").GetComponent<MeshRenderer>());
              if (GameObject.Find("DCPPInTunnels"))
                GameObject.Find("DCPPInTunnels").SetActive(false);
              break;
            case "skymeadow":
              if (rng < chance)
                Stage5.SkyMeadow(nightTerrainMat2, nightDetailMat, nightDetailMat3, nightDetailMat2);
              else if (rng > chance && rng < chance2)
                Stage5.SkyMeadow(ruinTerrain, ruinDetail, ruinDetail3, ruinDetail2);
              else
                Stage5.SkyMeadow(danTerrain, danDetail, danDetail3, danDetail2);
              break;
          }
        }
      }
      orig(self);
    }
  }
}