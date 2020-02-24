using System;
namespace HundirLaFlota
{
    public class Tablero
    {
        const int FILASTABLERO = 8;
        const int COLUMNASTABLERO = 8;
        private Casilla[,] casillas;
        private Barco[] barcos;

        public Tablero()
        {
            RellenaTablero();
            CreaBarcos();
        }


        public bool ComprobarVictoria()
        {
            bool victoria = true;
            for (int i = 0; i < barcos.Length; i++)
            {
                if (!barcos[i].EstaHundido())
                {
                    victoria = false;
                }
            }
            return victoria;
        }

        public void PintarTablero(bool oculto)
        {
            for (int i = 0; i < FILASTABLERO; i++)
            {
                for (int j = 0; j < COLUMNASTABLERO; j++)
                {
                    if (!oculto)
                    {
                        Console.Write(casillas[i, j].Estado.ToString() + ' ');
                    }
                    else
                    {
                        string dibujo = casillas[i, j].Estado == 'B' ? "." : casillas[i, j].Estado.ToString();
                        Console.Write(dibujo + ' ');
                    }
                }
                Console.WriteLine();
            }
        }

        public string ComprobarCasilla(int fila, int columna)
        {
            string respuesta = "AGUA";
            char estado = casillas[fila - 1, columna - 1].Estado;
            if (estado == 'B')
            {
                bool marcado = false;
                int indiceBarcos = 0;
                do
                {
                    Barco barco = barcos[indiceBarcos];
                    int indiceCasillas = 0;
                    do
                    {
                        Casilla casilla = barco.casillas[indiceCasillas];
                        if (casilla.Fila == fila && casilla.Columna == columna)
                        {
                            barcos[indiceBarcos].casillas[indiceCasillas].Estado = 'X';
                            if (barcos[indiceBarcos].EstaHundido())
                            {
                                respuesta = "TOCADO Y HUNDIDO";
                            }
                            else
                            {
                                respuesta = "TOCADO";
                            }
                            PintarBarco(indiceBarcos);
                            marcado = true;
                        }
                        indiceCasillas++;
                    } while (!marcado && indiceCasillas < barco.casillas.Length);
                    indiceBarcos++;
                } while (!marcado && indiceBarcos < barcos.Length);
            }
            return respuesta;
        }

        public void Rellenar()
        {
            char[] opcionesHorizontal = new char[2];
            bool datoIntro;
            opcionesHorizontal[0] = 'N';
            opcionesHorizontal[1] = 'S';
    
            int filaInicial;
            int columnaInicial;
            int esHorizontal;
            for (int i = 0; i < barcos.Length; i++)
            {
                do
                {
     
                        datoIntro = false;
                       
                    do
                    {
                        Console.Write("Introduce Fila para {0} (1-8): ", barcos[i].Tipo);
                        if (int.TryParse(Console.ReadLine(), out filaInicial))
                        {
                            datoIntro = false;
                        }
                        else
                        {
                            Console.WriteLine("No es número válido");
                            datoIntro = true;
                        }
                    } while (datoIntro);

                    do
                    {
                        Console.Write("Introduce Columna para {0} (1-8): ", barcos[i].Tipo);
                        if (int.TryParse(Console.ReadLine(), out columnaInicial))
                        {
                            datoIntro = false;
                        }
                        else
                        {
                            Console.WriteLine("No es número válido");
                            datoIntro = true;
                        }
                    } while (datoIntro);

                        Console.Write("¿Es Horizontal? (S-N): ");
                        char valorHoriz = Convert.ToChar(Console.ReadLine().ToUpper());
                        esHorizontal = Array.IndexOf(opcionesHorizontal, valorHoriz);



                    if (filaInicial < 1 || filaInicial > FILASTABLERO || columnaInicial < 1 || columnaInicial > COLUMNASTABLERO)
                    {
                        Console.WriteLine("Error, valores que no estan dentro de los límites del tablero");
                        datoIntro = true;
                    }


                }
                while (datoIntro || !PonerBarco(i, filaInicial, columnaInicial, esHorizontal, true));
                PintarBarco(i);
            }
        }

        public void Generar()
        {
            int filaInicial;
            int columnaInicial;
            int esHorizontal;
            for (int i = 0; i < barcos.Length; i++)
            {
                do
                {
                    int indice = i;
                    Random rand = new Random();
                    filaInicial = rand.Next(FILASTABLERO) + 1;
                    columnaInicial = rand.Next(COLUMNASTABLERO) + 1;
                    esHorizontal = rand.Next(2);
                } while (!PonerBarco(i, filaInicial, columnaInicial, esHorizontal, false));
                PintarBarco(i);
            }
        }

        private void RellenaTablero()
        {
            casillas = new Casilla[FILASTABLERO, COLUMNASTABLERO];
            for (int i = 0; i < FILASTABLERO; i++)
            {
                for (int j = 0; j < COLUMNASTABLERO; j++)
                {
                    casillas[i, j] = new Casilla(i + 1, j + 1);
                }
            }
        }

        private void CreaBarcos()
        {
            barcos = new Barco[4];
            barcos[0] = new Portaaviones();
            barcos[1] = new Buque();
            barcos[2] = new Fragata();
            barcos[3] = new Lancha();
        }

        private bool PonerBarco(int tipoBarco, int filaInicial, int columnaInicial, int esHorizontal, bool muestraMensaje)
        {
            bool ok = ComprobarCasillasDisponibles(tipoBarco, filaInicial, columnaInicial, esHorizontal, muestraMensaje);
            if (ok)
            {
                barcos[tipoBarco].AsignarCasillas(filaInicial, columnaInicial, esHorizontal);
            }
            return ok;
        }

        private bool ComprobarCasillasDisponibles(int tipoBarco, int filaInicial, int columnaInicial, int esHorizontal, bool muestraMensajes)
        {
            bool respuesta = true;
            string mensaje = "";
            int tamanobarco = 4 - tipoBarco;
            if (esHorizontal == 1)
            {
                if (columnaInicial - 1 + tamanobarco > COLUMNASTABLERO)
                    mensaje = "No cabe horizontal.";
                else
                {
                    for (int i = columnaInicial - 1; i < columnaInicial - 1 + tamanobarco; i++)
                    {
                        if (casillas[filaInicial - 1, i].Estado != '.')
                            mensaje = "Hay casillas ocupadas por otro barco. Introduzca otra posición.";
                    }
                }
            }
            else
            {
                if (filaInicial - 1 + tamanobarco > FILASTABLERO)
                    mensaje = "No cabe vertical.";
                else
                {
                    for (int i = filaInicial - 1; i < filaInicial - 1 + tamanobarco; i++)
                    {
                        if (casillas[i, columnaInicial - 1].Estado != '.')
                            mensaje = "Hay casillas ocupadas por otro barco. Introduzca otra posición.";
                    }
                }
            }
            if (mensaje != "")
            {
                if (muestraMensajes)
                    Console.WriteLine(mensaje);
                respuesta = false;
            }
            return respuesta;
        }

        private void PintarBarco(int indice)
        {
            Barco b = barcos[indice];
            foreach (Casilla c in b.casillas)
            {
                casillas[c.Fila - 1, c.Columna - 1].Estado = c.Estado;
            }
        }
    }
}
