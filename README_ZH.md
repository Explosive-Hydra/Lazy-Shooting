# 该项目已不再开发，请使用[量子](https://github.com/CNCUMC/Quantum)。
# 该项目已不再开发，请使用[量子](https://github.com/CNCUMC/Quantum)。
# 该项目已不再开发，请使用[量子](https://github.com/CNCUMC/Quantum)。

![](Cover.png)

[English Guide](README.md) | __中文指南__

# Lazy Shooting

[GitHub](https://github.com/Black-Moss/Lazy-Shooting) / [Nexus Mods](https://www.nexusmods.com/scavprototype/mods/14)

_按住你的鼠标左键射穿地层！_

__需要[Moss Lib](https://github.com/Black-Moss/Moss-Lib)作为前置！__

在配置文件或使用`lazyshooting`指令以更改各项配置的属性！

_使用`lazyshooting`更改配置会同步到配置文件中。_

---

## 目录

- [概述](#概述)
- [安装](#安装)
- [快速开始](#快速开始)
- [功能特性](#功能特性)
- [指令系统](#指令系统)
- [配置参考](#配置参考)
- [本地化](#本地化)
- [项目结构](#项目结构)

---

## 概述

**Lazy Shooting** 是一个针对 **Casualties Unknown** 的 BepInEx 插件，为枪械系统添加了一系列便利功能。无需再担心枪械耐久、弹药管理或卡壳问题——只需瞄准并射击。

| 功能     | 描述              |
|--------|-----------------|
| `弹药UI` | 在枪械菜单上方实时显示弹药数量 |
| `自动上膛` | 有弹药时自动拉栓并保持拉栓状态 |
| `不毁枪械` | 枪械永不损坏          |
| `无限弹药` | 无限子弹——无需再装填     |
| `永不卡壳` | 枪械永远不会卡壳        |
| `无后座力` | 消除所有武器后坐力       |

---

## 安装

1. 安装 [BepInEx 5.x](https://github.com/BepInEx/BepInEx) for Casualties Unknown。
2. 安装前置依赖 [Moss Lib](https://github.com/Black-Moss/Moss-Lib)。
3. 从 [发布页](https://github.com/Black-Moss/Lazy-Shooting/releases)
   或 [Nexus Mods](https://www.nexusmods.com/scavprototype/mods/14) 下载最新版 `LazyShooting.dll`。
4. 将 `LazyShooting.dll` 放入 `BepInEx/plugins/` 文件夹。

---

## 快速开始

安装后，大多数功能**默认禁用**（除特别标注外）。你可以通过两种方式配置：

### 1. 配置文件

用文本编辑器编辑 `BepInEx/config/org.explosivehydra.lazyshooting.cfg`。

### 2. 游戏内指令

打开控制台（默认键：`~`）并输入：

```
lazyshooting           # 列出所有配置及其当前值
lazyshooting <配置名>  # 开关指定配置
```

示例：

```
lazyshooting infinite_ammunition    # 切换无限弹药
```

---

## 功能特性

### 弹药UI

在枪械菜单上方实时显示弹匣内的弹药数量，并用颜色标识弹药状态：

| 弹药量   | 颜色                                                          |
|-------|-------------------------------------------------------------|
| ≥ 80% | ![#00FF00](https://placehold.co/15x15/00FF00/00FF00.png) 绿色 |
| ≥ 50% | ![#FFFF00](https://placehold.co/15x15/FFFF00/FFFF00.png) 黄色 |
| < 50% | ![#FF0000](https://placehold.co/15x15/FF0000/FF0000.png) 红色 |

启用无限弹药时，显示大型 `∞` 符号。

### 自动上膛

启用后，当背包中有弹药时，枪械会自动拉栓并保持拉栓状态。无需在装填后手动拉栓。

### 不毁枪械

防止枪械承受耐久度损伤。你的武器耐久度永远不会减少，无需维修。

### 无限弹药

提供无限弹药。启用时，弹药UI显示 `∞` 而非数字，无需再装填。

### 永不卡壳

完全消除枪械卡壳。每次扣动扳机都能可靠开火。

### 无后座力

消除所有武器后坐力，射击精准无上扬。

---

## 指令系统

[`lazyshooting`](ModCommand.cs) 指令让你可以直接在游戏控制台中切换配置。

### 用法

| 指令                   | 描述          |
|----------------------|-------------|
| `lazyshooting`       | 列出所有配置及其当前值 |
| `lazyshooting <配置名>` | 开关布尔类型配置    |

### 示例输出

```
── 当前配置设置：──
    弹药UI(ammunition_ui): True
    自动上膛(auto_rock): False
    不毁枪械(indestructible_gun): False
    无限弹药(infinite_ammunition): True
    永不卡壳(never_jam): False
    无后座力(recoilless): False
──────────────────────
```

```
配置 无限弹药(infinite_ammunition) 已切换为 False
```

### 实现

指令通过 Harmony 补丁 [`ConsoleScript.RegisterAllCommands`](ModCommand.cs) 注册，使用 Moss Lib 的 [
`ModCommandBase`](https://github.com/Black-Moss/Moss-Lib/blob/master/Base/ModCommandBase.cs)。

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

## 配置参考

所有配置项在 [`Plugin.cs`](Plugin.cs) 中通过 BepInEx `Config.Bind()` 注册。

| 键                     | 类型     | 默认值     | 描述              |
|-----------------------|--------|---------|-----------------|
| `ammunition_ui`       | `bool` | `true`  | 在枪械菜单上方实时显示弹药数量 |
| `auto_rock`           | `bool` | `false` | 有弹药时自动拉栓        |
| `indestructible_gun`  | `bool` | `false` | 枪械永不损坏          |
| `infinite_ammunition` | `bool` | `false` | 无限弹药            |
| `never_jam`           | `bool` | `false` | 枪械永不卡壳          |
| `recoilless`          | `bool` | `false` | 消除后坐力           |

通过 `lazyshooting` 指令更改的配置会自动保存到配置文件中。

---

## 本地化

Lazy Shooting 通过 Moss Lib 的本地化系统支持多语言。语言文件存储在 [`Lang/`](Lang) 目录中。

| 语言   | 文件                                                       |
|------|----------------------------------------------------------|
| 英语   | [`Lang/EnLangGenerator.cs`](Lang/EnLangGenerator.cs)     |
| 简体中文 | [`Lang/ZhCnLangGenerator.cs`](Lang/ZhCnLangGenerator.cs) |

本地化系统使用 Moss Lib 的 [`ModLangGenBase`](https://github.com/Black-Moss/Moss-Lib/blob/master/Base/ModLangGenBase.cs)
从 C# 代码生成 JSON 语言文件。

```csharp
public class ZhCnLangGenerator : ModLangGenBase
{
    protected override string LanguageCode => "zh-CN";

    protected override void BuildLocaleData()
    {
        Add("config.ammunition_ui.name", "弹药UI");
        Add("config.ammunition_ui.description", "在原枪械菜单的上方显示枪械剩余弹量和最大弹量");
        // ...
    }
}
```

---

## 项目结构

```
Lazy-Shooting/
├── Plugin.cs                    # 主插件入口和配置注册
├── ModCommand.cs                # 游戏内指令注册与处理
├── ModLocale.cs                 # 本地化单例封装
├── GunScriptPatch.cs            # 枪械相关的 Harmony 补丁
├── PlayerCameraPatch.cs         # 弹药UI渲染
├── Lang/
    ├── EnLangGenerator.cs       # 英语本地化生成器
    ├── ZhCnLangGenerator.cs     # 简体中文本地化生成器
    └── ZhTwLangGenerator.cs     # 繁体中文本地化生成器
```

## 许可证

本项目使用 GNU General Public License v3.0 许可证。详情请参阅 [`LICENSE.md`](LICENSE.md)。
