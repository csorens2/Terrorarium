namespace Terrorarium

type AnimalIndividual(fitness, chromosome) =
    member this.Fitness = fitness
    member this.Chromosome = chromosome
    new(animal:Animal) = 
        AnimalIndividual(float animal.Satiation, Animal.AsChromosome animal)
    member this.IntoAnimal config = 
        Animal.FromChromosome config this.Chromosome

    interface IIndividual with
        member this.Create(interfaceChromo) = 
            AnimalIndividual(0.0, interfaceChromo)
        member this.Chromosome = 
            this.Chromosome
        member this.Fitness = 
            this.Fitness
    
            
    