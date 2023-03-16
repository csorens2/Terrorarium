namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium

[<TestClass>]
type RouletteWheelSelectionTests () = 

    [<TestMethod>]
    member this.``Test Roulette Wheel Returns Correct Individual`` () =
        let testIndividuals = 
            [
                TestIndividual(1.0, {Chromosome.Genes = Array.empty})
                TestIndividual(2.0, {Chromosome.Genes = Array.empty})
                TestIndividual(3.0, {Chromosome.Genes = Array.empty})
            ]
            |> Seq.map(fun x -> x :> IIndividual)
            |> Seq.toArray
        
        let actual1 = RouletteWheelSelection.GetIndividual testIndividuals 0.1
        let actual2 = RouletteWheelSelection.GetIndividual testIndividuals 0.4
        let actual3 = RouletteWheelSelection.GetIndividual testIndividuals 0.6
        Assert.AreEqual(testIndividuals[0].Fitness, actual1.Fitness)
        Assert.AreEqual(testIndividuals[1].Fitness, actual2.Fitness)
        Assert.AreEqual(testIndividuals[2].Fitness, actual3.Fitness)