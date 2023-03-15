namespace Terrorarium

type ISelectionMethod = 
    abstract member Select: seq<IIndividual> -> IIndividual