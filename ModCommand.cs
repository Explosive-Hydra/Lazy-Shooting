using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MossLib;
using MossLib.Base;
using MossLib.Tool;

namespace LazyShooting;

[HarmonyPatch(typeof(ConsoleScript))]
public class ModCommand : ModCommandBase
{
    private new static readonly ManualLogSource Logger = Plugin.Logger;
    private const string LocaleKeyPre = "command.lazyshooting.";
    private static bool _autofillRegistered;
    private static List<string> _cachedConfigNames = [];

    [HarmonyPatch("RegisterAllCommands")]
    [HarmonyPostfix]
    public static void RegisterCustomCommands(ConsoleScript __instance)
    {
        try
        {
            var argAutofill = new Dictionary<int, List<string>>
            {
                {
                    0,
                    [
                        "ammunition_ui",
                        "auto_rock",
                        "indestructible_gun",
                        "infinite_ammunition",
                        "never_jam",
                        "recoilless"
                    ]
                }
            };

            var paramDescriptions = new[]
            {
                ("string", Locale("parameter"))
            };

            ConsoleScript.Commands.Add(new Command(
                "lazyshooting",
                Locale("description"),
                ExecuteCommand,
                new Dictionary<int, List<string>>(argAutofill),
                paramDescriptions)
            );

            RegisterDynamicAutoFills();
        }
        catch (Exception ex)
        {
            Error("register_failed", ex.Message, ex.StackTrace);
        }
    }

    private static void RegisterDynamicAutoFills()
    {
        if (_autofillRegistered)
            return;

        var targetCommands = ConsoleScript.Commands
            .Where(c => c != null && c.action == ExecuteCommand)
            .ToList();

        if (targetCommands.Count == 0)
            return;

        _cachedConfigNames = Plugin.ConfigRegistry.Keys.ToList();

        _autofillRegistered = true;
    }

    [HarmonyPatch("HandleDescriptionText")]
    [HarmonyPrefix]
    private static void PreHandleDescriptionText(string[] args)
    {
        UpdateAutofillContext(args);
    }

    [HarmonyPatch("TryFinishCommandPart")]
    [HarmonyPrefix]
    private static void PreTryFinishCommandPart(string[] args)
    {
        UpdateAutofillContext(args);
    }

    private static void UpdateAutofillContext(string[] args)
    {
        if (args == null || args.Length < 2)
            return;

        string cmdName = args[0];
        if (cmdName != "lazyshooting")
            return;

        var cmd = ConsoleScript.SearchExact(cmdName);
        if (cmd?.argAutofill == null)
            return;

        int key = args.Length - 2;
        if (key != 1)
            return;

        var contextList = new List<string>();
        string subcommand = args[1].ToLower();

        if (!string.IsNullOrEmpty(subcommand) && _cachedConfigNames.Count > 0)
        {
            contextList.AddRange(
                _cachedConfigNames.Where(name =>
                    name.StartsWith(subcommand, StringComparison.OrdinalIgnoreCase))
            );
        }

        cmd.argAutofill[key] = contextList;
    }

    private static void ExecuteCommand(string[] args)
    {
        if (args.Length == 1)
        {
            ListConfigs();
        }
        else
        {
            try
            {
                ToggleConfig(args[1]);
            }
            catch (Exception ex)
            {
                Error("unknown", args[1]);
            }
        }
    }

    private static void ListConfigs()
    {
        Log.Divider();
        Info("help_header");

        foreach (var kvp in Plugin.ConfigRegistry)
        {
            string key = kvp.Key;
            string displayName = ConfigDisplayName(key);
            object value = kvp.Value.BoxedValue;
            string description = ModLocale.GetFormat($"config.{key}.description");
            Info("help_item", displayName, key, value);
            Log.Info($"        {description}", Logger);
        }

        Log.Divider();
    }

    private static void ToggleConfig(string key)
    {
        if (!Plugin.ConfigRegistry.TryGetValue(key, out var entry))
        {
            Info("unknown", key);
            return;
        }

        if (entry.SettingType == typeof(bool))
        {
            bool newValue = !(bool)entry.BoxedValue;
            entry.BoxedValue = newValue;
            entry.ConfigFile?.Save();

            string displayName = ConfigDisplayName(key);
            Info("toggle", displayName, key, newValue);
        }
        else
        {
            Info("unknown", key);
        }
    }

    private static string ConfigDisplayName(string key)
    {
        var name = ModLocale.GetFormat($"config.{key}.name");
        return string.IsNullOrEmpty(name) || name.StartsWith("config.")
            ? key
            : name;
    }

    private static void Info(string key, params object[] args)
    {
        Log.Info(Locale(key, args), Logger);
    }

    private static void Error(string key, params object[] args)
    {
        Log.Error(Locale(key, args), Logger);
    }

    private static string Locale(string key, params object[] args)
    {
        return ModLocale.GetFormat($"{LocaleKeyPre}{key}", args);
    }
}