module ExtraFunctions

open MathNet.Spatial.Euclidean
open MathNet.Spatial.Units

// Within operator
let inline (>=<) a (b,c) = b <= a && a <= c

let Wrap (num:float) (min:float) (max:float) = 
    (((num - min) % (max - min)) + (max - min)) % (max - min) + min

let Clamp num (left, right) = 
    if num < left then
        left
    elif num > right then
        right
    else
        num

let XAxisVector = Vector2D.FromPolar(1.0, Angle.FromDegrees(0.0))