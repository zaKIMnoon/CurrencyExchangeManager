# CurrencyExchangeManager

This is an API that connects to https://openexchangerates.org/ api to get Currency conversion rates. 

## How to setup: 

* You need to install [mysql](https://dev.mysql.com/downloads/installer/) and [redis](https://redis.io/docs/latest/operate/oss_and_stack/install/install-redis/install-redis-on-windows/) for caching on your machine. 

**Alternatively**

* You can run both the MySql and Redis in [Docker](https://www.docker.com/products/docker-desktop/) using Docker Compose file ***docker-compose.yml*** with the below command:
* "docker compose up" from the terminal when cloning this repo. 

## Initialize Database Structure: 

* Once my sql is set up connect to Mysql Instance using [MySql Workbench]("https://dev.mysql.com/downloads/workbench/") and run the below scripts:

1. [Currency Conversion Datbase Initialization]("Scripts/1_CurrencyConversionDBInit.sql")
2. ["Currency Table with Stored Procedures"]("Scripts/2_CurrenciesInit.sql")
3. ["Source System Table with Stored Procedures"]("Scripts/3_SourceSystemInit.sql")
4. ["Currency Exchange Rate Table with Stored Procedures"]("Scripts/4_CurrencyExchangeRatesInit.sql")

## Design

1. A background service was created to fetch data from the external data source, this would be the CurrencyService. 
   - A currency Import function has to be created so that it can pull all supported currencies by the API. 
   - A currency rate import funcation has to be created so that it can pull base currency conversion and supported currencies. 

2. The endpoints generated in the API need to generate Swagger documentation so you can easy test. 
   - Source System Controller
	- Since the idea here is to make it extendable should another source be added you would need to be able to track that in the CurrencyRate table.
   - Currency Controller
	- This will display all currencies we could potentially support in the system. 
   - Currency Rate Controller
	- This will give back the currency rate data base on base currency and target currency. 