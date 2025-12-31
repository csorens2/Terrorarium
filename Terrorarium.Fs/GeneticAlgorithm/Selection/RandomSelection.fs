namespace Terrorarium

open ExtraFunctions

module RandomSelection = 

    let SelectWithRoll toSelectFrom roll =        
        let totalProbability = Seq.sumBy (fun (_, weight) -> weight) toSelectFrom
        let sumTolerance = 0.001

        if abs (totalProbability - 1.0) > sumTolerance then 
            failwith $"Cannot perform random selection: Total probability is '{totalProbability}' but should be 1.0 with '{sumTolerance}' tolerance"
        else if not (roll >=< (0.0,1.0)) then 
            failwith $"Invalid roll '{roll}'. Must be between 0.0 and 1.0"
        else 
            let rec rec_select remainingChoices remainingRoll = 
                let (nextChoice, nextProbability) = Seq.head remainingChoices
                let nextRoll = remainingRoll - nextProbability
                if nextRoll <= 0.0 then
                    nextChoice
                else
                    rec_select (Seq.tail remainingChoices) nextRoll

            rec_select toSelectFrom roll

    let Select toSelectFrom = 
        SelectWithRoll toSelectFrom (System.Random.Shared.NextDouble())

