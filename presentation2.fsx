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

//Units of measure
//Nasa spacecraft crashed in Mars because the computer calcuates orbit mixing meter system and imperial system
[<Measure>] type meters
[<Measure>] type sec
let someMeters = 12.0<meters>
let someSecs = 12.0<sec>

let speed = someMeters / someSecs
let res = speed * someMeters
//let notAllowed = someMeters + 2


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

//Modules and Namespaces
module MathStuff =
    let add x y = x + y

module OtherStuff =
    let otherAdd x = MathStuff.add x 1

//Attaching functions to types
module Person =
    type T = { First: string; Last: string } with 
        member this.FullName =
            this.First + " " + this.Last
    let create first last = 
        { First=first; Last=last }
let person = Person.create "John" "Doe"
let fullName = person.FullName

//Discriminated Unions
type PhoneNumber = PhoneNumber of int * int
type EmailAddress = EmailAddress of string

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

//High Order Functions for Dependency Injection
let PhoneCaller phone = ()
let EmailSender email = ()

// let contact phoneCaller emailSender customer =
//     match customer.ContactMethod with
//     | Telephone phone -> phoneCaller phone
//     | Email mail -> emailSender mail
//     | Postal address -> printfn("sending %s %s") address.Line1 address.Line2

// let Contact = contact PhoneCaller EmailSender
// Contact johnDoe

//Collection APIs - similar to LINQ
[|1..20|] |> Array.sum
[1..20] |> List.find(fun x -> x > 10)

//Measure execution time #time
let rec fibonacci1 n =
    if n <= 2L
        then 1L
        else fibonacci1(n - 1L) + fibonacci1(n - 2L)

let fibonacci2 n =
    let rec aux acc1 acc2 i =
        match i with
        | 0UL -> acc1
        | _ -> aux acc2 (acc1 + acc2) (i-1UL)
    aux 0UL 1UL n

fibonacci2 3000UL

//Asynchronous programming
let asyncLoop = async {
    for i in [1..20] do
        do! Async.Sleep 100
        printfn "%d" i
}
let result123 = Async.RunSynchronously asyncLoop

//F# Agents - MailboxProcessor
let agent = MailboxProcessor.Start(fun inbox ->
    let rec messageLoop() = async{
        let! msg = inbox.Receive()
        msg |> printfn "message received: %s"
        return! messageLoop()
    }
    messageLoop()
)

let feed (agt: MailboxProcessor<string>) = async {
    do! Async.Sleep 500
    agt.Post "Hello"
    do! Async.Sleep 1500
    agt.Post "Hello again"
}

Async.RunSynchronously (feed agent)

//Type providers - Data as Code
