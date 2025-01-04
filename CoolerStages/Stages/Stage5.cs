using UnityEngine;

namespace CoolerStages
{
    public class Stage5
    {

        public static void SkyMeadow(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Color grassColor)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    Transform meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if ((meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = terrainMat;
                            if ((meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13") || meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground") || meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat;
                            if ((meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks") || meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat2;
                            if ((meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = terrainMat;
                        }
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        /*
                        if (meshBase.name.Contains("spmSMGrass"))
                        {
                            if (renderer.sharedMaterial != null)
                            {
                                renderer.sharedMaterial.color = grassColor;
                                if (renderer.sharedMaterials.Length >= 2)
                                    renderer.sharedMaterials[1].color = grassColor;
                            }
                        }
                        */
                        if ((meshBase.name.Contains("SMPebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlGeyser")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if (meshBase.name.Contains("SMSpikeBridge") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if (meshBase.name.Contains("Ruin") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                    }
                }
            }
        }

    }
}
