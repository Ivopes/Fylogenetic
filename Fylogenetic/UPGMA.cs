using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fylogenetic
{
    internal class UPGMA
    {
        public static Leaf Calculate(List<Leaf> leafs)
        {
            int[,] distanceMatrix = new int[,] { { int.MaxValue, 20, 26, 26, 26 },
                                                { 20, int.MaxValue, 26, 26, 26 } ,
                                                { 26, 26, int.MaxValue,  16, 16 },
                                                { 26, 26, 16, int.MaxValue, 10 },
                                                { 26, 26, 16, 10, int.MaxValue}
                                                };
            var copy = new int[distanceMatrix.GetLength(0),distanceMatrix.GetLength(1)];
            for (int i = 0; i < distanceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < distanceMatrix.GetLength(0); j++)
                {
                    copy[i, j] = distanceMatrix[i, j];
                }
            }

            int counter = 4;
            

            Leaf resultRoot = null;
            while (counter-- > 0)
            {
                PickClosest(copy, out int i, out int j);
                var val = copy[i, j];

                var parentLeaf = new Leaf { heigh = val / 2 };
                resultRoot = parentLeaf;
                parentLeaf.child1 = leafs[i];
                parentLeaf.child2 = leafs[j];
                parentLeaf.name = parentLeaf.child1.name + parentLeaf.child2.name;
                parentLeaf.childIndexes = GetChildIndex(parentLeaf);
                parentLeaf.child1Length = parentLeaf.heigh - parentLeaf.child1.heigh;
                parentLeaf.child2Length = parentLeaf.heigh - parentLeaf.child2.heigh;
                // doplnit upravu matice, vymyslet spojeni s listem, at indexy odpovidaji

                //Upravit list
                int min = Math.Min(i, j);
                int max = Math.Max(i, j);
                leafs[min] = parentLeaf;
                //leafs.RemoveAt(max);
                parentLeaf.Index= min;

                //vypocitat vzdalenosti pole
                for (int index = 0; index < distanceMatrix.GetLength(0); index++)
                {
                    if (!leafs.Where(l => l.Index == index).Any()) continue;
                    int newDistance = 0;
                    for (int jIndex = 0; jIndex < parentLeaf.childIndexes.Count; jIndex++)
                    {
                        newDistance += distanceMatrix[parentLeaf.childIndexes[jIndex], index];
                    }
                    newDistance /= parentLeaf.childIndexes.Count;
                    copy[index, min] = newDistance;
                    copy[min, index] = newDistance;
                }
                copy[min, min] = int.MaxValue;
                // premazat spojene vzdalenosti
                for (int index = 0; index < parentLeaf.childIndexes.Count; index++)
                {
                    for (int jIndex = 0; jIndex < parentLeaf.childIndexes.Count; jIndex++)
                    {
                        copy[parentLeaf.childIndexes[index], parentLeaf.childIndexes[jIndex]] = int.MaxValue;
                    }
                }
                for (int index = 0; index < distanceMatrix.GetLength(0); index++)
                {
                    copy[max, index] = int.MaxValue;
                    copy[index, max] = int.MaxValue;
                }
            }

            return resultRoot;
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
        private static List<int> GetChildIndex(Leaf leaf)
        {
            if (leaf.child1 is null && leaf.child2 is null) return new() { leaf.Index };

            var res = GetChildIndex(leaf.child1);
            res.AddRange(GetChildIndex(leaf.child2));

            return res;
        }
    }
}
