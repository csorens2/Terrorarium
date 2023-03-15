namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open Chromosome

[<TestClass>]
type RouletteWheelSelectionTests () = 

    [<TestMethod>]
    member this.``Test Roulette Wheel Returns Correct Individual`` () =
        let testIndividuals = 
            [
                TestIndividual(1.0, {Chromosome.Genes = []})
                TestIndividual(2.0, {Chromosome.Genes = []})
                TestIndividual(3.0, {Chromosome.Genes = []})
            ]
            |> Seq.map(fun x -> x :> IIndividual)
            |> Seq.toList
        
        let actual1 = RouletteWheelSelection.GetIndividual testIndividuals 0.1
        let actual2 = RouletteWheelSelection.GetIndividual testIndividuals 0.4
        let actual3 = RouletteWheelSelection.GetIndividual testIndividuals 0.6
        Assert.AreEqual(testIndividuals[0].Fitness, actual1.Fitness)
        Assert.AreEqual(testIndividuals[1].Fitness, actual2.Fitness)
        Assert.AreEqual(testIndividuals[2].Fitness, actual3.Fitness)