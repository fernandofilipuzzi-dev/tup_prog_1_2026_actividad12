namespace Ejercicio2_VectorObjeto.Services;

// SERVICIO = capa de ABSTRACCIÓN: el formulario pide operaciones de alto nivel
// sin conocer si por dentro hay vectores separados o un vector de objetos.
public class NumericService
{
    // VECTOR DE OBJETOS: un único arreglo cuyo tipo base es la clase Alumno.
    // Cada celda guarda una REFERENCIA a un objeto que ya agrupa LU, Nombre y Nota.
    private Models.Alumno[] alumnos = new Models.Alumno[100];
    // ENCAPSULAMIENTO con propiedad: get público (lectura externa) y set privado
    // (solo esta clase puede modificar el contador).
    public int Contador { get; private set; } = 0;

    public NumericService()
    {
        // El arreglo nace lleno de referencias null: hay que INSTANCIAR cada objeto
        // Alumno antes de poder asignarle valores a sus propiedades.
        for (int i = 0; i < alumnos.Length; i++)
            alumnos[i] = new Models.Alumno();
    }

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        // Se accede a las propiedades del objeto ubicado en la posición Contador.
        alumnos[Contador].LU = lu;
        alumnos[Contador].Nombre = nombre;
        alumnos[Contador].Nota = nota.ToString();
        Contador++; // modificación interna permitida (set privado)
    }

    public string VerAlumno(int idx)
    {
        return "LU: " + alumnos[idx].LU + "  Nombre: " + alumnos[idx].Nombre + "  Nota: " + alumnos[idx].Nota;
    }

    // Fachada de búsqueda: el formulario solo pasa el método elegido (0 o 1).
    public int BuscarPorLU(int lu, int metodo)
    {
        if (metodo == 0)
            return BuscarPorLUSecuencial(lu);
        else
        {
            OrdenarPorLU(0);                // la binaria EXIGE el vector ordenado primero
            return BuscarPorLUBinario(lu);
        }
    }

    // BÚSQUEDA SECUENCIAL: recorre uno por uno; no exige orden previo.
    public int BuscarPorLUSecuencial(int lu)
    {
        for (int i = 0; i < Contador; i++)
            if (alumnos[i].LU == lu) return i;
        return -1; // convención: -1 significa "no encontrado"
    }

    // BÚSQUEDA BINARIA: requiere el vector ORDENADO; descarta media zona por paso.
    public int BuscarPorLUBinario(int lu)
    {
        int izq = 0, der = Contador - 1; // segmento activo donde aún puede estar el valor
        while (izq <= der)
        {
            int mid = (izq + der) / 2; // elemento del medio
            if (alumnos[mid].LU == lu) return mid;
            if (alumnos[mid].LU < lu) izq = mid + 1; // buscar en la mitad derecha
            else der = mid - 1;                      // buscar en la mitad izquierda
        }
        return -1; // izq > der: el valor no existe
    }

    // Fachada de ordenamiento: el formulario solo pasa el método elegido (0 o 1).
    public void OrdenarPorLU(int metodo)
    {
        if (metodo == 0)
            OrdenarPorLUBurbuja();
        else
            OrdenarPorLUQuickSort();
    }

    // ORDENAMIENTO BURBUJA (por intercambio): en cada pasada se fija la posición i
    // con el menor LU que quede entre i y el final.
    public void OrdenarPorLUBurbuja()
    {
        for (int i = 0; i < Contador - 1; i++)        // i = posición que queda fija en esta pasada
            for (int j = i + 1; j < Contador; j++)    // compara la posición i contra el resto
                if (alumnos[j-1].LU > alumnos[j].LU)    // hay un LU menor más adelante
                    Intercambiar(j-1, j);               // lo trae a la posición i
    }

    public void OrdenarPorLUQuickSort()
    {
        QuickSort(0, Contador - 1);
    }

    // QUICKSORT: divide y vencerás. Particiona alrededor de un pivote y se repite
    // de forma recursiva sobre el subgrupo izquierdo y el derecho.
    private void QuickSort(int izq, int der)
    {
        if (izq >= der) return;        // caso base: 0 o 1 elemento (ya ordenado)
        int pivote = alumnos[der].LU;  // se elige el último elemento como pivote
        int i = izq - 1;               // i marca el límite de la zona de "menores o iguales"
        for (int j = izq; j < der; j++)
        {
            if (alumnos[j].LU <= pivote) // este elemento pertenece a la zona izquierda
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

    // COHESIÓN DEL OBJETO: basta intercambiar las REFERENCIAS. Los tres atributos
    // viajan juntos porque pertenecen al mismo objeto (sin riesgo de desincronización).
    private void Intercambiar(int a, int b)
    {
        Models.Alumno temp = alumnos[a];
        alumnos[a] = alumnos[b];
        alumnos[b] = temp;
    }
}
