open FSharp.Data
open System

// type MyTypeProvider = HtmlProvider<"/Users/eltonsantana/Downloads/wikipedia/bojack.htm">

// let url = "https://en.wikipedia.org/wiki/BoJack_Horseman"
// let wikipediaInfo = MyTypeProvider.Load(url)

// for row in wikipediaInfo.Tables.``Top scorers``.Rows do
//     printfn "%s %s %s" row.Column1 row.Column2 row.Column3

type WeatherProvider = JsonProvider<"https://samples.openweathermap.org/data/2.5/weather?q=London,uk&appid=439d4b804bc8187953eb36d2a8c26a02">
let url = "https://api.openweathermap.org/data/2.5/weather?appid=7f2fa4b1d1f65cf50d73c206d4b21656&q="

let sf = WeatherProvider.Load(url + "San Francisco")
//sf.Coord

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code