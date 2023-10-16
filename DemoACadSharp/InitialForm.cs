using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoACadSharp
{
    public partial class InitialForm : Form
    {
        public InitialForm()
        {
            InitializeComponent();
        }

        public static string nameHouse;
        public static int numberOfFloors;
        public static string topFloor;

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            nameHouse = txtNameHouse.Text;
            numberOfFloors = Int32.Parse(txtNumberFloors.Text);
            topFloor = cbTopFloor.Text;
            MainForm f = new MainForm();
            f.ShowDialog();
            this.Close();
        }
    }
}
