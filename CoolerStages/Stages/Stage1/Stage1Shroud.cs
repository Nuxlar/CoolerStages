using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

public class Stage1Shroud
{
  private static readonly PostProcessProfile shroudProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneArenaCleared.asset").WaitForCompletion();
  private static readonly Material shroudTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion();
  private static readonly Material shroudTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion();
  private static readonly Material shroudDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCoral.mat").WaitForCompletion();
  private static readonly Material shroudDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCoralPlatformOrange.mat").WaitForCompletion();
  private static readonly Material shroudDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_TempleDecal2.mat").WaitForCompletion();
  private static readonly Material shroudSkyMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaSky.mat").WaitForCompletion();
  private static readonly Material shroudTreeLeaves = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidTallGrass.mat").WaitForCompletion();
  private static readonly Material shroudTreeFrond = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidCoral.mat").WaitForCompletion();
  private static readonly GameObject shroudPlainsGrass1 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/VoidTallGrass.prefab").WaitForCompletion();
  private static readonly GameObject shroudPlainsGrass2 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/VoidTallGrassPair.prefab").WaitForCompletion();
  private static readonly GameObject shroudPlainsGrass3 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/VoidTallGrassTrio.prefab").WaitForCompletion();
  private static readonly GameObject[] shroudGrassArr = new GameObject[3] { shroudPlainsGrass1, shroudPlainsGrass2, shroudPlainsGrass3 };
  private static readonly GameObject shroudTreeReplacement = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidCamp/VoidCampKelp.prefab").WaitForCompletion();

  private static void SetAmbience()
  {
    GameObject.Find("Directional Light (SUN)").SetActive(false);
    GameObject.Find("Reflection Probe").SetActive(false);
    GameObject origAmb = GameObject.Find("PP + Amb");
    GameObject weather = GameObject.Find("Weather, Golemplains");
    if (origAmb)
      origAmb.SetActive(false);
    if (weather)
      weather.SetActive(false);

    GameObject pp = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion());
    pp.transform.Rotate(new Vector3(180, 0, 0));
    pp.transform.GetChild(1).gameObject.SetActive(true);
    pp.transform.GetChild(5).gameObject.SetActive(false);
    pp.transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[1] { shroudSkyMat };
    pp.transform.GetChild(6).GetChild(2).gameObject.SetActive(false);
    GameObject amb = pp.transform.GetChild(0).GetChild(0).gameObject;
    PostProcessVolume ppv = amb.GetComponent<PostProcessVolume>();
    ppv.sharedProfile = shroudProfile;
    ppv.priority = 9999f;
    SetAmbientLight ambientLight = amb.GetComponent<SetAmbientLight>();
    //ambientLight.ambientSkyColor = new Color(0.7f, 0.7f, 1, 1); 
    ambientLight.ambientIntensity = 1.2f;
    ambientLight.ApplyLighting();

    Light sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
    sunLight.color = new Color(0.833f, 0.745f, 1f, 1);

  }

  public static void Roost1()
  {
    SetAmbience();

    Transform skybox = GameObject.Find("SKYBOX").transform;
    skybox.GetChild(2).gameObject.SetActive(true);
    skybox.GetChild(3).gameObject.SetActive(true);
    skybox.GetChild(4).gameObject.SetActive(true);
    skybox.GetChild(5).gameObject.SetActive(true);

    // Roost1(shroudTerrainMat, shroudTerrainMat2, shroudDetailMat, shroudDetailMat2, shroudTerrainMat, shroudTreeFrond, shroudTreeFrond, shroudTreeLeaves);
    GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    GameObject[] objectList = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
    foreach (GameObject gameObject in objectList)
    {
      if (gameObject.name.Contains("spmBbConif"))
      {
        foreach (Transform child in gameObject.transform)
          GameObject.Destroy(child.gameObject);

        GameObject.Instantiate(shroudTreeReplacement, gameObject.transform).transform.localPosition = Vector3.zero;
      }
    }
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform);
          cunt.transform.localPosition = Vector3.zero;
          cunt.transform.localScale *= 0.1f;
          GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
        }
        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat2;
        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("spmBbFern2"))
          meshBase.SetActive(false);
      }
    }
  }

  public static void Roost2()
  {
    SetAmbience();

    // Roost2(shroudTerrainMat, shroudTerrainMat2, shroudDetailMat, shroudDetailMat2, shroudTerrainMat, shroudTreeFrond, shroudTreeFrond, shroudTreeLeaves);
    Transform terrain = GameObject.Find("HOLDER: Terrain").transform.GetChild(0);
    terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    GameObject[] objectList = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
    foreach (GameObject gameObject in objectList)
    {
      if (gameObject.name.Contains("spmBbConif"))
      {
        foreach (Transform child in gameObject.transform)
          GameObject.Destroy(child.gameObject);

        GameObject.Instantiate(shroudTreeReplacement, gameObject.transform).transform.localPosition = Vector3.zero;
      }
    }
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform);
          cunt.transform.localScale *= 0.25f;
          cunt.transform.localPosition = Vector3.zero;
          GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
        }
        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat2;
        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("spmBbFern2"))
          meshBase.SetActive(false);
      }
    }
  }

  public static void Plains()
  {
    SetAmbience();

    // Plains(shroudTerrainMat, shroudDetailMat, shroudDetailMat2, shroudPlainsGrass);
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform.parent);
          cunt.transform.localPosition = Vector3.zero;
          cunt.transform.localScale *= 0.25f;
          GameObject.Destroy(meshBase);
        }
        if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat;
        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if (meshBase.name.Contains("Ring") && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("Block") || meshBase.name.Contains("MiniBridge") || meshBase.name.Contains("Circle")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
      }
    }
  }

  public static void Forest()
  {
    SetAmbience();

    GameObject[] objectList = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
    foreach (GameObject gameObject in objectList)
    {
      if (gameObject.name.Contains("spmTree"))
      {
        foreach (Transform child in gameObject.transform)
          GameObject.Destroy(child.gameObject);

        GameObject.Instantiate(shroudTreeReplacement, gameObject.transform).transform.localPosition = Vector3.zero;
      }
    }
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name == "meshSnowyForestGiantTreesTops")
          meshBase.gameObject.SetActive(false);
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform.parent);
          cunt.transform.localPosition = Vector3.zero;
          cunt.transform.localScale *= 0.25f;
          GameObject.Destroy(meshBase);
        }
        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat;
        if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if (meshBase.name.Contains("meshSnowyForestGiantTrees") && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if (meshBase.name.Contains("mdlSnowyForestTreeStump") && renderer.sharedMaterial)
          renderer.sharedMaterials = new Material[2] { shroudDetailMat, shroudDetailMat };
      }
    }
  }
}