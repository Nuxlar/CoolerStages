# Cooler Stages

### SUBMIT ANY ISSUES USING THE LINK ABOVE

Might work with StageAesthetic or CoolerEclipse idk. Randomzied, 10 material themes and 9 post processing profiles leading to around 100 theme combos per stage for stages 1-6, doesn't work on modded stages.

If you don't like the variant, `set_scene sceneName` (with DebugToolkit) and it'll give a new randomized theme.

Stage materials and post process profile print to the console on stage load. `alt + ctrl + ~` to open the console

![Roost](https://cdn.discordapp.com/attachments/968891050187972658/1216543298152566924/image.png?ex=6600c53c&is=65ee503c&hm=135c6d42a18f15f062185203ca374aff2e8cc510ab3ea0f44d850aae6a1d3ff8&)
![Aphelian](https://cdn.discordapp.com/attachments/968891050187972658/1216549350147358830/image.png?ex=6600cadf&is=65ee55df&hm=cc6096af3f9aa2b67ca0851574db404adfb07c84337155617f19ac4b934d3d6e&)
![Pools](https://cdn.discordapp.com/attachments/968891050187972658/1216550146280657028/image.png?ex=6600cb9d&is=65ee569d&hm=0fa300419bd82aab70b4008de3096c1d2697666f64f3fc136afd82603e7bde9a&)
![Abyssal](https://cdn.discordapp.com/attachments/968891050187972658/1216544236774625311/image.png?ex=6600c61c&is=65ee511c&hm=e5ba779a15c24ef2ef3c827a475d45259cee87b4d78e1f9f7624a1ecdb85d2f9&)
![Meadow](https://cdn.discordapp.com/attachments/968891050187972658/1216552062373073017/image.png?ex=6600cd66&is=65ee5866&hm=40a69ee6140cd9c422e9e3b0d4ce8ca4256f5901c7e87a749d3b30cf832002e0&)
![Moon](https://cdn.discordapp.com/attachments/968891050187972658/1216541826647982211/image.png?ex=6600c3dd&is=65ee4edd&hm=b3a93abd11c6e5d55d6a4a9304e9382dcf3a6356f4263bc39c2b3181d31565ad&)

## Credits

[StageAesthetic](https://thunderstore.io/package/HIFU/StageAesthetic/) by HIFU

## Changelog

**1.8.0**

- Removes/Replaces post processing profiles that were causing darker stages
- Removes 1 material theme (Locus) because the terrain was wonky
- Enhances color lightening method
- Adds conditions to account for specific materials/post processing causing darker stages
- Fixes certain stages accounting for wrong terrain material

**1.7.0**

- Adds ambient lighting that matches dynamic color
- Increases brighten threshold on darkness check
- Re-Fixes moon fog having terrain material (phantom walls at mass)
- Replaces README photos

**1.6.3**

- Changes "Scorched" terrain material to one that's less monotone
- Changes Abyssal material matching
- Fixes "Void Meadow" tree material
- Fixes Abyssal being dark a lot of the time
- Fixes moon material matching not applying to everything (soul pillar ramp)
- Adds check that brightens darker light colors
- Adds Abyssal Lighting, cave crystals and prop lights now match the dynamic sun color
- Brightens moon slightly
- Removes moon escape sequence post processing

**1.6.2**

- Adds new material theme "Bazaar"
- Changes Acres' and Aphelian's ambient lighting to be more neutral
- Changes Acres' and Aphelian's lighting value to be closer to the post processing
- Changes lighting to be flat values (prevent BRIGHT stages)
- Changes a material in Void Abyssal's theme
- Fixes Aphelian's wonky materials

**1.6.1**

- Enhances dynamic lighting to prevent dark stages (thanks Lawlzee)
- Removes ambient lighting (no longer needed)

**1.6.0**

- Adds dynamic lighting that matches the post process profile
- Adds ambient lighting to _try_ and prevent darker stages
- Removes unused R2API dependency and skybox code

**1.5.0**

- Adds "randomized" themes
- Adds themes to Moon2 stage
- Adds new icon
- Removes predefined themes

**1.0.4**

- Makes orange variant less orange
- Makes void stages less bright
- Swaps void rock and ruin materials on sundered grove

**1.0.3**

- Fixes "mod not working" bug

**1.0.2**

- Fixes _the ring_
- Fixes incompats (Desolate Reef)
- Reduces Ruin shadow strength
- Change rock material for Scorched
- Changes tree material for Scorched
- Change rock material for Void

**1.0.1**

- Removes planets from Scorched skybox
- Fixes occasional crashes
- Fixes terrain fog on Pools
- Aphelian changes to look better
- Changes void rock material
- Other slight fixes/tweaks for better looking themes

**1.0.0**

- Release
