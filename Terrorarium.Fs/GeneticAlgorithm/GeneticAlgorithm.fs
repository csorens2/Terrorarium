namespace Terrorarium

type GeneticAlgorithm = {
    SelectionMethod: Individual array -> Individual
    CrossoverMethod: Chromosome -> Chromosome -> Chromosome
    MutationMethod: Chromosome -> Chromosome
}

module GeneticAlgorithm = 
    let Evolve ga population = 
        let evolvePopulation individual = 
            let parent_a = ga.SelectionMethod population
            let parent_b = ga.SelectionMethod population
            let child = ga.CrossoverMethod parent_a.Chromosome parent_b.Chromosome
            let mutatedChild = ga.MutationMethod child
            individual.Create mutatedChild

        (Array.map evolvePopulation population, FitnessStatistics.New population)