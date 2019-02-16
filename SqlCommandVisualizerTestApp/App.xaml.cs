using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SqlCommandVisualizerTestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var visualizer = new SqlCommandVisualizer.SqlCommandVisualizer();
            visualizer.ShowDialog("select * from table");

            var cmd = new SqlCommand("select * from table where id = @id");
            cmd.Parameters.AddWithValue("id", 16);
            cmd.ExecuteNonQuery();
        }
    }
}
