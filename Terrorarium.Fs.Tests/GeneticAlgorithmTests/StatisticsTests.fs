namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium

[<TestClass>]
type StatisticsTests () = 
    [<TestMethod>]
    member this.``GAStatistics New Returns Correct Values`` () =
        let testPopulation1 = 
            [
                {TestIndividual.New with Fitness = 6.0}
                {TestIndividual.New with Fitness = 4.0}
                {TestIndividual.New with Fitness = 5.0}
            ]
            |> List.toArray

        let testPopulation2 = 
            [
                {TestIndividual.New with Fitness = 6.0}
                {TestIndividual.New with Fitness = 4.0}
                {TestIndividual.New with Fitness = 7.0}
                {TestIndividual.New with Fitness = 5.0}
            ]
            |> List.toArray
        
        let testData = 
            [
                {| 
                    Min = 4.0
                    Max = 6.0
                    Avg = 5.0
                    Median = 5.0
                    Statistics = GAStatistics.New testPopulation1
                |}
                {|
                    Min = 4.0
                    Max = 7.0
                    Avg = 5.5
                    Median = 5.5
                    Statistics = GAStatistics.New testPopulation2
                |}
            ]

        testData
        |> List.iter (fun data -> 
            Assert.AreEqual(data.Min, data.Statistics.MinFitness)
            Assert.AreEqual(data.Max, data.Statistics.MaxFitness)
            Assert.AreEqual(data.Avg, data.Statistics.AvgFitness)
            Assert.AreEqual(data.Median, data.Statistics.MedianFitness))
