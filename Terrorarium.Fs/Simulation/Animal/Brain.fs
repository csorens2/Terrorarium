namespace Terrorarium

type Brain = {
    SpeedAccel: float
    RotationAccel: float
    NeuralNetwork: NeuralNetwork
}

module Brain = 
    let Topology config = 
        [
            {LayerTopology.Neurons = config.EyeCells}
            {LayerTopology.Neurons = 2 * config.BrainNeurons}
            {LayerTopology.Neurons = 2}
        ]

    let New config nn = 
        {Brain.SpeedAccel = config.SimSpeedAccel; RotationAccel = config.SimRotationAccel; NeuralNetwork = nn}

    let Random config = 
        let nn = NeuralNetwork.Random (Topology config)
        New config nn

    let FromChromosome config chromosome = 
        let nn = NeuralNetwork.FromWeights (Topology config) chromosome.Genes
        New config nn

    let AsChromosome (brain:Brain) = 
        {Chromosome.Genes = NeuralNetwork.Weights brain.NeuralNetwork}

    let Propagate vision brain = 
        let response = 
            NeuralNetwork.Propagate brain.NeuralNetwork vision
            |> Seq.toList
        let r0 = (ExtraFunctions.Clamp response[0] (0.0, 1.0)) - 0.5
        let r1 = (ExtraFunctions.Clamp response[1] (0.0, 1.0)) - 0.5
        let speed = ExtraFunctions.Clamp (r0 + r1) (-brain.SpeedAccel, brain.SpeedAccel)
        let rotation = ExtraFunctions.Clamp (r0 - r1) (-brain.RotationAccel, brain.RotationAccel)
        (speed, rotation)