namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open ExtraFunctions
open System

[<TestClass>]
type ExtraFunctionTests () = 
    
    [<TestMethod>]
    member this.``Wrap throws when given invalid min and max`` () =
        let min = 1.0
        let max = 0.0
        let exceptionFunc : Func<obj> = Func<obj>(fun () -> Wrap 0.0 min max)
        Assert.ThrowsException(exceptionFunc)
        |> ignore

    [<TestMethod>]
    member this.``Wrap properly wraps around when it should`` () = 
        let min = 0.0
        let max = 1.0
        Assert.AreEqual(0.75, Wrap -0.25 min max)
        Assert.AreEqual(0.25, Wrap 1.25 min max)

    [<TestMethod>]
    member this.``Wrap keeps in bounds when no wrapping needed`` () = 
        let min = 0.0
        let max = 1.0
        Assert.AreEqual(0.0, Wrap 0.0 min max)
        Assert.AreEqual(0.5, Wrap 0.5 min max)
        Assert.AreEqual(0.0, Wrap 1.0 min max)


    [<TestMethod>]
    member this.``Clamp properly clamps number`` () = 
        let left = 0.0
        let right = 1.0
        let mid = (left + right) / 2.0
        Assert.AreEqual(0.0, Clamp -0.5 (left, right))
        Assert.AreEqual(1.0, Clamp 1.5 (left, right))
        Assert.AreEqual(mid, Clamp mid (left, right))