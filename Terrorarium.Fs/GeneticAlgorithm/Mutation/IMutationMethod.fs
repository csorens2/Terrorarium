namespace Terrorarium

open Chromosome

type IMutationMethod = 
    abstract member Mutate: child:Chromosome -> Chromosome 

