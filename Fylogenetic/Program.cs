using Fylogenetic;



var tree = CreateTree();

var score = FitchAlg.Calculate(tree);

Queue<Leaf> queue = new Queue<Leaf>();
queue.Enqueue(tree.root);
while (queue.Count > 0)
{
    var curr = queue.Dequeue();
    Console.WriteLine($"{curr.name}  {curr.Label.ElementAt(0)}");
    if (curr.child1 is not null)
    {
        queue.Enqueue(curr.child1);
    }
    if (curr.child2 is not null)
    {
        queue.Enqueue(curr.child2);
    }
}

 UPGMA.Calculate(null);


static Tree CreateTree()
{
    var human = new Leaf
    {
        name = "human",
        Label = new() { 'C' }
    };
    var chimp = new Leaf
    {
        name = "chimp",
        Label = new() { 'T' }
    };
    var humChim = new Leaf
    {
        name = "humChim",
        child1 = human,
        child2 = chimp
    };
    var gibbon = new Leaf
    {
        name = "gibbon",
        Label = new() { 'G' }
    };
    var lemur = new Leaf
    {
        name = "lemur",
        Label = new() { 'T' }
    };
    var gibLem = new Leaf
    {
        name = "gibLem",
        child1 = gibbon,
        child2 = lemur
    };
    var gorrila = new Leaf
    {
        name = "gorrila",
        Label = new() { 'A' }
    };
    var gibLemGorr = new Leaf
    {
        name = "gibLemGorr",
        child1 = gibLem,
        child2 = gorrila
    };
    var bonobo = new Leaf
    {
        name = "bonobo",
        Label = new() { 'A' }
    };
    var humChimGibLemGor = new Leaf
    {
        name = "humchimgiblemgor",
        child1 = humChim,
        child2 = gibLemGorr
    };
    var root = new Leaf
    {
        name = "root",
        child1 = humChimGibLemGor,
        child2 = bonobo
    };
    return new Tree { root = root };
}