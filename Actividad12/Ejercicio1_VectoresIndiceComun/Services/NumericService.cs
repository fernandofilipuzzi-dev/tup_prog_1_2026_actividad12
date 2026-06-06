namespace Ejercicio1_VectoresIndiceComun.Services;

// SERVICIO = capa de ABSTRACCIÓN: oculta cómo se almacenan los datos y cómo
// funcionan los algoritmos. El formulario solo pide operaciones de alto nivel.
public class NumericService
{
    // VECTORES DE ÍNDICE COMÚN: tres vectores separados donde la misma posición i
    // describe al mismo alumno (LUs[i], Nombres[i] y Notas[i] son inseparables).
    private int[] LUs = new int[100];
    private string[] Nombres = new string[100];
    private double[] Notas = new double[100];
    // Campo PRIVADO: el exterior no puede leerlo ni modificarlo directamente.
    private int contador = 0;

    public NumericService() { }

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        // Los tres valores se insertan en la MISMA posición (contador): índice compartido.
        LUs[contador] = lu;
        Nombres[contador] = nombre;
        Notas[contador] = nota;
        contador++; // avanza el índice para el próximo alumno
    }

    // ENCAPSULAMIENTO con método: expone el contador de forma controlada (solo lectura).
    public int VerContador()
    {
        return contador;
    }

    public string VerAlumno(int idx)
    {
        return "LU: " + LUs[idx] + "  Nombre: " + Nombres[idx] + "  Nota: " + Notas[idx];
    }

    // BÚSQUEDA SECUENCIAL: recorre uno por uno; no exige que el vector esté ordenado.
    public int BuscarPorLUSecuencial(int lu)
    {
        for (int i = 0; i < contador; i++)
            if (LUs[i] == lu) return i;
        return -1; // convención: -1 significa "no encontrado"
    }

    // BÚSQUEDA BINARIA: requiere el vector ORDENADO. En cada paso descarta media zona.
    public int BuscarPorLUBinario(int lu)
    {
        int izq = 0, der = contador - 1; // segmento activo donde aún puede estar el valor
        while (izq <= der)
        {
            int mid = (izq + der) / 2; // elemento del medio
            if (LUs[mid] == lu) return mid;
            if (LUs[mid] < lu) izq = mid + 1; // el valor está en la mitad derecha
            else der = mid - 1;               // el valor está en la mitad izquierda
        }
        return -1; // izq > der: el valor no existe
    }

    // ORDENAMIENTO BURBUJA (por intercambio): en cada pasada se fija la posición i
    // con el menor LU que quede entre i y el final.
    public void OrdenarPorLUBurbuja()
    {
        for (int i = 0; i < contador - 1; i++)        // i = posición que queda fija en esta pasada
            for (int j = i + 1; j < contador; j++)    // compara la posición i contra el resto
                if (LUs[j-1] > LUs[j])                  // hay un LU menor más adelante
                    Intercambiar(j-1, j);               // lo trae a la posición i
    }

    public void OrdenarPorLUQuickSort() => QuickSort(0, contador - 1);

    // QUICKSORT: divide y vencerás. Particiona alrededor de un pivote y se repite
    // de forma recursiva sobre el subgrupo izquierdo y el derecho.
    private void QuickSort(int izq, int der)
    {
        if (izq >= der) return;   // caso base: segmento de 0 o 1 elemento (ya ordenado)
        int pivote = LUs[der];    // se elige el último elemento como pivote
        int i = izq - 1;          // i marca el límite de la zona de "menores o iguales"
        for (int j = izq; j < der; j++)
        {
            if (LUs[j] <= pivote) // este elemento pertenece a la zona izquierda
            {
                i++;
                Intercambiar(i, j);
            }
        }
        Intercambiar(i + 1, der); // coloca el pivote en su posición DEFINITIVA
        int p = i + 1;
        QuickSort(izq, p - 1);    // ordena el grupo de los menores
        QuickSort(p + 1, der);    // ordena el grupo de los mayores
    }

    // Al mover un alumno hay que intercambiar los TRES vectores a la vez para
    // no desincronizar el índice común (fragilidad propia de este diseño).
    private void Intercambiar(int a, int b)
    {
        (LUs[a], LUs[b]) = (LUs[b], LUs[a]);
        (Nombres[a], Nombres[b]) = (Nombres[b], Nombres[a]);
        (Notas[a], Notas[b]) = (Notas[b], Notas[a]);
    }
}
