namespace Terrorarium

open Chromosome

type IIndividual =
    abstract member Create: chromosome:Chromosome -> IIndividual
    abstract member Chromosome: Chromosome
    abstract member Fitness: float
    
    