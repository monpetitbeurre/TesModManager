using System;
using System.Windows.Forms;

namespace NifViewer {
    public partial class HeightmapGenForm : Form {
        private static int iterations = 128;
        private static float scale = 0.0625f;

        public static int Iterations { get { return iterations; } }
        public new static float Scale { get { return scale; } }

        public HeightmapGenForm() {
            InitializeComponent();
            tbIterations.Text=iterations.ToString();
            tbScale.Text=scale.ToString();
        }

        private void bSave_Click(object sender, EventArgs e) {
            ushort i;
            float f;

            if(!ushort.TryParse(tbIterations.Text, out i)||i==0) {
                MessageBox.Show("Iterations value was illegal or out of range.");
                return;
            }
            if(!float.TryParse(tbScale.Text, out f)) {
                MessageBox.Show("Scale was illegal or out of range.");
                return;
            }
            iterations=i;
            scale=f;
            DialogResult=DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e) {
            DialogResult=DialogResult.Cancel;
            Close();
        }
    }
}