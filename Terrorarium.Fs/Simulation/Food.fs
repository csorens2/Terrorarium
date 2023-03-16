namespace Terrorarium

open MathNet.Spatial.Euclidean

type Food = {
    CollisionRadius: float
    Position: Point2D
}

module Food = 
    let New config = 
        {Food.CollisionRadius = config.FoodSize; Position = Point2D(RNG.NextDouble (), RNG.NextDouble ())}