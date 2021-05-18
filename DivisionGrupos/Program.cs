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

            public List<string> Temas;
            public List<string> Personas;
            public List<string> Grupos;
            public List<string> seleccion;

            //METODO QUE RECIBE LOS ARCHIVOS, LOS VAALIDA Y LOS INGRESA EN LISTAS
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


                //LEER EL ARCHIVO DE PERSONAS Y ENLISTARLOS
                while ((line = file.ReadLine()) != null)
                {

                    Personas.Add(line);
                    countEst++;
                }

                //LEER EL ARCHIVO DE TEMAS Y ENLISTARLOS
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

            //METODO QUE REALIZA LAS DIVISION DE GRUPO, TEMAS Y PERSONAS
            public void DivisionGTE(int cant)
            {
                seleccion = new List<string>();
                Grupos = new List<string>();
                List<int> exepciones = new List<int>();
                

                var random = new Random();
                int index;
                int index2;
                int index3;
                int corridas = 0;

                //PRUEBA
                double persistance = getPercistanceOfStudents(Personas.Count, Temas.Count);
                Console.WriteLine("Probabilidad " + persistance);


                //SELECCION DE TEMAS
                while (Temas.Count() > 0)
                {
                    //1-SE LLENA LA LISTA CON LOS GRUPOS
                    for (int i = 0; i < cant; i++)
                    {
                        Grupos.Add("Grupo# " + (i + 1));
                    }

                    //2-SI HAY GRUPOS Y TEMAS SE AñADEN A LA LISTA DE SELEC
                    while (Grupos.Count() > 0 && Temas.Count() > 0)
                    {
                        index = random.Next(Grupos.Count());
                        index2 = random.Next(Temas.Count());
                        index3 = random.Next(Personas.Count());

                        //2.1 si es la primera corrida se añade a la lista la palabra "[TEMAS]"
                        if (corridas == 0)
                        {
                            seleccion.Add(Grupos[index] + "   TEMAS: -> " + Temas[index2]);
                            Grupos.RemoveAt(index);
                            Temas.RemoveAt(index2);
                        }
                        //2.2 si NO es la primera corrida NO se añade a la lista la palabra "[TEMAS]"
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

                //3-AQUI SE INGRESA LA PALABRA "[ESTUDIANTE]" EN TODA LA LISTA
                for (int i = 0; i < seleccion.Count(); i++)
                {
                    seleccion[i] = (seleccion[i] + "     ESTUDIANTES: -> ");
                }

                //2-SI HAY ESTUDIANTES SE AñADEN A LA LISTA DE SELEC
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

            }

            //PRUEBA
            public double getPercistanceOfStudents(int students, int group)
            {
                int npr = fact(students) / fact(students - group);

                Console.WriteLine("Value of " + students + " P " + group + " = " + npr);

                return (100.0 / npr);
            }

            //PRUEBA
            private static int fact(int n)
            {
                int i, f = 1;
                for (i = 1; i <= n; i++)
                {
                    f = f * i;
                }
                return f;
            }

        }

            static void Main(string[] args)
            {
                bool bucle = true;
                int cantidad = 0;
                string pathEst = null;
                string pathTem = null;

                //INSTANCIA DE LA CLASE
                var Objeto = new Division();

                //SOLICITUD DE LAS DIRECCIONES DE LOS ARCHIVOS Y CANT DE GRUPOS
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
                    //SI LOS ELEMENTOS INGRESADOS SON VALIDOS SE SALDRA DEL BUCLE
                    bucle = Objeto.LeerArchivo(pathEst, pathTem, cantidad);
                }
           
                //METODO QUE REALIZA LA DIVISION DE ESTUDIANTES, GRUPOS Y TEMAS
                Objeto.DivisionGTE(cantidad);

                //IMPRIMIR LAS LISTA DE SELECCION DE ESTUDIANTES, GRUPOS Y TEMAS
                foreach (var item in Objeto.seleccion)
                {
                Console.WriteLine(item);
                }

                Console.ReadKey();
            }           
        }
    }

