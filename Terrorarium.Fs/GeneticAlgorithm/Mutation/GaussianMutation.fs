namespace Terrorarium

type GaussianMutation (chance: float, coeff: float) = 
    do
        if not (0.0 <= chance && chance <= 1.0) then
            failwith "Invalid mutation chance"
    member this.Chance = chance
    member this.Coefficient = coeff
    interface IMutationMethod with
        member this.Mutate child = 
            let mutatedGenes = 
                child.Genes
                |> Array.map (fun x -> 
                    let sign = 
                        if RNG.NextBool () then
                            -1.0
                        else
                            1.0
                    if RNG.NextDouble () <= this.Chance then
                        x + (sign * this.Coefficient * RNG.NextDouble ())
                    else
                        x)
            {Chromosome.Genes = mutatedGenes}