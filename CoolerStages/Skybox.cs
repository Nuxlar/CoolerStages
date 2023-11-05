using RoR2;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Rewired.ComponentControls.Effects;

namespace CoolerStages
{
    public class Skybox
    {
        private static readonly Material spaceSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion();
        private static readonly Material spaceStarsMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/eclipseworld/matEclipseStarsSpheres.mat").WaitForCompletion();
        private static readonly Material altSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matAWSkySphere.mat").WaitForCompletion();

        public static void Night(string scenename, PostProcessProfile ppProfile)
        {
            Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(242, 242, 122, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.75f;
            GameObject skybox = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), Vector3.zero, Quaternion.identity);
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppProfile;
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            // ambLight.ambientSkyColor = new Color32(242, 242, 122, 255);
            // ambLight.ambientGroundColor = new Color32(242, 242, 122, 255);
            // ambLight.ambientIntensity = 1f;
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
                    if (child2.gameObject.name.Contains("Opaque") || child2.gameObject.name.Contains("Moon") || child2.gameObject.name == "Rings")
                        child2.gameObject.SetActive(false);
                }
            }
        }

        public static void Stasis(string scenename, PostProcessProfile ppProfile)
        {
            Light sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(242, 122, 122, 255);
            sunLight.intensity = 1.5f;
            sunLight.shadowStrength = 0.75f;
            GameObject skybox = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), Vector3.zero, Quaternion.identity);
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppProfile;
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.ambientSkyColor = new Color32(242, 122, 122, 255);
            // ambLight.ambientIntensity = 0.5f;
            ambLight.skyboxMaterial = altSkyboxMat;
            ambLight.ApplyLighting();
            skybox.transform.GetChild(1).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { altSkyboxMat, spaceStarsMat2 };
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(6).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
            foreach (Transform child in skybox.transform.GetChild(4).GetChild(0).GetChild(1).transform)
            {
                foreach (Transform child2 in child)
                {
                    if (child2.gameObject.name.Contains("Opaque") || child2.gameObject.name.Contains("Moon") || child2.gameObject.name == "Rings")
                        child2.gameObject.SetActive(false);
                }
            }
        }

        public static void Void(string scenename, PostProcessProfile ppProfile)
        {
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            if (sun)
                sun.SetActive(false);
            GameObject skybox = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion(), Vector3.zero, Quaternion.identity);
            skybox.transform.Rotate(new Vector3(180, 0, 0));
            skybox.transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.ApplyLighting();
        }
    }
}