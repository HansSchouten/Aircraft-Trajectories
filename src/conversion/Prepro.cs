using System;
using FortranToC.Conversion;

namespace Company.Project.Main
{
   public class Prepro
   {
      AllObjects mobjAllObjects;
      FixedString mobjFixedString = new FixedString();
      FortranFunctions mobjFunctions = new FortranFunctions();

      public Prepro(AllObjects robjAllObjects)
      {
         mobjAllObjects = robjAllObjects;
      }

      
      public void mexFunction(ref int nlhs,
                              ref double plhs,
                              ref int nrhs,
                              ref double prhs)
      {
         string str1 = new string(' ', 4);
         string str2 = new string(' ', 3);
         double[,] dist;
         double dist_pr = 0;
         double[,] grid;
         double grid_pr = 0;
         double[,] Jac;
         double[,] SER;
         double SER_pr = 0;
         double size = 0;
         double[,] state;
         double state_pr = 0;
         double strlen = 0;
         double[,] tab;
         double tab_pr = 0;
         double[,] thr;
         double thr_pr = 0;
         int i = 0;
         int Jac_pr = 0;
         int m = 0;
         const int maxbuf1 = 4;
         const int maxbuf2 = 3;
         int mexErrMsgTxt = 0;
         int mwpointer = 0;
         int mwsize = 0;
         int mxCopyPtrToReal8 = 0;
         int mxCopyReal8ToPtr = 0;
         int mxCreateDoubleMatrix = 0;
         int mxCreateString = 0;
         int mxGetM = 0;
         int mxGetN = 0;
         int mxGetPr = 0;
         int mxGetString = 0;
         int n = 0;
         int status = 0;
         mwpointer :: plhs ( * ) , prhs ( * );
         mwpointer :: mxGetPr , mxCreateDoubleMatrix;
         //   Input arguments
         mwpointer :: state_pr , grid_pr , tab_pr , dist_pr , thr_pr;
         //   Output arguments
         mwpointer :: SER_pr , Jac_pr;
         mwpointer :: mxCreateString;
         mwpointer :: mxGetString;
         mwsize :: mxGetM , mxGetN , maxbuf1 , maxbuf2;
         mwsize :: m ( nrhs ) , n ( nrhs ) , size ( nrhs );
         mwpointer :: strlen;
         //   Get size of the I arrays
         for (i = 1; i <= nrhs; i++)
         {
            m ( i ) = mxGetM ( prhs ( i ) );
            n ( i ) = mxGetN ( prhs ( i ) );
            size ( i ) = m ( i ) * n ( i );
         }
         //   Check for proper number of arguments
         if ( nrhs != 7 )
         {
            mexErrMsgTxt ( "Seven inputs required" );
         }
         else if ( nlhs != 2 )
         {
            mexErrMsgTxt ( "Two outputs required" );
         }
         //   Extract string
         strlen = mxGetM ( prhs ( 6 ) ) * mxGetN ( prhs ( 6 ) );
         status = mxGetString ( prhs ( 6 ) , str1 , maxbuf1 );
         strlen = mxGetM ( prhs ( 7 ) ) * mxGetN ( prhs ( 7 ) );
         status = mxGetString ( prhs ( 7 ) , str2 , maxbuf2 );
         //   Create matrices Fortran array from the input arguments
         state = new double [m ( 1 ) + 1, n ( 1 ) + 1];
         grid = new double [m ( 2 ) + 1, n ( 2 ) + 1];
         tab = new double [m ( 3 ) + 1, n ( 3 ) + 1];
         dist = new double [m ( 4 ) + 1, n ( 4 ) + 1];
         thr = new double [m ( 5 ) + 1, n ( 5 ) + 1];
         state_pr = mxGetPr ( prhs ( 1 ) );
         grid_pr = mxGetPr ( prhs ( 2 ) );
         tab_pr = mxGetPr ( prhs ( 3 ) );
         dist_pr = mxGetPr ( prhs ( 4 ) );
         thr_pr = mxGetPr ( prhs ( 5 ) );
         mxCopyPtrToReal8 ( state_pr , state , size ( 1 ) );
         mxCopyPtrToReal8 ( grid_pr , grid , size ( 2 ) );
         mxCopyPtrToReal8 ( tab_pr , tab , size ( 3 ) );
         mxCopyPtrToReal8 ( dist_pr , dist , size ( 4 ) );
         mxCopyPtrToReal8 ( thr_pr , thr , size ( 5 ) );
         //   Create matrix for the return argument
         plhs ( 1 ) = mxCreateDoubleMatrix ( m ( 2 ) , 1 , 0 );
         plhs ( 2 ) = mxCreateDoubleMatrix ( m ( 1 ) * m ( 2 ) , n ( 1 ) , 0 );
         SER_pr = mxGetPr ( plhs ( 1 ) );
         Jac_pr = mxGetPr ( plhs ( 2 ) );
         //   I/O argument dimension
         Jac = new double [m ( 1 ) * m ( 2 ) + 1, n ( 1 ) + 1];
         SER = new double [m ( 2 ) + 1, 2];
         //   Call the computational subroutine
         noise_Prep ( state , grid , tab , dist , thr , SER , Jac , ref str1 , ref str2 , m ,
               n );
         //   Load the data into output arrays
         mxCopyReal8ToPtr ( SER , SER_pr , m ( 2 ) * 1 );
         mxCopyReal8ToPtr ( Jac , Jac_pr , m ( 1 ) * m ( 2 ) * n ( 1 ) );
         //   Clean memory
         mobjFunctions.Deallocate ( state , grid , tab , Jac , SER );
         
         return;
      }
      
