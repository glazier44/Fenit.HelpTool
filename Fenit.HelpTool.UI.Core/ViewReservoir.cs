using Fenit.Toolbox.WPF.UI;

namespace Fenit.HelpTool.UI.Core
{
    public class ViewReservoir: BaseViewReservoir
    {
        public static class SettingsModule
        {
            public static string Name = "SettingsModule";

            public static string Main = "SettingsModule.Main";
            public static string ShifterConfigSettings = "SettingsModule.ShifterConfigSettings";

        }

        public static class ShifterModule
        {
            public static string Name = "ShifterModule";

            public static string Main = "ShifterModule.Main";
            public static string MessageWindow = "ShifterModule.MessageWindow";
        }

        public static class SqlLogModule
        {
            public static string Name = "SqlLogModule";
            public static string Main = "SqlLogModule.Main";
        }

        public static class LoginModule
        {
            public static string Name = "LoginModule";
            public static string Login = "LoginModule.Login";
        }


        public static class RunnerModule
        {
            public static string Name = "RunnerModule";
            public static string Main = "RunnerModule.Main";
        }

        public static class HelpModule
        {
            public static string Name = "HelpModule";
            public static string Main = "HelpModule.Main";
            public static string About = "HelpModule.About";

        }
    }
}