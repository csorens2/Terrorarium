namespace Terrorarium

open MathNet.Spatial.Euclidean

open Config
open Animal
open Food
open World

type Simulation (config:Config, world:World, age:int, generation:int) = 

    member this.Config = config
    member this.World = world
    member this.Age = age
    member this.Generation = generation

    new (config:Config) =
        Simulation(config, World.New config, 0, 0)

    new () = 
        Simulation(Config.DefaultConfig)

    member this.Step() = 
        let ((nextWorld: World), (evolveStats: SimulationStatistics option)) =
            this.ProcessCollisions this.World
            |> this.ProcessBrains
            |> this.ProcessMovements
            |> this.TryEvolving

        if evolveStats.IsSome then
            (Simulation(this.Config, nextWorld, 0, this.Generation + 1), evolveStats)
        else
            (Simulation(this.Config, nextWorld, this.Age + 1, this.Generation), evolveStats)

    member this.ProcessCollisions world = 
        let rec processCollisions (processedAnimals:seq<Animal>) (unprocessedAnimals:seq<Animal>) uneatenFood totalEaten = 
            if Seq.isEmpty unprocessedAnimals then
                (processedAnimals, uneatenFood, totalEaten)
            else
                let nextAnimal = Seq.head unprocessedAnimals
                let foodMap = 
                    uneatenFood
                    |> Seq.groupBy (fun (food:Food) -> Line2D(nextAnimal.Position, food.Position).Length <= config.FoodSize )
                    |> Seq.fold (fun (acc: Map<bool, seq<Food>>) (boolVal, foodSeq) -> acc.Add (boolVal,foodSeq) ) Map.empty
                let amountEaten = 
                    if foodMap.ContainsKey true then
                        Seq.length foodMap[true]
                    else
                        0
                let remainingFood = 
                    if foodMap.ContainsKey false then
                        foodMap[false]
                    else
                        Seq.empty
                let processedAnimal = 
                    {nextAnimal with Satiation = nextAnimal.Satiation + amountEaten}
                processCollisions
                    (Seq.append processedAnimals [processedAnimal])
                    (Seq.tail unprocessedAnimals)
                    (remainingFood)
                    (totalEaten + amountEaten)
        let (animals, leftoverFood, eatenFood) = processCollisions (Seq.empty) world.Animals world.Foods 0
        let newFood = 
            Seq.init eatenFood (fun _ -> Food.New config)
        {world with Animals = animals; Foods = Seq.append leftoverFood newFood}

    member this.ProcessBrains world = 
        let processedAnimals = 
            world.Animals
            |> Seq.map(fun x -> Animal.ProcessBrain this.Config world.Foods x)
        {world with Animals = processedAnimals}
    
    member this.ProcessMovements world = 
        let processedAnimals = 
            world.Animals
            |> Seq.map(fun x -> Animal.ProcessMovement x)
        {world with Animals = processedAnimals}

    member this.TryEvolving world = 
        if this.Age > this.Config.SimGenerationLength then
            this.Evolve world
        else
            (world, None)

    member this.Evolve world = 
        let maxSatiation = 
            world.Animals
            |> Seq.map (fun animal -> animal.Satiation)
            |> Seq.maxBy (fun x -> x)

        let individuals = 
            world.Animals
            |> Seq.map (fun animal -> AnimalIndividual(animal))
            |> Seq.map (fun individual -> 
                if this.Config.GAReverse = 1 then
                    AnimalIndividual((float maxSatiation) - individual.Fitness, individual.Chromosome)
                else
                    individual)
            |> Seq.map (fun x -> x :> IIndividual)

        let ga = GeneticAlgorithm(
                    RouletteWheelSelection(), 
                    UniformCrossover(), 
                    GaussianMutation(this.Config.GAMutChance, this.Config.GAMutCoeff))

        let (evolvedIndividuals, statistics) = ga.Evolve individuals

        let newIndividuals = 
            evolvedIndividuals
            |> Seq.map (fun x -> Animal.FromChromosome this.Config x.Chromosome)

        let newFood =     
            Seq.init (Seq.length world.Foods) (fun _ -> Food.New this.Config)

        let stats = {SimulationStatistics.Generation = this.Generation; GAStatistics = statistics}

        ({world with Animals = newIndividuals; Foods = newFood}, Some(stats))



        
        

    