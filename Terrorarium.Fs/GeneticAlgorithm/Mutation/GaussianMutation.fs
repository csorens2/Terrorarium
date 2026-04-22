namespace Terrorarium

open System

module GaussianMutation = 
    let Mutate chance coefficient baseChromosome = 
        if not (0.0 <= chance && chance <= 1.0) then
            failwith "Invalid mutation chance. Must be between 0.0 and 1.0"
        else
            let mutateGene gene = 
                let sign = 
                    if List.randomChoice [true; false] then
                        -1.0
                    else
                        1.0
                if Random.Shared.NextDouble() <= chance then 
                    gene + (sign * coefficient * Random.Shared.NextDouble())
                else
                    gene

            {Chromosome.Genes = Array.map mutateGene baseChromosome.Genes}