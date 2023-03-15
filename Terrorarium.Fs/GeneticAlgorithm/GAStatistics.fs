﻿module GAStatistics

open Terrorarium

type GAStatistics = {
    MinFitness: float
    MaxFitness: float
    AvgFitness: float
    MedianFitness: float
}

let New (population: seq<IIndividual>) = 
    let len = Seq.length population

    let fitnesses = 
        population
        |> Seq.map (fun x -> x.Fitness)
        |> Seq.sortBy (fun x -> x)
        |> Seq.toList

    let minFit = fitnesses[0]
    let maxFit = fitnesses[len - 1]
    let avgFit = fitnesses |> Seq.averageBy(fun x -> x)
    let medianFit = 
        if len % 2 = 0 then
            (fitnesses[len / 2 - 1] + fitnesses[len / 2]) / 2.0
        else
            fitnesses[len / 2]
    {GAStatistics.MinFitness = minFit; MaxFitness = maxFit; AvgFitness = avgFit; MedianFitness = medianFit}

