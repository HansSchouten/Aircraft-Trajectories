using System;
using FortranToC.Conversion;

namespace Company.Project.Main
{
   public class Noisesubs
   {
      AllObjects mobjAllObjects;
      FixedString mobjFixedString = new FixedString();
      FortranFunctions mobjFunctions = new FortranFunctions();

      public Noisesubs(AllObjects robjAllObjects)
      {
         mobjAllObjects = robjAllObjects;
      }

      public void noisesub(double[] SER,
                           double[,] SERJac)
      {
         Noise objNoise = mobjAllObjects.Noise;
         Deriv_class objDeriv_class = mobjAllObjects.Deriv_class;
         int intDummy = 0;
         
         //   Initialize
         mobjFunctions.InitializeArray(ref SER, 0.0);
         mobjFunctions.InitializeArray(ref SERd, 0.0);
         for (int intTmpLoop1 = 0; intTmpLoop1 < SERJac.GetUpperBound(0); intTmpLoop1++)
         {
            for (int intTmpLoop2 = 0; intTmpLoop2 < 10; intTmpLoop2++)
            {
               SERJac[1 + intTmpLoop1, 1 + intTmpLoop2] = 0.0;
            }
         }
         //   Convert units
         for (int intTmpLoop1 = 0; intTmpLoop1 < 2; intTmpLoop1++)
         {
            xx[1 + intTmpLoop1] = ( / segment ( 1 ) , segment ( 2 ) / ) * m_ft;
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 2; intTmpLoop1++)
         {
            yy[1 + intTmpLoop1] = ( / segment ( 3 ) , segment ( 4 ) / ) * m_ft;
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 2; intTmpLoop1++)
         {
            zz[1 + intTmpLoop1] = ( / segment ( 5 ) , segment ( 6 ) / ) * m_ft;
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 2; intTmpLoop1++)
         {
            VV[1 + intTmpLoop1] = ( / segment ( 7 ) , segment ( 8 ) / ) * ms_kts;
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 2; intTmpLoop1++)
         {
            TT[1 + intTmpLoop1] = ( / segment ( 9 ) , segment ( 10 ) / ) * N_lbf;
         }
         
         //$omp   parallel
         tid = omp_get_num_threads ( );
         tid = mobjFunctions.Int ( tid * 3.0 / 4.0 );
         //$omp   end parallel
         
         //$omp   parallel num_threads(tid) default(private) shared(SERd, P_obs, xx, yy, zz, VV, TT, ngrid, tabNPD, dist, thr, met)
         //$omp   do schedule(dynamic)
         
         //   Start the noise calculation sequence
         for (i = 1; i <= objNoise.ngrid; i++)
         {
            //   Define the observer position and the start and end points of the segment
            for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
            {
               Pobs[1 + intTmpLoop1] = ( / objNoise.P_obs [ i , 1 ] , objNoise.P_obs [ i , 2 ]
                     , objNoise.P_obs [ i , 3 ] / );
            }
            for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
            {
               P_1[1 + intTmpLoop1] = ( / xx[ 1 ].DeepCopy() , yy[ 1 ].DeepCopy() , zz[ 1
                     ].DeepCopy() / );
            }
            for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
            {
               P_2[1 + intTmpLoop1] = ( / xx[ 2 ].DeepCopy() , yy[ 2 ].DeepCopy() , zz[ 2
                     ].DeepCopy() / );
            }
            //   Determine the slant ranges and relative position of the observer
            slant ( P_1 , P_2 , Pobs , ref q_seg , ref x_seg , ref SLRseg , ref SLRpth , ref
                  d_as );
            //   Interpolate the speed, altitude and thrust at the CPA
            V0 = ( VV[ 1 ].DeepCopy() + VV[ 2 ].DeepCopy() ) / 2.0;
            z0 = zz[ 1 ].DeepCopy() + ( d_as.DeepCopy() / x_seg.DeepCopy() ) * ( zz[ 2
                  ].DeepCopy() - zz[ 1 ].DeepCopy() );
            T0 = TT[ 1 ].DeepCopy() + ( d_as.DeepCopy() / x_seg.DeepCopy() ) * ( TT[ 2
                  ].DeepCopy() - TT[ 1 ].DeepCopy() );
            //   Interpolate the noise levels
            if ( objNoise.met == 1 || objNoise.met == 3 )
            {
               if ( objNoise.met == 1 )
               {
                  nt = objNoise.met;
               }
               if ( objNoise.met == 3 )
               {
                  nt = objNoise.met - 2;
               }
               intpol ( ref nt , ref T0 , ref SLRpth , ref SELniv );
               nt = nt + 1;
               if ( objNoise.tabNPD [ 1 , 1 , 2 ] != 0.0 )
               {
                  intpol ( ref nt , ref T0 , ref SLRpth , ref SLMnfniv );
                  intpol ( ref nt , ref T0 , ref SLRseg , ref SLMniv );
               }
               else
               {
                  SLMniv = SELniv.DeepCopy() - 7.19 - 7.73 * log10 ( SLRseg.DeepCopy() / 1.0e3
                        );
                  SLMnfniv = SELniv.DeepCopy() - 7.19 - 7.73 * log10 ( SLRpth.DeepCopy() /
                        1.0e3 );
               }
            }
            else if ( objNoise.met == 2 || objNoise.met == 4 )
            {
               if ( objNoise.met == 2 )
               {
                  nt = objNoise.met;
               }
               if ( objNoise.met == 4 )
               {
                  nt = objNoise.met - 2;
               }
               intpol ( ref nt , ref T0 , ref SLRseg , ref SLMniv );
            }
            //   Calculate the noise adjustments
            corr ( Pobs , ref SELniv , ref SLMnfniv , ref q_seg , ref x_seg , ref V0 , ref z0
                  , ref SLRseg , ref adjSEL , ref adjSLM );
            SELniv = SELniv.DeepCopy() + adjSEL.DeepCopy();
            SLMniv = SLMniv.DeepCopy() + adjSLM.DeepCopy();
            //   Determine the resulting noise levels
            SERniv = mobjFunctions.Pow(1.0e1, ( SELniv.DeepCopy() / 1.0e1 ));
            SERd[ i ] = SERd[ i ].DeepCopy() + SERniv.DeepCopy();
         }
         
         //$omp   end do
         //$omp   end parallel
         
         //   Extract values and derivatives
         for (i = 1; i <= objNoise.ngrid; i++)
         {
            for (int intTmpLoop1 = 0; intTmpLoop1 < 1; intTmpLoop1++)
            {
               extract ( SERd[ i ] , SER [ i ] , SERJac [ i ,  + intTmpLoop1] );
            }
         }
         
         return;
      }
      
