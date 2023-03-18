namespace Terrorarium

module AnimalIndividual = 
       
    let rec Create chromosome = {
        Individual.Create = Create 
        Chromosome = chromosome
        Fitness = 0.0
    }

    let FromAnimal animal = {
        Individual.Create = Create
        Chromosome = Animal.AsChromosome animal
        Fitness = float animal.Satiation
    }

    let IntoAnimal config animalIndivid =
        Animal.FromChromosome config animalIndivid