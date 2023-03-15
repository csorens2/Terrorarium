module RNG

let private random = System.Random()

let Next () = random.Next()
let NextMax maxValue = random.Next(maxValue)
let NextRangeInt (minValue,maxValue) = random.Next(minValue,maxValue)
let NextRangeFloat (minValue,maxValue) = (random.NextDouble() * (maxValue - minValue) + minValue)
let NextDouble () = random.NextDouble()
let NextBool () = 
    if random.Next(0,1) = 1 then
        true
    else
        false
let InfiniteBools () = Seq.initInfinite (fun _ -> NextBool ())

