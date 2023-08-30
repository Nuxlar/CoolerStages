using RoR2;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace CoolerStages
{
  public class Stage1
  {
    // Void Assets (some variants may have more materials but these are the base for the themes)
    private static readonly Material locusTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidTerrain.mat").WaitForCompletion();
    private static readonly Material locusTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidMetalTrimGrassy.mat").WaitForCompletion();
    private static readonly Material locusDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCrystal.mat").WaitForCompletion();
    private static readonly Material locusDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidMetalTrimGrassyVertexColorsOnly.mat").WaitForCompletion();
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


    private static readonly Material arenaTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion();
    private static readonly Material arenaTrimMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTrim.mat").WaitForCompletion();
    private static readonly Material arenaStalagmiteMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaStalagmite.mat").WaitForCompletion();
    private static readonly Material plainsTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrainAlt.mat").WaitForCompletion();

    // NEW THEMES foetor, Tangle, Foetor, Shroud, Sluice
    // RoR2/Base/intro/matIntroSkybox.mat  

    private static void Roost1(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material stubs, Material treeBase, Material branch, Material leaves)
    {
      if (terrainMat && terrainMat2 && detailMat && detailMat2)
      {
        GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
            {
              renderer.sharedMaterial = leaves;
              meshBase.transform.localScale /= 2;
            }
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat2;
            if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
            {
              if (meshBase.name.Contains("Young"))
                // leaves leaves base
                renderer.sharedMaterials = new Material[3] { leaves, leaves, treeBase };
              else
                // stubs, branch, base, leaves
                renderer.sharedMaterials = new Material[4] { leaves, branch, treeBase, leaves };
            }
          }
        }
      }
    }

    public static void Roost2(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material stubs, Material treeBase, Material branch, Material leaves)
    {
      if (terrainMat && terrainMat2 && detailMat && detailMat2)
      {
        Transform terrain = GameObject.Find("HOLDER: Terrain").transform.GetChild(0);
        terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;

        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
            {
              renderer.sharedMaterial = leaves;
              meshBase.transform.localScale /= 2;
            }
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
            if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
            {
              if (meshBase.name.Contains("Young"))
                renderer.sharedMaterials = new Material[3] { treeBase, leaves, treeBase };
              else if (meshBase.name.Contains("LOD1"))
                renderer.sharedMaterials = new Material[4] { leaves, branch, treeBase, leaves };
              else
                renderer.sharedMaterials = new Material[4] { leaves, treeBase, leaves, leaves };
            }
            if (meshBase.name.Contains("Decal") || meshBase.name.Contains("spmBbFern2"))
              meshBase.SetActive(false);
          }
        }
      }
    }

    private static void Plains(Material terrainMat, Material detailMat, Material detailMat2, GameObject grass)
    {
      if (terrainMat && detailMat && detailMat2)
      {
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
            {
              GameObject.Instantiate(grass, meshBase.transform.parent);
              GameObject.Destroy(meshBase);
            }
            if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
            if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if (meshBase.name.Contains("Ring") && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("Block") || meshBase.name.Contains("MiniBridge") || meshBase.name.Contains("Circle")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
          }
        }
      }
    }

    private static void Forest(Material terrainMat, Material detailMat, Material detailMat2)
    {
      if (terrainMat && detailMat && detailMat2)
      {
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];

        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name == "meshSnowyForestGiantTreesTops")
              meshBase.gameObject.SetActive(false);
            if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
            if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if (meshBase.name.Contains("meshSnowyForestGiantTrees") && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if (meshBase.name.Contains("mdlSnowyForestTreeStump") && renderer.sharedMaterial)
              renderer.sharedMaterials[0] = detailMat2;
          }
        }
      }
    }

    // SIPHONED

    public static void ForestBazaar()
    {
      GameObject artifactSkybox = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/artifactworld/ArtifactWorldSkybox.prefab").WaitForCompletion();
      UnityEngine.Object.Instantiate(artifactSkybox, Vector3.zero, Quaternion.identity);
      GameObject voidPP = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion().transform.GetChild(0).GetChild(0).gameObject, Vector3.zero, Quaternion.identity);
      voidPP.GetComponent<PostProcessVolume>().sharedProfile = stasisPP;
      voidPP.GetComponent<SetAmbientLight>().ambientSkyColor = new Color(0.3804f, 0.8779f, 1, 1);
      voidPP.GetComponent<SetAmbientLight>().ambientIntensity = 0.25f;
      voidPP.GetComponent<SetAmbientLight>().ApplyLighting();
      Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
      sunLight.color = new Color(0.3804f, 0.8779f, 1, 1);
      sunLight.intensity = 2;
      sunLight.shadowStrength = 0.25f;

      Material terrainMat = stasisTerrainMat;
      Material detailMat = stasisDetailMat3;
      Material detailMat2 = stasisDetailMat2;
      Material water = stasisWater;
      Material fire = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/VFX/matFireStaticBlueLarge.mat").WaitForCompletion();
      Material detailMat4 = stasisDetailMat;
      Material detailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarHangingLights.mat").WaitForCompletion();

      if (terrainMat && detailMat && detailMat2 && water && detailMat4 && detailMat5)
      {
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        Light[] lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
        ParticleSystemRenderer[] particleList = UnityEngine.Object.FindObjectsOfType(typeof(ParticleSystemRenderer)) as ParticleSystemRenderer[];
        foreach (Light light in lightList)
        {
          GameObject lightBase = light.gameObject;
          if (lightBase && lightBase.transform.parent && lightBase.transform.parent.gameObject.name.Contains("Fire"))
          {
            light.GetComponent<FlickerLight>().initialLightIntensity = 5;
            light.color = new Color(0.3804f, 0.8779f, 1, 1);
            light.intensity = 5f;
            light.range = 100f;
          }
        }
        foreach (ParticleSystemRenderer renderer in particleList)
        {
          GameObject particleBase = renderer.gameObject;
          if (particleBase)
          {
            if (particleBase.name.Contains("Fire") && renderer.sharedMaterial)
              renderer.sharedMaterial = fire;
          }
        }
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name == "meshSnowyForestGiantTreesTops")
              meshBase.gameObject.SetActive(false);
            if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
            if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat4;
            if (meshBase.name.Contains("mdlSnowyForestTreeStump") && renderer.sharedMaterial)
            {
              renderer.sharedMaterial = detailMat4;
              renderer.sharedMaterials[1] = detailMat4;
            }
            if ((meshBase.name.Contains("mdlSFHangingLanternRope") || meshBase.name.Contains("mdlSFLanternRope")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat5;
            if ((meshBase.name == "meshSnowyForestFirepitFloor (1)" || meshBase.name.Contains("meshSnowyForestSap") || meshBase.name.Contains("goo")) && renderer.sharedMaterial)
              renderer.sharedMaterial = water;
          }
        }
      }
    }

    public static void ForestLunar()
    {
      GameObject.Find("Directional Light (SUN)").GetComponent<Light>().color = new Color(0.66f, 0.66f, 0.66f, 1);
      GameObject.Find("meshSnowyForestLeafPlane").SetActive(false);
      //GameObject.Find("Directional Light (SUN)").GetComponent<Light>().intensity = 0.3f;
      GameObject voidPP = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion().transform.GetChild(0).GetChild(0).gameObject, Vector3.zero, Quaternion.identity);
      voidPP.GetComponent<PostProcessVolume>().sharedProfile = lunarPP;
      voidPP.GetComponent<SetAmbientLight>().ambientSkyColor = new Color(0.66f, 0.66f, 0.66f, 1);
      // voidPP.GetComponent<SetAmbientLight>().ambientSkyColor = Color.grey;
      voidPP.GetComponent<SetAmbientLight>().ambientIntensity = 0.5f;
      voidPP.GetComponent<SetAmbientLight>().ApplyLighting();

      Material terrainMat = lunarTerrainMat;
      Material detailMat = lunarDetailMat;
      Material detailMat2 = lunarDetailMat2;
      // Material detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonRuinsDirtyArena.mat").WaitForCompletion();
      Material water = lunarWater;
      Material fire = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/VFX/matFireStaticBlueLarge.mat").WaitForCompletion();
      Material detailMat4 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon2/matMoonbatteryGlassOverlay.mat").WaitForCompletion();
      Material detailMat4Distortion = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon2/matMoonbatteryGlassDistortion.mat").WaitForCompletion();
      Material detailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarHangingLights.mat").WaitForCompletion();

      if (terrainMat && detailMat && detailMat2 && water && detailMat4 && detailMat5)
      {
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        Light[] lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
        ParticleSystemRenderer[] particleList = UnityEngine.Object.FindObjectsOfType(typeof(ParticleSystemRenderer)) as ParticleSystemRenderer[];
        foreach (Light light in lightList)
        {
          GameObject lightBase = light.gameObject;
          if (lightBase && lightBase.transform.parent && lightBase.transform.parent.gameObject.name.Contains("Fire"))
          {
            light.color = new Color(0.3804f, 0.8779f, 1, 1);
            light.intensity = 1f;
            light.range = 40f;
          }
        }
        foreach (ParticleSystemRenderer renderer in particleList)
        {
          GameObject particleBase = renderer.gameObject;
          if (particleBase)
          {
            if (particleBase.name.Contains("Fire") && renderer.sharedMaterial)
              renderer.sharedMaterial = fire;
          }
        }
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name == "meshSnowyForestGiantTreesTops")
              meshBase.gameObject.SetActive(false);
            if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
            if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees")) && renderer.sharedMaterial)
              renderer.sharedMaterials = new Material[2] { detailMat4, detailMat4Distortion };
            if (meshBase.name.Contains("mdlSnowyForestTreeStump") && renderer.sharedMaterial)
            {
              renderer.sharedMaterials = new Material[4] { detailMat4, detailMat4Distortion, detailMat4, detailMat4Distortion };
            }
            if ((meshBase.name.Contains("mdlSFHangingLanternRope") || meshBase.name.Contains("mdlSFLanternRope")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat5;
            if ((meshBase.name == "meshSnowyForestFirepitFloor (1)" || meshBase.name.Contains("meshSnowyForestSap") || meshBase.name.Contains("goo")) && renderer.sharedMaterial)
              renderer.sharedMaterial = water;
          }
        }
      }
    }
  }
}
