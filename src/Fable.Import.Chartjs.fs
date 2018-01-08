module Fable.Import.Chartjs

open Fable.Core
open Fable.Core.JsInterop

type Color=
    | Hex of string
    | RGBA of int * int * int * int

type ChartTicks =
    { BeginAtZero : bool }

type ChartAxis =
    { Ticks : ChartTicks }

type ChartScales =
    { 
        YAxes : ChartAxis array 
        XAxes : ChartAxis array 
    } 

type LegendPosition=
    | Top
    | Left
    | Bottom
    | Right

type ChartLegend=
    { 
        Position : LegendPosition option
        FullWidth : bool option
    }

type ChartOptions =
    { 
        Scales : ChartScales option
        Title : string option
        Legend : ChartLegend option
    }

type GenericDataset =
    { 
        Label :string option
        Data : seq<obj> 
    }

type CubicInterpolationMode =
    | Default
    | Monotone    

type LineDataset =
    { 
        Label :string option
        Data : seq<obj> 
        Fill : bool
        InterpolationMode : CubicInterpolationMode
    }
    
type BarDataset=
    {
        Label :string option
        Data : seq<obj> 
        BackgroundColor : Color array option
        BorderColor : Color array option
        BorderWidth : int option
    }

type PieDataset=
    {
        Label :string option
        Data : seq<obj> 
        BackgroundColor : Color array option
        BorderColor : Color array option
        BorderWidth : int option
    }

type ChartDataWrapper<'a> =
    { 
        Labels : string[]
        Datasets : 'a array
    }

type ChartData =
    | Bar of ChartDataWrapper<BarDataset>
    | HorizontalBar of ChartDataWrapper<BarDataset>
    | Line of ChartDataWrapper<LineDataset>
    | Radar of ChartDataWrapper<GenericDataset>
    | PolarArea of ChartDataWrapper<GenericDataset>
    | Pie of ChartDataWrapper<PieDataset>
    | Doughnut of ChartDataWrapper<GenericDataset>
    | Bubble of ChartDataWrapper<GenericDataset>

type ChartInfo =
    { 
        Options : ChartOptions option
        Data : ChartData
        CanvasId : string 
    }
 
let private append (fields: (string*obj) list) key o map =
    match o with
    | Some v -> 
        (key ==> (map v)) :: fields
    | None -> fields
   
let private createGenericDataset (dataset:GenericDataset)=
    createObj [
        "label" ==> dataset.Label
        "data"==> (dataset.Data  |> Seq.toArray) ]
   
let private cubicinterpolationMode = function
    | Default -> "default"
    | Monotone -> "monotone"
   
let private createLineDataset (dataset:LineDataset)=
    createObj [
        "label" ==> dataset.Label
        "data"==> (dataset.Data  |> Seq.toArray)
        "fill" ==> dataset.Fill
        "cubicInterpolationMode" ==> cubicinterpolationMode dataset.InterpolationMode ]
   
let colorString color =
    match color with
    | RGBA (r, g, b, a) -> sprintf "rgba(%i,%i,%i,%i)" r g b a
    | Hex v -> v

let colorArray =
    Seq.map colorString >> Seq.toArray
let private createBarDataset (dataset:BarDataset)=
    let fields = [
        "label" ==> dataset.Label
        "data"==> (dataset.Data  |> Seq.toArray) ]
    let fields = append fields "backgroundColor" dataset.BackgroundColor colorArray
    let fields = append fields "borderColor" dataset.BorderColor colorArray
    let fields = append fields "borderWidth" dataset.BorderWidth id
    createObj fields
    
let private createPieDataset (dataset:PieDataset)=
    let fields = [
        "label" ==> dataset.Label
        "data"==> (dataset.Data  |> Seq.toArray) ]
    let fields = append fields "backgroundColor" dataset.BackgroundColor colorArray
    let fields = append fields "borderColor" dataset.BorderColor colorArray
    let fields = append fields "borderWidth" dataset.BorderWidth id
    createObj fields
   
let private chartDataFields data createDataset=    
        [
            "labels" ==> data.Labels
            "datasets"==> (data.Datasets |> Seq.map createDataset |> Seq.toArray)
        ]

let private createData data =
    let dataObjectFields = 
        match data with
        | Line data -> chartDataFields data createLineDataset
        | Bar data 
        | HorizontalBar data  -> chartDataFields data createBarDataset
        | Pie data -> chartDataFields data createPieDataset
        | Radar data
        | PolarArea data
        | Doughnut data
        | Bubble data -> chartDataFields data createGenericDataset
    createObj dataObjectFields
        
let private createAxis a = 
    createObj [
        "ticks" ==> 
            createObj [
                "beginAtZero" ==> a.Ticks.BeginAtZero]]

let private createScales scales=
    createObj [
        "yAxes" ==> (scales.YAxes |> Seq.map createAxis |> Seq.toArray)
        "xAxes" ==> (scales.XAxes |> Seq.map createAxis |> Seq.toArray)]

let private createLegendPosition = function
    | Top -> "top"
    | Left -> "left"
    | Bottom -> "bottom"
    | Right -> "right"

let private createLegend legend=
    match legend with
    | Some legend -> 
        let fields = ["display" ==> true]
        let fields = append fields "fullWidth" legend.FullWidth id
        let fields = append fields "position" legend.Position createLegendPosition
        createObj fields
    | None -> createObj ["display" ==> false]

let private createTitle title=
    match title with
    | Some title -> createObj ["display" ==> true; "text" ==> title]
    | None -> createObj ["display" ==> false]

let private createOptions o =
    let fields =  []
    let fields = append fields "scales" o.Scales createScales
    let fields = ("legend"==> createLegend o.Legend) :: fields
    let fields = ("title" ==> createTitle o.Title) :: fields

    createObj fields

let private convertType = function
    | Line _-> "line"
    | Bar _-> "bar"
    | HorizontalBar _ -> "horizontalBar"
    | Radar _ -> "radar"
    | PolarArea _ -> "polarArea"
    | Pie _ -> "pie"
    | Doughnut _ -> "doughnut"
    | Bubble _ -> "bubble"

[<Global>]
type private Chart(ctx:Browser.HTMLElement, settings:obj) =
    member x.destroy () = jsNative

type RenderedChart =
    {
        Destroy : unit -> unit
    }

let renderChart chart=
    let ctx = Browser.document.getElementById chart.CanvasId

    let settings = 
        createObj [
            "type" ==> convertType chart.Data
            "data"==> createData chart.Data 
            "options" ==>  (chart.Options |> Option.map createOptions) ]
    let chart = new Chart(ctx, settings)
    { Destroy = chart.destroy }