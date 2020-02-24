using System;
namespace HundirLaFlota
{
    public class Buque : Barco
    {
        public Buque() : base(3)
        {
            this.tipo = "BUQUE";
        }
    }
}
