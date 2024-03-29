﻿using System;
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
       }

       public void addByEiendom(int enr, Etype type, int verditakst, string adresse, bool solgt)
       {
          
           //Sjekker at ENR ikke er benyttet fra før
           if (findEiendom(enr) != null)
           {
               throw new Exception ("Enr er allerede i bruk.");
           }
           ByEiendom nyeiendom = new ByEiendom(enr, type, verditakst, solgt, adresse);
           objBase.Store(nyeiendom);
       }

       public void addLandEiendom(int enr, Etype type, int verditakst, string matrikkelnr, bool solgt)
       {
           //Sjekker at ENR ikke er benyttet fra før
           if (findEiendom(enr) != null)
           {
               throw new Exception("Enr er allerede i bruk.");
           }
           LandEiendom nyeiendom = new LandEiendom(enr, type, verditakst, solgt, matrikkelnr);
           objBase.Store(nyeiendom);
       }


       //Sletter en eiendom
       public void delEiendom(int enr)
       {
           //Sjekker at det finnes en eiendom med oppgitt enr.
           if (findEiendom(enr) == null)
           {
               throw new Exception("Finner ikke eiendom med enr: " + enr);
           }

           //Sletter eiendom
           objBase.Delete(findEiendom(enr));
       }

       //Finner en eiendom, og oppdaterer info
       public void updateEiendom(int enr, int verditakst, bool solgt)
       {
          //Søker opp og oppretter aktuell eiendom
           Eiendom eiendom = findEiendom(enr);

           //Kaster feil hvis det ikke finnes en eiendom med oppgitt enr
           if (eiendom != null)
           {
               throw new Exception("Finner ikke en eiendom med enr: " + enr);
           }

           //Oppdaterer info på eiendoms-objektet    
           eiendom.setVerditakst(verditakst);
           eiendom.setSolgt(solgt);

           //Lagrer endringene i databasen
           objBase.Store(eiendom);
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
                
           //Kaster feil hvis det ikke finnes en eiendom med oppgitt enr
           if (eiendom == null)
           {
               throw new Exception("Finner ikke en eiendom med enr: " + enr);
           }

           eiendom.addBud(beløp);
           objBase.Store(eiendom); //Lagrer endringene
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


       //RETURNER RIKTIG EIENDOMMER MED VERDITAKST I INTERVALLET
       public List<Eiendom> findEiendom(int fraTakst, int tilTakst)
       {
           List<Eiendom> eiendommer = getEiendomsliste();
           List<Eiendom> utEiendommer = new List<Eiendom>();

           foreach (Eiendom tmp in eiendommer)
           {
               if (tmp.getVerditakst() >= fraTakst && tmp.getVerditakst() <= tilTakst)
               {
                   utEiendommer.Add(tmp);
               }
           }
           return utEiendommer;
       }

    }
}
