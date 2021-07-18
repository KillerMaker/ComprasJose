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
    public partial class Departamento : Form
    {
        Form form;
        CDepartamento departamento;
        public Departamento()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            departamento = new CDepartamento(int.Parse(txtId.Text), txtNombre.Text, int.Parse(cmbEstado.Text));

            if (await departamento.Update() > 0)
                MessageBox.Show("Departamento insertado correctamente");
            else
                MessageBox.Show("Algo ha salido mal");

            dataGridView1.DataSource = await CDepartamento.Select();
        }

        private async void btnInsertar_Click(object sender, EventArgs e)
        {
            departamento = new CDepartamento(null, txtNombre.Text, int.Parse(cmbEstado.Text));

            if (await departamento.Insert() > 0)
                MessageBox.Show("Departamento insertado correctamente");
            else
                MessageBox.Show("Algo ha salido mal");

            dataGridView1.DataSource = await CDepartamento.Select();
        }

        private async void Departamento_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await CDepartamento.Select();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (dataGridView1.SelectedRows.Count.Equals(1)) ?
                int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) : 0;

            if (id == 0)
                MessageBox.Show("Seleccione un registro de la lista por favor");
            else
                departamento = new CDepartamento(id);

            await departamento.Delete();
            dataGridView1.DataSource = await CDepartamento.Select();
        }

        private async void btnBusqueda_Click(object sender, EventArgs e)
        {
            string searchString = $"WHERE ID = {int.Parse(txtBusqueda.Text)}";
            dataGridView1.DataSource = await CDepartamento.Select(searchString);
        }

        private void btnCargarData_Click(object sender, EventArgs e)
        {
            txtId.Text = (dataGridView1.SelectedRows.Count.Equals(1)) ?
                dataGridView1.SelectedRows[0].Cells[0].Value.ToString() : txtId.Text;
        }

        private void administracionToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
