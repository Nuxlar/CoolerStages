using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

public class Stage1Tangle
{
  private static readonly PostProcessProfile tangleProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneMoonInCave.asset").WaitForCompletion();
  private static readonly Material tangleTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TerrainInfiniteTower.mat").WaitForCompletion();
  private static readonly Material tangleTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TerrainInfiniteTower.mat").WaitForCompletion();
  private static readonly Material tangleDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_BoulderInfiniteTower.mat").WaitForCompletion();
  private static readonly Material tangleDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TempleInfiniteTower.mat").WaitForCompletion();
  private static readonly Material tangleTreeLeaves = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/FacingLeaves_3unchangingInfiniteTower.mat").WaitForCompletion();
  private static readonly Material tangleTreeFrond = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/Fronds_ITAL.mat").WaitForCompletion();
  private static readonly GameObject tanglePlainsGrass = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/rootjungle/RJShroomSmall.prefab").WaitForCompletion();

  private static void SetAmbience()
  {
    GameObject ambHolder = new GameObject("CoolerStages: Tangle PP + Amb");
    GameObject rain = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/moon/MoonSkyboxPrefab.prefab").WaitForCompletion().transform.GetChild(0).gameObject, ambHolder.transform);
    // rain.transform.position = new Vector3(112.4f, -235.3f, -201.5f);
    rain.transform.localScale = new Vector3(5, 5, 5);

    GameObject pp = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion().transform.GetChild(0).GetChild(0).gameObject, ambHolder.transform);
    pp.GetComponent<PostProcessVolume>().sharedProfile = tangleProfile;
    SetAmbientLight ambientLight = pp.GetComponent<SetAmbientLight>();
    ambientLight.ambientSkyColor = new Color(0.52f, 0.8f, 1, 1);
    pp.GetComponent<SetAmbientLight>().ambientIntensity = 0.5f;
    pp.GetComponent<SetAmbientLight>().ApplyLighting();

    Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
    sunLight.color = new Color(0.52f, 0.8f, 1, 1);
  }

  public static void Roost1()
  {
    SetAmbience();

    GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
    GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(tanglePlainsGrass, meshBase.transform);
          cunt.transform.localScale *= 0.025f;
          cunt.transform.Rotate(new Vector3(90, 0, 0));
          GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
          GameObject.Destroy(cunt.GetComponent<MeshCollider>());
        }
        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat;
        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat2;
        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
          renderer.sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
        if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
        {
          if (meshBase.name.Contains("Young"))
            // leaves leaves base
            renderer.sharedMaterials = new Material[3] { tangleTreeLeaves, tangleTreeLeaves, tangleTreeFrond };
          else
            // stubs, branch, base, leaves
            renderer.sharedMaterials = new Material[4] { tangleTreeLeaves, tangleTreeFrond, tangleTreeFrond, tangleTreeLeaves };
        }
      }
    }
  }

  public static void Roost2()
  {
    SetAmbience();

    Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
    sunLight.color = new Color(0.52f, 0.8f, 1, 1);

    Transform terrain = GameObject.Find("HOLDER: Terrain").transform.GetChild(0);
    terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
    terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
    terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };

    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(tanglePlainsGrass, meshBase.transform);
          cunt.transform.localScale *= 0.05f;
          cunt.transform.Rotate(new Vector3(90, 0, 0));
          GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
          GameObject.Destroy(cunt.GetComponent<MeshCollider>());
        }
        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat;
        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat2;
        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
          renderer.sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
        if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
        {
          if (meshBase.name.Contains("Young"))
            renderer.sharedMaterials = new Material[3] { tangleTreeFrond, tangleTreeLeaves, tangleTreeFrond };
          else if (meshBase.name.Contains("LOD1"))
            renderer.sharedMaterials = new Material[4] { tangleTreeLeaves, tangleTreeFrond, tangleTreeFrond, tangleTreeLeaves };
          else
            renderer.sharedMaterials = new Material[4] { tangleTreeLeaves, tangleTreeFrond, tangleTreeLeaves, tangleTreeLeaves };
        }
      }
    }
  }

  public static void Plains()
  {
    SetAmbience();

    Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
    sunLight.color = new Color(0.52f, 0.8f, 1, 1);

    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(tanglePlainsGrass, meshBase.transform.parent);
          cunt.transform.localScale *= 0.05f;
          cunt.transform.Rotate(new Vector3(90, 0, 0));
          GameObject.Destroy(meshBase);
          GameObject.Destroy(cunt.GetComponent<MeshCollider>());
        }
        if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
          renderer.sharedMaterials = new Material[2] { tangleTerrainMat, tangleTerrainMat2 };
        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser")) && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat;
        if (meshBase.name.Contains("Ring") && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat2;
        if ((meshBase.name.Contains("Block") || meshBase.name.Contains("MiniBridge") || meshBase.name.Contains("Circle")) && renderer.sharedMaterial)
          renderer.sharedMaterial = tangleDetailMat2;
      }
    }
  }
}