namespace Terrorarium.Tests
open Terrorarium

type TestIndividual(fitness: float, chromosome: Chromosome) = 
    member this.Fitness = fitness
    member this.Chromosome = chromosome
    interface IIndividual with
        member this.Create(interfaceChromo) = 
            AnimalIndividual(0.0, interfaceChromo)
        member this.Chromosome = 
            this.Chromosome
        member this.Fitness = 
            this.Fitness