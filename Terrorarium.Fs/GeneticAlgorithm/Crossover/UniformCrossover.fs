﻿namespace Terrorarium

module UniformCrossover = 
    let PerformCrossover parent_a parent_b crossover_choices = 
        if Array.length parent_a.Genes <> Array.length parent_b.Genes then
            failwith "Cannot perform crossover: Parents not of same length"

        let childGenes = 
            (Seq.zip (Array.zip parent_a.Genes parent_b.Genes) crossover_choices)
            |> Seq.map (fun ((a_gene, b_gene), select) -> 
                if select then
                    a_gene
                else
                    b_gene)
            |> Seq.toArray
        {Chromosome.Genes = childGenes}

    let Crossover parent_a parent_b = 
        PerformCrossover parent_a parent_b (RNG.InfiniteBools ())

