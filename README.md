![Cover](Cover.png)

__English Guide__ | [中文指南](README_ZH.md)

# Lazy Shooting

[GitHub](https://github.com/Black-Moss/Lazy-Shooting) / [Nexus Mods](https://www.nexusmods.com/scavprototype/mods/14)

_Hold your left mouse button to shoot through the layers!_

**Requires [Moss Lib](https://github.com/Black-Moss/Moss-Lib) as a dependency!**

You can change configuration properties in the config file or by using the [`lazyshooting`](ModCommand.cs) command!

_Changes made via [`lazyshooting`](ModCommand.cs) are automatically synced to the config file._

---

## Table of Contents

- [Overview](#overview)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Features](#features)
- [Command System](#command-system)
- [Configuration Reference](#configuration-reference)
- [Localization](#localization)
- [Project Structure](#project-structure)

---

## Overview

**Lazy Shooting** is a BepInEx plugin for **Casualties Unknown** that adds quality-of-life enhancements for gunplay. No
more worrying about gun durability, ammunition management, or jamming — just point and shoot.

| Feature                                    | Description                                     |
|--------------------------------------------|-------------------------------------------------|
| [`Ammunition UI`](PlayerCameraPatch.cs)    | Real-time ammo display above the gun menu       |
| [`Auto Rack`](GunScriptPatch.cs)           | Automatic weapon racking when ammo is available |
| [`Indestructible Gun`](GunScriptPatch.cs)  | Guns never break or lose condition              |
| [`Infinite Ammunition`](GunScriptPatch.cs) | Unlimited bullets — never reload again          |
| [`Never Jam`](GunScriptPatch.cs)           | Guns will never jam during combat               |
| [`Recoilless`](GunScriptPatch.cs)          | Eliminate all weapon recoil                     |

---

## Installation

1. Install [BepInEx 5.x](https://github.com/BepInEx/BepInEx) for Casualties Unknown.
2. Install [Moss Lib](https://github.com/Black-Moss/Moss-Lib) as a dependency.
3. Download the latest `LazyShooting.dll` from the [Releases](https://github.com/Black-Moss/Lazy-Shooting/releases) page
   (or [Nexus Mods](https://www.nexusmods.com/scavprototype/mods/14)).
4. Place `LazyShooting.dll` into your `BepInEx/plugins/` folder.

---

## Quick Start

Once installed, most features are **disabled by default** (except those marked otherwise). You can configure them in two
ways:

### 1. Configuration File

Edit `BepInEx/config/org.explosivehydra.lazyshooting.cfg` with any text editor.

### 2. In-Game Command

Open the console (default: `~`) and type:

```
lazyshooting           # List all configs and their current values
lazyshooting <config>  # Toggle a specific config on/off
```

Example:

```
lazyshooting infinite_ammunition    # Toggle infinite ammo
```

---

## Features

### Ammunition UI

Displays current magazine ammo count above the gun menu in real time, with color-coded status:

| Ammo Level | Color                                                           |
|------------|-----------------------------------------------------------------|
| >= 80%     | ![#00FF00](https://placehold.co/15x15/00FF00/00FF00.png) Green  |
| >= 50%     | ![#FFFF00](https://placehold.co/15x15/FFFF00/FFFF00.png) Yellow |
| < 50%      | ![#FF0000](https://placehold.co/15x15/FF0000/FF0000.png) Red    |

When infinite ammo is enabled, displays a large `INF` symbol instead.

### Auto Rack

When enabled, guns automatically rack and stay racked whenever ammunition is available in your inventory. No more manual
racking after reloading.

### Indestructible Gun

Prevents guns from taking condition damage. Your weapon's durability will never decrease, eliminating the need for
repairs.

### Infinite Ammunition

Grants unlimited ammunition. When enabled, the ammo UI displays `INF` instead of numeric values, and you will never need
to reload.

### Never Jam

Eliminates weapon jamming entirely. Your gun will fire reliably every time you pull the trigger.

### Recoilless

Removes all weapon recoil, making your shots perfectly accurate with no muzzle climb.

---

## Command System

The [`lazyshooting`](ModCommand.cs) command lets you toggle configurations directly from the in-game console.

### Usage

| Command                                  | Description                                       |
|------------------------------------------|---------------------------------------------------|
| [`lazyshooting`](ModCommand.cs)          | List all configurations with their current values |
| [`lazyshooting <config>`](ModCommand.cs) | Toggle a boolean configuration on/off             |

### Example Output

```
-- Current configuration settings: --
    Ammunition UI(ammunition_ui): True
    Auto Rack(auto_rock): False
    Indestructible Gun(indestructible_gun): False
    Infinite Ammunition(infinite_ammunition): True
    Never Jam(never_jam): False
    Recoilless(recoilless): False
--------------------------------------
```

```
Config Infinite Ammunition(infinite_ammunition) toggled to False
```

### Implementation

The command is registered via Harmony patch on [`ConsoleScript.RegisterAllCommands`](ModCommand.cs) and uses
[`ModCommandBase`](https://github.com/Black-Moss/Moss-Lib/blob/master/Base/ModCommandBase.cs) from Moss Lib.

```csharp
[HarmonyPatch(typeof(ConsoleScript))]
public class ModCommand : ModCommandBase
{
    [HarmonyPatch("RegisterAllCommands")]
    [HarmonyPostfix]
    public static void RegisterCustomCommands(ConsoleScript __instance)
    {
        ConsoleScript.Commands.Add(new Command(
            "lazyshooting",
            Locale("description"),
            ExecuteCommand,
            argAutofill,
            paramDescriptions)
        );
    }
}
```

---

## Configuration Reference

All configuration entries are registered in [`Plugin.cs`](Plugin.cs) via BepInEx `Config.Bind()`.

| Key                   | Type   | Default | Description                                     |
|-----------------------|--------|---------|-------------------------------------------------|
| `ammunition_ui`       | `bool` | `true`  | Display real-time ammo count above the gun menu |
| `auto_rock`           | `bool` | `false` | Automatically rack guns when ammo is available  |
| `indestructible_gun`  | `bool` | `false` | Guns never lose condition                       |
| `infinite_ammunition` | `bool` | `false` | Unlimited ammunition                            |
| `never_jam`           | `bool` | `false` | Guns never jam                                  |
| `recoilless`          | `bool` | `false` | Remove all weapon recoil                        |

Config changes made via the [`lazyshooting`](ModCommand.cs) command are automatically saved to the config file.

---

## Localization

Lazy Shooting supports multiple languages via Moss Lib's localization system. Language files are stored in the
[`Lang/`](Lang/) directory.

| Language            | Generator                                            |
|---------------------|------------------------------------------------------|
| English             | [`Lang/EnLangGenerator.cs`](Lang/EnLangGenerator.cs) |
| Simplified Chinese  | [`Lang/ZhCnLangGenerator.cs`](Lang/ZhCnLangGenerator.cs) |

The localization system uses Moss Lib's
[`ModLangGenBase`](https://github.com/Black-Moss/Moss-Lib/blob/master/Base/ModLangGenBase.cs) to generate JSON locale
files from C# code.

```csharp
public class ZhCnLangGenerator : ModLangGenBase
{
    protected override string LanguageCode => "zh-CN";

    protected override void BuildLocaleData()
    {
        Add("config.ammunition_ui.name", "Ammunition UI");
        Add("config.ammunition_ui.description", "Display your ammunition in real time!");
        // ...
    }
}
```

---

## Project Structure

```
Lazy-Shooting/
+-- Plugin.cs                    # Main plugin entry point and config registry
+-- ModCommand.cs                # In-game command registration and handling
+-- ModLocale.cs                 # Localization singleton wrapper
+-- GunScriptPatch.cs            # Gun-related Harmony patches
+-- PlayerCameraPatch.cs         # Ammunition UI rendering
+-- Lang/
    +-- EnLangGenerator.cs       # English locale generator
    +-- ZhCnLangGenerator.cs     # Simplified Chinese locale generator
    +-- ZhTwLangGenerator.cs     # Traditional Chinese locale generator
```

## License

This project is licensed under the GNU General Public License v3.0. See [`LICENSE.md`](LICENSE.md) for details.
