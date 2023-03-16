namespace Terrorarium.Tests
open Terrorarium

module TestIndividual = 
    let rec Create chromosome = 
        {
            Individual.Create = Create 
            Chromosome = chromosome
            Fitness = 0.0
        }

    let New = 
        {
            Individual.Create = Create
            Chromosome = {Chromosome.Genes = [||]}
            Fitness = 0.0
        }