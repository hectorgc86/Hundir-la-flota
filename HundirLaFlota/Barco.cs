using System;
namespace HundirLaFlota
{
    public abstract class Barco
    {
        protected string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public Casilla[] casillas;

        protected Barco(int numCasillas)
        {
            casillas = new Casilla[numCasillas];
        }

        public void AsignarCasillas(int filaInicial, int columnaInicial, int esHorizontal)
        {

            for (int i = 0; i < casillas.Length; i++)
            {
                int fila = esHorizontal == 1 ? filaInicial : filaInicial + i;
                int columna = esHorizontal == 1 ? columnaInicial + i : columnaInicial;
                casillas[i] = new Casilla(fila, columna);
                casillas[i].Estado = 'B';
            }
        }

        public bool EstaHundido()
        {
            bool hundido = true;
            foreach (Casilla c in casillas)
            {
                if (c.Estado == 'B')
                    hundido = false;
            }
            return hundido;
        }
    }
}
