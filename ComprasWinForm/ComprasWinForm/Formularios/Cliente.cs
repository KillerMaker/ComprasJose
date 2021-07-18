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

namespace ComprasWinForm.Formularios
{
	public partial class Cliente : Form
	{
        Form form;
		private CCliente cliente;
		public Cliente()
		{
			InitializeComponent();
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private async void btnInsertar_Click(object sender, EventArgs e)
		{
			cliente = new CCliente(null, txtNombre.Text,mtxtCedula.Text, int.Parse(cmbEstado.Text));

			if (await cliente.Insert() > 0)
				MessageBox.Show("Departamento insertado correctamente");
			else
				MessageBox.Show("Algo ha salido mal");

			dataGridView1.DataSource = await CCliente.Select();	
		}

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
			cliente = new CCliente(int.Parse(txtId.Text),mtxtCedula.Text, txtNombre.Text, int.Parse(cmbEstado.Text));

			if (await cliente.Update() > 0)
				MessageBox.Show("Departamento insertado correctamente");
			else
				MessageBox.Show("Algo ha salido mal");

			dataGridView1.DataSource = await CCliente.Select();
		}

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
			int id = (dataGridView1.SelectedRows.Count.Equals(1)) ?
				int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) : 0;

			if (id == 0)
				MessageBox.Show("Seleccione un registro de la lista por favor");
			else
				cliente = new CCliente(id);

			await cliente.Delete();
			dataGridView1.DataSource = await CCliente.Select();
		}

        private async void Cliente_Load(object sender, EventArgs e)
        {
			dataGridView1.DataSource = await CCliente.Select();
		}

        private void btnCargarData_Click(object sender, EventArgs e)
        {
			txtId.Text = (dataGridView1.SelectedRows.Count.Equals(1)) ?
				dataGridView1.SelectedRows[0].Cells[0].Value.ToString() : txtId.Text;
		}

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Empleado();
            Close();
            form.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Cliente();
            Close();
            form.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Proveedor();
            Close();
            form.Show();
        }

        private void solicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Solicitud();
            Close();
            form.Show();
        }

        private void ordenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new OrdenCompra();
            Close();
            form.Show();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Marca();
            Close();
            form.Show();
        }

        private void unidadDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new UnidadMedida();
            Close();
            form.Show();
        }

        private void articuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Articulo();
            Close();
            form.Show();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form = new Departamento();
            Close();
            form.Show();
        }
    }
}
