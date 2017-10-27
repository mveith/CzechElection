module CzechElection

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

let init() =
    let contentElement = Browser.document.getElementById("content")
    contentElement.innerText <- "TODO"

init()