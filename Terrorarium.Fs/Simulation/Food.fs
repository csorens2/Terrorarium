namespace Terrorarium

open MathNet.Spatial.Euclidean
open System

type Food = {
    CollisionRadius: float
    Position: Point2D
}

module Food = 
    let New config = {Food.CollisionRadius = config.FoodSize; Position = Point2D(Random.Shared.NextDouble(), Random.Shared.NextDouble())}