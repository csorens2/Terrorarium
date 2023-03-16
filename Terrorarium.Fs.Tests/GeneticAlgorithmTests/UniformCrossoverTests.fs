namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

[<TestClass>]
type UniformCrossoverTests () = 

    [<TestMethod>]
    member this.``Gene Crossover throws exception if parents are not the same length`` () =
        let parent_a = {Chromosome.Genes = [|1.0;1.0|]}
        let parent_b = {Chromosome.Genes = [|0.0|]}
        let crossover_choices = [true;true;false;false;true]

        let exceptionFunc : Func<obj> = Func<obj>(fun () -> (UniformCrossover.PerformCrossover parent_a parent_b crossover_choices)) 
        Assert.ThrowsException(exceptionFunc)
        |> ignore

    [<TestMethod>]
    member this.``Test Successful Gene Crossover`` () =
        let parent_a = {Chromosome.Genes = [|1.0;1.0;1.0;1.0;1.0|]} 
        let parent_b = {Chromosome.Genes = [|0.0;0.0;0.0;0.0;0.0|]}   
        let crossover_choices = [true;true;false;false;true]
        let expected_child = {Chromosome.Genes = [|1.0;1.0;0.0;0.0;1.0|]}

        let actual = UniformCrossover.PerformCrossover parent_a parent_b crossover_choices
        
        let chromeMatch = 
            expected_child.Genes
            |> Seq.zip actual.Genes
            |> Seq.map (fun (actual, expected) -> actual = expected)
            |> Seq.fold (fun acc next -> acc && next) true
        Assert.IsTrue(chromeMatch)