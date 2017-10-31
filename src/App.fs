module CzechElection

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
module R = Fable.Helpers.React
open R.Props
open Fable.Core
open Fable.Import
module R = Fable.Helpers.React

type HtmlPropExtensions=
        | [<CompiledName("data-target")>] DataTarget of string            
        interface IHTMLProp

type App(props) =
    inherit React.Component<obj, obj>(props)

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
                    R.div [ClassName "tab-pane active"; Id "tab1"][unbox "TODO Výsledky"]
                    R.div [ClassName "tab-pane"; Id "tab2"][unbox "TODO Účast"]
                ]
            ]
        ]

let init() =
     ReactDom.render(
        R.com<App,_,_> [] [],
        Browser.document.getElementById("content")
    )

init()