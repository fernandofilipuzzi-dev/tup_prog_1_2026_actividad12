namespace Ejercicio2_VectorObjeto;

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
            MessageBox.Show("Alumno registrado. Total: " + servicio.Contador, "Info",
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

        int metodo;
        if (rbSecuencial.Checked)
            metodo = 0;
        else
            metodo = 1;
        int idx = servicio.BuscarPorLU(lu, metodo);

        if (idx == -1)
            MessageBox.Show("Alumno con LU " + lu + " no encontrado.", "Búsqueda",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
            MessageBox.Show(servicio.VerAlumno(idx), "Alumno encontrado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnMostrarListadoOrdenado_Click(object sender, EventArgs e)
    {
        int metodo;
        if (rbBurbuja.Checked)
            metodo = 0;
        else
            metodo = 1;
        servicio.OrdenarPorLU(metodo);

        FormSalidas formSalidas = new FormSalidas();
        for (int i = 0; i < servicio.Contador; i++)
            formSalidas.lsbListado.Items.Add(servicio.VerAlumno(i));

        formSalidas.ShowDialog();
    }
}
