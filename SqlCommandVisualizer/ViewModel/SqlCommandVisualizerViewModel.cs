using SqlCommandVisualizer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SqlCommandVisualizer.ViewModel
{
    public class SqlCommandVisualizerViewModel : INotifyPropertyChanged
    {
        private ConfigSettings _configSettings;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler OnClose;

        public SqlCommandVisualizerViewModel()
        {
            var configManager = new SqlVisualizerConfigManager();
            _configSettings = configManager.LoadConfigSettings();
            if (_configSettings.IsMaximized)
            {
                IsMaximized = true;
            }
            else
            {
                WindowWidth = _configSettings.MainWindowWidth;
                WindowHeight = _configSettings.MainWindowHeight;
            }
            IsWordWrap = _configSettings.IsWordWrap;
        }

        private string _sqlText;
        public string SqlText
        {
            get { return _sqlText; }
            set
            {
                _sqlText = value;
                RaisePropertyChanged("SqlText");
            }
        }

        private bool _isWordWrap;
        public bool IsWordWrap
        {
            get { return _isWordWrap; }
            set
            {
                _isWordWrap = value;
                RaisePropertyChanged("IsWordWrap");
            }
        }

        private bool _isMaximized;
        public bool IsMaximized
        {
            get { return _isMaximized; }
            set
            {
                _isMaximized = value;
                RaisePropertyChanged("IsMaximized");
            }
        }

        private double _windowWidth;
        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                RaisePropertyChanged("WindowWidth");
            }
        }

        private double _windowHeight;
        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                _windowHeight = value;
                RaisePropertyChanged("WindowHeight");
            }
        }

        public ICommand CopyToClipboardCommand { get { return new DelegateCommand(CopyToClipboard); } }

        private void CopyToClipboard()
        {
            Clipboard.SetText(SqlText);
        }

        public ICommand OpenAsSqlFileCommand { get { return new DelegateCommand(OpenAsSqlFile); } }

        private void OpenAsSqlFile()
        {
            var tempFileManager = new TempFileManager();
            var tmpSqlFile = tempFileManager.GetTempFile(SqlText, "sql");
            Process.Start(tmpSqlFile);
        }

        public ICommand CloseCommand { get { return new DelegateCommand(Close); } }

        public void Close()
        {
            _configSettings.IsMaximized = IsMaximized;
            _configSettings.MainWindowWidth = WindowWidth;
            _configSettings.MainWindowHeight = WindowHeight;
            _configSettings.IsWordWrap = IsWordWrap;
            var configManager = new SqlVisualizerConfigManager();
            configManager.SaveConfigSettings(_configSettings);
            if (OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
