module ChartProviders

open Fable.Import.Chartjs
open Data

let partiesChart title (values : PartyValue[]) =
    let dataset = 
        { 
            BarDataset.Label  = None
            Data = values |> Array.map (fun v ->  v.Value :> obj)
            BackgroundColor =Some (values |> Array.map (fun v -> Hex v.ColorCode))
            BorderColor = Some (values |> Array.map (fun v -> Hex v.ColorCode))
            BorderWidth = Some 1
        }
    { 
        Options = Some { Scales = Some { XAxes = [||]; YAxes = [| { Ticks = { BeginAtZero = true }} |]}; Title = Some title; Legend = None }
        CanvasId = "my-chart"; 
        Data = 
            Bar
                {
                    Labels =  values |> Array.map (fun v -> v.Name)
                    Datasets = [| dataset |]
                }                                 
    }

let regionsChart title (values : RegionValue[]) =
    let chartInfo = 
        { 
            Options = Some { Scales = Some { XAxes = [||]; YAxes = [| { Ticks = { BeginAtZero = true }} |]}; Title = Some title; Legend = None }
            CanvasId = "regions-chart"; 
            Data = 
                Bar
                    {
                        Labels =  values |> Array.map (fun v -> v.Name)
                        Datasets = 
                            [| 
                                { 
                                    Label  = None
                                    Data = values |> Array.map (fun v ->  v.Value :> obj)
                                    BackgroundColor =None
                                    BorderColor = None
                                    BorderWidth = Some 1
                                }
                            |]
                    }                                 
        }
    chartInfo