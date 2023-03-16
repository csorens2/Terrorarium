namespace Terrorarium

type AnimalIndividual = {
    Individual: Individual
}

module AnimalIndividual = 
       
    let rec Create chromosome = 
        {
            Individual.Create = Create 
            Chromosome = chromosome
            Fitness = 0.0
        }

    let New animal = 
        {AnimalIndividual.Individual = {
            Individual.Create = Create
            Individual.Chromosome = Animal.AsChromosome animal
            Individual.Fitness = float animal.Satiation}}

    let IntoAnimal config animalIndivid =
        Animal.FromChromosome config animalIndivid