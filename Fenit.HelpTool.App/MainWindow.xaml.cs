using System.Windows;
using Prism.Modularity;

namespace Fenit.HelpTool.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IModuleManager _moduleManager;

        public MainWindow(IModuleManager moduleManager)
        {
            InitializeComponent();
            _moduleManager = moduleManager;

            _moduleManager.LoadModule("ModuleLogin");
        }
    }
}
