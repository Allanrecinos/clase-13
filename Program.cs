//// See https://aka.ms/new-console-template for more information
using Clase13;
using Biblioteca;

Database database = new Database();

if (database.ValidarConexion())
{
    Console.WriteLine("La conexion es correcta");
}
else
{
    Console.WriteLine("La conexion fallo");
}

//Recuperamos datos
var alumnos = database.GetAlumnos();

foreach (var alumno in alumnos)
{
    Console.WriteLine($"{alumno.Id}-{alumno.Nombre}");
}

Alumno nuevo = new Alumno
{
    Nombre ="Pepito",
    Direccion ="Los chistes",
    Carnet = "93240932"
};
var resultado = database.InsertarAlumno(nuevo);
Console.WriteLine(resultado);

var miClase = new Biblioteca.MiSuperClase();
miClase.Saluda();
miClase.SetDatoSuperPrivado("Hola");
