using System;
using System.Collections.Generic;
using Db4objects.Db4o;

namespace Server
{
   public class Kontroll
    {
       public static readonly Kontroll kontroll = new Kontroll(); //Singleton
       private const string odbName = "db4Objectbase.Db4o";
       private Db4objects.Db4o.IObjectContainer objBase;

       private Kontroll()
       {
           Db4objects.Db4o.Config.IEmbeddedConfiguration minConfig = Db4oEmbedded.NewConfiguration();
           minConfig.Common.ActivationDepth = 5;
           minConfig.Common.UpdateDepth = 2;
           objBase = Db4oEmbedded.OpenFile(minConfig, odbName);
           hentAlt();
       }

       public void addByEiendom(int enr, Etype type, int verditakst, string adresse, bool solgt)
       {
           ByEiendom nyeiendom = new ByEiendom(enr, type, verditakst, solgt, adresse);
           objBase.Store(nyeiendom);
       }

       public void addLandEiendom(int enr, Etype type, int verditakst, string matrikkelnr, bool solgt)
       {
           LandEiendom nyeiendom = new LandEiendom(enr, type, verditakst, solgt, matrikkelnr);
           objBase.Store(nyeiendom);
       }


       //Sletter en eiendom
       public void delEiendom(int enr)
       {
           objBase.Delete(findEiendom(enr));
       }

       //Finner en eiendom, og oppdaterer info
       public void updateEiendom(int enr, int verditakst, bool solgt)
       {
           Eiendom eiendom = findEiendom(enr);
          
           //Verditakst
           if (verditakst != null){
               eiendom.setVerditakst(verditakst);
           }

           //Solgt
           if (solgt != null)
           {
               eiendom.setSolgt(solgt);
           }
       }

       //Teller hvor mange eiendommer det er i databasen
       public int countEiendom()
       {

           int antallEiendommer = 0;

           foreach (Eiendom eiendom in objBase.Query<Eiendom>())
           {
            antallEiendommer++;
           }

           return antallEiendommer;
       }

       //Søker opp riktig eiendom og legger inn bud på den aktuelle eiendommen
       public void addBud(int enr, int beløp)
       {
           Eiendom eiendom = findEiendom(enr);
           eiendom.addBud(beløp);
       }

       //Returnere alle eiendommer fra DB
       public List<Eiendom> getEiendomsliste()
       {
           List<Eiendom> eiendommer = new List<Eiendom>();
           
           foreach (Eiendom eiendom in objBase.Query<Eiendom>())
           {
               eiendommer.Add(eiendom);
           }

           return eiendommer;
       }

       //Returnerer en bestemt eiendom basert på enr
       public Eiendom findEiendom(int enr)
       {
           foreach (Eiendom eiendom in objBase.Query<Eiendom>())
           {
              if (eiendom.getEnr() == enr) 
              {
                  return eiendom;
              }
           }
          return null;
       }

       //Finner alle eiendommer av en oppgitt type, legger de i en liste og returnerer listen
       public List<Eiendom> findEiendom(Etype type)
       {
           List<Eiendom> eiendommer = new List<Eiendom>();

           foreach (Eiendom eiendom in objBase.Query<Eiendom>())
           {
               if (eiendom.getType() == type)
               {
                   eiendommer.Add(eiendom);
               }
           }

           return eiendommer;
       }


       public List<Eiendom> findEiendom(int fraTakst, int tilTakst)
       {
           //TODO: RETURNER RIKTIG EIENDOMMER MED VERDITAKST I INTERVALLET
           return null;
       }

       public void lagreAlt()
       {
           //TODO: LAGRE ALT PÅ DB
       }

       public void hentAlt()
       {
           //TODO: HENT ALT PÅ DB
           int maks = 0;

           foreach(Eiendom eiendom in objBase.Query<Eiendom>())
           {
               maks = eiendom.getEnr() > maks ? eiendom.getEnr() : maks;
           }
          // Eiendom.NR = maks;
       }


    }
}
