using System;
namespace HundirLaFlota
{
    public class Lancha : Barco
    {
        public Lancha(): base(1)
        {
            this.tipo = "LANCHA";
        }
    }
}