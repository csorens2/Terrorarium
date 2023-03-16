module World

open Config
open Animal
open Food

type World = {
    Animals: Animal array
    Foods: Food array
}

let New config = 
    let animalCount = config.WorldAnimals
    let foodCount = config.WorldFoods
    let animals = 
        Array.init animalCount (fun _ -> Animal.Random config)
    let foods = 
        Array.init foodCount (fun _ -> Food.New config)
    {World.Animals = animals; Foods = foods}
        
        
        