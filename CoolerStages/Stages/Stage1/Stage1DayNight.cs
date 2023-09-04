using RoR2;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace CoolerStages
{
  public class Stage1DayNight
  {
    private static readonly PostProcessProfile ppNight = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneArenaCleared.asset").WaitForCompletion();
    private static GameObject daySkybox = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion();
    private static readonly Material terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion();
    private static readonly Material terrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrainDistant.mat").WaitForCompletion();
    private static readonly Material detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion();
    private static readonly Material detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMossyRuinsProjectedHuge.mat").WaitForCompletion();
    private static readonly Material detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMossyRuinsProjected.mat").WaitForCompletion();
    private static readonly Material detailMat4 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderHeavyMoss.mat").WaitForCompletion();
    private static readonly Material grassMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/Leaves_0.mat").WaitForCompletion();
    private static readonly GameObject grass = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/golemplains/spmGPGrass.spm").WaitForCompletion();
    private static readonly GameObject gpWeather = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/golemplains/Weather, Golemplains.prefab").WaitForCompletion();
    private static readonly Material spaceSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/intro/matIntroSkybox.mat").WaitForCompletion();
    private static readonly Material spaceStarsMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/intro/matIntroSkyboxStars.mat").WaitForCompletion();

    public static void Day(RampFog fog, string scenename, ColorGrading cgrade)
    {
      GameObject skybox = GameObject.Find("HOLDER: Skybox");
      if (scenename != "snowyforest")
      {
        skybox = UnityEngine.Object.Instantiate(daySkybox, Vector3.zero, Quaternion.identity);
        SetupDaySkybox(skybox);
      }
      Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
      sunLight.color = new Color32(253, 225, 204, 255);
      sunLight.intensity = 2.2f;
      if (!scenename.Contains("golemplains"))
      {
        GameObject pp = UnityEngine.Object.Instantiate(gpWeather.transform.GetChild(2).gameObject, skybox.transform);
        pp.GetComponent<PostProcessVolume>().priority = 9999f;
        SetAmbientLight amb = pp.GetComponent<SetAmbientLight>();
        amb.ambientSkyColor = new Color(0.5294f, 0.825f, 0.8549f, 1);
        amb.ambientIntensity = 0.9f;
        amb.ApplyLighting();
      }
      if (scenename == "blackbeach")
      {
        GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
        GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
              renderer.sharedMaterial.color = grassMat.color;
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat2;
          }
        }
      }
      if (scenename == "blackbeach2")
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
              renderer.sharedMaterial.color = grassMat.color;
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
          }
        }
      }
      if (scenename.Contains("golemplains"))
      {
        sunLight.color = new Color32(225, 255, 225, 255);
        sunLight.intensity = 1.8f;
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
              renderer.sharedMaterial.color = terrainMat.color;
          }
        }
      }
      if (scenename == "snowyforest")
      {
        // GameObject.Find("mdlSnowyForestSurroundingTrees").SetActive(false);
        GameObject.Find("CAMERA PARTICLES: SnowParticles").SetActive(false);
        // GameObject.Find("Distant Refinery").SetActive(false);
        // GameObject.Find("Godrays").SetActive(false);
        GameObject.Find("mdlSnowyForestAurora").SetActive(false);
        GameObject foliage = SceneManager.GetActiveScene().GetRootGameObjects()[3];
        if (foliage)
        {
          Transform icicles = foliage.transform.GetChild(5);
          icicles.gameObject.SetActive(false);
        }
        MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name == "meshSnowyForestGiantTreesTops")
              meshBase.gameObject.SetActive(false);
            if (renderer.sharedMaterial)
            {
              if (meshBase.name.Contains("Grass"))
                renderer.sharedMaterial.color = grassMat.color;
              if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile"))
                renderer.sharedMaterial = terrainMat2;
              if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes"))
                renderer.sharedMaterial = detailMat;
              if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate"))
                renderer.sharedMaterial = detailMat2;
              if (meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees") || meshBase.name.Contains("Barrier"))
                renderer.sharedMaterial = detailMat4;
              if (meshBase.name.Contains("mdlSnowyForestTreeStump"))
              {
                renderer.sharedMaterial = detailMat4;
                renderer.sharedMaterials = new Material[2] { detailMat4, detailMat4 };
              }
            }
          }
        }
      }
    }

    public static void Night(RampFog fog, string scenename, ColorGrading cgrade)
    {
      GameObject.Find("Directional Light (SUN)").SetActive(false);
      GameObject.Find("Reflection Probe").SetActive(false);
      GameObject origAmb = GameObject.Find("PP + Amb");
      if (origAmb)
        origAmb.SetActive(false);
      if (scenename.Contains("golemplains"))
        GameObject.Find("Weather, Golemplains").SetActive(false);
      GameObject skybox = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), Vector3.zero, Quaternion.identity);
      skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
      SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
      ambLight.ambientIntensity = 1f;
      ambLight.ApplyLighting();
      Light sunLight = skybox.transform.GetChild(1).GetComponent<Light>();
      sunLight.color = new Color(0.4863f, 0.8667f, 0.8629f, 1);
      sunLight.shadowStrength = 0.5f;
      skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
      skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { spaceSkyboxMat, spaceStarsMat };
      skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
      if (scenename == "blackbeach")
      {
        GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
              renderer.sharedMaterial.color = grassMat.color;
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat2;
          }
        }
      }
      if (scenename == "blackbeach2")
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
              renderer.sharedMaterial.color = grassMat.color;
            if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat;
            if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
              renderer.sharedMaterial = detailMat2;
            if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
              renderer.sharedMaterial = terrainMat;
          }
        }
      }
      if (scenename.Contains("golemplains"))
      {
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
              renderer.sharedMaterial.color = terrainMat.color;
          }
        }
      }
      if (scenename == "snowyforest")
      {
        GameObject.Find("Treecards").SetActive(false);
        GameObject foliage = SceneManager.GetActiveScene().GetRootGameObjects()[3];
        if (foliage)
        {
          Transform icicles = foliage.transform.GetChild(5);
          icicles.gameObject.SetActive(false);
        }
        MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name == "meshSnowyForestGiantTreesTops")
              meshBase.gameObject.SetActive(false);
            if (renderer.sharedMaterial)
            {
              if (meshBase.name.Contains("Grass"))
                renderer.sharedMaterial.color = grassMat.color;
              if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile"))
                renderer.sharedMaterial = terrainMat2;
              if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes"))
                renderer.sharedMaterial = detailMat;
              if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate"))
                renderer.sharedMaterial = detailMat2;
              if (meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees") || meshBase.name.Contains("Barrier"))
                renderer.sharedMaterial = detailMat4;
              if (meshBase.name.Contains("mdlSnowyForestTreeStump"))
              {
                renderer.sharedMaterial = detailMat4;
                renderer.sharedMaterials = new Material[2] { detailMat4, detailMat4 };
              }
            }
          }
        }
      }
    }

    public static void Night2(RampFog fog, string scenename, ColorGrading cgrade)
    {
      GameObject skybox = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), Vector3.zero, Quaternion.identity);
      skybox.transform.GetChild(0).gameObject.SetActive(false);
      skybox.transform.GetChild(1).gameObject.SetActive(false);
      skybox.transform.GetChild(2).gameObject.SetActive(false);
      skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
      skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { spaceSkyboxMat, spaceStarsMat };
      fog.fogColorStart.value = new Color(0.2157f, 0.3412f, 0.3232f, 0.0471f);
      fog.fogColorMid.value = new Color(0.2118f, 0.2915f, 0.349f, 0.5373f);
      fog.fogColorEnd.value = new Color(0.1859f, 0.2492f, 0.3216f, 0.8706f);
      fog.skyboxStrength.value = 0.057f;
      fog.fogZero.value = 0;
      fog.fogOne.value = 0.2f;
      Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
      sunLight.color = new Color(0.4863f, 0.8667f, 0.8629f, 1);
      sunLight.shadowStrength = 0.25f;
      sunLight.intensity = 1f;
      cgrade.colorFilter.value = new Color(1, 1, 1, 1);
      cgrade.colorFilter.overrideState = true;

      if (scenename.Contains("golemplains"))
      {
        MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
            {
              GameObject cunt = GameObject.Instantiate(grass, meshBase.transform);
              cunt.transform.localPosition = Vector3.zero;
              cunt.transform.localRotation = Quaternion.identity;
              GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
            }
            if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
              renderer.sharedMaterial.color = terrainMat.color;
          }
        }
      }
      if (scenename == "snowyforest")
      {
        // GameObject.Find("mdlSnowyForestSurroundingTrees").SetActive(false);
        // GameObject.Find("CAMERA PARTICLES: SnowParticles").SetActive(false);
        // GameObject.Find("Distant Refinery").SetActive(false);
        // GameObject.Find("Godrays").SetActive(false);
        // GameObject.Find("mdlSnowyForestAurora").SetActive(false);
        GameObject foliage = SceneManager.GetActiveScene().GetRootGameObjects()[3];
        if (foliage)
        {
          Transform icicles = foliage.transform.GetChild(5);
          icicles.gameObject.SetActive(false);
        }
        MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        foreach (MeshRenderer renderer in meshList)
        {
          GameObject meshBase = renderer.gameObject;
          if (meshBase != null)
          {
            if (meshBase.name == "meshSnowyForestGiantTreesTops")
              meshBase.gameObject.SetActive(false);
            if (renderer.sharedMaterial)
            {
              if (meshBase.name.Contains("Grass"))
              {
                GameObject cunt = GameObject.Instantiate(grass, meshBase.transform);
                cunt.transform.localPosition = Vector3.zero;
                cunt.transform.localRotation = Quaternion.identity;
                GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
              }
              if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile"))
                renderer.sharedMaterial = terrainMat;
              if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes"))
                renderer.sharedMaterial = detailMat;
              if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate"))
                renderer.sharedMaterial = detailMat2;
              if (meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees"))
                renderer.sharedMaterial = detailMat4;
              if (meshBase.name.Contains("mdlSnowyForestTreeStump"))
              {
                renderer.sharedMaterial = detailMat4;
                renderer.sharedMaterials = new Material[2] { detailMat4, detailMat4 };
              }
            }
          }
        }
      }
    }

    private static void SetupDaySkybox(GameObject skybox)
    {
      skybox.transform.GetChild(0).gameObject.SetActive(false);
      skybox.transform.GetChild(1).gameObject.SetActive(false);
      skybox.transform.GetChild(2).gameObject.SetActive(false);
      skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
      GameObject.Destroy(skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>());
      GameObject.Destroy(skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshFilter>());
      skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
      foreach (Transform child in skybox.transform.GetChild(4).GetChild(0).GetChild(1).transform)
      {
        if (child.gameObject.name.Contains("Icey"))
          child.gameObject.SetActive(false);
      }
    }
  }
}
