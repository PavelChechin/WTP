using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WTPCoreExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportPlanExample importer = new ImportPlanExample();
            importer.Import();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
