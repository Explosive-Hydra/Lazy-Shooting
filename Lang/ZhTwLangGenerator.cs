using MossLib.Base;

namespace LazyShooting.Lang;

public class ZhTwLangGenerator : ModLangGenBase
{
    protected override string LanguageCode => "zh-TW";

    protected override void BuildLocaleData()
    {
        // Config - 名稱
        Add("config.ammunition_ui.name", "彈藥UI");
        Add("config.auto_rock.name", "自動上膛");
        Add("config.indestructible_gun.name", "不毁槍械");
        Add("config.infinite_ammunition.name", "無限彈藥");
        Add("config.never_jam.name", "永不卡殼");
        Add("config.recoilless.name", "無後座力");

        // Config - 描述
        Add("config.ammunition_ui.description", "在原槍械選單的上方顯示槍械剩餘彈量和最大彈量");
        Add("config.auto_rock.description", "開啟後，當有彈藥時，槍械將自動拉栓並保持拉栓狀態");
        Add("config.indestructible_gun.description", "開啟後，槍械將不會損壞");
        Add("config.infinite_ammunition.description", "∞ 無限子彈 ∞");
        Add("config.never_jam.description", "開啟後，槍械將不會卡殼");
        Add("config.recoilless.description", "開啟後，槍械將沒有後座力");

        // Command - LazyShooting
        Add("command.lazyshooting.description", "設定懶人射擊的各項配置");
        Add("command.lazyshooting.help_header", "當前配置設定：");
        Add("command.lazyshooting.help_item", "    {0}({1}): {2}");
        Add("command.lazyshooting.parameter", "配置項名稱");
        Add("command.lazyshooting.toggle", "配置 {0}({1}) 已切換為 {2}");
        Add("command.lazyshooting.unknown", "未知配置: '{0}'");
        Add("command.lazyshooting.register_failed", "註冊指令失敗: {0}\n{1}");
    }
}