module CzechElection

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

module R = Fable.Helpers.React

type App(props) =
    inherit React.Component<obj, obj>(props)

    member this.render () =
        R.div [] [unbox "TODO"]

let init() =
     ReactDom.render(
        R.com<App,_,_> [] [],
        Browser.document.getElementById("content")
    )

init()