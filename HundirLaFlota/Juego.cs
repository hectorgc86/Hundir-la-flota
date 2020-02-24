
/*
Granja Cortés, Héctor
 */

using System;

namespace HundirLaFlota
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool turnoJugador = true;
            bool victoriaJugador = false;
            bool victoriaOrdenador = false;
            bool validaIntro;
            int fila = 0;
            int columna = 0;
            Tablero tableroJugador = new Tablero();
            Tablero tableroOrdenador = new Tablero();
            tableroOrdenador.Generar();
            tableroJugador.Rellenar();
            do
            {
                Console.Clear();
                if (turnoJugador)
                {
                    Console.WriteLine("TURNO DEL JUGADOR");
                    Console.WriteLine();
                    tableroOrdenador.PintarTablero(true);
                    Console.WriteLine();
                    do
                    {
                        try
                        {
                            validaIntro = false;
                            Console.Write("Introduzca Fila (1-8): ");
                            fila = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("La fila debe contener un número");
                            validaIntro = true;
                        }
                    } while (validaIntro);

                    do
                    {
                        try
                        {
                            validaIntro = false;
                            Console.Write("Introduzca Columna (1-8): ");
                            columna = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("La columna debe contener un número");
                            validaIntro = true;
                        }
                    } while (validaIntro);


                    string respuesta = tableroOrdenador.ComprobarCasilla(fila, columna);
                    Console.WriteLine(respuesta);
                    tableroOrdenador.PintarTablero(true);
                    Console.Write("Pulse intro para continuar");
                    Console.ReadLine();
                    victoriaJugador = tableroOrdenador.ComprobarVictoria();
                    turnoJugador = false;
                }
                else
                {
                    Console.WriteLine("TURNO DEL ORDENADOR");
                    Console.WriteLine();
                    Random rand = new Random();
                    fila = rand.Next(8) + 1;
                    columna = rand.Next(8) + 1;
                    string respuesta = tableroJugador.ComprobarCasilla(fila, columna);
                    Console.WriteLine("Fila: {0}, Columna: {1}", fila, columna);
                    Console.WriteLine(respuesta);
                    tableroJugador.PintarTablero(false);
                    Console.Write("Pulse intro para continuar");
                    Console.ReadLine();
                    victoriaOrdenador = tableroJugador.ComprobarVictoria();
                    turnoJugador = true;
                }
            } while (!victoriaJugador && !victoriaOrdenador);
            if (victoriaJugador)
            {
                Console.WriteLine("HAS GANADO");
            }
            else
            {
                Console.WriteLine("HAS PERDIDO");
            }
        }
    }
}
