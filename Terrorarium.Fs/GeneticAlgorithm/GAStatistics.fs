namespace Terrorarium

type GAStatistics = {
    MinFitness: float
    MaxFitness: float
    AvgFitness: float
    MedianFitness: float
}

module GAStatistics = 
    let New population = 
        let fitnesses = 
            population
            |> Array.map (fun x -> x.Fitness)
            |> Array.sort

        let fitnessesLength = Array.length fitnesses

        let minFit = fitnesses[0]
        let maxFit = fitnesses[fitnessesLength - 1]
        let avgFit =  Array.averageBy id fitnesses
        let medianFit = 
            if fitnessesLength % 2 = 0 then
                (fitnesses[fitnessesLength / 2 - 1] + fitnesses[fitnessesLength / 2]) / 2.0
            else
                fitnesses[fitnessesLength / 2]
        {GAStatistics.MinFitness = minFit; MaxFitness = maxFit; AvgFitness = avgFit; MedianFitness = medianFit}