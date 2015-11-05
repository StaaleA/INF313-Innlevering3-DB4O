using System;


namespace Server
{
   public class LandEiendom : Eiendom
    {
       private string matrikkelnr;

       internal LandEiendom(int enr, Etype type, int verditakst, bool solgt, string matrikkelnr) : base(enr, type, verditakst, solgt)
       {
           setMatrikkelnr(matrikkelnr);
       }

       public string getMatrikkelnr()
       {
           return matrikkelnr;
       }

       internal void setMatrikkelnr(string matrikkelnr)
       {
           this.matrikkelnr = matrikkelnr;
       }

       public override string ToString()
         {
            string alleBud = "";
            foreach (Bud bud in base.getBud())
            {
                alleBud += "Kr. " + bud.ToString() + " ";
            }

            return "Landeiendom: " + base.getEnr() + " " + base.getType() + " " + this.matrikkelnr + " kr. " + base.getVerditaks() + " " + (base.getSolgt() ? "er solgt" : "er ikke solgt") + " Bud: " + alleBud;
       }
    }
}
