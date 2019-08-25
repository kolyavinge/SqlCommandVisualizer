using Microsoft.VisualStudio.DebuggerVisualizers;
using SqlCommandVisualizer.View;
using SqlCommandVisualizer.ViewModel;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

[assembly: DebuggerVisualizer(
typeof(SqlCommandVisualizer.SqlCommandDialogDebuggerVisualizer),
typeof(SqlCommandVisualizer.SqlCommandVisualizerObjectSource),
Target = typeof(SqlCommand),
Description = "SqlCommand Visualizer")]

namespace SqlCommandVisualizer
{
    /// <summary>
    /// Визуализатор
    /// </summary>
    public class SqlCommandDialogDebuggerVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var wrapper = (SqlCommandWrapper)objectProvider.GetObject();
            ShowDialog(wrapper.SqlText);
        }

        public void ShowDialog(string rawSql)
        {
            var vm = new SqlCommandVisualizerViewModel();
            vm.SqlText = rawSql;
            var view = new SqlCommandVisualizerView { DataContext = vm };
            vm.OnClose += (s, e) => view.Close();
            view.ShowDialog();
        }
    }

    /// <summary>
    /// Источник данных
    /// </summary>
    public class SqlCommandVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            var sqlCommand = target as SqlCommand;
            var wrapper = new SqlCommandWrapper(sqlCommand);
            base.GetData(wrapper, outgoingData);
        }
    }

    /// <summary>
    /// Обертка для SqlCommand. Нужна потому что источник должен иметь атрибут Serializable.
    /// </summary>
    [Serializable]
    public class SqlCommandWrapper
    {
        public SqlCommandWrapper(SqlCommand sqlCommand)
        {
            SqlText = sqlCommand.GetSqlText().FormatedSqlText;
        }

        public string SqlText { get; private set; }
    }
}
