# Algoritmos de Búsqueda y Ordenamiento

## Tabla de contenidos

1. [Introducción](#1-introducción)
2. [Búsqueda Secuencial](#2-búsqueda-secuencial)
3. [Búsqueda Binaria](#3-búsqueda-binaria)
4. [Comparación: Búsqueda Secuencial vs. Binaria](#4-comparación-búsqueda-secuencial-vs-binaria)
5. [Ordenamiento Burbuja](#5-ordenamiento-burbuja)
6. [Ordenamiento QuickSort](#6-ordenamiento-quicksort)
7. [Comparación: Burbuja vs. QuickSort](#7-comparación-burbuja-vs-quicksort)
8. [Conclusión](#8-conclusión)

---

## 1. Introducción

Dos operaciones aparecen constantemente en cualquier programa que trabaje con colecciones de datos: **buscar** un elemento y **ordenar** la colección.

- **Buscar** significa recorrer una colección para localizar un valor específico y conocer su posición.
- **Ordenar** significa reorganizar los elementos de una colección según un criterio (generalmente de menor a mayor).

Estas operaciones no son independientes: como se verá en la sección 3, el algoritmo de búsqueda más eficiente (Búsqueda Binaria) **exige** que la colección esté previamente ordenada. Comprender los algoritmos de búsqueda primero, y luego los de ordenamiento, permite entender por qué el ordenamiento existe y cuándo vale la pena aplicarlo.

### El vector de referencia

Para seguir todos los ejemplos de este documento se usa **siempre el mismo vector** de seis enteros:

```csharp
int[] numeros = { 5, 3, 8, 1, 9, 2 };
int n = 6; // cantidad de elementos válidos
```

Los índices van del `0` al `5`. El valor buscado en los ejemplos de búsqueda es siempre el **8**, que se encuentra en la posición `2`.

---

## 2. Búsqueda Secuencial

### Objetivo conceptual

La búsqueda secuencial responde a una pregunta simple: **¿está el valor `buscado` en alguna posición del vector?**

La estrategia es la más directa posible: empezar desde el principio y revisar cada elemento uno por uno hasta encontrar el valor o agotar el vector. No hace ninguna suposición sobre el orden de los elementos.

> **Idea central:** si recorro todos los elementos y ninguno coincide, el valor no está.

El costo de esta búsqueda depende de cuántos elementos hay: en el peor caso (el valor no existe o está al final) se revisan **todos** los elementos.

### Código en C#

```csharp
public static int BuscarSecuencial(int[] numeros, int n, int buscado)
{
    for (int i = 0; i < n; i++)
    {
        if (numeros[i] == buscado)
            return i;       // retorna la posición donde lo encontró
    }
    return -1;              // retorna -1 si no lo encontró
}
```

El método retorna el **índice** del elemento encontrado, o `-1` si no existe. Esta convención (retornar `-1` como "no encontrado") es la misma utilizada en `NumericService` de ambos ejercicios.

### Seguimiento paso a paso

Vector: `{ 5, 3, 8, 1, 9, 2 }` — buscamos el **8**

| Paso | i | `numeros[i]` | ¿Es igual a 8? | Acción |
|:---:|:---:|:---:|:---:|---|
| 1 | 0 | 5 | No | Continúa |
| 2 | 1 | 3 | No | Continúa |
| 3 | 2 | 8 | **Sí** | Retorna `2` |

El algoritmo encontró el **8** en la posición `2` tras revisar 3 elementos.

**Caso: valor no encontrado** — buscamos el **7**

| Paso | i | `numeros[i]` | ¿Es igual a 7? | Acción |
|:---:|:---:|:---:|:---:|---|
| 1 | 0 | 5 | No | Continúa |
| 2 | 1 | 3 | No | Continúa |
| 3 | 2 | 8 | No | Continúa |
| 4 | 3 | 1 | No | Continúa |
| 5 | 4 | 9 | No | Continúa |
| 6 | 5 | 2 | No | Continúa |
| — | — | — | — | Retorna `-1` |

En este caso se revisaron los **6 elementos** antes de concluir que el valor no existe.

### Diagrama de flujo

```mermaid
flowchart TD
    A([Inicio]) --> B["i = 0"]
    B --> C{"i < n ?"}
    C -- No --> D(["Retorna -1\n— No encontrado —"])
    C -- Sí --> E{"numeros[i] == buscado ?"}
    E -- Sí --> F(["Retorna i\n— Encontrado —"])
    E -- No --> G["i = i + 1"]
    G --> C
```

### Observaciones clave

- No requiere ninguna condición previa sobre el vector (puede estar desordenado).
- En el **mejor caso** (el elemento está en la posición 0) se hace **1 comparación**.
- En el **peor caso** (el elemento no existe) se hacen **n comparaciones**.
- Es el único algoritmo aplicable cuando el vector **no está ordenado** y no queremos ordenarlo.

---

## 3. Búsqueda Binaria

### Objetivo conceptual

La búsqueda binaria responde a la misma pregunta que la secuencial —¿está el valor en el vector?— pero aprovechando una característica crucial: el vector **está ordenado de menor a mayor**.

> **Idea central:** si el vector está ordenado, al inspeccionar el elemento del medio puedo saber en qué mitad debe estar el valor buscado y descartar la otra mitad por completo. En cada paso, el espacio de búsqueda se **divide a la mitad**.

Imagine buscar una palabra en un diccionario: nadie lee desde la página 1. Se abre por el medio y se decide si la palabra está antes o después. La búsqueda binaria aplica exactamente esa lógica.

**Prerequisito obligatorio:** el vector debe estar **ordenado** antes de invocar este algoritmo. Si no lo está, los resultados son incorrectos.

### Código en C#

```csharp
public static int BuscarBinario(int[] numeros, int n, int buscado)
{
    int izq = 0;
    int der = n - 1;

    while (izq <= der)
    {
        int mid = (izq + der) / 2;

        if (numeros[mid] == buscado)
            return mid;             // encontrado en la posición mid

        if (numeros[mid] < buscado)
            izq = mid + 1;          // el valor está en la mitad derecha
        else
            der = mid - 1;          // el valor está en la mitad izquierda
    }

    return -1;                      // izq > der: el valor no existe
}
```

Las variables `izq` y `der` delimitan el **segmento activo** del vector (la región donde todavía puede estar el valor). Al comenzar, ese segmento cubre todo el vector. En cada iteración se estrecha.

### Seguimiento paso a paso

El vector debe estar ordenado. Usamos el resultado del ordenamiento: `{ 1, 2, 3, 5, 8, 9 }` — buscamos el **8**.

**Índices:** 0=1, 1=2, 2=3, 3=5, 4=8, 5=9

| Iteración | `izq` | `der` | `mid` | `numeros[mid]` | Decisión |
|:---:|:---:|:---:|:---:|:---:|---|
| 1 | 0 | 5 | 2 | 3 | 3 < 8 → `izq = mid + 1 = 3` |
| 2 | 3 | 5 | 4 | 8 | **8 == 8 → retorna `4`** |

El algoritmo encontró el **8** en la posición `4` revisando solo **2 elementos** (en lugar de los 6 de la secuencial).

**Caso: valor no encontrado** — buscamos el **7** en `{ 1, 2, 3, 5, 8, 9 }`

| Iteración | `izq` | `der` | `mid` | `numeros[mid]` | Decisión |
|:---:|:---:|:---:|:---:|:---:|---|
| 1 | 0 | 5 | 2 | 3 | 3 < 7 → `izq = 3` |
| 2 | 3 | 5 | 4 | 8 | 8 > 7 → `der = 3` |
| 3 | 3 | 3 | 3 | 5 | 5 < 7 → `izq = 4` |
| — | 4 | 3 | — | — | `izq > der` → retorna `-1` |

El valor **7** no existe. Se revisaron **3 elementos** para un vector de 6.

### Diagrama de flujo

```mermaid
flowchart TD
    A([Inicio]) --> B["izq = 0\nder = n - 1"]
    B --> C{"izq <= der ?"}
    C -- No --> D(["Retorna -1\n— No encontrado —"])
    C -- Sí --> E["mid = (izq + der) / 2"]
    E --> F{"numeros[mid] == buscado ?"}
    F -- Sí --> G(["Retorna mid\n— Encontrado —"])
    F -- No --> H{"numeros[mid] < buscado ?"}
    H -- Sí --> I["izq = mid + 1\n(descarta mitad izquierda)"]
    H -- No --> J["der = mid - 1\n(descarta mitad derecha)"]
    I --> C
    J --> C
```

### ¿Por qué se puede descartar la mitad?

Supongamos que `numeros[mid] = 3` y `buscado = 8`. Como el vector está ordenado y `3 < 8`, **todos los elementos a la izquierda de `mid` son menores o iguales a 3**, por lo tanto también son menores que 8. El valor 8 no puede estar en esa mitad. Se descarta por completo moviendo `izq = mid + 1`.

Este razonamiento es válido **únicamente** porque el vector está ordenado. En un vector desordenado, no se puede garantizar nada sobre los elementos adyacentes.

---

## 4. Comparación: Búsqueda Secuencial vs. Binaria

| Criterio | Búsqueda Secuencial | Búsqueda Binaria |
|---|---|---|
| **Prerequisito** | Ninguno — funciona con cualquier vector | El vector **debe estar ordenado** |
| **Estrategia** | Recorre uno por uno de izq. a der. | Divide el espacio de búsqueda a la mitad |
| **Comparaciones (caso promedio, n=6)** | ~3 | ~2 |
| **Comparaciones (peor caso, n=1.000.000)** | 1.000.000 | ~20 |
| **Comparaciones (peor caso, n=1.000.000.000)** | 1.000.000.000 | ~30 |
| **Cuándo usarla** | Vector desordenado; colección pequeña | Vector ordenado; colección grande |

La diferencia se vuelve drástica a medida que crece el vector. Para un millón de elementos, la búsqueda secuencial puede hacer un millón de comparaciones; la binaria nunca hace más de 20. Este es el beneficio de **pagar el costo de ordenar una sola vez** para luego buscar de forma muy eficiente muchas veces.

---

## 5. Ordenamiento Burbuja

### Objetivo conceptual

El ordenamiento burbuja reorganiza el vector de menor a mayor recorriendo repetidamente los elementos adyacentes e intercambiando los que están en el orden incorrecto.

> **Idea central:** en cada pasada completa por el vector, el elemento **más grande del segmento no ordenado** "flota" como una burbuja hacia el final. Después de la primera pasada, el mayor está en su lugar definitivo. Después de la segunda, el segundo mayor también. Y así sucesivamente.

El nombre proviene precisamente de esta imagen: los valores grandes suben hacia la derecha como burbujas de aire suben hacia la superficie del agua.

### Estructura del algoritmo: dos bucles anidados

```
Bucle exterior (i): controla la cantidad de pasadas.
    En la pasada i, los últimos i elementos ya están ordenados
    y no es necesario revisarlos.

Bucle interior (j): recorre los pares adyacentes del segmento no ordenado.
    Si numeros[j] > numeros[j+1] → están en el orden incorrecto → intercambiar.
```

### Código en C#

```csharp
public static void OrdenarBurbuja(int[] numeros, int n)
{
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - 1 - i; j++)
        {
            if (numeros[j] > numeros[j + 1])
                Intercambiar(numeros, j, j + 1);
        }
        // al terminar esta pasada, numeros[n-1-i] está en su lugar definitivo
    }
}

private static void Intercambiar(int[] numeros, int a, int b)
{
    int temp = numeros[a];
    numeros[a] = numeros[b];
    numeros[b] = temp;
}
```

Obsérvese que el límite del bucle interior es `n - 1 - i`: en cada pasada se revisa un elemento menos porque los últimos `i` ya están ordenados. Este detalle evita comparaciones innecesarias.

### Seguimiento paso a paso

Vector inicial: `{ 5, 3, 8, 1, 9, 2 }`, n = 6

---

#### Pasada i = 0 — el mayor (9) llega a su posición definitiva

| j | Comparación | ¿Intercambio? | Estado del vector |
|:---:|---|:---:|---|
| 0 | `numeros[0]=5 > numeros[1]=3` | **Sí** | `{ 3, 5, 8, 1, 9, 2 }` |
| 1 | `numeros[1]=5 > numeros[2]=8` | No | `{ 3, 5, 8, 1, 9, 2 }` |
| 2 | `numeros[2]=8 > numeros[3]=1` | **Sí** | `{ 3, 5, 1, 8, 9, 2 }` |
| 3 | `numeros[3]=8 > numeros[4]=9` | No | `{ 3, 5, 1, 8, 9, 2 }` |
| 4 | `numeros[4]=9 > numeros[5]=2` | **Sí** | `{ 3, 5, 1, 8, 2, 9 }` |

Resultado: `{ 3, 5, 1, 8, 2, `**`9`**` }` — el **9** está en su lugar definitivo.

---

#### Pasada i = 1 — el segundo mayor (8) llega a su posición definitiva

| j | Comparación | ¿Intercambio? | Estado del vector |
|:---:|---|:---:|---|
| 0 | `numeros[0]=3 > numeros[1]=5` | No | `{ 3, 5, 1, 8, 2, 9 }` |
| 1 | `numeros[1]=5 > numeros[2]=1` | **Sí** | `{ 3, 1, 5, 8, 2, 9 }` |
| 2 | `numeros[2]=5 > numeros[3]=8` | No | `{ 3, 1, 5, 8, 2, 9 }` |
| 3 | `numeros[3]=8 > numeros[4]=2` | **Sí** | `{ 3, 1, 5, 2, 8, 9 }` |

Resultado: `{ 3, 1, 5, 2, `**`8, 9`**` }` — el **8** y **9** están en sus lugares definitivos.

---

#### Pasada i = 2 — el 5 llega a su posición definitiva

| j | Comparación | ¿Intercambio? | Estado del vector |
|:---:|---|:---:|---|
| 0 | `numeros[0]=3 > numeros[1]=1` | **Sí** | `{ 1, 3, 5, 2, 8, 9 }` |
| 1 | `numeros[1]=3 > numeros[2]=5` | No | `{ 1, 3, 5, 2, 8, 9 }` |
| 2 | `numeros[2]=5 > numeros[3]=2` | **Sí** | `{ 1, 3, 2, 5, 8, 9 }` |

Resultado: `{ 1, 3, 2, `**`5, 8, 9`**` }` — el **5**, **8** y **9** están en sus lugares definitivos.

---

#### Pasada i = 3 — el 3 llega a su posición definitiva

| j | Comparación | ¿Intercambio? | Estado del vector |
|:---:|---|:---:|---|
| 0 | `numeros[0]=1 > numeros[1]=3` | No | `{ 1, 3, 2, 5, 8, 9 }` |
| 1 | `numeros[1]=3 > numeros[2]=2` | **Sí** | `{ 1, 2, 3, 5, 8, 9 }` |

Resultado: `{ 1, 2, `**`3, 5, 8, 9`**` }` — el **3**, **5**, **8** y **9** están en sus lugares definitivos.

---

#### Pasada i = 4 — verificación final

| j | Comparación | ¿Intercambio? | Estado del vector |
|:---:|---|:---:|---|
| 0 | `numeros[0]=1 > numeros[1]=2` | No | `{ 1, 2, 3, 5, 8, 9 }` |

Resultado: `{` **`1, 2, 3, 5, 8, 9`** `}` — vector completamente ordenado.

---

### Resumen visual de pasadas

| Pasada | Estado al finalizar | Elemento fijado |
|:---:|---|:---:|
| Inicio | `{ 5, 3, 8, 1, 9, 2 }` | — |
| i = 0 | `{ 3, 5, 1, 8, 2,` **`9`**` }` | 9 |
| i = 1 | `{ 3, 1, 5, 2,` **`8, 9`**` }` | 8 |
| i = 2 | `{ 1, 3, 2,` **`5, 8, 9`**` }` | 5 |
| i = 3 | `{ 1, 2,` **`3, 5, 8, 9`**` }` | 3 |
| i = 4 | `{` **`1, 2, 3, 5, 8, 9`**` }` | 2 y 1 |

### Observaciones clave

- Dos bucles anidados: el exterior hace **n−1** pasadas; el interior hace entre 1 y n−1 comparaciones por pasada.
- El total de comparaciones es siempre el mismo independientemente del contenido del vector.
- Si en alguna pasada no hubo ningún intercambio, el vector ya está ordenado y se podría detener antes (optimización opcional no incluida en la implementación base).

---

## 6. Ordenamiento QuickSort

### Objetivo conceptual

QuickSort ordena usando la estrategia de **divide y vencerás**: en lugar de comparar todos los pares adyacentes, elige un elemento llamado **pivote** y reorganiza el vector de modo que:

> **Todos los elementos menores o iguales al pivote quedan a su izquierda.**
> **Todos los elementos mayores quedan a su derecha.**
> **El pivote queda en su posición definitiva.**

Luego el proceso se **repite de forma independiente** para el subvector izquierdo y para el subvector derecho. Cada llamada recursiva trabaja sobre un segmento más pequeño hasta que llega a subvectores de 0 o 1 elementos (que ya están ordenados por definición).

### La partición: el corazón del algoritmo

La operación fundamental es la **partición**: dado un segmento `[izq..der]`, elegir el pivote (se toma el último elemento), recorrer el segmento y dejar:

```
[ elementos <= pivote | pivote | elementos > pivote ]
```

Para lograrlo se usan dos "cursores":
- `j`: recorre el segmento de izq. a der.
- `i`: marca el límite de la zona de "menores o iguales".

Cada vez que `numeros[j] <= pivote`, el elemento de la posición `j` pertenece a la zona izquierda: se incrementa `i` y se intercambia `numeros[i]` con `numeros[j]`. Al terminar, se coloca el pivote en la posición `i + 1`.

### Código en C#

```csharp
public static void OrdenarQuickSort(int[] numeros, int n)
{
    QuickSort(numeros, 0, n - 1);
}

private static void QuickSort(int[] numeros, int izq, int der)
{
    if (izq >= der)
        return;                             // caso base: 0 o 1 elementos

    int p = Particionar(numeros, izq, der); // p = posición definitiva del pivote

    QuickSort(numeros, izq, p - 1);         // ordena el subvector izquierdo
    QuickSort(numeros, p + 1, der);         // ordena el subvector derecho
}

private static int Particionar(int[] numeros, int izq, int der)
{
    int pivote = numeros[der];  // se elige el último elemento como pivote
    int i = izq - 1;           // límite de la zona "menores o iguales"

    for (int j = izq; j < der; j++)
    {
        if (numeros[j] <= pivote)
        {
            i++;
            Intercambiar(numeros, i, j);
        }
    }

    Intercambiar(numeros, i + 1, der); // coloca el pivote en su lugar definitivo
    return i + 1;                      // retorna la posición del pivote
}

private static void Intercambiar(int[] numeros, int a, int b)
{
    int temp = numeros[a];
    numeros[a] = numeros[b];
    numeros[b] = temp;
}
```

### Seguimiento paso a paso

Vector inicial: `{ 5, 3, 8, 1, 9, 2 }`, n = 6

---

#### Primera partición — QuickSort(0, 5)

Segmento completo: `{ 5, 3, 8, 1, 9, 2 }` — pivote = `numeros[5]` = **2**, i = −1

| j | `numeros[j]` | ¿<= pivote (2)? | i | Acción | Estado del vector |
|:---:|:---:|:---:|:---:|---|---|
| 0 | 5 | No | −1 | — | `{ 5, 3, 8, 1, 9, 2 }` |
| 1 | 3 | No | −1 | — | `{ 5, 3, 8, 1, 9, 2 }` |
| 2 | 8 | No | −1 | — | `{ 5, 3, 8, 1, 9, 2 }` |
| 3 | 1 | **Sí** | 0 | swap(0, 3) | `{ 1, 3, 8, 5, 9, 2 }` |
| 4 | 9 | No | 0 | — | `{ 1, 3, 8, 5, 9, 2 }` |

Al terminar el bucle: swap(i+1, der) = swap(1, 5) → `{ 1,` **`2`**`, 8, 5, 9, 3 }`

El **2** queda en la posición **1** de forma definitiva. La partición produce:

```
Izquierda [0..0]: { 1 }       — todos menores que 2
Pivote    [1]:    { 2 }       — en su lugar definitivo
Derecha   [2..5]: { 8, 5, 9, 3 }  — todos mayores que 2
```

---

#### Segunda partición — QuickSort(2, 5)

Segmento: `{ 8, 5, 9, 3 }` (posiciones 2 a 5) — pivote = `numeros[5]` = **3**, i = 1

| j | `numeros[j]` | ¿<= pivote (3)? | i | Acción | Estado del vector |
|:---:|:---:|:---:|:---:|---|---|
| 2 | 8 | No | 1 | — | `{ 1, 2, 8, 5, 9, 3 }` |
| 3 | 5 | No | 1 | — | `{ 1, 2, 8, 5, 9, 3 }` |
| 4 | 9 | No | 1 | — | `{ 1, 2, 8, 5, 9, 3 }` |

Al terminar el bucle: swap(i+1, der) = swap(2, 5) → `{ 1, 2,` **`3`**`, 5, 9, 8 }`

El **3** queda en la posición **2** de forma definitiva:

```
Izquierda [2..1]: vacío         — ningún elemento menor que 3 en el segmento
Pivote    [2]:    { 3 }         — en su lugar definitivo
Derecha   [3..5]: { 5, 9, 8 }  — todos mayores que 3
```

---

#### Tercera partición — QuickSort(3, 5)

Segmento: `{ 5, 9, 8 }` (posiciones 3 a 5) — pivote = `numeros[5]` = **8**, i = 2

| j | `numeros[j]` | ¿<= pivote (8)? | i | Acción | Estado del vector |
|:---:|:---:|:---:|:---:|---|---|
| 3 | 5 | **Sí** | 3 | swap(3, 3) | `{ 1, 2, 3, 5, 9, 8 }` |
| 4 | 9 | No | 3 | — | `{ 1, 2, 3, 5, 9, 8 }` |

Al terminar el bucle: swap(i+1, der) = swap(4, 5) → `{ 1, 2, 3, 5,` **`8`**`, 9 }`

El **8** queda en la posición **4** de forma definitiva:

```
Izquierda [3..3]: { 5 }  — caso base (un solo elemento)
Pivote    [4]:    { 8 }  — en su lugar definitivo
Derecha   [5..5]: { 9 }  — caso base (un solo elemento)
```

---

### Árbol de llamadas recursivas

Cada nodo muestra el segmento procesado y el pivote resultante. Las hojas son casos base (0 o 1 elementos).

```mermaid
graph TD
    A["QuickSort(0, 5)\n{ 5, 3, 8, 1, 9, 2 }\npivote = 2 → pos. 1"]
    A --> B["QuickSort(0, 0)\n{ 1 }\ncaso base ✓"]
    A --> C["QuickSort(2, 5)\n{ 8, 5, 9, 3 }\npivote = 3 → pos. 2"]
    C --> D["QuickSort(2, 1)\nvacío\ncaso base ✓"]
    C --> E["QuickSort(3, 5)\n{ 5, 9, 8 }\npivote = 8 → pos. 4"]
    E --> F["QuickSort(3, 3)\n{ 5 }\ncaso base ✓"]
    E --> G["QuickSort(5, 5)\n{ 9 }\ncaso base ✓"]
```

**Resultado final: `{ 1, 2, 3, 5, 8, 9 }`** ✓

### Observaciones clave

- El pivote queda en su posición definitiva después de cada partición: **nunca se mueve de nuevo**.
- La recursión continúa sobre subvectores cada vez más pequeños hasta llegar al caso base (`izq >= der`).
- El orden en que se resuelven las llamadas recursivas (primero la izquierda, luego la derecha, o viceversa) no afecta el resultado final.
- La eficiencia depende de la elección del pivote: elegir siempre el último elemento funciona bien en promedio, pero puede degradarse con vectores ya ordenados (razón por la que variantes avanzadas eligen el pivote de otra manera).

---

## 7. Comparación: Burbuja vs. QuickSort

### Tabla comparativa

| Criterio | Burbuja | QuickSort |
|---|---|---|
| **Estrategia** | Intercambia pares adyacentes repetidamente | Particiona alrededor de un pivote; divide y vencerás |
| **Estructura** | Dos bucles `for` anidados, iterativo | Función recursiva con llamadas sobre subvectores |
| **¿Necesita recursión?** | No | Sí |
| **Comparaciones (peor caso, n=6)** | 15 | ~9 |
| **Comparaciones (peor caso, n=1.000.000)** | ~500.000.000.000 | ~20.000.000 |
| **Intercambios por operación** | 1 par adyacente | Puede ser no adyacente |
| **Posición definitiva del pivote** | No aplica | Después de cada partición |
| **Facilidad de comprensión** | Alta — lógica directa y visual | Media — requiere entender recursión y partición |
| **Cuándo usarlo** | Colecciones pequeñas, fines didácticos | Colecciones de cualquier tamaño en producción |

### Ejemplo concreto de la diferencia en escala

Para ordenar un vector de **1.000.000 de elementos**:

- **Burbuja** realiza aproximadamente 500.000 millones de comparaciones. En una computadora moderna, esto puede tardar minutos u horas.
- **QuickSort** realiza aproximadamente 20 millones de comparaciones. El mismo vector se ordena en **milisegundos**.

La diferencia no es de grado sino de **naturaleza**: burbuja crece con el cuadrado del tamaño (`n²`); QuickSort crece mucho más lentamente (`n · log₂ n`).

---

## 8. Conclusión

Los cuatro algoritmos presentados en este documento cubren dos operaciones fundamentales y dos niveles de eficiencia para cada una:

| Operación | Algoritmo simple | Algoritmo eficiente | Condición de uso del eficiente |
|---|---|---|---|
| **Búsqueda** | Secuencial | Binaria | El vector debe estar **ordenado** |
| **Ordenamiento** | Burbuja | QuickSort | Ninguna condición especial |

### La relación entre ordenar y buscar

El vínculo entre estas cuatro operaciones no es arbitrario: la búsqueda binaria es el **motivo** por el que ordenar tiene valor. Ordenar cuesta tiempo ahora; buscar de forma binaria ahorra tiempo después. Si se van a realizar muchas búsquedas sobre el mismo conjunto de datos, pagar el costo de ordenar una sola vez es ampliamente rentable.

### Progresión de complejidad

```
Búsqueda Secuencial → comprensible sin ningún prerequisito
Búsqueda Binaria    → requiere orden; introduce el concepto de "descartar mitades"
Ordenamiento Burbuja → dos bucles anidados; introduce intercambio e invariante de pasada
QuickSort           → introduce recursión, pivote, partición y "divide y vencerás"
```

Cada algoritmo incorpora una idea nueva sobre la anterior. Comprender esta progresión no solo permite usar los algoritmos: permite **elegir el correcto** según el contexto, que es la habilidad definitiva del programador.
