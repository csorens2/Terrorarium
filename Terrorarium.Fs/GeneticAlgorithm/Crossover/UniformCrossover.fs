namespace Terrorarium

module UniformCrossover = 
    let PerformCrossover parent_a parent_b crossover_choices = 
        if Array.length parent_a.Genes <> Array.length parent_b.Genes then
            failwith "Cannot perform crossover: Parents not of same length"

        let childGenes = 
            Array.zip parent_a.Genes parent_b.Genes
            |> Seq.zip crossover_choices
            |> Seq.map (fun (choice, (a_gene, b_gene)) -> if choice then a_gene else b_gene)
            |> Seq.toArray
        {Chromosome.Genes = childGenes}

    let Crossover parent_a parent_b = 
        let choices = Seq.initInfinite (fun _ -> List.randomChoice [true; false])
        PerformCrossover parent_a parent_b choices

