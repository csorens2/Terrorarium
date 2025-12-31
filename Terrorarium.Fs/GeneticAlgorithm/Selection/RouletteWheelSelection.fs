namespace Terrorarium

module RouletteWheelSelection = 
    let Select individuals = 
        let totalWeight = Array.sumBy (fun individual -> individual.Fitness) individuals

        individuals
        |> Array.map (fun individual -> (individual, individual.Fitness / totalWeight))
        |> RandomSelection.Select