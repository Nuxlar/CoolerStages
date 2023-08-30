using BepInEx;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using System;

namespace CoolerStages
{
  [BepInPlugin("com.Nuxlar.CoolerStages", "CoolerStages", "1.0.0")]

  public class CoolerStages : BaseUnityPlugin
  {

    public void Awake()
    {
      On.RoR2.Stage.Start += Stage_Start;
    }

    private void Stage_Start(On.RoR2.Stage.orig_Start orig, Stage self)
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
        if (volume)
        {
          RampFog rampFog = volume.profile.GetSetting<RampFog>();

          ColorGrading colorGrading = volume.profile.GetSetting<ColorGrading>() ?? volume.profile.AddSettings<ColorGrading>();
          switch (sceneName)
          {
            case "blackbeach":
              Stage1DayNight.Night(rampFog, sceneName, colorGrading);
              break;
            case "blackbeach2":
              Stage1DayNight.Night(rampFog, sceneName, colorGrading);
              break;
            case "golemplains":
              Stage1DayNight.Night(rampFog, sceneName, colorGrading);
              break;
            case "golemplains2":
              Stage1DayNight.Night(rampFog, sceneName, colorGrading);
              break;
            case "snowyforest":
              Stage1DayNight.Night(rampFog, sceneName, colorGrading);
              break;
          }

        }
      }

      /*
    if (sceneName == "moon2")
    {
      volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
      volume.profile.AddSettings<RampFog>();

      volume.enabled = true;
      volume.isGlobal = true;
      volume.priority = 9999f;
    }*/

      orig(self);
    }

    private void RollVariant(Action action1, Action action2, Action action3)
    {
      /*
      float percent = 1f / 4f;
      float random = UnityEngine.Random.value;
      if (random < percent)
        action1();
      else if (random < (percent * 2))
        action2();
      else if (random < (percent * 3))
        action3();
        */
      float percent = 1f / 3f;
      float random = UnityEngine.Random.value;
      if (random < percent)
        action1();
      else if (random < (percent * 2))
        action2();
      else
        action3();
    }

  }
}