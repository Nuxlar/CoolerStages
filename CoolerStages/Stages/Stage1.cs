using UnityEngine;

namespace CoolerStages
{
    public class Stage1
    {
        public static void Falls(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Color32 color)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("TLTerrain") && !meshBase.name.Contains("Vines") && !meshBase.name.Contains("GiantFlower") && !meshBase.name.Contains("Ship") && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("TLTerrain_GiantFlower") || meshBase.name.Contains("TLTerrain_TreePads")) && renderer.sharedMaterial)
                        {
                            /*
                            if (meshBase.name.Contains("TreePads"))
                                renderer.sharedMaterials = new Material[] { terrainMat, renderer.sharedMaterials[1], renderer.sharedMaterials[2] };
                            else
                                renderer.sharedMaterials = new Material[] { terrainMat, renderer.sharedMaterials[1] };
                                */
                            foreach (Material mat in renderer.sharedMaterials)
                            {
                                mat.color = color;
                            }
                        }
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if (meshBase.name.Contains("Vines") || meshBase.name.Contains("TLRoot") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if ((meshBase.name.Contains("TLTerrain_Ship") || meshBase.name.Contains("TLArchi") || meshBase.name.Contains("TLDoor")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if (meshBase.name.Contains("TLRock") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                    }
                }
            }
            // TLTerrain without Vines/GiantFlower/TreePads/Ship/VinesLeaves
            // TLRock
            // TLArchi/TLDoor
        }

        public static void Roost1(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material treeBase, Material branch, Material leaves, GameObject treeReplacement = null)
        {
            GameObject.Find("GAMEPLAY SPACE").transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            GameObject.Find("GAMEPLAY SPACE").transform.GetChild(1).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;

            MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                GameObject meshBase = renderer.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                    {
                        meshBase.SetActive(false);
                    }
                    if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
                        renderer.sharedMaterial = detailMat;
                    if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
                        renderer.sharedMaterial = detailMat2;
                    if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
                        renderer.sharedMaterial = terrainMat;
                    if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
                    {
                        if (treeReplacement == null)
                        {
                            if (meshBase.name.Contains("Young"))
                                // leaves leaves base
                                renderer.sharedMaterials = new Material[3] { leaves, leaves, treeBase };
                            else
                                // stubs, branch, base, leaves
                                renderer.sharedMaterials = new Material[4] { leaves, branch, treeBase, leaves };
                        }
                        else
                        {
                            foreach (Transform child in meshBase.transform.parent)
                                child.gameObject.SetActive(false);
                            GameObject.Instantiate(treeReplacement, meshBase.transform.parent).transform.localPosition = Vector3.zero;
                        }
                    }
                    if (meshBase.name.Contains("Fern"))
                    {
                        GameObject.Destroy(meshBase.gameObject);
                    }
                    if (meshBase.name.Contains("Fern"))
                        meshBase.SetActive(false);
                }
            }
        }

        public static void Roost2(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material treeBase, Material branch, Material leaves, GameObject treeReplacement = null)
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
                            meshBase.SetActive(false);
                        }
                        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
                        {
                            if (treeReplacement == null)
                            {
                                if (meshBase.name.Contains("Young"))
                                    renderer.sharedMaterials = new Material[3] { treeBase, leaves, treeBase };
                                else if (meshBase.name.Contains("LOD1"))
                                    renderer.sharedMaterials = new Material[4] { leaves, branch, treeBase, leaves };
                                else
                                    renderer.sharedMaterials = new Material[4] { leaves, treeBase, leaves, leaves };
                            }
                            else
                            {
                                foreach (Transform child in meshBase.transform.parent)
                                    child.gameObject.SetActive(false);
                                GameObject.Instantiate(treeReplacement, meshBase.transform.parent).transform.localPosition = Vector3.zero;
                            }
                        }
                        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("Fern"))
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

        public static void Forest(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];

                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if (meshBase.name == "meshSnowyForestGiantTreesTops")
                            meshBase.gameObject.SetActive(false);
                        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if (meshBase.name.Contains("mdlSnowyForestTreeStump") && renderer.sharedMaterial)
                        {
                            renderer.sharedMaterial = detailMat3;
                            renderer.sharedMaterials = new Material[] { detailMat3, detailMat3 };
                        }
                    }
                }
            }
        }

    }
}
