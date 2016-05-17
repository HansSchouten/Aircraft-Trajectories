using System;
using FortranToC.Conversion;

namespace Company.Project.Main
{
   public class Noise
   {
      AllObjects mobjAllObjects;
      FixedString mobjFixedString = new FixedString();
      FortranFunctions mobjFunctions = new FortranFunctions();
      public string enmount = new string(' ', 4);
      public double[] dist;
      public double[,] P_obs;
      public double[,,] tabNPD;
      public double[] thr;
      public int inod = 0;
      public int met = 0;
      public int ndist = 0;
      public int ngrid = 0;
      public int nthr = 0;
      public Noise(AllObjects robjAllObjects)
      {
         mobjAllObjects = robjAllObjects;
      }
      
      //    use deriv_class
      
      //    type (func)             ::  segment(10)
      
   }
}
