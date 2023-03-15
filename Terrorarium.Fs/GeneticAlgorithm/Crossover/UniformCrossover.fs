namespace Terrorarium

open Chromosome

type UniformCrossover() = 
    interface ICrossoverMethod with
        member this.Crossover parent_a parent_b = 
            UniformCrossover.PerformCrossover parent_a parent_b (RNG.InfiniteBools ())

    static member PerformCrossover parent_a parent_b crossover_choices =
        if Seq.length parent_a.Genes <> Seq.length parent_b.Genes then
            failwith "Cannot perform crossover: Parents not of same length"

        let childGenes = 
            (Seq.zip (Seq.zip parent_a.Genes parent_b.Genes) crossover_choices)
            |> Seq.map (fun ((a_gene, b_gene), select) -> 
                if select then
                    a_gene
                else
                    b_gene)
        {Chromosome.Genes = childGenes}

