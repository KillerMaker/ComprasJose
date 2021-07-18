using ComprasWinForm.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComprasWinForm.Modelos;

namespace ComprasWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            if (await CUsuario.Login(txtNombreUsuario.Text, txtClave.Text))
            {
                MessageBox.Show("bienvenido");
                Form form = new Home();
                form.Show();
                Hide();
            }
                
            else
                MessageBox.Show("Usuario no valido");
        }
    }
}
