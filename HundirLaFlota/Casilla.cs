using System;
namespace HundirLaFlota
{
    public class Casilla
    {
        private int fila;
        public int Fila
        {
            get { return fila; }
            set { fila = value; }
        }

        private int columna;
        public int Columna
        {
            get { return columna; }
            set { columna = value; }
        }

        private char estado;
        public char Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Casilla(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            estado = '.';
        }
    }
}
