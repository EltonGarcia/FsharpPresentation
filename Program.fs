open System

//Type inference - A function: domain -> range
let infer x =
    x + 1m

let add a b = a + b
ignore (add "" "")

let printX x = 
    printfn ("%f") x

//Generic type - apostrophe symbol
let toString x y = x.ToString() + y.ToString()
let inline isEqual x y = x = y
type GR<'a> = { Foo: 'a }
let genericInstance = {Foo = 1}

//The Unit type
let print () = printfn "Hello"
do print ()

//Record
type Person = { Id: int; Name: String }
let jhon = { Id=123; Name= "John Doe" }
let notAnAssignment = jhon.Name = "Jane Doe"

//Value base comparison
let jane = { Id=123; Name="John Doe" }
let isJaneJhon = jhon = jane

//Naming values, (apostrophe, double backticks)
let customer'sOrder = ()
let ``is first time customer?`` = true
let ``add gift to order`` = ()
if ``is first time customer?`` then ``add gift to order``

//Options
let safeDivide x y = 
    if y = 0.0
        then None
        else Some (x/y)

let divide = safeDivide 2.0 1.0

//Currying: Haskell Curry
//f(x) = x * 2
let sum a b =
    (+) a b

let explictSum a = 
    let subFunction b =
        a + b
    subFunction

let result = (explictSum 1) 5

let add1 = (+) 1

let isLessThanFive = (>) 5
let r = isLessThanFive 8

let add1ToEach = List.map add1
let printEach = List.iter (printfn ("%d"))

[1..10] 
    |> add1ToEach
    |> List.filter isLessThanFive
    |> printEach

//let sum a b c = a + b + c
//sum 1 2

//Function composition
let f (x: int) = float x * 2.0
let g (x: float) = x >= 4.0

let h x =
    let y = f x
    g y

let compose x = g(f(x))
let comp = f >> g

//Anonyous functions - Lambdas
let lambda = fun x y -> x + y
[1..10] |> List.map (fun x -> x * 2) |> ignore
[1..10] |> List.filter (fun x -> x % 2 = 0) |> ignore

//Pattern matching
type Name = { First: string; Last: string }
let john = { First="John"; Last="Doe" }

let f1 name =
    let { First=f; Last=l } = name
    printfn "first=%s; last:%s" f l

let f2 { First=f; Last=l } = 
    printfn "first=%s; last:%s" f l

f1 john

let (a, b) = (1, 2)
let y = sprintf "%d" a

let list = [1..10]
let x::xs = list
printfn("%A") xs

let patternMatching list =
    match list with
    | x::xs -> x
    | x -> 1
    | x::y::xs -> x + y

//Modules and Namespaces
//namespace Utilities
// module MathStuff =
//     let add x y = x + y

// module OtherStuff =
//     let otherAdd x = MathStuff.add x 1

// //Attaching functions to types
// module Person =
//     type T = { First: string; Last: string } with 
//         member this.FullName =
//             this.First + " " + this.Last
//     let create first last = 
//         { First=first; Last=last }
// let person = Person.create "John" "Doe"
// let fullName = person.FullName

//Discriminated Unions
type ContactMethod =
    | Telephone of int * int
    | Email of string
    | Postal of {| Line1: string; Line2: string; PostalCode: string; |}

type Customer = { Name: string; ContactMethod: ContactMethod }
let johnDoe = { Name= "John Doe"; ContactMethod = Telephone (55, 12324) }

let contact customer =
   match customer.ContactMethod with
   | Telephone (country, number) -> printfn("calling %d %d") country number
   | Email mail -> printfn("emailing %s") mail
   | Postal address -> printfn("sending %s %s") address.Line1 address.Line2


// Designing Behaviour Befor Data
    // To open a checking account, a customer provides
    // an application,
    // two forms of identification,
    // and an initial deposit.
    // The deposit must be at least $500.00.


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code