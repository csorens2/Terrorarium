module Layer

open Neuron

type Layer = {
    Neurons: seq<Neuron>
}

let FromWeights inputSize weights = 
    let rec buildNeurons remainingWeights = seq {
        if not (Seq.isEmpty remainingWeights) then
            let neuronWeightCount = inputSize + 1
            yield Neuron.FromWeights inputSize (Seq.take neuronWeightCount remainingWeights)
            yield! buildNeurons (Seq.skip neuronWeightCount remainingWeights)
    }
    {Layer.Neurons = (buildNeurons weights)}

let Random inputNeurons outputNeurons = 
    {Layer.Neurons = Seq.cache (Seq.init outputNeurons (fun _ -> Neuron.Random(inputNeurons)))}

let Propagate inputs layer = 
    layer.Neurons
    |> Seq.map (fun x -> 
        let test = Neuron.Propogate inputs x
        test)
        