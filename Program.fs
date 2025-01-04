printfn "Running webscraper"

open System
open OpenQA.Selenium
open OpenQA.Selenium.Chrome
open OpenQA.Selenium.Support

// Create driver

let mutable options = new ChromeOptions()

options.AddArguments([
    "--verbose";
    "--headless";
    "--disable-dev-shm-usage"
])

let driver = new ChromeDriver(options)

//  Gather input from user about stock and exchange

printf "Enter a stock name (e.g., AAPL for Apple Inc.): "            
let stockName: string = Console.ReadLine()  
printf "Enter a stock exchange (e.g., NASDAQ for The Nasdaq Stock Market): "
let exchangeName : string = Console.ReadLine()
printf "Enter keyword for data range; '1D' (1 day), '5D' (5 days), '1M' (1 month), '6M' (6 months), 'YTD' (Year to date), '1Y' (1 year), '5Y' (5 years), MAX (Since company was established): "  
let range : string = Console.ReadLine()

// Navigate to webpage

let urlForUse : string = "https://www.google.com/finance/quote/" + stockName + ":" + exchangeName + "?window=" + range
driver.Navigate().GoToUrl(urlForUse)             
printfn "Stock: %A" driver.Title

// Get all text

let bodyReadByScraper = 
    (driver.FindElements(By.TagName("body")))
    |> Seq.map (
        fun e ->
            e.Text
    )
    |> Seq.filter (
        fun t -> t.Length > 0
    )
    |> Seq.toList
// printfn "Info: %A" bodyReadByScraper

// Get only text of use

let extractRightOfKeyword (keyword: string) (input: string list) : string list =
    input 
    |> List.map (fun str ->
        match str.IndexOf(keyword) with
        | -1 -> str // Keyword not found, return the original string
        | index -> str.Substring(index + keyword.Length)
    )

let extractLeftOfKeyword (keyword: string) (input: string list) : string list =
    input 
    |> List.map (fun str ->
        match str.IndexOf(keyword) with
        | -1 -> str // Keyword not found, return the original string
        | index -> str.Substring(0, index)
    )

let mutable textForOutput_1 : string list = extractRightOfKeyword "Share" bodyReadByScraper
textForOutput_1 <- extractLeftOfKeyword "Disclaimer" textForOutput_1

let mutable textForOutput_2 : string list = extractRightOfKeyword "Y/Y CHANGE" bodyReadByScraper
textForOutput_2 <- extractLeftOfKeyword "CEO" textForOutput_2

printfn "Info: %A" textForOutput_1
printfn "%s%A" Environment.NewLine textForOutput_2

