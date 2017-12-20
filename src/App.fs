module CzechElection

open Fable.Import
module R = Fable.Helpers.React
open R.Props
open Fable.Import.Chartjs
open Fable.Core
open Data
module R = Fable.Helpers.React

type HtmlPropExtensions=
        | [<CompiledName("data-target")>] DataTarget of string            
        interface IHTMLProp

[<Pojo>]
type AppProps = 
    {
        Parties : PartyValue list
    }

type App(props) =
    inherit React.Component<AppProps, obj>(props)

    let viewParties (parties:PartyValue list) =
        let visibleParties = parties |> List.filter (fun p -> p.Value >= 5.0) |> List.sortByDescending(fun p -> p.Value)
        let others = 
            { 
                Name = "Ostatní"
                Value = 
                    let value = parties |> Seq.except visibleParties |> Seq.sumBy (fun p -> p.Value)
                    System.Math.Round(value, 2)
                ColorCode = "#6f6f6f"
            }
        visibleParties @ [ others ]


    let renderChart title (values : PartyValue[]) =
        let chartInfo = 
            { 
                Options = Some { Scales = Some { XAxes = [||]; YAxes = [| { Ticks = { BeginAtZero = true }} |]}; Title = Some title; Legend = None }
                CanvasId = "my-chart"; 
                Data = 
                    Bar
                        {
                            Labels =  values |> Array.map (fun v -> v.Name)
                            Datasets = 
                                [| 
                                    { 
                                        Label  = None
                                        Data = values |> Array.map (fun v ->  v.Value :> obj)
                                        BackgroundColor =Some (values |> Array.map (fun v -> Hex v.ColorCode))
                                        BorderColor = Some (values |> Array.map (fun v -> Hex v.ColorCode))
                                        BorderWidth = Some 1
                                    }
                                |]
                        }                                 
            }
        renderChart chartInfo

    member x.componentDidMount()= 
        viewParties props.Parties |> Seq.toArray |> renderChart "Výsledky"

    member this.render () =
        R.div [] [
            R.nav [ ClassName "navbar navbar-toggleable-md navbar-inverse bg-inverse fixed-top"][
                R.button [ClassName "navbar-toggler navbar-toggler-right";  DataToggle "collapse"; DataTarget "#navbar-collapse"][                    
                    R.span [ClassName "navbar-toggler-icon"][]
                ]
                
                R.a [ClassName "navbar-brand"; Href "#";][ unbox "Volby 2017"]
                
                R.div [ClassName "navbar-collapse collapse"; Id "navbar-collapse"][
                    R.ul [ClassName "nav navbar-nav"][
                        R.li [ClassName "nav-item active"][
                            R.a [ClassName "nav-link"; Href "#tab1"; DataToggle "tab"; ][ unbox "Výsledky"]
                        ]
                        R.li [ClassName "nav-item "][
                            R.a [ClassName "nav-link"; Href "#tab2"; DataToggle "tab"; ][ unbox "Účast"]
                        ]
                    ]
                ]
            ]
            R.div [ClassName "container"][
                R.div [ClassName "tab-content" ][
                    R.div [ClassName "tab-pane active"; Id "tab1"]
                        [
                            R.canvas [Id "my-chart"] []
                        ]
                    R.div [ClassName "tab-pane"; Id "tab2"][unbox "TODO Účast"]
                ]
            ]
        ]

let init() =
     ReactDom.render(
        R.com<App,_,_> { Parties = data} [],
        Browser.document.getElementById("content"))

init()