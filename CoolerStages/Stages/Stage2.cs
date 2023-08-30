using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace CoolerStages
{
  public class Stage2
  {
    // Void Assets (some variants may have more materials but these are the base for the themes) 
    private static readonly Material locusTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCTerrainFloorInfiniteTower.mat").WaitForCompletion();
    private static readonly Material locusTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCTerrainGiantColumnsInfiniteTower.mat").WaitForCompletion();
    private static readonly Material locusDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCrystal.mat").WaitForCompletion();
    private static readonly Material locusDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matTrimSheetLemurianMetalLightInfiniteTower.mat").WaitForCompletion();
    private static readonly Material locusDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidTrim.mat").WaitForCompletion();
    private static readonly Material locusGrass = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidGrass.mat").WaitForCompletion();
    private static readonly Material locusWater = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidWater1.mat").WaitForCompletion();
    // Stasis Assets
    private static readonly PostProcessProfile stasisPP = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneMysterySpace.asset").WaitForCompletion();
    private static readonly Material stasisTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarTerrainTriplanar.mat").WaitForCompletion();
    private static readonly Material stasisTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarTerrainTriplanar.mat").WaitForCompletion();
    private static readonly Material stasisDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/mysteryspace/matMSObeliskHeart.mat").WaitForCompletion();
    private static readonly Material stasisDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetPurpleStoneBazaar.mat").WaitForCompletion();
    private static readonly Material stasisDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarIceCore.mat").WaitForCompletion();
    private static readonly Material stasisWater = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarWater.mat").WaitForCompletion();
    // Lunar Assets
    private static readonly PostProcessProfile lunarPP = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneMoonFoggy.asset").WaitForCompletion();
    private static readonly Material lunarTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonTerrain.mat").WaitForCompletion();
    private static readonly Material lunarTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonTerrainDistant.mat").WaitForCompletion();
    private static readonly Material lunarDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonStibs.mat").WaitForCompletion();
    private static readonly Material lunarDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonRuinsDirty.mat").WaitForCompletion();
    private static readonly Material lunarDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonRuinsDirtyArena.mat").WaitForCompletion();
    private static readonly Material lunarWater = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonWater.mat").WaitForCompletion();

    public static void WetlandLocus()
    {
      GameObject.Find("Directional Light (SUN)").SetActive(false);
      GameObject.Find("Reflection Probe").SetActive(false);

      GameObject voidSkybox = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion(), Vector3.zero, Quaternion.identity);
      voidSkybox.transform.Rotate(90, 0, 0);
      voidSkybox.transform.GetChild(0).GetChild(0).GetComponent<SetAmbientLight>().ApplyLighting();
      WetlandChanges(locusTerrainMat, locusTerrainMat, locusDetailMat, locusDetailMat2, locusDetailMat3, locusWater);
    }

    public static void WetlandLunar()
    {
      Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
      sunLight.color = new Color(0.6f, 0.6f, 0.6f, 1);
      // sunLight.intensity = 1f;
      sunLight.shadowStrength = 0.5f;

      GameObject voidPP = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion().transform.GetChild(0).GetChild(0).gameObject, Vector3.zero, Quaternion.identity);
      voidPP.GetComponent<PostProcessVolume>().sharedProfile = lunarPP;
      voidPP.GetComponent<SetAmbientLight>().ambientSkyColor = new Color(0.66f, 0.66f, 0.66f, 1);
      // voidPP.GetComponent<SetAmbientLight>().ambientSkyColor = Color.grey;
      voidPP.GetComponent<SetAmbientLight>().ambientIntensity = 0.5f;
      voidPP.GetComponent<SetAmbientLight>().ApplyLighting();
      WetlandChanges(lunarTerrainMat, lunarTerrainMat, lunarDetailMat, lunarDetailMat2, lunarDetailMat3, lunarWater);
    }

    public static void WetlandChanges(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material detailMat3, Material water)
    {
      Transform s = GameObject.Find("HOLDER: Skybox").transform;
      Transform terrain = GameObject.Find("HOLDER: Hero Assets").transform;

      if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3 && water)
      {
        terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
        terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
        terrain.GetChild(3).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
        terrain.GetChild(4).GetComponent<MeshRenderer>().sharedMaterial = water;
        terrain.GetChild(5).GetComponent<MeshRenderer>().sharedMaterial = water;
        terrain.GetChild(6).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        s.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
        GameObject.Find("HOLDER: Ruin Pieces").transform.GetChild(6).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
        MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Blender") || meshBase.name.Contains("Trunk")) && renderer.sharedMaterial)
            {
              renderer.sharedMaterial = detailMat;
              if (meshBase.transform.GetComponentInChildren<MeshRenderer>())
                meshBase.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = detailMat;
            }

            Transform meshParent = meshBase.transform.parent;
            if (meshParent != null)
            {
              if (meshBase.name.Contains("Mesh") && (meshParent.name.Contains("FSTree") || meshParent.name.Contains("FSRootBundle")))
                renderer.sharedMaterial = detailMat3;
              if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("FSRuinPillar"))
                renderer.sharedMaterial = detailMat2;
              if ((meshBase.name.Contains("RootBundleLargeCards") || meshBase.name.Contains("RootBundleSmallCards")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                meshBase.gameObject.SetActive(false);
              if ((meshBase.name.Contains("RootBundleLarge_LOD0") || meshBase.name.Contains("RootBundleLarge_LOD1") || meshBase.name.Contains("RootBundleLarge_LOD2") || meshBase.name.Contains("RootBundleSmall_LOD0") || meshBase.name.Contains("RootBundleSmall_LOD1") || meshBase.name.Contains("RootBundleSmall_LOD2")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                renderer.sharedMaterial = detailMat3;
            }
            if ((meshBase.name.Contains("Door") || meshBase.name.Contains("Frame")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if (meshBase.name.Contains("Ruin") && meshBase.name != "FSGiantRuinDoorCollision" && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
          }
        }
      }
    }

    public static void AqueductChanges()
    {
      Material terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain2.mat").WaitForCompletion();
      Material terrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain.mat").WaitForCompletion();
      Material detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJSandstone.mat").WaitForCompletion();
      Material detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidMetalTrimGrassy.mat").WaitForCompletion();
      Material detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion();

      if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3)
      {
        MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Terrain") && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
            if (meshBase.name.Contains("SandDune") && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat2;
            if ((meshBase.name.Contains("SandstonePillar") || meshBase.name.Contains("Dam") || meshBase.name.Contains("AqueductFullLong") || meshBase.name.Contains("AqueductPartial")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("Bridge") && !meshBase.name.Contains("Decal") || meshBase.name.Contains("RuinedRing") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Eel")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("FlagPoleMesh") || meshBase.name.Contains("RuinTile")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat3;
            if (meshBase.name.Contains("AqueductCap"))
            {
              try
              {
                Material[] sharedMaterials = renderer.sharedMaterials;
                for (int i = 0; i < sharedMaterials.Length; i++)
                  sharedMaterials[i] = detailMat2;
                renderer.sharedMaterials = sharedMaterials;
              }
              catch (System.Exception e) { Debug.LogWarning(e.Message + "\n" + e.StackTrace); };
            }
          }
        }
      }
    }

    public static void AphelianChanges()
    {
      GameObject cloud = GameObject.Find("Cloud3");
      MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
      SkinnedMeshRenderer[] sMeshList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];
      Material terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion();
      Material terrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion();
      Material detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Titan/matTitanGold.mat").WaitForCompletion();
      Material detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion();
      Material detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/snowyforest/matSFSap.mat").WaitForCompletion();

      if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3)
      {
        cloud.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          Transform meshParent = meshBase.transform.parent;
          if (meshBase != null)
          {
            if (meshParent != null)
            {
              if ((meshParent.name.Contains("TempleTop") && meshBase.name.Contains("RuinBlock") || meshBase.name.Contains("GPRuinBlockQuarter")) && renderer.sharedMaterial)
                renderer.sharedMaterial = terrainMat;
            }
            if (meshBase.name.Equals("Terrain") && renderer.sharedMaterial)
            {
              Material[] sharedMaterials = renderer.sharedMaterials;
              for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                sharedMaterials[i] = detailMat2;
              renderer.sharedMaterials = sharedMaterials;
            }
            if ((meshBase.name.Contains("Platform") || (meshBase.name.Contains("Terrain") && !meshBase.name.Equals("Terrain")) || meshBase.name.Contains("Temple") || meshBase.name.Contains("Bridge") || meshBase.name.Contains("Dirt")) && renderer.sharedMaterial)
            {
              Material[] sharedMaterials = renderer.sharedMaterials;
              for (int i = 0; i < renderer.sharedMaterials.Length; i++)
              {
                sharedMaterials[i] = terrainMat;
                if (i == 1)
                  sharedMaterials[i] = terrainMat2;
              }
              renderer.sharedMaterials = sharedMaterials;
            }
            bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("BrokenPot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pillar") || meshBase.name.Contains("Boulder") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
            if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble") || biggerProps || meshBase.name.Contains("AncientLoft_SculptureSM") || meshBase.name.Contains("FountainSM")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if (meshBase.name.Contains("CircleArchwayAnimatedMesh"))
            {
              Material[] sharedMaterials = renderer.sharedMaterials;
              for (int i = 0; i < renderer.sharedMaterials.Length; i++)
              {
                sharedMaterials[i] = terrainMat;
                if (i == 1)
                  sharedMaterials[i] = detailMat;
              }
              renderer.sharedMaterials = sharedMaterials;
            }
            if (meshBase.name.Contains("SunCloud") || meshBase.name.Contains("spmGlGrass") || meshBase.name.Contains("AncientLoftGrass") || meshBase.name.Contains("LilyPad"))
              meshBase.SetActive(false);
            if ((meshBase.name.Contains("Tile") || meshBase.name.Contains("Step")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat3;
          }
        }
        foreach (SkinnedMeshRenderer sRenderer in sMeshList)
        {
          GameObject meshBase = sRenderer.gameObject;
          if (meshBase != null)
          {
            bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Tile") || meshBase.name.Contains("RuinBlock") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pillar") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Step") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
            if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble") || biggerProps) && sRenderer.sharedMaterial)
              sRenderer.sharedMaterial = detailMat2;
          }
        }
      }
    }
  }
}
