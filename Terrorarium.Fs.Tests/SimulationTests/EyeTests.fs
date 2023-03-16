namespace Terrorarium.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Terrorarium
open MathNet.Spatial.Units
open MathNet.Spatial.Euclidean
open ExtraFunctions
open Eye

[<TestClass>]
type EyeTests () =

    [<TestMethod>]
    member this.``Test Cells are Properly Computed`` () =
        let animalPos = Point2D(0.0,0.0)
        let animalRotation = Vector2D.FromPolar(1.0, Angle.FromDegrees(0.0))
        let testEyeRange = 5.0
        let testEyeFOV = Angle.FromDegrees(90.0)
        let testEyeCells = 3
        let testEye = {Eye.FOVRange = testEyeRange; FOVAngle = testEyeFOV; Cells = testEyeCells}

        let testFoodPolar = [
            // Test in left and right cells
            Vector2D.FromPolar(1.0, testEyeFOV / 4.0)
            Vector2D.FromPolar(1.0, -testEyeFOV / 4.0)

            // Test straight ahead
            Vector2D.FromPolar(1.0, Angle.FromDegrees(0.0))
        ]

        let testFood = 
            testFoodPolar 
            |> Seq.map (fun vec -> {Food.Position = Point2D(vec.X, vec.Y); Food.CollisionRadius = 0.01})
            |> Seq.toArray

        let expectedResult = 0.8
        let processResult = 
            Eye.ProcessVision animalPos animalRotation testFood testEye
            |> Seq.toList
            |> List.forall(fun x -> x = expectedResult)

        Assert.IsTrue(processResult)

    [<TestMethod>]
    member this.``Test Food Out of Range`` () = 
        let animalPos = Point2D(0.0,0.0)
        let animalRotation = Vector2D.FromPolar(1.0, Angle.FromDegrees(0.0))
        let testEyeRange = 1.0
        let testEyeFOV = Angle.FromDegrees(45.0)
        let testEyeCells = 2
        let testEye = {Eye.FOVRange = testEyeRange; FOVAngle = testEyeFOV; Cells = testEyeCells}
        
        let testFoodPolar = [
            // Testing out of range
            Vector2D.FromPolar(testEyeRange * 10.0, testEyeFOV / 4.0)

            // Testing out of FOV
            Vector2D.FromPolar(testEyeRange / 2.0, (testEyeFOV / 1.99))
            Vector2D.FromPolar(testEyeRange / 2.0, -(testEyeFOV / 1.99))
        ]

        let testFood = 
            testFoodPolar 
            |> Seq.map (fun vec -> {Food.Position = Point2D(vec.X, vec.Y); Food.CollisionRadius = 0.01})
            |> Seq.toArray

        let processResult = 
            Eye.ProcessVision animalPos animalRotation testFood testEye
            |> Seq.toList
            |> Seq.forall (fun x -> x = 0.0)

        Assert.IsTrue(processResult)
