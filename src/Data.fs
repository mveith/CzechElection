module Data

type PartyValue = 
    {
        Name : string
        Value : float
        ColorCode : string        
    }

type RegionValue = 
    {
        Name : string
        Value : float
    }

let data = 
    [
        { Name = "ODS"; Value = 11.320000; ColorCode = "#183E8E" }
        { Name = "ŘN - VU"; Value = 0.170000; ColorCode = "#000000" }
        { Name = "CESTA"; Value = 0.070000; ColorCode = "#000000" }
        { Name = "ČSSD"; Value = 7.270000; ColorCode =  "#E89544" }
        { Name = "PB"; Value = 0.000000; ColorCode = "#000000" }
        { Name = "RČ"; Value = 0.070000; ColorCode = "#000000" }
        { Name = "STAN"; Value = 5.180000; ColorCode = "#84C4F0"}
        { Name = "KSČM"; Value = 7.760000; ColorCode = "#D22221" }
        { Name = "Zelení"; Value = 1.460000; ColorCode = "#000000" }
        { Name = "Rozumní"; Value = 0.720000; ColorCode = "#000000" }
        { Name = "SPDV"; Value = 0.000000; ColorCode = "#000000" }
        { Name = "Svobodní"; Value = 1.560000; ColorCode = "#000000" }
        { Name = "BPI"; Value = 0.100000; ColorCode = "#000000" }
        { Name = "ODA"; Value = 0.150000; ColorCode = "#000000" }
        { Name = "Piráti"; Value = 10.790000; ColorCode = "#000000" }
        { Name = "OBČANÉ 2011"; Value = 0.000000; ColorCode = "#000000" }
        { Name = "Unie H.A.V.E.L."; Value = 0.000000; ColorCode = "#000000" }
        { Name = "ČNF"; Value = 0.000000; ColorCode = "#000000" }
        { Name = "Referendum o EU"; Value = 0.080000; ColorCode = "#000000" }
        { Name = "TOP 09"; Value = 5.310000; ColorCode = "#693C91" }
        { Name = "ANO"; Value = 29.640000; ColorCode = "#251359" }
        { Name = "DV 2016"; Value = 0.070000; ColorCode = "#000000" }
        { Name = "SPRRSČ M.Sládka"; Value = 0.190000; ColorCode = "#000000" }
        { Name = "KDU-ČSL"; Value = 5.800000; ColorCode = "#EAC618" }
        { Name = "ČSNS"; Value = 0.030000; ColorCode = "#000000" }
        { Name = "REAL"; Value = 0.710000; ColorCode = "#000000" }
        { Name = "SPORTOVCI"; Value = 0.200000; ColorCode = "#000000" }
        { Name = "DSSS"; Value = 0.200000; ColorCode = "#000000" }
        { Name = "SPD"; Value = 10.640000; ColorCode = "#917c6f" }
        { Name = "SPO"; Value = 0.360000; ColorCode = "#000000" }
        { Name = "NáS"; Value = 0.000000; ColorCode = "#000000" }
    ]

let regions = 
    [
        { Name = "Celá ČR"; Value = 60.84 }
        { Name = "Hl. m. Praha"; Value = 67.130000 }
        { Name = "Středočeský"; Value = 63.440000 }
        { Name = "Jihočeský"; Value = 61.740000 }
        { Name = "Plzeňský"; Value = 59.910000 }
        { Name = "Karlovarský"; Value = 52.110000 }
        { Name = "Ústecký"; Value = 52.380000 }
        { Name = "Liberecký"; Value = 60.050000 }
        { Name = "Královéhradecký"; Value = 63.250000 }
        { Name = "Pardubický"; Value = 63.050000 }
        { Name = "Vysočina"; Value = 64.030000 }
        { Name = "Jihomoravský"; Value = 61.750000 }
        { Name = "Olomoucký"; Value = 59.770000 }
        { Name = "Zlínský"; Value = 62.120000 }
        { Name = "Moravskoslezský"; Value = 55.860000 }
    ]