namespace Ejercicio2_VectorObjeto;

public partial class FormPrincipal : Form
{
    // DECLARACIÓN del servicio: el formulario (presentación) delega los datos y
    // algoritmos en NumericService (capa de datos), sin conocer su implementación.
    private Services.NumericService servicio;

    public FormPrincipal()
    {
        InitializeComponent();                     // crea los controles del Designer
        servicio = new Services.NumericService();  // INSTANCIACIÓN del servicio
    }

    // Manejador del evento Click: se ejecuta al presionar "Registrar".
    private void btnRegistrarAlumno_Click(object sender, EventArgs e)
    {
        // VENTANA MODAL: ShowDialog() bloquea esta ventana hasta cerrar FormDatos.
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
            // servicio.Contador: lectura de la propiedad (get público, set privado).
            MessageBox.Show("Alumno registrado. Total: " + servicio.Contador, "Info",
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

        // RadioButton.Checked: traduce la elección del usuario a un código de método.
        int metodo;
        if (rbSecuencial.Checked)
            metodo = 0; // 0 = búsqueda secuencial
        else
            metodo = 1; // 1 = búsqueda binaria (el servicio ordena internamente)
        // El método fachada decide el algoritmo según el código recibido.
        int idx = servicio.BuscarPorLU(lu, metodo);

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
        // RadioButton.Checked: traduce la elección a un código de método de ordenamiento.
        int metodo;
        if (rbBurbuja.Checked)
            metodo = 0; // 0 = burbuja
        else
            metodo = 1; // 1 = quicksort
        servicio.OrdenarPorLU(metodo); // fachada: elige el algoritmo según el código

        // Se crea la ventana de salida y se llena su ListBox antes de mostrarla.
        FormSalidas formSalidas = new FormSalidas();
        for (int i = 0; i < servicio.Contador; i++)
            formSalidas.lsbListado.Items.Add(servicio.VerAlumno(i)); // una línea por alumno

        formSalidas.ShowDialog(); // muestra el listado como ventana modal
    }
}
