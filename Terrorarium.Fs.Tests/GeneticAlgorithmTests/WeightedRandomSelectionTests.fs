namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

[<TestClass>]
type RandomSelectionTests () = 
    
    [<TestMethod>]
    member this.``Select Properly Selects`` () = 
        let testChoicesWithWeights = 
            [
                ('a', 0.2)
                ('b', 0.3)
                ('c', 0.5)
            ]
        let testData = 
            [
                ('a', 0.0)
                ('a', 0.1)
                ('a', 0.2)
                ('b', 0.35)
                ('b', 0.5)
                ('c', 0.75)
                ('c', 1.0)
            ]

        List.iter (fun (expected, roll) -> Assert.AreEqual(expected, WeightedRandomSelection.SelectWithRoll testChoicesWithWeights roll)) testData