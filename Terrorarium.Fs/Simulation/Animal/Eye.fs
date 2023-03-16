module Eye

open MathNet.Spatial.Euclidean
open MathNet.Spatial.Units

open ExtraFunctions
open Config
open Food

type Eye = {
    FOVRange: float
    FOVAngle: Angle
    Cells: int
}

let New config = 
    {Eye.FOVRange = config.EyeFOVRange; FOVAngle = config.EyeFOVAngle; Cells = config.EyeCells}

let ProcessVision (position:Point2D) (facing:Vector2D) (foods: Food array) (eye:Eye) = 
    let foodInVision (food:Food) = 
        let foodVector = position.VectorTo(food.Position)
        let foodAngle = facing.SignedAngleTo(foodVector, false, true)
        let eyeHalves = eye.FOVAngle / 2.0
        foodAngle.Degrees >=< (-eyeHalves.Degrees, eyeHalves.Degrees) && foodVector.Length <= eye.FOVRange
    let getCellForFood (food:Food) = 
        let foodVector = position.VectorTo(food.Position)
        let foodAngle = 
            XAxisVector.SignedAngleTo(foodVector, false, true)
        let shiftedFoodAngle = 
            foodAngle - 
            XAxisVector.SignedAngleTo(facing, false, true) + 
            (eye.FOVAngle / 2.0)
        let rangePercentage = 
            (shiftedFoodAngle.Radians / eye.FOVAngle.Radians)
        let cellFloat = rangePercentage * (float eye.Cells)
        // Corner case for food on the exact farthest edge
        let cell = min (int cellFloat) (eye.Cells - 1)
        cell
    let cellEnergy (cellFood: Food array) = 
        cellFood
        |> Array.fold (fun acc next -> acc + ((eye.FOVRange - position.DistanceTo(next.Position)) / eye.FOVRange)) 0.0
    let eyeCellDict = 
        foods
        |> Array.where (fun x -> foodInVision x)
        |> Array.groupBy (fun x -> getCellForFood x)
        |> Array.map (fun (cell,food) -> (cell, cellEnergy food))
        |> Array.fold (fun (acc: Map<int,float>) (cell,energy) -> acc.Add(cell, energy)) Map.empty
    [|0..eye.Cells - 1|]
    |> Array.map (fun cell -> 
        if eyeCellDict.ContainsKey(cell) then
            eyeCellDict[cell]
        else
            0)