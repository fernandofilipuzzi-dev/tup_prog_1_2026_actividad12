namespace Ejercicio1_VectoresIndiceComun.Services;

public class NumericService
{
    private int[] LUs = new int[100];
    private string[] Nombres = new string[100];
    private double[] Notas = new double[100];
    private int contador = 0;

    public NumericService() { }

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        LUs[contador] = lu;
        Nombres[contador] = nombre;
        Notas[contador] = nota;
        contador++;
    }

    public int VerContador()
    {
        return contador;
    }

    public string VerAlumno(int idx)
    {
        return "LU: " + LUs[idx] + "  Nombre: " + Nombres[idx] + "  Nota: " + Notas[idx];
    }

    public int BuscarPorLUSecuencial(int lu)
    {
        for (int i = 0; i < contador; i++)
            if (LUs[i] == lu) return i;
        return -1;
    }

    public int BuscarPorLUBinario(int lu)
    {
        int izq = 0, der = contador - 1;
        while (izq <= der)
        {
            int mid = (izq + der) / 2;
            if (LUs[mid] == lu) return mid;
            if (LUs[mid] < lu) izq = mid + 1;
            else der = mid - 1;
        }
        return -1;
    }

    public void OrdenarPorLUBurbuja()
    {
        for (int i = 0; i < contador - 1; i++)
            for (int j = 0; j < contador - 1 - i; j++)
                if (LUs[j] > LUs[j + 1])
                    Intercambiar(j, j + 1);
    }

    public void OrdenarPorLUQuickSort() => QuickSort(0, contador - 1);

    private void QuickSort(int izq, int der)
    {
        if (izq >= der) return;
        int pivote = LUs[der];
        int i = izq - 1;
        for (int j = izq; j < der; j++)
        {
            if (LUs[j] <= pivote)
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
        (LUs[a], LUs[b]) = (LUs[b], LUs[a]);
        (Nombres[a], Nombres[b]) = (Nombres[b], Nombres[a]);
        (Notas[a], Notas[b]) = (Notas[b], Notas[a]);
    }
}
