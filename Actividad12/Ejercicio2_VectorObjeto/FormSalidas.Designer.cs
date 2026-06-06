namespace Ejercicio2_VectorObjeto;

partial class FormSalidas
{
    private System.ComponentModel.IContainer components = null;

    public ListBox lsbListado = null!;
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
        this.lsbListado = new ListBox();
        this.btnAceptar = new Button();
        this.btnCancelar = new Button();
        this.SuspendLayout();

        // lsbListado
        this.lsbListado.Location = new Point(12, 12);
        this.lsbListado.Size = new Size(360, 200);

        // btnCancelar
        this.btnCancelar.DialogResult = DialogResult.Cancel;
        this.btnCancelar.Location = new Point(80, 225);
        this.btnCancelar.Size = new Size(80, 30);
        this.btnCancelar.Text = "Cancelar";

        // btnAceptar
        this.btnAceptar.DialogResult = DialogResult.OK;
        this.btnAceptar.Location = new Point(220, 225);
        this.btnAceptar.Size = new Size(80, 30);
        this.btnAceptar.Text = "Aceptar";

        // FormSalidas
        this.AcceptButton = this.btnAceptar;
        this.CancelButton = this.btnCancelar;
        this.ClientSize = new Size(385, 270);
        this.Controls.AddRange(new Control[] {
            this.lsbListado, this.btnCancelar, this.btnAceptar
        });
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FormSalidas";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Mostrar Valor";
        this.ResumeLayout(false);
    }
}
