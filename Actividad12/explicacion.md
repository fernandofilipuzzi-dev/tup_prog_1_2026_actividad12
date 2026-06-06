# Explicación de conceptos — Actividad 12

## Tabla de contenidos

1. [Introducción](#1-introducción)
2. [Vectores en C#](#2-vectores-en-c)
3. [Vectores de índice común](#3-vectores-de-índice-común)
4. [Encapsulamiento](#4-encapsulamiento)
   - 4.1 [Encapsulamiento con métodos](#41-encapsulamiento-con-métodos)
   - 4.2 [Encapsulamiento con propiedades (getters y setters)](#42-encapsulamiento-con-propiedades-getters-y-setters)
5. [Abstracción](#5-abstracción)
6. [Clases y objetos — la clase `Alumno`](#6-clases-y-objetos--la-clase-alumno)
7. [Vector de objetos](#7-vector-de-objetos)
8. [Comparación: vectores de índice común vs. vector de objetos](#8-comparación-vectores-de-índice-común-vs-vector-de-objetos)
9. [Conclusión](#9-conclusión)

---

## 1. Introducción

Esta actividad presenta dos soluciones para un mismo problema: registrar alumnos, buscarlos por legajo universitario (LU) y mostrarlos ordenados. Ambas soluciones comparten la misma interfaz gráfica y la misma lógica de búsqueda y ordenamiento, pero difieren fundamentalmente en **cómo organizan los datos en memoria**.

- El **Ejercicio 1** usa tres vectores separados que comparten un índice común.
- El **Ejercicio 2** usa un único vector de objetos de la clase `Alumno`.

Estudiar esta diferencia permite entender los conceptos de **encapsulamiento**, **abstracción** y **programación orientada a objetos** de manera progresiva, partiendo desde la estructura más sencilla hacia la más expresiva.

---

## 2. Vectores en C#

Un **vector** (o arreglo unidimensional) es una estructura de datos que almacena una colección de elementos del mismo tipo en posiciones de memoria contiguas. Cada elemento se accede mediante un **índice entero** que comienza en `0`.

```csharp
// Declaración e inicialización de un vector de 5 enteros
int[] numeros = new int[5];

// Asignación de valores
numeros[0] = 10;
numeros[1] = 20;
numeros[2] = 30;

// Lectura
int primero = numeros[0]; // primero == 10
```

### Características clave

| Característica | Detalle |
|---|---|
| **Tamaño fijo** | Se define al crear el arreglo y no cambia |
| **Acceso por índice** | O(1) — acceso directo a cualquier posición |
| **Tipo homogéneo** | Todos los elementos son del mismo tipo |
| **Base cero** | El primer elemento está en el índice `0` |

Un vector por sí solo representa una lista de valores de un único tipo. Cuando se necesita representar entidades con **múltiples atributos** (por ejemplo, un alumno que tiene LU, nombre y nota), la solución más directa —aunque no la más elegante— es usar varios vectores relacionados por su índice.

---

## 3. Vectores de índice común

### Definición

Los **vectores de índice común** son dos o más vectores que almacenan atributos distintos de una misma entidad, de manera que la **misma posición `i` en cada vector describe al mismo elemento**.

El índice actúa como la "clave" que une los datos dispersos: si el alumno número 3 tiene LU `4521`, nombre `"García"` y nota `8.5`, entonces:

```
LUs[3]     == 4521
Nombres[3] == "García"
Notas[3]   == 8.5
```

### Implementación en el Ejercicio 1

En `Ejercicio1_VectoresIndiceComun/Services/NumericService.cs`, los datos se organizan así:

```csharp
public class NumericService
{
    private int[]    LUs     = new int[100];
    private string[] Nombres = new string[100];
    private double[] Notas   = new double[100];
    private int contador = 0;

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        LUs[contador]     = lu;
        Nombres[contador] = nombre;
        Notas[contador]   = nota;
        contador++;          // avanza el índice para el próximo alumno
    }
}
```

Cuando se registra un alumno, los tres valores se insertan **en la misma posición `contador`** en sus respectivos vectores. El contador actúa como índice compartido.

### El problema de mantener la coherencia

La consecuencia más importante de este diseño es que cualquier operación que modifique el orden de un vector **debe modificar también los demás**. Por ejemplo, al ordenar por LU con el algoritmo burbuja:

```csharp
private void Intercambiar(int a, int b)
{
    // Si se intercambia LUs, hay que intercambiar Nombres y Notas también
    (LUs[a],     LUs[b])     = (LUs[b],     LUs[a]);
    (Nombres[a], Nombres[b]) = (Nombres[b], Nombres[a]);
    (Notas[a],   Notas[b])   = (Notas[b],   Notas[a]);
}
```

Si se olvidara intercambiar uno de los vectores, los datos quedarían desincronizados: el LU de la posición 5 no correspondería al nombre de la posición 5.

> Esta fragilidad es exactamente lo que motiva el uso de **clases y objetos**, que se presentan más adelante en la sección 6.

---

## 4. Encapsulamiento

El **encapsulamiento** es uno de los pilares de la programación orientada a objetos. Consiste en **ocultar el estado interno** de un objeto y exponer solo lo necesario a través de una interfaz controlada (métodos o propiedades).

El objetivo es que el código que usa un objeto no necesite conocer cómo funciona por dentro, solo qué puede pedirle.

### 4.1 Encapsulamiento con métodos

La forma más directa de encapsular es mediante **métodos públicos** que operan sobre campos privados.

En el Ejercicio 1, el campo `contador` es privado. Ningún código externo puede leerlo o modificarlo directamente. Solo puede conocer su valor a través del método `VerContador()`:

```csharp
public class NumericService
{
    private int contador = 0;   // campo privado: el exterior no lo ve

    // Método público que expone el valor de forma controlada
    public int VerContador()
    {
        return contador;        // solo lectura, no se puede cambiar desde afuera
    }
}
```

Uso desde `FormPrincipal`:

```csharp
// Correcto: accedo al contador a través del método
int total = servicio.VerContador();

// Incorrecto (no compilaría): acceso directo al campo privado
// int total = servicio.contador;
```

Esta restricción garantiza que `contador` solo aumente cuando se registra un alumno válido, y nunca baje ni tome un valor arbitrario por error externo.

---

### 4.2 Encapsulamiento con propiedades (getters y setters)

C# ofrece una sintaxis más expresiva para el encapsulamiento: las **propiedades**. Una propiedad es un miembro que parece un campo desde el exterior, pero internamente puede ejecutar lógica mediante dos bloques:

- `get`: se ejecuta al **leer** el valor.
- `set`: se ejecuta al **asignar** un valor.

#### Sintaxis completa (con campo de respaldo)

```csharp
public class Alumno
{
    private string _nombre = string.Empty;

    public string Nombre
    {
        get
        {
            return _nombre;
        }
        set
        {
            // validación antes de asignar
            if (value != null && value.Length > 0)
                _nombre = value;
        }
    }
}
```

#### Sintaxis automática (auto-property)

Cuando no se necesita lógica extra, C# genera el campo de respaldo automáticamente:

```csharp
public class Alumno
{
    public string Nombre { get; set; } = string.Empty;
}
```

Esta es la sintaxis usada en `Ejercicio2_VectorObjeto/Models/Alumno.cs`:

```csharp
public class Alumno
{
    public int    LU     { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Nota   { get; set; } = string.Empty;
}
```

#### Propiedad de solo lectura (solo getter)

En el Ejercicio 2, `NumericService` expone `Contador` como una propiedad con `set` privado: el exterior puede **leerla** pero no **escribirla**:

```csharp
public class NumericService
{
    // Solo el código dentro de la clase puede incrementar Contador
    public int Contador { get; private set; } = 0;

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        alumnos[Contador].LU     = lu;
        alumnos[Contador].Nombre = nombre;
        alumnos[Contador].Nota   = nota.ToString();
        Contador++;   // modificación interna permitida
    }
}
```

Uso desde `FormPrincipal`:

```csharp
// Lectura: permitida (getter público)
for (int i = 0; i < servicio.Contador; i++)
    formSalidas.lsbListado.Items.Add(servicio.VerAlumno(i));

// Escritura: error de compilación (setter privado)
// servicio.Contador = 5;
```

#### Diferencia entre método `VerContador()` y propiedad `Contador`

| | Método `VerContador()` (Ej. 1) | Propiedad `Contador` (Ej. 2) |
|---|---|---|
| **Sintaxis de acceso** | `servicio.VerContador()` | `servicio.Contador` |
| **Apariencia** | Invocación explícita | Acceso como campo |
| **Getter/Setter separados** | No — es un único método | Sí — `get` y `set` independientes |
| **Convención** | Apropiada para operaciones | Apropiada para atributos |

Ambas técnicas logran encapsulamiento. La propiedad es la forma **idiomática en C#** para exponer atributos de una clase.

---

## 5. Abstracción

La **abstracción** consiste en representar una entidad del mundo real identificando solo los atributos y comportamientos **relevantes** para el problema, e ignorando los detalles irrelevantes.

En esta actividad, la abstracción se expresa en la clase `NumericService`, que oculta completamente los detalles de almacenamiento y algoritmos, y expone solo operaciones de alto nivel:

```
FormPrincipal no sabe:
  - Cuántos vectores hay internamente
  - Cómo funciona el algoritmo burbuja
  - Cómo se implementa la búsqueda binaria

FormPrincipal solo sabe:
  - Registrar un alumno           → RegistrarAlumno(lu, nombre, nota)
  - Buscar por LU                 → BuscarPorLUSecuencial(lu)
  - Ordenar                       → OrdenarPorLUBurbuja()
  - Ver un alumno formateado      → VerAlumno(idx)
  - Saber cuántos hay registrados → VerContador() / Contador
```

Esto se puede ver claramente en el método de búsqueda de `FormPrincipal` del Ejercicio 1:

```csharp
private void btnBuscarAlumno_Click(object sender, EventArgs e)
{
    int lu = Convert.ToInt32(tbLU.Text);

    int idx;
    if (rbSecuencial.Checked)
        idx = servicio.BuscarPorLUSecuencial(lu);
    else
    {
        servicio.OrdenarPorLUBurbuja();
        idx = servicio.BuscarPorLUBinario(lu);
    }

    if (idx == -1)
        MessageBox.Show("Alumno con LU " + lu + " no encontrado.", "Búsqueda", ...);
    else
        MessageBox.Show(servicio.VerAlumno(idx), "Alumno encontrado", ...);
}
```

`FormPrincipal` trabaja con índices y textos formateados, sin conocer si internamente hay tres vectores o un array de objetos. Esta es la frontera de abstracción: **el formulario delega toda responsabilidad de datos al servicio**.

> La abstracción permite que en el Ejercicio 2 se cambie completamente la estructura interna (de tres vectores a un array de objetos) **sin modificar el formulario**. La interfaz pública de `NumericService` es la misma en ambos ejercicios.

---

## 6. Clases y objetos — la clase `Alumno`

Una **clase** es una plantilla que describe los atributos (datos) y comportamientos (métodos) de una entidad. Un **objeto** es una instancia concreta de esa clase.

La clase `Alumno` del Ejercicio 2 agrupa los tres atributos que en el Ejercicio 1 estaban dispersos en tres vectores separados:

```csharp
// Ejercicio2_VectorObjeto/Models/Alumno.cs
public class Alumno
{
    public int    LU     { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Nota   { get; set; } = string.Empty;
}
```

Cada instancia de `Alumno` **encapsula** un registro completo. Los tres datos viajan juntos en un único objeto:

```csharp
// Crear un objeto Alumno
Alumno a = new Alumno();
a.LU     = 4521;
a.Nombre = "García";
a.Nota   = "8.5";

// Los tres atributos siempre están juntos
Console.WriteLine(a.LU + " " + a.Nombre + " " + a.Nota);
```

Las propiedades con `{ get; set; }` garantizan el encapsulamiento: internamente C# genera campos privados invisibles para el programador, y el acceso es siempre controlado.

---

## 7. Vector de objetos

Un **vector de objetos** es un arreglo cuyo tipo base es una clase. En lugar de almacenar valores primitivos (`int`, `double`, `string`), almacena **referencias a objetos**.

### Implementación en el Ejercicio 2

```csharp
public class NumericService
{
    private Models.Alumno[] alumnos = new Models.Alumno[100];
    public int Contador { get; private set; } = 0;

    public NumericService()
    {
        // Se deben crear los 100 objetos antes de usarlos
        for (int i = 0; i < alumnos.Length; i++)
            alumnos[i] = new Models.Alumno();
    }

    public void RegistrarAlumno(int lu, string nombre, double nota)
    {
        alumnos[Contador].LU     = lu;
        alumnos[Contador].Nombre = nombre;
        alumnos[Contador].Nota   = nota.ToString();
        Contador++;
    }
}
```

### La cohesión del objeto en el intercambio

La diferencia más ilustrativa frente al Ejercicio 1 aparece en el método `Intercambiar`. Con vectores de índice común era necesario intercambiar **tres pares de valores**:

```csharp
// Ejercicio 1 — tres intercambios separados
private void Intercambiar(int a, int b)
{
    (LUs[a],     LUs[b])     = (LUs[b],     LUs[a]);
    (Nombres[a], Nombres[b]) = (Nombres[b], Nombres[a]);
    (Notas[a],   Notas[b])   = (Notas[b],   Notas[a]);
}
```

Con el vector de objetos, basta con **intercambiar las referencias**. Los tres atributos se mueven juntos porque pertenecen al mismo objeto:

```csharp
// Ejercicio 2 — un solo intercambio
private void Intercambiar(int a, int b)
{
    Models.Alumno temp = alumnos[a];
    alumnos[a] = alumnos[b];
    alumnos[b] = temp;
}
```

Esto elimina el riesgo de desincronización que existe en el Ejercicio 1.

---

## 8. Comparación: vectores de índice común vs. vector de objetos

### Tabla comparativa

| Criterio | Vectores de índice común (Ej. 1) | Vector de objetos (Ej. 2) |
|---|---|---|
| **Estructura** | 3 arrays separados (`LUs[]`, `Nombres[]`, `Notas[]`) | 1 array de objetos (`alumnos[]`) |
| **Cohesión del dato** | Baja — los atributos están dispersos | Alta — los atributos viajan juntos |
| **Riesgo de desincronización** | Alto — cada operación debe tocar 3 arrays | Nulo — el objeto es indivisible |
| **Intercambio en ordenamiento** | 3 líneas (una por vector) | 1 línea (se intercambia la referencia) |
| **Encapsulamiento de datos** | Solo en el servicio (el objeto Alumno no existe) | En la clase `Alumno` y en el servicio |
| **Legibilidad** | Media — el concepto "alumno" no existe explícitamente | Alta — `alumno.Nombre` es autoexplicativo |
| **Extensibilidad** | Difícil — agregar un atributo implica crear un nuevo vector | Fácil — se agrega una propiedad a la clase |
| **Complejidad inicial** | Baja — solo tipos primitivos | Media — requiere entender clases y objetos |
| **Propiedad `Contador`** | Método `VerContador()` con campo `private` | Propiedad `Contador` con `private set` |

### Análisis narrativo

En el **Ejercicio 1**, los datos del alumno existen de manera implícita: no hay ningún tipo llamado `Alumno`. El "alumno número 3" es el conjunto de valores `LUs[3]`, `Nombres[3]` y `Notas[3]`. Esta representación es funcional pero frágil: el programador debe recordar siempre que estos tres vectores son inseparables y tratarlos como tal en cada algoritmo.

En el **Ejercicio 2**, el alumno existe como un **concepto explícito en el código**. La clase `Alumno` refleja directamente el vocabulario del dominio del problema. El vector `alumnos[]` almacena objetos que tienen identidad propia, y cualquier operación sobre ellos es más segura y más expresiva.

El paso de un enfoque al otro ilustra la transición fundamental en el aprendizaje de la programación: de pensar en **datos separados** a pensar en **entidades con comportamiento y estado**.

### Ejemplo concreto: agregar el atributo "Carrera"

**Ejercicio 1** — hay que agregar un cuarto vector y actualizar todos los métodos:

```csharp
private string[] Carreras = new string[100];   // nuevo vector

public void RegistrarAlumno(int lu, string nombre, double nota, string carrera)
{
    LUs[contador]      = lu;
    Nombres[contador]  = nombre;
    Notas[contador]    = nota;
    Carreras[contador] = carrera;  // hay que acordarse de este también
    contador++;
}

private void Intercambiar(int a, int b)
{
    // ahora hay que agregar esta línea también
    (Carreras[a], Carreras[b]) = (Carreras[b], Carreras[a]);
    // ... y los demás intercambios
}
```

**Ejercicio 2** — solo se modifica la clase `Alumno`:

```csharp
public class Alumno
{
    public int    LU      { get; set; }
    public string Nombre  { get; set; } = string.Empty;
    public string Nota    { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;  // solo esta línea
}
```

El resto del código (`NumericService`, `FormPrincipal`) no necesita cambios para el almacenamiento ni el ordenamiento.

---

## 9. Conclusión

Los dos ejercicios de esta actividad representan dos formas de modelar la misma realidad:

1. **Vectores de índice común**: apropiados para introducir el concepto de almacenamiento múltiple, comprensibles con conocimientos básicos, pero costosos de mantener a medida que crece la complejidad.

2. **Vector de objetos**: aprovecha las herramientas de la programación orientada a objetos. Al definir la clase `Alumno` con propiedades encapsuladas, el modelo de datos coincide con el modelo mental del dominio. El encapsulamiento —ya sea mediante métodos o mediante propiedades con `get`/`set`— protege la integridad de los datos. La abstracción que ofrece `NumericService` permite que el formulario opere sobre alumnos sin conocer ningún detalle de implementación.

La progresión natural es: **tipos primitivos → vectores → vectores de índice común → clases → vector de objetos → colecciones genéricas**. Cada paso agrega expresividad y seguridad al código, al precio de un mayor nivel de abstracción que el programador debe incorporar.
