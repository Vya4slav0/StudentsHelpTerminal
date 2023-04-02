using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DocumentViewers
{
    public partial class DocxViewer : UserControl, IDocumentViewer
    {
        public DocxViewer()
        {
            InitializeComponent();
        }

        public void LoadFile(string fileName)
        {
            if (File.Exists(fileName))
                docViewer1.LoadFromFile(fileName);
        }
    }
}
