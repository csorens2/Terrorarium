module Food

open MathNet.Spatial.Euclidean
open Config

type Food = {
    CollisionRadius: float
    Position: Point2D
}

let New config = 
    {Food.CollisionRadius = config.FoodSize; Position = Point2D(RNG.NextDouble (), RNG.NextDouble ())}