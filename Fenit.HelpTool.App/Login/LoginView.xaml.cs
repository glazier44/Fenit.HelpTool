using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Fenit.HelpTool.App.Login
{

    public partial class LoginView
    {
        private bool log;

        public LoginView(LoginViewModel model)
        {
            DataContext = model;
            InitializeComponent();
            Closing += OnClosing;
            Loaded += LoginView_OnLoaded;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;            
        }        

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            if (log) return;
            ((LoginViewModel) DataContext).Close();
        }

        private void LoginView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((LoginViewModel) DataContext).RequestClose += Close;
        }

        public new void Close()
        {
            log = true;
            base.Close();
        }

        private void PasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((LoginViewModel)DataContext).Login();
            }
        }
    }
}