namespace Terrorarium
    
open System
open System.IO
open System.Text.RegularExpressions

module WeightFile
    let LoadWeightFile filePath = 
        File.