using RoR2;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace CoolerStages
{
    public class Skybox
    {
        private static readonly Material spaceSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion();
        private static readonly Material spaceStarsMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/eclipseworld/matEclipseStarsSpheres.mat").WaitForCompletion();
        private static readonly Material altSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matAWSkySphere.mat").WaitForCompletion();
        private static readonly GameObject ruinSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), "RuinSkybox", false);
        private static readonly GameObject scorchedSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), "ScorchedSkybox", false);
        private static readonly GameObject voidSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), "VoidSkybox", false);

        public static void Night(PostProcessProfile ppProfile)
        {
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            GameObject probe = GameObject.Find("Reflection Probe");
            if ((bool)sun)
            {
                Light sunLight = sun.GetComponent<Light>();
                sunLight.color = new Color32(242, 242, 122, 255);
                sunLight.intensity = 1f;
                sunLight.shadowStrength = 0.8f;
            }
            if ((bool)probe)
                probe.SetActive(false);
            GameObject skybox = Object.Instantiate(ruinSkybox, Vector3.zero, Quaternion.identity);
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppProfile;
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.skyboxMaterial = spaceSkyboxMat;
            ambLight.ApplyLighting();
            skybox.transform.GetChild(1).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { spaceSkyboxMat, spaceStarsMat2 };
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(6).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
            foreach (Transform child in skybox.transform.GetChild(4).GetChild(0).GetChild(1).transform)
            {
                foreach (Transform child2 in child)
                {
                    if (child2.gameObject.name.Contains("Opaque") || child2.gameObject.name.Contains("Moon") || child2.gameObject.name.Contains("Rings"))
                        child2.gameObject.SetActive(false);
                }
            }
        }

        public static void Stasis(PostProcessProfile ppProfile)
        {
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            GameObject probe = GameObject.Find("Reflection Probe");
            if ((bool)sun)
            {
                Light sunLight = sun.GetComponent<Light>();
                sunLight.color = new Color32(242, 122, 122, 255);
                sunLight.intensity = 2f;
                sunLight.shadowStrength = 0.75f;
            }
            if ((bool)probe)
                probe.SetActive(false);
            GameObject skybox = Object.Instantiate(scorchedSkybox, Vector3.zero, Quaternion.identity);
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppProfile;
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.ambientSkyColor = new Color32(242, 122, 122, 255);
            ambLight.skyboxMaterial = altSkyboxMat;
            ambLight.ApplyLighting();
            skybox.transform.GetChild(1).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { altSkyboxMat, spaceStarsMat2 };
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).gameObject.SetActive(false);

        }

        public static void Void()
        {
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            GameObject probe = GameObject.Find("Reflection Probe");
            if (sun)
                sun.SetActive(false);
            if ((bool)probe)
                probe.SetActive(false);
            GameObject skybox = Object.Instantiate(voidSkybox, Vector3.zero, Quaternion.identity);
            skybox.transform.Rotate(new Vector3(180, 0, 0));
            skybox.transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.ApplyLighting();
        }
    }
}