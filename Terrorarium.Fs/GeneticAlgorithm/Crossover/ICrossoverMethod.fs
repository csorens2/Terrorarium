namespace Terrorarium

open Chromosome

type ICrossoverMethod = 
    abstract member Crossover: parent_a:Chromosome -> parent_b:Chromosome -> Chromosome
