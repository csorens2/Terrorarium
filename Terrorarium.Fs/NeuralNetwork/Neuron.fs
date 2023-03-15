module Neuron

// Possible changes: Performance boost from weights as list if the neuron connections get too big
// Change third constructor

type Neuron = {
    Bias: float
    Weights: seq<float>
    Threshold: float
}

let New bias weights = 
    {Neuron.Bias = bias; Weights = weights; Threshold = 0.0}

let Random outputNeurons = 
    let randomBias = RNG.NextRangeFloat(-1.0,1.0)
    let randomWeights = 
        Seq.init outputNeurons (fun _ -> RNG.NextRangeFloat(-1.0,1.0))
        |> Seq.cache
    New randomBias randomWeights

let FromWeights outputNeurons weights = 
    if outputNeurons <> ((Seq.length weights) - 1) then
            failwith "Insufficient weights given to Neuron"
    New (Seq.head weights) (Seq.tail weights)

let Propogate inputs neuron = 
    if Seq.length inputs <> Seq.length neuron.Weights then
            failwith "Neuron weight count does not match input count"
    let output = 
        inputs
        |> Seq.zip neuron.Weights
        |> Seq.map (fun (next_in,next_weight) -> next_in * next_weight)
        |> Seq.sum
    max (output + neuron.Bias) neuron.Threshold