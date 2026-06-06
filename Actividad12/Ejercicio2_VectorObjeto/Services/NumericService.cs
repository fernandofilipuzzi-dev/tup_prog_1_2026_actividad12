namespace Ejercicio2_VectorObjeto.Services;

public class NumericService
{
    private Models.Alumno[] alumnos = new Models.Alumno[100];
    public int Contador { get; private set; } = 0;

    public NumericService()
    {
        for (int i = 0; i < alumnos.Length; i++)
            alumnos[i] = new Models.Alumno();
    }

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        alumnos[Contador].LU = lu;
        alumnos[Contador].Nombre = nombre;
        alumnos[Contador].Nota = nota.ToString();
        Contador++;
    }

    public string VerAlumno(int idx)
    {
        return "LU: " + alumnos[idx].LU + "  Nombre: " + alumnos[idx].Nombre + "  Nota: " + alumnos[idx].Nota;
    }

    public int BuscarPorLU(int lu, int metodo)
    {
        if (metodo == 0)
            return BuscarPorLUSecuencial(lu);
        else
        {
            OrdenarPorLU(0);
            return BuscarPorLUBinario(lu);
        }
    }

    public int BuscarPorLUSecuencial(int lu)
    {
        for (int i = 0; i < Contador; i++)
            if (alumnos[i].LU == lu) return i;
        return -1;
    }

    public int BuscarPorLUBinario(int lu)
    {
        int izq = 0, der = Contador - 1;
        while (izq <= der)
        {
            int mid = (izq + der) / 2;
            if (alumnos[mid].LU == lu) return mid;
            if (alumnos[mid].LU < lu) izq = mid + 1;
            else der = mid - 1;
        }
        return -1;
    }

    public void OrdenarPorLU(int metodo)
    {
        if (metodo == 0)
            OrdenarPorLUBurbuja();
        else
            OrdenarPorLUQuickSort();
    }

    public void OrdenarPorLUBurbuja()
    {
        for (int i = 0; i < Contador - 1; i++)
            for (int j = 0; j < Contador - 1 - i; j++)
                if (alumnos[j].LU > alumnos[j + 1].LU)
                    Intercambiar(j, j + 1);
    }

    public void OrdenarPorLUQuickSort()
    {
        QuickSort(0, Contador - 1);
    }

    private void QuickSort(int izq, int der)
    {
        if (izq >= der) return;
        int pivote = alumnos[der].LU;
        int i = izq - 1;
        for (int j = izq; j < der; j++)
        {
            if (alumnos[j].LU <= pivote)
            {
                i++;
                Intercambiar(i, j);
            }
        }
        Intercambiar(i + 1, der);
        int p = i + 1;
        QuickSort(izq, p - 1);
        QuickSort(p + 1, der);
    }

    private void Intercambiar(int a, int b)
    {
        Models.Alumno temp = alumnos[a];
        alumnos[a] = alumnos[b];
        alumnos[b] = temp;
    }
}
