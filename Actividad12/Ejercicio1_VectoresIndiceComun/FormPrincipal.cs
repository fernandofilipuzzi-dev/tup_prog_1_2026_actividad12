namespace Ejercicio1_VectoresIndiceComun;

public partial class FormPrincipal : Form
{
    private Services.NumericService servicio;

    public FormPrincipal()
    {
        InitializeComponent();
        servicio = new Services.NumericService();
    }

    private void btnRegistrarAlumno_Click(object sender, EventArgs e)
    {
        FormDatos formDatos = new FormDatos();
        if (formDatos.ShowDialog() == DialogResult.OK)
        {
            int lu;
            try
            {
                lu = Convert.ToInt32(formDatos.tbLU.Text);
            }
            catch
            {
                MessageBox.Show("LU inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double nota;
            try
            {
                nota = Convert.ToDouble(formDatos.tbNota.Text);
            }
            catch
            {
                MessageBox.Show("Nota inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nombre = formDatos.tbNombre.Text;
            servicio.RegistrarAlumno(lu, nombre, nota);
            MessageBox.Show("Alumno registrado. Total: " + servicio.VerContador(), "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnBuscarAlumno_Click(object sender, EventArgs e)
    {
        int lu;
        try
        {
            lu = Convert.ToInt32(tbLU.Text);
        }
        catch
        {
            MessageBox.Show("LU inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        int idx;
        if (rbSecuencial.Checked)
            idx = servicio.BuscarPorLUSecuencial(lu);
        else
        {
            servicio.OrdenarPorLUBurbuja();
            idx = servicio.BuscarPorLUBinario(lu);
        }

        if (idx == -1)
            MessageBox.Show("Alumno con LU " + lu + " no encontrado.", "Búsqueda",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
            MessageBox.Show(servicio.VerAlumno(idx), "Alumno encontrado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnMostrarListadoOrdenado_Click(object sender, EventArgs e)
    {
        if (rbBurbuja.Checked)
            servicio.OrdenarPorLUBurbuja();
        else
            servicio.OrdenarPorLUQuickSort();

        FormSalidas formSalidas = new FormSalidas();
        int total = servicio.VerContador();
        for (int i = 0; i < total; i++)
            formSalidas.lsbListado.Items.Add(servicio.VerAlumno(i));

        formSalidas.ShowDialog();
    }
}
