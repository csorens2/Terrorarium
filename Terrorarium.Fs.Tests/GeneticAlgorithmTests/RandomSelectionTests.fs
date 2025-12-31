namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

[<TestClass>]
type RandomSelectionTests () = 
    
    [<TestMethod>]
    member this.``Random Selection Properly Selects`` () =
        let testChoicesWithWeights = 
            [
                ('a', 0.2)
                ('b', 0.3)
                ('c', 0.5)
            ]
        let testData = 
            [
                ('a', 0.1)
                ('b', 0.35)
                ('c', 0.75)
            ]

        List.iter (fun (expected, roll) -> Assert.AreEqual(expected, RandomSelection.SelectWithRoll testChoicesWithWeights roll)) testData

    [<TestMethod>]
    member this.``Random Selection Throws Exception When Given Invalid Weight Collection`` () =
        let testLists = 
            [
                [
                    (obj(), 0.5)
                    (obj(), 0.5)
                    (obj(), 0.5)
                ]
                [
                    (obj(), 0.3)
                    (obj(), 0.3)
                    (obj(), 0.3)
                ]

            ]
        
        List.iter (fun toTest -> Assert.ThrowsException<Exception> (fun () -> RandomSelection.Select toTest) |> ignore) testLists

    [<TestMethod>]
    member this.``Random Selection Throws Exception When Given Invalid Roll`` () =
        let testChoicesWithWeights = 
            [
                (obj(), 0.2)
                (obj(), 0.3)
                (obj(), 0.5)
            ]
        
        let testRolls = 
            [
                1.01
                -0.01
            ]
            
        List.iter (fun testRoll -> Assert.ThrowsException<Exception> (fun () -> RandomSelection.SelectWithRoll testChoicesWithWeights testRoll) |> ignore) testRolls
