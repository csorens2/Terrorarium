namespace Terrorarium

type RouletteWheelSelection() =
    interface ISelectionMethod with
        member this.Select individuals = 
            RouletteWheelSelection.GetIndividual individuals (RNG.NextDouble ())

    static member GetIndividual (individuals: IIndividual array) (roll:float) = 
        let rec rec_getIndividual (remainingIndividuals: seq<float * IIndividual>) remainingRoll =
            let (individSlice, nextIndivid) = Seq.head remainingIndividuals
            let nextRoll = remainingRoll - individSlice
            if nextRoll <= 0 then
                nextIndivid
            else
                rec_getIndividual (Seq.tail remainingIndividuals) nextRoll
        let totalFitness = 
            individuals
            |> Array.sumBy (fun x -> x.Fitness)
        let individualSlices = 
            individuals
            |> Seq.map (fun x -> x.Fitness / totalFitness, x)
        rec_getIndividual individualSlices roll