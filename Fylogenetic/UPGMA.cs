using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fylogenetic
{
    internal class UPGMA
    {
        public static void Calculate(List<Leaf> leafs)
        {
            int[,] distanceMatrix = new int[,] { { int.MaxValue, 20, 26, 26, 26 }, 
                                                { 20, int.MaxValue, 26, 26, 26 } ,
                                                { 26, 26, int.MaxValue,  16, 16 },
                                                { 26, 26, 16, int.MaxValue, 10 },
                                                { 26, 26, 16, 10, int.MaxValue}
                                                };
            int counter  = 4;

            while (counter-- > 0)
            {
                PickClosest(distanceMatrix, out int i, out int j);
                var val = distanceMatrix[i, j];

                var parentLeaf = new Leaf { heigh = val / 2 };
                parentLeaf.child1 = leafs[i];
                parentLeaf.child2 = leafs[j];
                
                parentLeaf.child1Length = parentLeaf.child1Length - parentLeaf.child1.heigh;
                parentLeaf.child2Length = parentLeaf.child2Length - parentLeaf.child2.heigh;
                // doplnit upravu matice, vymyslet spojeni s listem, at indexy odpovidaji
            }

        }
        private static void PickClosest(int[,] matrix, out int y, out int x)
        {
            int closes = int.MaxValue;
            x = 0;
            y = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (closes > matrix[i, j])
                    {
                        closes = matrix[i, j];
                        x = j;
                        y = i;
                    }
                }
            }
        }
    }
}
