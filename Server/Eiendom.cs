using System;
using System.Collections.Generic;

namespace Server
{
   public abstract class Eiendom
    {
       private readonly int enr;
       private Etype type;
       private int verditakst; //skal være >0
       private bool solgt;
       private List<Bud> budliste = new List<Bud>();

       public Eiendom(int enr, Etype type, int verditakst, bool solgt)
       {
           //SETTENR
           this.enr = enr;
           setType(type);
           setVerditakst(verditakst);
           setSolgt(solgt);
       }

       public int getEnr()
       {
           return this.enr;
       }

       public Etype getType()
       {
           return this.type;
       }

       internal void setType(Etype type) 
       {
           this.type = type;
       }

       public int getVerditaks()
       {
           return this.verditakst;
       }

       internal void setVerditakst(int verditakst) 
       {
           this.verditakst = verditakst;
       }

       public bool getSolgt()
       {
           return this.solgt;
       }

       internal void setSolgt(bool solgt)
       {
           this.solgt = solgt;
       }

       internal void addBud(int beløp) 
       { 
           //TODO: LAG NYTT BUD OG LEGG DET I BUDLISTA
       }

       public List<Bud> getBud()
       {
           return budliste;
       }

       public int getSnittBud()
       {
           //TODO: REGN UT GJENNOMSNITTBUDET FRA BUDLISTA
           return 0;
       }

       public override abstract string ToString();

    }
}
