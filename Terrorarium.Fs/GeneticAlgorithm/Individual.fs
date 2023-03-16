namespace Terrorarium

type Individual = {
    Create: Chromosome -> Individual
    Chromosome: Chromosome
    Fitness: float
}
    