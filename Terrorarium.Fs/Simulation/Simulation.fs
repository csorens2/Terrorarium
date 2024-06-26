﻿namespace Terrorarium

open MathNet.Spatial.Euclidean

type Simulation = {
    World: World
    Config: Config
    Age: int
    Generation: int
    Statistics: SimulationStatistics list
}

module Simulator = 
    let ProcessCollisions simulation =
        let rec processCollisions (processedAnimals: Animal list) (unprocessedAnimals:Animal list) uneatenFood totalEaten = 
            if List.isEmpty unprocessedAnimals then
                (processedAnimals, uneatenFood, totalEaten)
            else
                let nextAnimal = List.head unprocessedAnimals
                let foodMap = 
                    uneatenFood
                    |> Array.groupBy (fun (food:Food) -> Line2D(nextAnimal.Position, food.Position).Length <= simulation.Config.FoodSize )
                    |> Array.fold (fun (acc: Map<bool, Food array>) (boolVal, foodSeq) -> acc.Add (boolVal,foodSeq) ) Map.empty
                let amountEaten = 
                    if foodMap.ContainsKey true then
                        Array.length foodMap[true]
                    else
                        0
                let remainingFood = 
                    if foodMap.ContainsKey false then
                        foodMap[false]
                    else
                        Array.empty
                let processedAnimal = 
                    {nextAnimal with Satiation = nextAnimal.Satiation + amountEaten}
                processCollisions
                    (processedAnimal :: processedAnimals)
                    (List.tail unprocessedAnimals)
                    (remainingFood)
                    (totalEaten + amountEaten)
        let world = simulation.World
        let (animals, leftoverFood, eatenFood) = processCollisions (List.empty) (Array.toList world.Animals) (world.Foods) 0
        let newFood = 
            Array.init eatenFood (fun _ -> Food.New simulation.Config)
        {simulation with 
            World = {World.Animals = List.toArray animals; Foods = Array.append leftoverFood newFood}}
    

    let ProcessBrains simulation =
        let world = simulation.World
        let processedAnimals = 
            world.Animals
            |> Array.map(fun x -> Animal.ProcessBrain simulation.Config world.Foods x)
        {simulation with 
            World = {world with Animals = processedAnimals}}

    let ProcessMovements simulation =
        let world = simulation.World
        let processedAnimals = 
            world.Animals
            |> Array.map(fun x -> Animal.ProcessMovement x)
        {simulation with 
            World = {world with Animals = processedAnimals}}

    let Evolve simulation = 
        let world = simulation.World
        let maxSatiation = 
            world.Animals
            |> Array.map (fun animal -> animal.Satiation)
            |> Array.maxBy (fun x -> x)

        let individuals = 
            world.Animals
            |> Array.map (fun animal -> AnimalIndividual.FromAnimal animal)
            |> Array.map (fun individual -> 
                if simulation.Config.GAReverse then
                    {individual with Fitness = (float maxSatiation) - individual.Fitness}
                else
                    individual)

        let ga = {
            GeneticAlgorithm.SelectionMethod = {SelectionMethod.Select = RouletteWheelSelection.Select};
            GeneticAlgorithm.CrossoverMethod = {CrossoverMethod.Crossover = UniformCrossover.Crossover};
            GeneticAlgorithm.MutationMethod = {
                MutationMethod.Mutate = 
                    GaussianMutation.Mutate simulation.Config.GAMutChance simulation.Config.GAMutCoeff}}

        let (evolvedIndividuals, gaStats) = GeneticAlgorithm.Evolve ga individuals

        let newAnimals = 
            evolvedIndividuals
            |> Array.map (fun x -> Animal.FromChromosome simulation.Config x.Chromosome)

        let newFood =     
            Array.init (Seq.length world.Foods) (fun _ -> Food.New simulation.Config)

        let stats = {SimulationStatistics.Generation = simulation.Generation; GAStatistics = gaStats}

        {simulation with 
            World = {World.Animals = newAnimals; Foods = newFood};
            Statistics = stats :: simulation.Statistics;
            Generation = simulation.Generation + 1;
            Age = 0}

    let TryEvolving simulation = 
        if simulation.Age > simulation.Config.SimGenerationLength then
            Evolve simulation
        else
            simulation

    let Step simulation = 
        let stepResult = 
            simulation
            |> ProcessCollisions
            |> ProcessBrains
            |> ProcessMovements
            |> TryEvolving
        {stepResult with Age = stepResult.Age + 1}

    let New config = 
        {Simulation.Config = config; World = World.New config; Age = 0; Generation = 0; Statistics = List.empty}
    