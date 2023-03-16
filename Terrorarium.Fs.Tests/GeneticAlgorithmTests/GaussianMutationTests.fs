namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

[<TestClass>]
type GaussianMutationTests () = 

    [<TestMethod>]
    member this.``Gaussian Mutation throws exception if using invalid chance`` () =
        let testChromosome = {Chromosome.Genes = Array.empty}
        let exceptionFunc : Func<obj> = Func<obj>(fun () -> GaussianMutation.Mutate 1.1 0.5 testChromosome)
        let exceptionFunc2 : Func<obj> = Func<obj>(fun () -> GaussianMutation.Mutate -0.1 0.5 testChromosome)
        Assert.ThrowsException(exceptionFunc)
        |> ignore
        Assert.ThrowsException(exceptionFunc2)
        |> ignore