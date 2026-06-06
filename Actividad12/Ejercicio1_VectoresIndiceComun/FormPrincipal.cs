namespace Ejercicio1_VectoresIndiceComun;

public partial class FormPrincipal : Form
{
    // DECLARACIÓN del servicio: el formulario (capa de presentación) delega toda
    // la responsabilidad de datos y algoritmos en NumericService (capa de datos).
    private Services.NumericService servicio;

    public FormPrincipal()
    {
        InitializeComponent();                     // crea los controles del Designer
        servicio = new Services.NumericService();  // INSTANCIACIÓN del servicio
    }

    // Manejador del evento Click: se ejecuta al presionar "Registrar".
    private void btnRegistrarAlumno_Click(object sender, EventArgs e)
    {
        // VENTANA MODAL: ShowDialog() abre FormDatos y BLOQUEA esta ventana hasta cerrarla.
        // Solo se procesan los datos si el usuario confirmó (DialogResult.OK).
        FormDatos formDatos = new FormDatos();
        if (formDatos.ShowDialog() == DialogResult.OK)
        {
            int lu;
            try
            {
                // ACCESO POR INSTANCIA: tbLU es un miembro de instancia de FormDatos,
                // por eso se accede a través del objeto formDatos (formDatos.tbLU).
                // Es visible desde fuera del form porque su propiedad Modifiers = Public
                // en el Diseñador (por defecto los controles serían private).
                // .Text es a su vez una propiedad de instancia del TextBox y siempre
                // devuelve string, de ahí la necesidad de convertirlo a número.
                lu = Convert.ToInt32(formDatos.tbLU.Text);
            }
            catch
            {
                // MessageBox: diálogo de sistema con icono de error.
                MessageBox.Show("LU inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double nota;
            try
            {
                // Mismo acceso por instancia y propiedad Text (string) que tbLU.
                nota = Convert.ToDouble(formDatos.tbNota.Text);
            }
            catch
            {
                MessageBox.Show("Nota inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nombre = formDatos.tbNombre.Text;
            // SECUENCIA: el formulario invoca la operación de alto nivel del servicio.
            servicio.RegistrarAlumno(lu, nombre, nota);
            // VerContador(): encapsulamiento con método (lectura controlada del total).
            MessageBox.Show("Alumno registrado. Total: " + servicio.VerContador(), "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Manejador del evento Click: se ejecuta al presionar "Buscar".
    private void btnBuscarAlumno_Click(object sender, EventArgs e)
    {
        int lu;
        try
        {
            lu = Convert.ToInt32(tbLU.Text); // lee el LU a buscar desde el TextBox
        }
        catch
        {
            MessageBox.Show("LU inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        int idx;
        // RadioButton.Checked: determina qué algoritmo de búsqueda eligió el usuario.
        if (rbSecuencial.Checked)
            idx = servicio.BuscarPorLUSecuencial(lu);
        else
        {
            // La búsqueda binaria EXIGE ordenar el vector antes de invocarla.
            servicio.OrdenarPorLUBurbuja();
            idx = servicio.BuscarPorLUBinario(lu);
        }

        // El servicio devuelve -1 cuando el alumno no existe.
        if (idx == -1)
            MessageBox.Show("Alumno con LU " + lu + " no encontrado.", "Búsqueda",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
            MessageBox.Show(servicio.VerAlumno(idx), "Alumno encontrado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Manejador del evento Click: se ejecuta al presionar "Listar Ordenado".
    private void btnMostrarListadoOrdenado_Click(object sender, EventArgs e)
    {
        // RadioButton.Checked: elige el algoritmo de ordenamiento.
        if (rbBurbuja.Checked)
            servicio.OrdenarPorLUBurbuja();
        else
            servicio.OrdenarPorLUQuickSort();

        // Se crea la ventana de salida y se llena su ListBox antes de mostrarla.
        FormSalidas formSalidas = new FormSalidas();
        int total = servicio.VerContador();
        for (int i = 0; i < total; i++)
            formSalidas.lsbListado.Items.Add(servicio.VerAlumno(i)); // agrega una línea por alumno

        formSalidas.ShowDialog(); // muestra el listado como ventana modal
    }
}
