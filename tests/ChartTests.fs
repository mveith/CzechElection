module ChartTests

open Expecto
open CzechElection
open Data
open ChartProviders
open Fable.Import.Chartjs

let partiesChartDataset parties = 
      let chart  = partiesChart "" parties
      match chart.Data with
      | Bar data -> Some (data.Datasets |> Seq.head)
      | _ -> None

[<Tests>]
let tests =
  testList "Parties Chart" [
    testCase "For empty parties create empty bar char" <| fun _ ->
      match partiesChartDataset [||] with
      | Some value -> Expect.isEmpty value.Data ""
      | None -> failtest "Expected bar chart"

    testCase "Data from party values" <| fun _ ->
      let parties = 
        [|
          { Name = "A"; Value = 123.0; ColorCode = "" }
          { Name = "B"; Value = 321.0; ColorCode = "" }
        |]
      match partiesChartDataset parties with
      | Some value -> Expect.sequenceEqual [ 123.0 :> obj; 321.0 :> obj] value.Data ""
      | None -> failtest "Expected bar chart"
  ]
