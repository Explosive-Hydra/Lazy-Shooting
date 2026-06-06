using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LazyShooting.Lang;
using MossLib.Tool;

namespace LazyShooting;

[BepInDependency("org.explosivehydra.mosslib")]
[BepInPlugin(Guid, Name, "1.1.0")]
public class Plugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger;
    public const string Guid = "org.explosivehydra.lazyshooting";
    public const string Name = "Lazy Shooting";
    private readonly Harmony _harmony = new(Guid);
    internal static readonly Dictionary<string, ConfigEntryBase> ConfigRegistry = new();

    public static ConfigEntry<bool> AmmunitionUi;
    public static ConfigEntry<bool> AutoRack;
    public static ConfigEntry<bool> IndestructibleGun;
    public static ConfigEntry<bool> InfiniteAmmunition;
    public static ConfigEntry<bool> NeverJam;
    public static ConfigEntry<bool> Recoilless;

    public void Awake()
    {
        Logger = base.Logger;
        
        LocaleGenerator.SetLogger(Logger);
        LocaleGenerator.Register(new EnLangGenerator(), Logger);
        LocaleGenerator.Register(new ZhCnLangGenerator(), Logger);
        LocaleGenerator.GenerateAll();
        
        _harmony.PatchAll();
        ModLocale.Initialize(Logger);
        
        AmmunitionUi = RegisterConfig("ammunition_ui", true);
        AutoRack = RegisterConfig("auto_rock", false);
        IndestructibleGun = RegisterConfig("indestructible_gun", false);
        InfiniteAmmunition = RegisterConfig("infinite_ammunition", false);
        NeverJam = RegisterConfig("never_jam", false);
        Recoilless = RegisterConfig("recoilless", false);
    }

    private ConfigEntry<T> RegisterConfig<T>(string key, T defaultValue)
    {
        var entry = Config.Bind("General", key, defaultValue, ConfigLocale($"{key}.description"));
        ConfigRegistry[key] = entry;
        return entry;
    }

    private static string ConfigLocale(string key)
    {
        return Locale($"config.{key}");
    }

    private static string Locale(string key)
    {
        return ModLocale.GetFormat(key);
    }
}