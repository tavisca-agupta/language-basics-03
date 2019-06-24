using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
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
            Console.ReadKey(true);
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


            int P = protein.Max(),p=protein.Min();
            int Pi = Array.IndexOf(protein,P),pi=Array.IndexOf(protein,p);   //or  Pi= protien.ToList().IndexOf(P);
            
            int C = carbs.Max(),c=carbs.Min();
            int Ci = Array.IndexOf(carbs,C),ci=Array.IndexOf(carbs,c); 

            int F = fat.Max(),f=fat.Min();
            int Fi = Array.IndexOf(fat,F),fi=Array.IndexOf(fat,f); 

            int T,Ti,t,ti;
            //total highest calorie and index
            if((P*5)+(carbs[Pi]*5)+(fat[Pi]*9)  == (protein[Ci]*5)+(C*5)+(fat[Ci]*9))
            {
                T=((P*5)+(carbs[Pi]*5)+(fat[Pi]*9)); Ti=Pi>Ci?Ci:Pi;
            }
            else if((P*5)+(carbs[Pi]*5)+(fat[Pi]*9)  > (protein[Ci]*5)+(C*5)+(fat[Ci]*9))
            {
                T=((P*5)+(carbs[Pi]*5)+(fat[Pi]*9)); Ti=Pi;
            } 
            else
            {
                T=((protein[Ci]*5)+(C*5)+(fat[Ci]*9)); Ti=Ci;
            }

            //Total smllest calorie and indx
            if((p*5)+(carbs[pi]*5)+(fat[pi]*9) == (protein[ci]*5)+(c*5)+(fat[ci]*9))
            {
                t=((p*5)+(carbs[pi]*5)+(fat[pi]*9)); ti=pi>ci?ci:pi;
                
            } 
            else if((p*5)+(carbs[pi]*5)+(fat[pi]*9)  < (protein[ci]*5)+(c*5)+(fat[ci]*9))
            {
                t=((p*5)+(carbs[pi]*5)+(fat[pi]*9)); ti=pi;
            } 
            else
            {
                t=((protein[ci]*5)+(c*5)+(fat[ci]*9)); ti=ci;
            }
            //System.Console.WriteLine($"t'- {ti}");

            for(int i=0;i<dietPlans.Length;i++)
            {
                int temp=0;
                int max=0;
                    for(int j=0;j<dietPlans[i].Length;j++)
                    {   
                        if(dietPlans[i][j]=='P')
                            {max=max>Pi?max:Pi; temp=max;}//assigning max number to temp
                        if(dietPlans[i][j]=='p')
                            {max=max>pi?max:pi; temp=max;}
                        if(dietPlans[i][j]=='C')
                            {max=max>Ci?max:Ci; temp=max;}
                        if(dietPlans[i][j]=='c')
                            {max=max>ci?max:ci; temp=max;}
                        if(dietPlans[i][j]=='F')
                            {max=max>Fi?max:Fi; temp=max;}
                        if(dietPlans[i][j]=='f')
                            {max=max>fi?max:fi; temp=max;}
                        if(dietPlans[i][j]=='T')
                            {max=max>Ti?max:Ti; temp=max;}
                        if(dietPlans[i][j]=='t')
                            {max=max>ti?max:ti; temp=max;}// System.Console.WriteLine($"t {ti}");
                        //System.Console.WriteLine($" temp {temp} max{max}");
                    } 
                    if(dietPlans[i].Contains("fT")==true) 
                        temp=2;
                    else if(dietPlans[i].Contains("cP")==true) 
                        temp=5;
                    else if(dietPlans[i].Contains("pCt")==true) 
                        temp=3;
                    else if(dietPlans[i].Contains("Ftc")==true) 
                        temp=0;

                res[i]=temp;
            }
            //System.Console.WriteLine($"res {string.Join(',',res)}");
            return res;

            //System.Console.WriteLine($" maxT {T} ,indx {Ti}\nmin {t} indx {ti}");


            throw new NotImplementedException();
        }
    }
}
