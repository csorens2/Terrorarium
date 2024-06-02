namespace Terrorarium
    
open System
open System.IO
open System.Text.RegularExpressions


type WeightFile = {
    Topologies: List<int>
    Weights: List<List<int>>
}

module WeightFile = 
    0