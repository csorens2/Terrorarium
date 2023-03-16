namespace Terrorarium

module GaussianMutation = 
    let Mutate chance coeff child = 
        if not (0.0 <= chance && chance <= 1.0) then
            failwith "Invalid mutation chance"
        let mutatedGenes = 
            child.Genes
            |> Array.map (fun x -> 
                let sign = 
                    if RNG.NextBool () then
                        -1.0
                    else
                        1.0
                if RNG.NextDouble () <= chance then
                    x + (sign * coeff * RNG.NextDouble ())
                else
                    x)
        {Chromosome.Genes = mutatedGenes}