using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Collections;

namespace DivisionGrupos
{
    public class Program
    {

        public class Division
        {

            public  List<string> Temas;
            public  List<string> Personas;
            public  List<string> Grupos;

            public bool LeerArchivo(string pathEst, string pathTem, int cant)
            {

                string line;
                bool valid = false;
                int countEst = 0;
                int count = 0;

                Personas = new List<string>();
                Temas = new List<string>();

                System.IO.StreamReader file = new System.IO.StreamReader(pathEst);
                System.IO.StreamReader file2 = new System.IO.StreamReader(pathTem);


                //VALIDACIONES
                if (pathEst == null || pathTem == null)
                {
                    Console.Clear();
                    valid = true;
                    Console.WriteLine("Uno de los valores ingresados no contiene nada!!!");
                }
                if (cant <= 0)
                {
                    Console.Clear();
                    valid = true;
                    Console.WriteLine("LA CANTIDAD DE GRUPOS NO PUEDE SER CERO!!!");
                }
                /*if (Directory.Exists(pathEst) == false)
                 {

                     Console.Clear();
                     valid = true;
                     Console.WriteLine("El archivo de Estudiantes no ha sido encontrado o esta vacio");
                 }
                 if (Directory.Exists(pathTem) == false)
                 {
                     Console.Clear();
                     valid = true;
                     Console.WriteLine("El archivo de Temas no ha sido encontrado o esta vacio");
                 }*/


                //LEER ARCHIVOS Y ENLISTARLOS
                while ((line = file.ReadLine()) != null)
                {

                    Personas.Add(line);
                    countEst++;
                }

                line = null;
                while ((line = file2.ReadLine()) != null)
                {
                    this.Temas.Add(line);

                }

                //VALIDACIONES
                if (cant > countEst)
                {
                    Console.Clear();
                    valid = true;
                    Console.WriteLine("La cantida de grupos no puede ser mayor que la cantida de estudiantes ");
                }

                return valid;

            }


            public void DivisionTemas(int cant)
            {
                List<string> seleccion = new List<string>();
                List<int> exepciones = new List<int>();
                Grupos = new List<string>();

                var random = new Random();
                int index;
                int index2;
                int index3;
                int corridas = 0;

                double persistance = getPercistanceOfStudents(Personas.Count, Temas.Count);
                Console.WriteLine("Probabilidad " + persistance);

                while (Temas.Count() > 0)
                {
                    for (int i = 0; i < cant; i++)
                    {
                        Grupos.Add("Grupo# " + (i + 1));
                    }

                    while (Grupos.Count() > 0 && Temas.Count() > 0)
                    {
                        index = random.Next(Grupos.Count());
                        index2 = random.Next(Temas.Count());
                        index3 = random.Next(Personas.Count());

                        if (corridas == 0)
                        {
                            seleccion.Add(Grupos[index] + "   TEMAS: -> " + Temas[index2]);
                            Grupos.RemoveAt(index);
                            Temas.RemoveAt(index2);
                        }
                        else
                        {

                            for (int e = 0; e < seleccion.Count(); e++)
                            {
                                if (seleccion[e].IndexOf(Grupos[index]) != -1)
                                {
                                    seleccion[e] = (seleccion[e] + ", " + Temas[index2]);
                                    Grupos.RemoveAt(index);
                                    Temas.RemoveAt(index2);
                                    break;
                                }
                            }

                        }
                    }
                    corridas++;
                }

                for (int i = 0; i < seleccion.Count(); i++)
                {
                    seleccion[i] = (seleccion[i] + "     ESTUDIANTES: -> ");
                }
                while (Personas.Count() > 0)
                {
                    for (int i = 0; i < cant; i++)
                    {
                        Grupos.Add("Grupo# " + (i + 1));
                    }

                    while (Grupos.Count() > 0 && Personas.Count() > 0)
                    {
                        index = random.Next(Grupos.Count());
                        index3 = random.Next(Personas.Count());


                        for (int e = 0; e < seleccion.Count(); e++)
                        {
                            if (seleccion[e].IndexOf(Grupos[index]) != -1)
                            {
                                seleccion[e] = (seleccion[e] + " " + Personas[index3] + ", ");
                                Grupos.RemoveAt(index);
                                Personas.RemoveAt(index3);
                                break;
                            }

                        }
                    }

                }


                foreach (var item in seleccion)
                {
                    Console.WriteLine(item);
                }


            }

            public double getPercistanceOfStudents(int students, int group)
            {
                int npr = fact(students) / fact(students - group);

                Console.WriteLine("Value of " + students + " P " + group + " = " + npr);
                
                return (100.0 / npr);
            }

            private static int fact(int n)
            {
                int i, f = 1;
                for (i = 1; i <= n; i++)
                {
                    f = f * i;
                }
                return f;
            }





            static void Main(string[] args)
            {
                bool bucle = true;
                int cantidad = 0;
                string pathEst = null;
                string pathTem = null;

                var Objeto = new Division();

                while (bucle)
                {
                    Console.WriteLine("Ingrese el numero la cantida de grupos:");
                    cantidad = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine("Ingrese la direccion del archivo en donde se encuentras los estudiantes:");
                    pathEst = "C:\\Users\\maria\\Desktop\\Estudiantes.txt";
                    //pathEst = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Ingrese la direccion del archivo en donde se encuentras los temas:");
                    pathTem = "C:\\Users\\maria\\Desktop\\Temas.txt";
                    ///pathTem = Convert.ToString(Console.ReadLine());
                    bucle = false;

                    bucle = Objeto.LeerArchivo(pathEst, pathTem, cantidad);
                }
                Console.WriteLine("YA SALIO");

                Objeto.DivisionTemas(cantidad);                

                Console.ReadKey();
            }

           
        }
    }
}
