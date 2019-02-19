using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SqlCommandVisualizer
{
    public class SqlCommandVisualizerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SqlCommandVisualizerViewModel()
        {
            var configManager = new SqlVisualizerConfigManager();
            var configSettings = configManager.LoadConfigSettings();
            var view = new SqlCommandVisualizerView();
            if (configSettings.IsMaximized)
            {
                view.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                view.Width = configSettings.MainWindowWidth;
                view.Height = configSettings.MainWindowHeight;
            }
            view.textEditor.Text = rawSql;
            view.cbIsWordWrap.IsChecked = configSettings.IsWordWrap;
            view.ShowDialog();

            configSettings.IsMaximized = view.WindowState == System.Windows.WindowState.Maximized;
            configSettings.MainWindowWidth = view.Width;
            configSettings.MainWindowHeight = view.Height;
            configSettings.IsWordWrap = view.cbIsWordWrap.IsChecked ?? false;
            configManager.SaveConfigSettings(configSettings);
        }

        public string SqlText { get; set; }

        public ICommand CopyToClipboardCommand { get; private set; }

        private void CopyToClipboard()
        {
            Clipboard.SetText(SqlText);
        }

        public ICommand OpenInSSMSCommand { get; private set; }

        private void OpenInSSMS()
        {
            var ssmsManager = new SSMSManager();
            //ssmsManager.Check()
        }

        public ICommand CloseCommand { get; private set; }

        public void Close()
        {

        }
    }
}