      //************************************************************************************************************************************************************************************
      //************************************************************************************************************************************************************************************
      
      //************************************************************************************************************************************************************************************
      //************************************************************************************************************************************************************************************
      
      public void noise_Prep(double[,] p_state, 
                             double[,] p_grid, 
                             double[,] p_tab, 
                             double[,] p_slr, 
                             double[,] p_thrl, 
                             double[,] p_SER, 
                             double[,] p_Jac, 
                             ref string str1, 
                             ref string str2, 
                             int[] m, 
                             int[] n)
      {
         double[] state = new double[m ( 1 + 1];
         for( int i1 = 0; i1 < m ( 1 + 1; i1++)
         {
            state[i1] = p_state[1, 1];
         }
         double[] grid = new double[m ( 2 + 1];
         for( int i1 = 0; i1 < m ( 2 + 1; i1++)
         {
            grid[i1] = p_grid[1, 1];
         }
         double[] tab = new double[m ( 3 + 1];
         for( int i1 = 0; i1 < m ( 3 + 1; i1++)
         {
            tab[i1] = p_tab[1, 1];
         }
         double[] slr = new double[m ( 4 + 1];
         for( int i1 = 0; i1 < m ( 4 + 1; i1++)
         {
            slr[i1] = p_slr[1, 1];
         }
         double[] thrl = new double[m ( 5 + 1];
         for( int i1 = 0; i1 < m ( 5 + 1; i1++)
         {
            thrl[i1] = p_thrl[1, 1];
         }
         double[] SER = new double[m ( 2 + 1];
         for( int i1 = 0; i1 < m ( 2 + 1; i1++)
         {
            SER[i1] = p_SER[1, 1];
         }
         double[] Jac = new double[m ( 1 + 1];
         for( int i1 = 0; i1 < m ( 1 + 1; i1++)
         {
            Jac[i1] = p_Jac[1, 1];
         }
         noise_Prep(state, grid, tab, slr, thrl, SER, Jac, ref str1, ref str2, m, n);
         for( int i1 = 0; i1 < m ( 1 + 1; i1++)
         {
            p_state[1, 1] = state[i1];
         }
         for( int i1 = 0; i1 < m ( 2 + 1; i1++)
         {
            p_grid[1, 1] = grid[i1];
         }
         for( int i1 = 0; i1 < m ( 3 + 1; i1++)
         {
            p_tab[1, 1] = tab[i1];
         }
         for( int i1 = 0; i1 < m ( 4 + 1; i1++)
         {
            p_slr[1, 1] = slr[i1];
         }
         for( int i1 = 0; i1 < m ( 5 + 1; i1++)
         {
            p_thrl[1, 1] = thrl[i1];
         }
         for( int i1 = 0; i1 < m ( 2 + 1; i1++)
         {
            p_SER[1, 1] = SER[i1];
         }
         for( int i1 = 0; i1 < m ( 1 + 1; i1++)
         {
            p_Jac[1, 1] = Jac[i1];
         }
      }

      public void noise_Prep(double[] state,
                             double[] grid,
                             double[] tab,
                             double[] slr,
                             double[] thrl,
                             double[] SER,
                             double[] Jac,
                             ref string str1,
                             ref string str2,
                             int[] m,
                             int[] n)
      {
         Noise objNoise = mobjAllObjects.Noise;
         Deriv_class objDeriv_class = mobjAllObjects.Deriv_class;
         double 1 = 0;
         double 2 = 0;
         double derivative = 0;
         double dist = 0;
         double enmount = 0;
         double[,,] Jact;
         double[,] Jactmp;
         double P_obs = 0;
         double segment = 0;
         double[] SERtmp;
         double[] sttmp = new double[11];
         double tabNPD = 0;
         double thr = 0;
         int i = 0;
         int indep_vector = 0;
         int inf = 0;
         int inod = 0;
         int j = 0;
         int k = 0;
         int m_ft = 0;
         int met = 0;
         int ndist = 0;
         int ngrid = 0;
         int noise = 0;
         int nthr = 0;
         int outf = 0;
         //   Initialize
         objNoise.dist = new double [m [ 4 ] + 1];
         objNoise.thr = new double [m [ 5 ] + 1];
         mobjFixedString.SetString(ref objNoise.enmount, 0, 4 , 4, str1.Substring(0,4 ));
         if ( str2.Substring(0,1 ) == "S" )
         {
            objNoise.met = (int) (1);
         }
         else if ( str2.Substring(0,1 ) == "L" )
         {
            objNoise.met = (int) (2);
         }
         else if ( str2.Substring(0,1 ) == "E" )
         {
            objNoise.met = 3;
         }
         else if ( str2.Substring(0,1 ) == "P" )
         {
            objNoise.met = 4;
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < objNoise.dist.GetUpperBound(0); intTmpLoop1++)
         {
            objNoise.dist[1 + intTmpLoop1] = slr [ + intTmpLoop1, 1 ];
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < objNoise.thr.GetUpperBound(0); intTmpLoop1++)
         {
            objNoise.thr[1 + intTmpLoop1] = thrl [ + intTmpLoop1, 1 ];
         }
         //   Get problem sizes
         objNoise.inod = m [ 1 ];
         objNoise.ngrid = m [ 2 ];
         objNoise.ndist = m [ 4 ];
         objNoise.nthr = m [ 5 ];
         //   Allocate variables
         SERtmp = new double [objNoise.ngrid + 1];
         Jactmp = new double [objNoise.ngrid + 1, 11];
         Jact = new double [objNoise.inod + 1, n [ 1 ] + 1, objNoise.ngrid + 1];
         objNoise.P_obs = new double [objNoise.ngrid + 1, 4];
         objNoise.tabNPD = new double [m [ 5 ] + 1, m [ 4 ] + 1, 3];
         for (int intTmpLoop1 = 0; intTmpLoop1 < objNoise.P_obs.GetUpperBound(0);
               intTmpLoop1++)
         {
            for (int intTmpLoop2 = 0; intTmpLoop2 < objNoise.P_obs.GetUpperBound(1);
                  intTmpLoop2++)
            {
               objNoise.P_obs[1 + intTmpLoop1, 1 + intTmpLoop2] = grid[1 + intTmpLoop1] * m_ft;
            }
         }
         //   Reshape NPD data
         for (int intTmpLoop1 = 0; intTmpLoop1 < 1; intTmpLoop1++)
         {
            for (int intTmpLoop2 = 0; intTmpLoop2 < 1; intTmpLoop2++)
            {
         
            }
         }
         for (int intTmpLoop1 = 0; intTmpLoop1 < 1; intTmpLoop1++)
         {
            for (int intTmpLoop2 = 0; intTmpLoop2 < 1; intTmpLoop2++)
            {
         
            }
         }
         //   Call automatic differentiation and noise model
         mobjFunctions.InitializeArray(ref SER, 0.0);
         mobjFunctions.InitializeArray(ref Jact, 0.0);
         for (i = 1; i <= objNoise.inod - 1; i++)
         {
            for (j = 1; j <= 5; j++)
            {
               inf = (int) (2 * j - 1);
               outf = (int) (2 * j);
               for (int intTmpLoop1 = 0; intTmpLoop1 < outf - (inf) + 1; intTmpLoop1++)
               {
                  sttmp [inf + intTmpLoop1] = state [i + intTmpLoop1, j ];
               }
            }
            derivative ( 1 );
            indep_vector ( segment , sttmp );
            noise ( SERtmp , Jactmp );
            for (int intTmpLoop1 = 0; intTmpLoop1 < 1; intTmpLoop1++)
            {
               SER [ + intTmpLoop1, 1 ] = SER [ + intTmpLoop1, 1 ] + SERtmp [ + intTmpLoop1];
            }
            for (j = 1; j <= 5; j++)
            {
               for (k = 1; k <= objNoise.ngrid; k++)
               {
                  for (int intTmpLoop1 = 0; intTmpLoop1 < i + 1 - (i) + 1; intTmpLoop1++)
                  {
                     Jact [i + intTmpLoop1, j , k ] = Jact [i + intTmpLoop1, j , k ] + Jactmp
                           [ k , j * 2 - 1 + intTmpLoop1];
                  }
               }
            }
         }
         //   Prepare Jacobian for return argument
         j = (int) (1);
         mobjFunctions.InitializeArray(ref Jac, 0.0);
         for (k = 1; k <= objNoise.ngrid; k++)
         {
            for (i = 1; i <= objNoise.inod; i++)
            {
               for (int intTmpLoop1 = 0; intTmpLoop1 < 1; intTmpLoop1++)
               {
                  Jac [ j ,  + intTmpLoop1] = Jact [ i ,  + intTmpLoop1, k ];
               }
               j = (int) (j + 1);
            }
         }
         //   Clean memory
         mobjFunctions.Deallocate ( SERtmp , Jactmp , Jact , objNoise.P_obs , objNoise.tabNPD
               , objNoise.dist , objNoise.thr );
         
      }
      
      //************************************************************************************************************************************************************************************
      //************************************************************************************************************************************************************************************
   }
}
