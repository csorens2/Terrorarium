namespace Terrorarium

module WeightedRandomSelection = 

    let SelectWithRoll toSelectFrom roll = 
        let cumulativeWeightArray = 
            Seq.scan (fun acc (_, nextWeight) -> acc +  nextWeight) 0.0 toSelectFrom
            |> Seq.skip 1
            |> Seq.toArray

        let rec binarySearchForEqualOrFirstOver left right target = 
            if left = right then 
                left
            else
                let middle = (right + left) / 2
                if (Array.get cumulativeWeightArray middle) = target then 
                    middle
                else if (Array.get cumulativeWeightArray middle) < target then 
                    binarySearchForEqualOrFirstOver (middle+1) right target
                else 
                    binarySearchForEqualOrFirstOver left middle target

        let foundIndex = binarySearchForEqualOrFirstOver 0 (Array.length cumulativeWeightArray - 1) roll

        let (toReturn, _) = Seq.item foundIndex toSelectFrom

        toReturn

    let Select toSelectFrom = 
        let totalWeight = Seq.sumBy (fun (_, weight) -> weight) toSelectFrom
        SelectWithRoll toSelectFrom (RNG.NextRangeFloat(0.0, totalWeight))

    let SelectIndividual toSelectFrom = 
        toSelectFrom
        |> Seq.map (fun indiv -> (indiv, indiv.Fitness))
        |> Select
   
        
            




