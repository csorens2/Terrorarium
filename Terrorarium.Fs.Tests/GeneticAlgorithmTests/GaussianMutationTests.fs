namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

[<TestClass>]
type GaussianMutationTests () = 

    [<TestMethod>]
    member this.``Gaussian Mutation throws exception if constructing with invalid chance`` () =
        let exceptionFunc : Func<obj> = Func<obj>(fun () -> (GaussianMutation(1.1, 0.5)))
        let exceptionFunc2 : Func<obj> = Func<obj>(fun () -> (GaussianMutation(-0.1, 0.5)))
        Assert.ThrowsException(exceptionFunc)
        |> ignore
        Assert.ThrowsException(exceptionFunc2)
        |> ignore