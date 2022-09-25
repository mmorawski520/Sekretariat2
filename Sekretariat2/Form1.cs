using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sekretariat2
{
    public partial class Sekretariat : Form
    {
        string user="admin";
        string password = "Qwerty1@34";
        bool canBeLogged = true;

        int choosenCaptcha;
        string[] captchas = {"mxyxw","b5nmm","74853","cg5dd","x3deb","befhd","c7gb3" };

        public Sekretariat()
        {
            InitializeComponent();
            this.setCaptcha();
        }

        private void setCaptcha()
        {
            var rnd = new Random();
            choosenCaptcha = rnd.Next(1, 7);
            pictureBox1.Image = Image.FromFile(@"..\..\"+choosenCaptcha+".png");
        }

        private void labelCaptcha_Click(object sender, EventArgs e)
        {
            setCaptcha();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            canBeLogged = true;
            labelError.Text = "";

            if(textBoxPassword.Text != "Qwerty1@34" || textBoxUser.Text != "admin")
            {
                labelError.Text += "Nieprawidłowy login lub hasło";
                canBeLogged = false;
            }
            
            if (captchas[choosenCaptcha-1] != textBoxCaptcha.Text)
            {
                labelError.Text += "\nNieprawidłowy kod captcha";
                canBeLogged = false;
            }

            if (canBeLogged)
            {
                this.Hide();
                Form2 window = new Form2();
                window.ShowDialog();
                this.Close();
            }
        }
    }
}
