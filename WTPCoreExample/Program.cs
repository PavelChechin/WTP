using ISEnvironmentSolution;
using RefBookHelper.RBDE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WTPCoreExample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ISEnvironmentSolution.Reports.ReportProvider.RegisterProvider(new ReportService.ReportProvider());
            ISEnvironmentSolution.Common.MapperProvider.RegisterMapper(new RefLib.RefLibMapper());
            RBDEShowRef.Initialize();
            ISEnvironment.Initialize();
            Application.Run(new ImportForm());
        }
    }
}
