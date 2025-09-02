using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class FormMain: Form
    {

        public FormMain()
        {
            InitializeComponent();
        }
       
        private void btnS_Click(object sender, EventArgs e)
        {
            Sydoka sydokaForm = new Sydoka();
            sydokaForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnKR_Click(object sender, EventArgs e)
        {
            FindCard f = new FindCard();
            f.Show();
            
        }

        private void btnGA_Click(object sender, EventArgs e)
        {
            Arithmetic f = new Arithmetic();
            f.Show();
        }
    }
}
