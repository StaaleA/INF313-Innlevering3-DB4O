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
           return "Byeiendom: " + base.getEnr() + " " + base.getType() + " " + this.getMatrikkelnr() + " kr. " + base.getVerditaks() + " " + (base.getSolgt() ? "er solgt" : "er ikke solgt") + " " + base.getBud().ToString();
       }
    }
}
