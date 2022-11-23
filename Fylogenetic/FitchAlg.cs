using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fylogenetic
{
    internal class FitchAlg
    {
        public static int Calculate(Tree tree)
        {
            var leafs = new List<Leaf>();
            int score = 0;
            var rootLabel = GetJoin(tree.root.child1, tree.root.child2, ref score);

            char rL = rootLabel.ElementAt(new Random().Next(rootLabel.Count));
            tree.root.Label = new() { rL };

            Queue<Leaf> queue = new Queue<Leaf>();
            queue.Enqueue(tree.root);

            while(queue.Count > 0)
            {
                var curr = queue.Dequeue();
                var currLabel = curr.Label.ElementAt(0);
                if (curr.child1 is not null)
                {
                    queue.Enqueue(curr.child1);
                    if (curr.child1.Label.Contains(currLabel))
                    {
                        curr.child1.Label = new() { currLabel }; // podedil stejny
                    }
                    else
                    {
                        curr.child1.Label = new() { curr.child1.Label.ElementAt(new Random().Next(curr.child1.Label.Count)) }; // random element
                    }
                }
                if (curr.child2 is not null)
                {
                    queue.Enqueue(curr.child2);
                    if (curr.child2.Label.Contains(currLabel))
                    {
                        curr.child2.Label = new() { currLabel }; // podedil stejny
                    }
                    else
                    {
                        curr.child2.Label = new() { curr.child2.Label.ElementAt(new Random().Next(curr.child1.Label.Count)) }; // random element
                    }
                }
            }
            return score;
        }
        private static HashSet<char> GetJoin(Leaf leaf1, Leaf leaf2, ref int score)
        {
            var leaf1Label = GetLabel(leaf1, ref score);
            var leaf2Label = GetLabel(leaf2, ref score);
            var intersection = leaf2Label.Intersect(leaf1Label).ToHashSet();
            if (intersection.Count == 0)
            {
                score = score + 1;
                intersection = new HashSet<char>(leaf1Label);
                foreach (var c in leaf2Label)
                {
                    intersection.Add(c);
                }
            }
            return intersection;
        }
        private static HashSet<char> GetLabel(Leaf leaf, ref int score)
        {
            if (leaf.child1 is null && leaf.child2 is null) return leaf.Label;
            leaf.Label = GetJoin(leaf.child1, leaf.child2, ref score);
            return leaf.Label;
        }
    }
}
