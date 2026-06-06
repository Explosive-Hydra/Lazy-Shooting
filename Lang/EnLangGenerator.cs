using MossLib.Base;

namespace LazyShooting.Lang;

public class EnLangGenerator : ModLangGenBase
{
    protected override string LanguageCode => "EN";

    protected override void BuildLocaleData()
    {
        // Config - Names
        Add("config.ammunition_ui.name", "Ammunition UI");
        Add("config.auto_rock.name", "Auto Rack");
        Add("config.indestructible_gun.name", "Indestructible Gun");
        Add("config.infinite_ammunition.name", "Infinite Ammunition");
        Add("config.never_jam.name", "Never Jam");
        Add("config.recoilless.name", "Recoilless");

        // Config - Descriptions
        Add("config.ammunition_ui.description", "Display your ammunition in real time!");
        Add("config.auto_rock.description", "If true, guns will automatically rack and stay racked when ammo is available.");
        Add("config.indestructible_gun.description", "If true, guns will not be destroyed.");
        Add("config.infinite_ammunition.description", "∞ INFINITE AMMUNITION ∞");
        Add("config.never_jam.description", "If true, guns will never jam.");
        Add("config.recoilless.description", "If true, guns will not have recoil.");

        // Command - LazyShooting
        Add("command.lazyshooting.description", "Set various configurations for Lazy Shooting");
        Add("command.lazyshooting.help_header", "Current configuration settings:");
        Add("command.lazyshooting.help_item", "    {0}({1}): {2}");
        Add("command.lazyshooting.parameter", "Configuration name");
        Add("command.lazyshooting.toggle", "Config {0}({1}) toggled to {2}");
        Add("command.lazyshooting.unknown", "Unknown config: '{0}'");
        Add("command.lazyshooting.register_failed", "Failed to register command: {0}\n{1}");
    }
}