      //************************************************************************************************************************************************************************************
      //************************************************************************************************************************************************************************************
      
      public void slant(         intent ( inf ) :: P_1 , P_2 , Pobs;
         intent ( outf ) :: q_seg , x_seg , SLRseg , SLRpth , d_as;
         
         //   Determine the orientation and length of the segment
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            P1P2[1 + intTmpLoop1] = P_1[1 + intTmpLoop1].DeepCopy() - P_2[1 +
                  intTmpLoop1].DeepCopy();
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            x_seg = sqrt ( dot_product ( P1P2[1 + intTmpLoop1].DeepCopy() , P1P2[1 +
                  intTmpLoop1].DeepCopy() ) );
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            unit[1 + intTmpLoop1] = P1P2[1 + intTmpLoop1].DeepCopy() / x_seg.DeepCopy();
         }
         //   Define the slant ranges from the observer to the segment
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            P1Pobs[1 + intTmpLoop1] = P_1[1 + intTmpLoop1].DeepCopy() - Pobs[1 + intTmpLoop1];
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            P2Pobs[1 + intTmpLoop1] = P_2[1 + intTmpLoop1].DeepCopy() - Pobs[1 + intTmpLoop1];
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            r1 = sqrt ( dot_product ( P1Pobs[1 + intTmpLoop1].DeepCopy() , P1Pobs[1 +
                  intTmpLoop1].DeepCopy() ) );
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            r2 = sqrt ( dot_product ( P2Pobs[1 + intTmpLoop1].DeepCopy() , P2Pobs[1 +
                  intTmpLoop1].DeepCopy() ) );
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 3; intTmpLoop1++)
         {
            q_seg = dot_product ( P1Pobs[1 + intTmpLoop1].DeepCopy() , unit[1 +
                  intTmpLoop1].DeepCopy() );
         }
         SLRpth = sqrt ( mobjFunctions.Pow(r1.DeepCopy(), 2.0) -
               mobjFunctions.Pow(q_seg.DeepCopy(), 2.0) );
         if ( q_seg < 0.0 )
         {
            d_as = 0.0;
            SLRseg = r1.DeepCopy();
         }
         else if ( q_seg < x_seg )
         {
            d_as = q_seg.DeepCopy();
            SLRseg = SLRpth.DeepCopy();
         }
         else
         {
            d_as = x_seg.DeepCopy();
            SLRseg = r2.DeepCopy();
         }
         if ( SLRseg == 0.0 )
         {
            SLRseg = 3.67;
         }
         if ( SLRpth == 0.0 )
         {
            SLRpth = 3.67;
         }
         
