using System;
using Company.Project.DataObject;
using FortranToC.Conversion;

namespace Company.Project.Main
{
   public class AllObjects
   {
      Noise mobjNoise;
      Param mobjParam;
      IOSilver mobjIO = new IOSilver();

      public AllObjects()
      {
          mobjNoise = new Noise(this);
          mobjParam = new Param(this);
      }

      public Noise Noise
      {
         get
         {
            return mobjNoise;
         }
      }
      public Param Param
      {
         get
         {
            return mobjParam;
         }
      }
      public IOSilver IOSilver
      {
         get
         {
            return mobjIO;
         }
      }
   }
}
