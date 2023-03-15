module World

open Config
open Animal
open Food

type World = {
    Animals: seq<Animal>
    Foods: seq<Food>
}

let New config = 
    let animalCount = config.WorldAnimals
    let foodCount = config.WorldFoods
    let animals = 
        Seq.init animalCount (fun _ -> Animal.Random config)
    let foods = 
        Seq.init foodCount (fun _ -> Food.New config)
    {World.Animals = animals; Foods = foods}
        
        
        