﻿using System;


namespace Server
{
    public class ByEiendom : Eiendom
    {
        private string adresse;

        internal ByEiendom(int enr, Etype type, int verditakst, bool solgt, string adresse) : base(enr, type, verditakst, solgt)
        {
            setAdresse(adresse);
        }

        public string getAdresse()
        {
            return this.adresse;
        }

        internal void setAdresse(string adresse)
        {
            this.adresse = adresse;
        }

        public override string ToString()
        {
            string alleBud = "";
            foreach (Bud bud in base.getBud())
            {
                alleBud += "Kr. " + bud.ToString() + " ";
            }

            return "Byeiendom: " + base.getEnr() + " " + base.getType() + " " + this.getAdresse() + " kr. " + base.getVerditakst() + " " + (base.getSolgt() ? "er solgt" : "er ikke solgt")  + " Bud: " + alleBud;
        }
    }

   
}
