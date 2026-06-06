namespace Ejercicio1_VectoresIndiceComun;

partial class FormPrincipal
{
    private System.ComponentModel.IContainer components = null;

    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private Button btnRegistrarAlumno;
    private TextBox tbLU;
    private Button btnBuscarYVerAlumno;
    private RadioButton rbSecuencial;
    private RadioButton rbBinaria;
    private Button btnMostrarListadoOrdenado;
    private RadioButton rbBurbuja;
    private RadioButton rbQuickSort;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.groupBox1 = new GroupBox();
        this.btnRegistrarAlumno = new Button();
        this.groupBox2 = new GroupBox();
        this.tbLU = new TextBox();
        this.btnBuscarYVerAlumno = new Button();
        this.rbSecuencial = new RadioButton();
        this.rbBinaria = new RadioButton();
        this.groupBox3 = new GroupBox();
        this.btnMostrarListadoOrdenado = new Button();
        this.rbBurbuja = new RadioButton();
        this.rbQuickSort = new RadioButton();

        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.SuspendLayout();

        // groupBox1 - Solicitud datos alumno
        this.groupBox1.Location = new Point(12, 12);
        this.groupBox1.Size = new Size(360, 70);
        this.groupBox1.Text = "Solicitud datos alumno";
        this.groupBox1.Controls.Add(this.btnRegistrarAlumno);

        // btnRegistrarAlumno
        this.btnRegistrarAlumno.Location = new Point(100, 28);
        this.btnRegistrarAlumno.Size = new Size(120, 30);
        this.btnRegistrarAlumno.Text = "Registrar";
        this.btnRegistrarAlumno.Click += new EventHandler(this.btnRegistrarAlumno_Click);

        // groupBox2 - Salidas (búsqueda)
        this.groupBox2.Location = new Point(12, 95);
        this.groupBox2.Size = new Size(360, 110);
        this.groupBox2.Text = "Salidas";
        this.groupBox2.Controls.AddRange(new Control[] {
            this.tbLU, this.btnBuscarYVerAlumno, this.rbSecuencial, this.rbBinaria
        });

        // LU label + tbLU
        Label lblLU = new Label();
        lblLU.AutoSize = true;
        lblLU.Location = new Point(15, 30);
        lblLU.Text = "LU:";
        this.groupBox2.Controls.Add(lblLU);

        this.tbLU.Location = new Point(50, 27);
        this.tbLU.Size = new Size(100, 23);

        this.btnBuscarYVerAlumno.Location = new Point(165, 25);
        this.btnBuscarYVerAlumno.Size = new Size(80, 30);
        this.btnBuscarYVerAlumno.Text = "Buscar";
        this.btnBuscarYVerAlumno.Click += new EventHandler(this.btnBuscarAlumno_Click);

        this.rbSecuencial.Location = new Point(15, 65);
        this.rbSecuencial.AutoSize = true;
        this.rbSecuencial.Checked = true;
        this.rbSecuencial.Text = "Secuencial";

        this.rbBinaria.Location = new Point(130, 65);
        this.rbBinaria.AutoSize = true;
        this.rbBinaria.Text = "Binaria";

        // groupBox3 - Salidas (listado)
        this.groupBox3.Location = new Point(12, 220);
        this.groupBox3.Size = new Size(360, 110);
        this.groupBox3.Text = "Salidas";
        this.groupBox3.Controls.AddRange(new Control[] {
            this.btnMostrarListadoOrdenado, this.rbBurbuja, this.rbQuickSort
        });

        this.btnMostrarListadoOrdenado.Location = new Point(100, 25);
        this.btnMostrarListadoOrdenado.Size = new Size(130, 30);
        this.btnMostrarListadoOrdenado.Text = "Listar Ordenado";
        this.btnMostrarListadoOrdenado.Click += new EventHandler(this.btnMostrarListadoOrdenado_Click);

        this.rbBurbuja.Location = new Point(15, 70);
        this.rbBurbuja.AutoSize = true;
        this.rbBurbuja.Checked = true;
        this.rbBurbuja.Text = "Burbuja";

        this.rbQuickSort.Location = new Point(130, 70);
        this.rbQuickSort.AutoSize = true;
        this.rbQuickSort.Checked = false;
        this.rbQuickSort.Text = "QuickSort";

        // FormPrincipal
        this.ClientSize = new Size(390, 350);
        this.Controls.AddRange(new Control[] {
            this.groupBox1, this.groupBox2, this.groupBox3
        });
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "FormPrincipal";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Actividad 12.";

        this.groupBox1.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        this.ResumeLayout(false);
    }
}
