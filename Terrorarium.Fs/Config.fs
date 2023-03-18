namespace Terrorarium

open MathNet.Spatial.Units

type Config = {
        BrainNeurons:int

        EyeFOVRange:float
        EyeFOVAngle:Angle
        EyeCells:int

        FoodSize:float
        
        GAReverse:bool
        GAMutChance:float
        GAMutCoeff:float

        SimSpeedMin:float
        SimSpeedMax:float
        SimSpeedAccel:float
        SimRotationAccel:float
        SimGenerationLength:int

        WorldAnimals:int
        WorldFoods:int
    }

module ConfigPresets = 
    let DefaultConfig = {
        BrainNeurons = 9

        EyeFOVRange = 0.25
        EyeFOVAngle = Angle.FromRadians(MathNet.Numerics.Constants.Pi * 1.25)
        EyeCells = 9

        FoodSize = 0.01
        
        GAReverse = false
        GAMutChance = 0.01
        GAMutCoeff = 0.3

        SimSpeedMin = 0.001
        SimSpeedMax = 0.005
        SimSpeedAccel = 0.2
        SimRotationAccel = MathNet.Numerics.Constants.Pi / 2.0
        SimGenerationLength = 2500

        WorldAnimals = 40
        WorldFoods = 60
    }

    let UpgradeConfig = {
        BrainNeurons = 18

        EyeFOVRange = 0.25
        EyeFOVAngle = Angle.FromDegrees(180.0)
        EyeCells = 18

        FoodSize = 0.01
        
        GAReverse = false
        GAMutChance = 0.01
        GAMutCoeff = 0.3

        SimSpeedMin = 0.001
        SimSpeedMax = 0.005
        SimSpeedAccel = 0.2
        SimRotationAccel = MathNet.Numerics.Constants.Pi / 2.0
        SimGenerationLength = 2500

        WorldAnimals = 40
        WorldFoods = 60
    }

