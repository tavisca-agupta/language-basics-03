using System;
using System.Linq;

namespace c_
{
    class Program02
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
    
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            //Console.ReadKey(true);
            
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int[] res=new int[dietPlans.Length];
            //suffix i means index, letter means value
            int P = protein.Max(),p=protein.Min();
            int Pi = Array.IndexOf(protein,P),pi=Array.IndexOf(protein,p);   //or  Pi= protien.ToList().IndexOf(P);
            
            int C = carbs.Max(),c=carbs.Min();    //values
            int Ci = Array.IndexOf(carbs,C),ci=Array.IndexOf(carbs,c); //indices

            int F = fat.Max(),f=fat.Min();
            int Fi = Array.IndexOf(fat,F),fi=Array.IndexOf(fat,f); 

            int T,Ti,t,ti,Flag=-1;
            int Pcal=((P*5)+(carbs[Pi]*5)+(fat[Pi]*9)),Ccal=((protein[Ci]*5)+(C*5)+(fat[Ci]*9));
            int pcal=((p*5)+(carbs[pi]*5)+(fat[pi]*9)),ccal=((protein[ci]*5)+(c*5)+(fat[ci]*9));

            //total highest calorie and index
            if(Pcal  == Ccal)
            {
                T=Pcal; 
                Ti=Pi>Ci?Ci:Pi;
            }
            else if(Pcal  > Ccal)
            {
                T=Pcal; Ti=Pi; //index of highest protien calorie
            } 
            else
            {
                T=Ccal; Ti=Ci;
            }

            //Total smllest calorie and indx
            if(pcal == ccal)
            {
                t=pcal; 
                ti=pi>ci ?ci : pi;  //selecting smallest index 
                Flag= pi>ci ?pi: ci;  //selecting the upper indx in flag to counter exception
            } 
            else if(pcal  < ccal)
            {
                t=pcal;  ti=pi;
            } 
            else
            {
                t=ccal;  ti=ci;
            }

            for(int i=0;i<dietPlans.Length;i++)
            {
                int temp=0;
                if(dietPlans[i]=="") //handling empty string
                    {res[i]=0;continue;}
                if(dietPlans[i][0]=='P')
                    temp=Pi;
                if(dietPlans[i][0]=='p')
                    temp=pi;
                if(dietPlans[i][0]=='C')
                    temp=Ci;
                if(dietPlans[i][0]=='c')
                    temp=ci;
                if(dietPlans[i][0]=='F')
                    temp=Fi;
                if(dietPlans[i][0]=='f')
                    temp=fi;
                if(dietPlans[i][0]=='T')
                    temp=Ti;
                if(dietPlans[i][0]=='t')
                {
                    temp=ti;
                    if(Flag>-1)    //Handling conflict condition
                        for(int j=1;j<dietPlans[i].Length;j++)
                        {
                            if(fat[ti]==fat[Flag] && dietPlans[i][j]=='F')
                                continue;
                            if(carbs[ti]>carbs[Flag] && dietPlans[i][j]=='c')
                                temp=Flag;
                        }      
                }
                
                res[i]=temp;
            }
            return res;

            throw new NotImplementedException();
        }
    }
}
