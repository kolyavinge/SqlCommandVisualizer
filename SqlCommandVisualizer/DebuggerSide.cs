using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

[assembly: DebuggerVisualizer(
typeof(SqlCommandVisualizer.SqlCommandVisualizer),
typeof(SqlCommandVisualizer.SqlCommandVisualizerObjectSource),
Target = typeof(SqlCommand),
Description = "SqlCommand Visualizer")]

namespace SqlCommandVisualizer
{
    /// <summary>
    /// Визуализатор
    /// </summary>
    public class SqlCommandVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var wrapper = (SqlCommandWrapper)objectProvider.GetObject();
            ShowDialog(wrapper.RawSql);
        }

        public void ShowDialog(string rawSql)
        {
            var vm = new SqlCommandVisualizerViewModel();
            var view = new SqlCommandVisualizerView { DataContext = vm };
            view.ShowDialog();
            vm.Close();
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
            RawSql = sqlCommand.GetRawSql().Trim();
        }

        public string RawSql { get; private set; }
    }
}
