using MossLib.Base;

namespace LazyShooting.Lang;

public class ZhCnLangGenerator : ModLangGenBase
{
    protected override string LanguageCode => "zh-CN";

    protected override void BuildLocaleData()
    {
        // Config - 名称
        Add("config.ammunition_ui.name", "弹药UI");
        Add("config.auto_rock.name", "自动上膛");
        Add("config.indestructible_gun.name", "不毁枪械");
        Add("config.infinite_ammunition.name", "无限弹药");
        Add("config.never_jam.name", "永不卡壳");
        Add("config.recoilless.name", "无后座力");

        // Config - 描述
        Add("config.ammunition_ui.description", "在原枪械菜单的上方显示枪械剩余弹量和最大弹量");
        Add("config.auto_rock.description", "开启后，当有弹药时，枪械将自动拉栓并保持拉栓状态");
        Add("config.indestructible_gun.description", "开启后，枪械将不会损坏");
        Add("config.infinite_ammunition.description", "∞ 无限子弹 ∞");
        Add("config.never_jam.description", "开启后，枪械将不会卡壳");
        Add("config.recoilless.description", "开启后，枪械将没有后坐力");

        // Command - LazyShooting
        Add("command.lazyshooting.description", "设置懒人射击的各项配置");
        Add("command.lazyshooting.help_header", "当前配置设置：");
        Add("command.lazyshooting.help_item", "    {0}({1}): {2}");
        Add("command.lazyshooting.parameter", "配置项名称");
        Add("command.lazyshooting.toggle", "配置 {0}({1}) 已切换为 {2}");
        Add("command.lazyshooting.unknown", "未知配置: '{0}'");
        Add("command.lazyshooting.register_failed", "注册指令失败: {0}\n{1}");
    }
}
