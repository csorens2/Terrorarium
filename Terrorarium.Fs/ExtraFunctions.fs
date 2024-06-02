module ExtraFunctions

open MathNet.Spatial.Euclidean
open MathNet.Spatial.Units

// Within operator
let inline (>=<) a (b,c) = b <= a && a <= c

let Wrap (num:float) (min:float) (max:float) = 
    
    if min >= max then 
        failwith "Failed attempting to wrap with %f as min, and %f as max" min max
    else
        let leftShift = num - min
        let range = max - min
        ((leftShift % range) + range) % range + min

let Clamp num (left, right) = 
    if num < left then
        left
    elif num > right then
        right
    else
        num

let XAxisVector = Vector2D.FromPolar(1.0, Angle.FromDegrees(0.0))