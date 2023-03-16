module Layer

open Neuron

type Layer = {
    Neurons: Neuron array
}

let FromWeights inputSize weights = 
    let rec buildNeurons remainingWeights = seq {
        if not (Seq.isEmpty remainingWeights) then
            let neuronWeightCount = inputSize + 1
            yield Neuron.FromWeights inputSize (Seq.toArray (Seq.take neuronWeightCount remainingWeights))
            yield! buildNeurons (Seq.skip neuronWeightCount remainingWeights)
    }
    {Layer.Neurons = Seq.toArray (buildNeurons (Array.toSeq weights))}

let Random inputNeurons outputNeurons = 
    {Layer.Neurons = Array.init outputNeurons (fun _ -> Neuron.Random(inputNeurons))}

let Propagate inputs layer = 
    layer.Neurons
    |> Array.map (fun x -> Neuron.Propogate inputs x)
        