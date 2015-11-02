using System;

namespace Server
{
  public class Bud
    {
      private int beløp;

      public Bud(int beløp)
      {
          this.beløp = beløp;
      }

      public int getBeløp()
      {
          return this.beløp;
      }

      public override string ToString()
      {
          return "Budbeløp: " + beløp;
      }
    }
}