         return;
      }
      
      //************************************************************************************************************************************************************************************
      //************************************************************************************************************************************************************************************
      
      public void intpol(ref int nt,
                         ref          intent ( outf ) :: sVal;
         
         //   Find the proper interpolation interval for the distance
         if ( d_m >= objNoise.dist [ objNoise.ndist ] )
         {
            k = objNoise.ndist;
         }
         else if ( d_m <= objNoise.dist [ 1 ] )
         {
            k = 1;
         }
         else
         {
            for (i = objNoise.ndist; i >= 1; i = i -  1)
            {
               k = i + 1;
               if ( objNoise.dist [ i ] <= d_m )
               {
                  break;
               }
            }
         }
         //   Determine the proper interpolation interval for the thrust
         if ( T0 >= objNoise.thr [ objNoise.nthr ] )
         {
            l = objNoise.nthr;
         }
         else if ( T0 <= objNoise.thr [ 1 ] )
         {
            l = 2;
         }
         else
         {
            for (i = 2; i <= objNoise.nthr; i++)
            {
               l = i;
               if ( objNoise.thr [ i ] > T0 )
               {
                  break;
               }
            }
         }
         //   Interpolate the noise levels
         thrdif = ( T0.DeepCopy() - objNoise.thr [ l - 1 ] ) / ( objNoise.thr [ l ] -
               objNoise.thr [ l - 1 ] );
         if ( k != 1 )
         {
            d_mdif = ( log10 ( d_m.DeepCopy() ) - log10 ( objNoise.dist [ k - 1 ] ) ) / (
                  log10 ( objNoise.dist [ k ] ) - log10 ( objNoise.dist [ k - 1 ] ) );
         }
         else
         {
            d_mdif = ( log10 ( d_m.DeepCopy() ) - log10 ( objNoise.dist [ k ] ) ) / ( log10 (
                  objNoise.dist [ k + 1 ] ) - log10 ( objNoise.dist [ k ] ) );
         }
         if ( k == 1 && nt == 1 )
         {
            sVal1 = objNoise.tabNPD [ l - 1 , 1 , nt ] + 1.0e1 * log10 ( objNoise.dist [ 1 ] /
                  d_m.DeepCopy() );
            sVal2 = objNoise.tabNPD [ l , 1 , nt ] + 1.0e1 * log10 ( objNoise.dist [ 1 ] /
                  d_m.DeepCopy() );
            sVal = sVal1.DeepCopy() + ( sVal2.DeepCopy() - sVal1.DeepCopy() ) *
                  thrdif.DeepCopy();
         }
         else if ( k == 1.0 && nt == 2 )
         {
            sVal1 = objNoise.tabNPD [ l - 1 , 1 , nt ] + 2.0e1 * log10 ( objNoise.dist [ 1 ] /
                  d_m.DeepCopy() );
            sVal2 = objNoise.tabNPD [ l , 1 , nt ] + 2.0e1 * log10 ( objNoise.dist [ 1 ] /
                  d_m.DeepCopy() );
            sVal = sVal1.DeepCopy() + ( sVal2.DeepCopy() - sVal1.DeepCopy() ) *
                  thrdif.DeepCopy();
         }
         else
         {
            sVal1 = objNoise.tabNPD [ l - 1 , k - 1 , nt ] + ( objNoise.tabNPD [ l - 1 , k ,
                  nt ] - objNoise.tabNPD [ l - 1 , k - 1 , nt ] ) * d_mdif.DeepCopy();
            sVal2 = objNoise.tabNPD [ l , k - 1 , nt ] + ( objNoise.tabNPD [ l , k , nt ] -
                  objNoise.tabNPD [ l , k - 1 , nt ] ) * d_mdif.DeepCopy();
            sVal = sVal1.DeepCopy() + ( sVal2.DeepCopy() - sVal1.DeepCopy() ) *
                  thrdif.DeepCopy();
         }
         //   Correct the noise levels for low thrust levels
         if ( T0 < objNoise.thr [ 1 ] )
         {
            if ( k == 1 )
            {
               k = 2;
            }
            cVal = objNoise.tabNPD [ 1 , k - 1 , nt ] + d_mdif.DeepCopy() * ( objNoise.tabNPD
                  [ 1 , k , nt ] - objNoise.tabNPD [ 1 , k - 1 , nt ] ) - 5.0;
            if ( sVal < cVal )
            {
               sVal = cVal.DeepCopy();
            }
         }
         
         return;
      }
      
      //************************************************************************************************************************************************************************************
      //************************************************************************************************************************************************************************************
      
      public void corr(double[] Pobs,
                       ref          intent ( inf ) :: Pobs , Sniv , SMnfniv , q_seg , x_seg , v0 , z0;
         intent ( outf ) :: adjS , adjM;
         
         //   Acoustic impedance adjustment
         alt = Pobs [ 3 ] * 0.3048;
         Tamb = Param.temp0 + Param.lambda * alt.DeepCopy();
         pamb = Param.p0 * mobjFunctions.Pow(( Tamb.DeepCopy() / Param.temp0 ), ( - Param.g0 /
               Param.lambda / Param.Rgas ));
         rho = Param.rho0 * pamb.DeepCopy() / Param.p0 * Param.temp0 / Tamb.DeepCopy();
         v_snd = sqrt ( Param.gamma * Param.Rgas * Tamb.DeepCopy() );
         adjAI = 1.0e1 * log10 ( rho.DeepCopy() * v_snd.DeepCopy() / 409.81 );
         //   Noise fraction adjustment
         if ( objNoise.met == 1 || objNoise.met == 3 )
         {
            if ( objNoise.met == 1 )
            {
               s0 = 171.92;
            }
            else
            {
               s0 = 1719.2;
            }
            sL = s0.DeepCopy() * mobjFunctions.Pow(1.0e1, ( ( Sniv.DeepCopy() -
                  SMnfniv.DeepCopy() ) / 1.0e1 ));
            alpha1 = - q_seg.DeepCopy() / sL.DeepCopy();
            alpha2 = ( x_seg.DeepCopy() - q_seg.DeepCopy() ) / sL.DeepCopy();
            F_12 = ( alpha2.DeepCopy() / ( 1.0 + mobjFunctions.Pow(alpha2.DeepCopy(), 2) ) +
                  atan ( alpha2.DeepCopy() ) - alpha1.DeepCopy() / ( 1.0 +
                  mobjFunctions.Pow(alpha1.DeepCopy(), 2) ) - atan ( alpha1.DeepCopy() ) ) /
                  Param.pi;
            if ( F_12 <= Param.0.d0 )
            {
               adjNF = - 1.0e5;
            }
            else
            {
               adjNF = 1.0e1 * log10 ( F_12.DeepCopy() );
            }
         }
         else if ( objNoise.met == 2 || objNoise.met == 4 )
         {
            adjNF = Param.0.d0 * adjAI.DeepCopy();
         }
         //   Duration adjustment
         if ( objNoise.met == 1 || objNoise.met == 3 )
         {
            adjDUR = 1.0e1 * log10 ( 1.6e2 / v0.DeepCopy() );
         }
         else if ( objNoise.met == 2 || objNoise.met == 4 )
         {
            adjDUR = Param.0.d0 * adjAI.DeepCopy();
         }
         //   Lateral attenuation adjustment
         if ( Math.Abs ( SLRseg - z0 ) <= 1.0e-10 )
         {
            SLRseg = SLRseg.DeepCopy() + 1.0;
         }
         beta = asin ( ( z0.DeepCopy() - Pobs [ 3 ] ) / SLRseg.DeepCopy() ) * 1.8e2 / Param.pi;
         phi = asin ( ( z0.DeepCopy() - Pobs [ 3 ] ) / SLRseg.DeepCopy() );
         xseg = sqrt ( mobjFunctions.Pow(SLRseg.DeepCopy(), 2.0) - mobjFunctions.Pow((
               z0.DeepCopy() - Pobs [ 3 ] ), 2.0) ) * 0.3048;
         //   Engine installation effects
         if ( objNoise.enmount == "FUSE" )
         {
            E = 1.0e1 * log10 ( mobjFunctions.Pow(( 0.1225 * mobjFunctions.Pow(cos (
                  phi.DeepCopy() ), 2.0) + mobjFunctions.Pow(sin ( phi.DeepCopy() ), 2.0) ),
                  0.329) );
         }
         else if ( objNoise.enmount == "WING" && beta >= Param.0.d0 && beta <= 1.8e2 )
         {
            E = 1.0e1 * log10 ( mobjFunctions.Pow(( 3.9e-3 * mobjFunctions.Pow(cos (
                  phi.DeepCopy() ), 2.0) + mobjFunctions.Pow(sin ( phi.DeepCopy() ), 2.0) ),
                  0.062) / ( 0.8786 * mobjFunctions.Pow(sin ( 2.0 * phi.DeepCopy() ), 2.0) +
                  mobjFunctions.Pow(cos ( 2.0 * phi.DeepCopy() ), 2.0) ) );
         }
         else if ( objNoise.enmount == "WING" && beta < Param.0.d0 && beta > - 1.8e2 )
         {
            E = 1.0e1 * log10 ( 3.9e-mobjFunctions.Pow(3, 0.062) );
         }
         else if ( objNoise.enmount == "PROP" )
         {
            E = Param.0.d0;
         }
         else
         {
            E = Param.0.d0;
         }
         //   Ground effect
         if ( xseg < 914.4 )
         {
            G = 11.83 * ( 1.0 - Math.Exp ( - 2.74e-3 * xseg.DeepCopy() ) );
         }
         else
         {
            G = 11.83 * ( 1.0 - Math.Exp ( - 2.74e-3 * 914.4 ) );
         }
         //   Refraction scattering
         if ( beta <= Param.0.d0 )
         {
            xLambda = 10.857;
         }
         else if ( beta <= 5.0e1 )
         {
            xLambda = 1.137 - 2.29e-2 * beta.DeepCopy() + 9.72 * Math.Exp ( - 0.142 *
                  beta.DeepCopy() );
         }
         else
         {
            xLambda = Param.0.d0;
         }
         //   Determine total lateral attenuation
         adjLA = G.DeepCopy() * xLambda.DeepCopy() / 10.86 - E.DeepCopy();
         //   Determine the total adjustments
         adjS = adjAI.DeepCopy() + adjDUR.DeepCopy() - adjLA.DeepCopy() + adjNF.DeepCopy();
         adjM = adjAI.DeepCopy() - adjLA.DeepCopy();
         
         return;
      }
   }
}
