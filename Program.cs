using System;
using System.Collections.Generic;

class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }

    public Persona(int id, string nombre, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
    }
}

class Cita
{
    public int PersonalId { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; }

    public Cita(int personalId, DateTime fecha, string descripcion)
    {
        PersonalId = personalId;
        Fecha = fecha;
        Descripcion = descripcion;
    }
}

class Program
{
    static List<Persona> personas = new List<Persona>();
    static List<Cita> citas = new List<Cita>();

    static void Main()
    {
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n--- Menú AgendaPro ---");
            Console.WriteLine("1. Registrar persona");
            Console.WriteLine("2. Listar personas");
            Console.WriteLine("3. Crear cita");
            Console.WriteLine("4. Listar citas por Persona");
            Console.WriteLine("5. Mostrar todas las citas");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarPersona();
                    break;
                case "2":
                    ListarPersonas();
                    break;
                case "3":
                    CrearCita();
                    break;
                case "4":
                    ListarCitasPorPersona();
                    break;
                case "5":
                    MostrarTodasLasCitas();
                    break;
                case "6":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        Console.WriteLine("Programa finalizado.");
    }

    static void RegistrarPersona()
    {
        int id;
        while (true)
        {
            Console.Write("Ingrese ID de la persona: ");
            string idInput = Console.ReadLine();
            try
            {
                id = int.Parse(idInput);
                // Validar ID único
                if (personas.Exists(p => p.Id == id))
                {
                    Console.WriteLine("Ya existe una persona con este ID. Intente con otro.");
                    continue;
                }
                break;
            }
            catch
            {
                Console.WriteLine("Ingrese un ID numérico válido.");
            }
        }

        Console.Write("Ingrese nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese teléfono: ");
        string telefono = Console.ReadLine();

        personas.Add(new Persona(id, nombre, telefono));
        Console.WriteLine("✅ Persona registrada exitosamente.");
    }

    static void ListarPersonas()
    {
        if (personas.Count == 0)
        {
            Console.WriteLine("No hay personas registradas.");
            return;
        }

        Console.WriteLine("\nID | Nombre | Teléfono");
        foreach (var p in personas)
        {
            Console.WriteLine($"{p.Id} | {p.Nombre} | {p.Telefono}");
        }
    }

    static void CrearCita()
    {
        int personalId;
        while (true)
        {
            Console.Write("Ingrese ID de la persona para la cita: ");
            string idInput = Console.ReadLine();
            try
            {
                personalId = int.Parse(idInput);
                if (!personas.Exists(p => p.Id == personalId))
                {
                    Console.WriteLine("Persona no encontrada. Registre la persona primero.");
                    return;
                }
                break;
            }
            catch
            {
                Console.WriteLine("Ingrese un ID numérico válido.");
            }
        }

        DateTime fecha;
        while (true)
        {
            Console.Write("Ingrese fecha de la cita (formato: yyyy-MM-dd): ");
            string fechaInput = Console.ReadLine();
            try
            {
                fecha = DateTime.Parse(fechaInput);
                break;
            }
            catch
            {
                Console.WriteLine("Fecha no válida. Intente de nuevo.");
            }
        }

        Console.Write("Ingrese descripción de la cita: ");
        string descripcion = Console.ReadLine();

        citas.Add(new Cita(personalId, fecha, descripcion));
        Console.WriteLine("Cita creada exitosamente.");
    }

    static void ListarCitasPorPersona()
    {
        Console.Write("Ingrese ID de la persona: ");
        string idInput = Console.ReadLine();
        int personalId;
        if (!int.TryParse(idInput, out personalId))
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        var citasPersona = citas.FindAll(c => c.PersonalId == personalId);

        if (citasPersona.Count == 0)
        {
            Console.WriteLine("No hay citas para esta persona.");
            return;
        }

        Console.WriteLine("\nPersonaId | Fecha | Descripción");
        foreach (var c in citasPersona)
        {
            Console.WriteLine($"{c.PersonalId} | {c.Fecha.ToShortDateString()} | {c.Descripcion}");
        }
    }

    static void MostrarTodasLasCitas()
    {
        if (citas.Count == 0)
        {
            Console.WriteLine("No hay citas registradas.");
            return;
        }

        Console.WriteLine("\nPersonaId | Fecha | Descripción");
        foreach (var c in citas)
        {
            Console.WriteLine($"{c.PersonalId} | {c.Fecha.ToShortDateString()} | {c.Descripcion}");
        }
    }
}
