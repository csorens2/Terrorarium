namespace Terrorarium

// Future idea:
// Fitness function as parameter to evolve
// Switch mate selection to rank selection
        
type GeneticAlgorithm (
        selectionMethod: ISelectionMethod, 
        crossoverMethod: ICrossoverMethod, 
        mutationMethod: IMutationMethod) = 
    member this.SelectionMethod = selectionMethod
    member this.CrossoverMethod = crossoverMethod
    member this.MutationMethod = mutationMethod
    member this.Evolve (population: seq<IIndividual>) = 
        let evolvedPopulation =  
            population
            |> Seq.map (fun x ->
                let parent_a = this.SelectionMethod.Select population
                let parent_b = this.SelectionMethod.Select population
                let child = this.CrossoverMethod.Crossover parent_a.Chromosome parent_b.Chromosome
                let mutatedChild = this.MutationMethod.Mutate child
                x.Create mutatedChild)
        (evolvedPopulation, GAStatistics.New population)