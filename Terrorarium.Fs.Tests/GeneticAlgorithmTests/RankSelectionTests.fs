namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

[<TestClass>]
type RankSelectionTests () = 
    let testIndividuals = 
        [
            {
                TestIndividual.New with Fitness = 5.0
            }
            {
                TestIndividual.New with Fitness = 4.0
            }
            {
                TestIndividual.New with Fitness = 3.0
            }
            {
                TestIndividual.New with Fitness = 2.0
            }
            {
                TestIndividual.New with Fitness = 1.0
            }
        ]
        |> List.toArray

    [<TestMethod>]
    member this.``Rank Selections Throw Exceptions When Given Invalid Selection Pressure`` () =
        let testPressures = 
            [
                0.9
                2.1
            ]

        List.iter (fun testPressure -> Assert.ThrowsException<Exception> (fun () -> RankSelection.Linear testPressure testIndividuals |> ignore) |> ignore) testPressures

    [<TestMethod>]
    member this.``Rank Selections Properly Works`` () =
        let selectionPressure = 2.0

        let test = RankSelection.Linear selectionPressure testIndividuals
        ()