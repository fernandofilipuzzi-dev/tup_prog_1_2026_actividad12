namespace Ejercicio2_VectorObjeto;

partial class FormDatos
{
    private System.ComponentModel.IContainer components = null;

    public TextBox tbLU = null!;
    public TextBox tbNombre = null!;
    public TextBox tbNota = null!;
    private Label label1;
    private Label label2;
    private Label label3;
    private Button btnAceptar;
    private Button btnCancelar;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.label1 = new Label();
        this.label2 = new Label();
        this.label3 = new Label();
        this.tbLU = new TextBox();
        this.tbNombre = new TextBox();
        this.tbNota = new TextBox();
        this.btnAceptar = new Button();
        this.btnCancelar = new Button();
        this.SuspendLayout();

        // label1 - LU
        this.label1.AutoSize = true;
        this.label1.Location = new Point(20, 20);
        this.label1.Text = "LU:";

        // tbLU
        this.tbLU.Location = new Point(100, 17);
        this.tbLU.Size = new Size(160, 23);

        // label2 - Nombre
        this.label2.AutoSize = true;
        this.label2.Location = new Point(20, 55);
        this.label2.Text = "Nombre:";

        // tbNombre
        this.tbNombre.Location = new Point(100, 52);
        this.tbNombre.Size = new Size(160, 23);

        // label3 - Nota
        this.label3.AutoSize = true;
        this.label3.Location = new Point(20, 90);
        this.label3.Text = "Nota:";

        // tbNota
        this.tbNota.Location = new Point(100, 87);
        this.tbNota.Size = new Size(160, 23);

        // btnCancelar
        this.btnCancelar.DialogResult = DialogResult.Cancel;
        this.btnCancelar.Location = new Point(80, 130);
        this.btnCancelar.Size = new Size(80, 30);
        this.btnCancelar.Text = "Cancelar";

        // btnAceptar
        this.btnAceptar.DialogResult = DialogResult.OK;
        this.btnAceptar.Location = new Point(180, 130);
        this.btnAceptar.Size = new Size(80, 30);
        this.btnAceptar.Text = "Aceptar";

        // FormDatos
        this.AcceptButton = this.btnAceptar;
        this.CancelButton = this.btnCancelar;
        this.ClientSize = new Size(290, 180);
        this.Controls.AddRange(new Control[] {
            this.label1, this.tbLU,
            this.label2, this.tbNombre,
            this.label3, this.tbNota,
            this.btnCancelar, this.btnAceptar
        });
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FormDatos";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Mostrar Valor";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
