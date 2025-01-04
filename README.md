This project is a simple web scraper for google.com/finance. Specifically, a sort of minimal stock screener built entirely in F# / .NET framework (.NET 8.0)
The program spins up a container, setting up all the infrastructure and building the project.
It then starts a chrome browser, navigates to the website and gets elements with the HTML tag <body>
This text is retreived and then manipulated so only information relevant to the stock the user is searching for is stored.
I used selenium and docker compose and docker container for this project.
I have a windows x64 computer so i had to set my dockerfile around that but it's the same to set it up for Linux by just replacing windows keywords.
