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
using System.Media;


namespace Puzzles
{
    public partial class FormMain: Form
    {
        private SoundPlayer player;
        private bool isSoundOn = false;
        private bool isDarkTheme = false;

        public FormMain()
        {
            InitializeComponent();
            
        }
       
        private void btnS_Click(object sender, EventArgs e) //sydoka
        {
            player.Stop();
            isSoundOn = false;
            btnSound.BackgroundImage = Properties.Resources.sound_off;
            pictureBoxGif.Visible = false;

            Sydoka sydokaForm = new Sydoka();

            sydokaForm.StartPosition = FormStartPosition.Manual;
            sydokaForm.Location = this.Location;

            sydokaForm.SetFormMain(this);
            sydokaForm.Show();
            this.Hide();
        }

        private void btnKR_Click(object sender, EventArgs e)  //find para
        {
            player.Stop();
            isSoundOn = false;
            btnSound.BackgroundImage = Properties.Resources.sound_off;
            pictureBoxGif.Visible = false;

            FindCard f = new FindCard();

            f.StartPosition = FormStartPosition.Manual;
            f.Location = this.Location;

            f.SetFormMain(this);
            f.Show();
            this.Hide();

        }
        private void btnGA_Click(object sender, EventArgs e) //arithmetic
        {
            player.Stop();
            isSoundOn = false;
            btnSound.BackgroundImage = Properties.Resources.sound_off;
            pictureBoxGif.Visible = false;

            Arithmetic arithmetic = new Arithmetic();

            arithmetic.StartPosition = FormStartPosition.Manual;
            arithmetic.Location = this.Location;

            arithmetic.SetFormMain(this);
            arithmetic.Show();
            this.Hide();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
            player = new SoundPlayer("sound2.wav");

            if (isSoundOn)
            {
                player.PlayLooping();
                btnSound.BackgroundImage = Properties.Resources.sound_on;
            }
            else
            {
                btnSound.BackgroundImage = Properties.Resources.sound_off;
            }

            btnSound.BackgroundImageLayout = ImageLayout.Stretch;

            ApplyTheme();
        }

        private void btnSound_Click(object sender, EventArgs e)        //zvyk
        {
            if (isSoundOn)
            {
                player.Stop();
                btnSound.BackgroundImage = Properties.Resources.sound_off;
                btnSound.BackgroundImageLayout = ImageLayout.Stretch;
                isSoundOn = false;
                pictureBoxGif.Visible = false;

            }
            else
            {
                player.PlayLooping();
                btnSound.BackgroundImage = Properties.Resources.sound_on;
                btnSound.BackgroundImageLayout = ImageLayout.Stretch;
                isSoundOn = true;
                pictureBoxGif.Visible = true;
            }
        }
        private void ApplyTheme()   //tema
        {
            if (isDarkTheme)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.ForeColor = Color.White;

                    if (ctrl is System.Windows.Forms.Label)
                    {
                        ctrl.BackColor = Color.Transparent; // фон  Label
                    }
                    else if (ctrl is PictureBox)
                    {
                        ctrl.BackColor = Color.Transparent;  //гіфки
                    }
                    else
                    {
                        ctrl.BackColor = Color.FromArgb(45, 45, 45);
                    }
                }
                btnTheme.BackgroundImage = Properties.Resources.white;
            }
            else
            {
                this.BackColor = Color.Azure;
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.ForeColor = SystemColors.ControlText;

                    if (ctrl is System.Windows.Forms.Label)
                    {
                        ctrl.BackColor = Color.Transparent; 
                    }
                    else if (ctrl is PictureBox)
                    {
                        ctrl.BackColor = Color.Transparent;  // гіфки
                    }
                    else
                    {
                        ctrl.BackColor = SystemColors.Control;
                    }
                    btnTheme.BackgroundImage = Properties.Resources.black;
                }
            }
            btnTheme.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnTheme_Click(object sender, EventArgs e)  //btn tema
        {
            isDarkTheme = !isDarkTheme;
            ApplyTheme();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
