namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium

[<TestClass>]
type RouletteWheelSelectionTests () = 

    [<TestMethod>]
    member this.``Test Roulette Wheel Returns Correct Individual`` () =
        let testIndividuals = 
            [
                {TestIndividual.New with Fitness = 1.0} 
                {TestIndividual.New with Fitness = 2.0} 
                {TestIndividual.New with Fitness = 3.0} 
            ]
            |> Seq.toArray
        
        let actual1 = RouletteWheelSelection.GetIndividual testIndividuals 0.1
        let actual2 = RouletteWheelSelection.GetIndividual testIndividuals 0.4
        let actual3 = RouletteWheelSelection.GetIndividual testIndividuals 0.6
        Assert.AreEqual(testIndividuals[0].Fitness, actual1.Fitness)
        Assert.AreEqual(testIndividuals[1].Fitness, actual2.Fitness)
        Assert.AreEqual(testIndividuals[2].Fitness, actual3.Fitness)