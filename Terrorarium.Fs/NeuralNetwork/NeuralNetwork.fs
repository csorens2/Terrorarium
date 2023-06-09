﻿namespace Terrorarium

type NeuralNetwork = {
    Layers: Layer array
}

module NeuralNetwork = 
    let New layers = 
        {NeuralNetwork.Layers = layers}

    let Random layerTopologies = 
        let rec buildLayers (remainLayers: seq<LayerTopology array>) = seq {
                if not (Seq.isEmpty remainLayers) then
                    let nextLayers = Seq.head remainLayers
                    yield Layer.Random nextLayers[0].Neurons nextLayers[1].Neurons
                    yield! buildLayers (Seq.tail remainLayers)
            }
        let randomLayers = 
            buildLayers (Seq.windowed 2 layerTopologies)
            |> Seq.toArray
        {NeuralNetwork.Layers = randomLayers}

    let FromWeights layerTopologies weights = 
        let rec buildLayers (windowedLayers: seq<LayerTopology array>) remainingWeights = seq {
            if not (Seq.isEmpty windowedLayers) then
                let window = Seq.head windowedLayers
                let totalWeights = (1 + window[0].Neurons) * window[1].Neurons
                yield Layer.FromWeights window[0].Neurons (Seq.toArray (Seq.take totalWeights remainingWeights))
                yield! buildLayers
                    (Seq.tail windowedLayers)
                    (Seq.skip totalWeights remainingWeights)
        }
        let windowedLayers = 
            layerTopologies
            |> Seq.windowed 2
        {NeuralNetwork.Layers = Seq.toArray (buildLayers windowedLayers weights)}

    let Propagate neuralNetwork inputs  = 
        neuralNetwork.Layers
        |> Array.fold(fun lastLayerResult nextLayer -> 
            Layer.Propagate lastLayerResult nextLayer) inputs

    let Weights neuralNetwork = 
        neuralNetwork.Layers
        |> Array.collect (fun x -> 
            x.Neurons 
            |> Array.collect (fun y -> Array.append [|y.Bias|] y.Weights)) 
