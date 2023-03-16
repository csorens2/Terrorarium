namespace Terrorarium

type IMutationMethod = 
    abstract member Mutate: child:Chromosome -> Chromosome 

