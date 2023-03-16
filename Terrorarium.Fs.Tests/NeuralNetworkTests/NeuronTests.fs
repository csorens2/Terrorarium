namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open System

open Neuron

[<TestClass>]
type NeuronTests () =

    [<TestMethod>]
    member this.``Neuron propogate throws exception if input length does not match weights`` () = 
        let testList = [|1.0|]
        let testNeuron = {Neuron.Bias = 0; Weights = testList; Threshold = 0.0}
        let exceptionFunc : Func<obj> = Func<obj>(fun () -> Neuron.Propogate [|2.0;3.0|] testNeuron) 
        Assert.ThrowsException(exceptionFunc)
        |> ignore

    [<TestMethod>]
    member this.``Neuron Propogate Returns Expected Output When Threshold Reached`` () =
        let testList = [|1.0|]
        let testNeuron = {Neuron.Bias = 0; Weights = testList; Threshold = 0.0}
        let output = Neuron.Propogate testList testNeuron
        Assert.AreEqual(1.0, output)

    [<TestMethod>]
    member this.``Neuron Propogate Returns Threshold If Threshold Not Reached`` () = 
        let testList = [|1.0|]
        let testThreshold = 10.0
        let testNeuron = {Neuron.Bias = 0; Weights = testList; Threshold = testThreshold}
        let output = Neuron.Propogate testList testNeuron
        Assert.AreEqual(testThreshold, output)
