﻿namespace Terrorarium

open MathNet.Spatial.Euclidean
open MathNet.Spatial.Units

type Animal = {
    Position: Point2D
    Rotation: Vector2D
    Vision: float array
    Speed: float
    Eye: Eye
    Brain: Brain
    Satiation: int
}

module Animal = 
    let New config brain = {
        Animal.Position = Point2D(RNG.NextDouble (), RNG.NextDouble ())
        Rotation = Vector2D.FromPolar(1.0, Angle.FromDegrees(RNG.NextRangeFloat(-180.0, 180.0)))
        Vision = Array.init config.EyeCells (fun _ -> 0.0)
        Speed = config.SimSpeedMax
        Eye = Eye.New config
        Brain = brain
        Satiation = 0
    }    

    let Random config =
        let brain = Brain.Random config
        New config brain

    let FromChromosome config chromosome = 
        let brain = Brain.FromChromosome config chromosome
        New config brain

    let AsChromosome animal = 
        Brain.AsChromosome animal.Brain

    let ProcessBrain config foods animal = 
        let nextVision = Eye.ProcessVision animal.Position animal.Rotation foods animal.Eye
        let (speed,rotation) = Brain.Propagate nextVision animal.Brain
        let nextSpeed = ExtraFunctions.Clamp (animal.Speed + speed) (config.SimSpeedMin, config.SimSpeedMax)
        let nextRotation = animal.Rotation.Rotate(Angle.FromRadians(rotation))
        {animal with Vision = nextVision; Speed = nextSpeed; Rotation = nextRotation }

    let ProcessMovement animal = 
        let nextPos = animal.Position + animal.Rotation * animal.Speed
        let nextPoint = 
            Point2D(
                (ExtraFunctions.Wrap nextPos.X 0.0 1.0),
                (ExtraFunctions.Wrap nextPos.Y 0.0 1.0))
        {animal with Position = nextPoint}