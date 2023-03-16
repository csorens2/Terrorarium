namespace Terrorarium

open MathNet.Spatial.Euclidean
open MathNet.Spatial.Units

open ExtraFunctions

type Eye = {
    FOVRange: float
    FOVAngle: Angle
    Cells: int
}

module Eye = 
    let New config = 
        {Eye.FOVRange = config.EyeFOVRange; FOVAngle = config.EyeFOVAngle; Cells = config.EyeCells}

    let ProcessVision (position:Point2D) (facing:Vector2D) (food: Food array) (eye:Eye) = 
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
            |> Array.fold (fun acc next -> acc + (1.0 - (position.DistanceTo(next.Position) / eye.FOVRange))) 0.0
        let rec getWrappedFood remainingFood = seq {
            if not (List.isEmpty remainingFood) then
                let translationVecs = [
                    Vector2D(1.0,0.0)
                    Vector2D(-1.0,0.0)
                    Vector2D(0.0,1.0)
                    Vector2D(0.0,-1.0)
                    Vector2D(0.0,0.0)
                ]
                let nextFood = List.head remainingFood
                let translatedFood = 
                    translationVecs
                    |> List.map(fun x -> {nextFood with Position = nextFood.Position + x})
                yield! translatedFood
                yield! getWrappedFood (List.tail remainingFood)
        }
        let eyeCellDict = 
            food
            |> Array.toList
            |> getWrappedFood
            |> Seq.toArray
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