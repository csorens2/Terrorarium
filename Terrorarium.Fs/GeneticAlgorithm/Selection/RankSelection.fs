namespace Terrorarium

open ExtraFunctions

module RankSelection = 

    let Linear selectionPressure individuals = 
        if not (selectionPressure >=< (1.0, 2.0)) then 
            failwith $"Invalid Selection Pressure '{selectionPressure}'. Must be between 1.0 and 2.0"
        else

            let totalIndividuals = double (Array.length individuals)
            let sortedIndividuals = Array.sortByDescending (fun individual -> individual.Fitness) individuals

            let getWeightFromRank (rank, individual) = 
                let probability = 
                    (1.0 / totalIndividuals) * (selectionPressure - (2.0 * selectionPressure - 2.0) * ((rank - 1.0) / (totalIndividuals - 1.0)))
                (individual, probability)

            sortedIndividuals
            |> Array.indexed
            |> Array.map (fun (index, individual) -> (index + 1, individual))
            |> Array.map (fun (rank, individual) -> getWeightFromRank (double rank, individual))
            |> WeightedRandomSelection.Select

    let Exponential selectionPressure individuals = 
        failwith "TODO"