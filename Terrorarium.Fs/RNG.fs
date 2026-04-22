module RNG

open System

let NextRangeFloat (minValue,maxValue) = minValue + Random.Shared.NextDouble() * (maxValue - minValue)