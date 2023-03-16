namespace Terrorarium

// Future idea:
// Switch mate selection to rank selection

type GeneticAlgorithm = {
    SelectionMethod: SelectionMethod
    CrossoverMethod: CrossoverMethod
    MutationMethod: MutationMethod
}

module GeneticAlgorithm = 
    let Evolve ga population = 
        let evolvedPopulation =  
            population
            |> Array.map (fun x ->
                let parent_a = ga.SelectionMethod.Select population
                let parent_b = ga.SelectionMethod.Select population
                let child = ga.CrossoverMethod.Crossover parent_a.Chromosome parent_b.Chromosome
                let mutatedChild = ga.MutationMethod.Mutate child
                x.Create mutatedChild)
        (evolvedPopulation, GAStatistics.New population)