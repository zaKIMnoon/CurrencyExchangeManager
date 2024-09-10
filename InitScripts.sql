/*
	1. Script to create the currency conversion Database
*/  

CREATE DATABASE IF NOT EXISTS CurrencyConversionDB;

/* 
	2. Preloaded Currencies that system will support. 
*/
CREATE TABLE IF NOT EXISTS CurrencyConversionDB.currencies
(
    currency_id INT AUTO_INCREMENT PRIMARY KEY,
    currency_code VARCHAR(3) UNIQUE NOT NULL,
    currency_name VARCHAR(50) NOT NULL,
    create_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

/*
	3. The currency data that will poplulated based on the system. 
*/

CREATE TABLE IF NOT EXISTS CurrencyConversionDB.currency_source_system
(
	source_system_id INT AUTO_INCREMENT PRIMARY KEY, 
    source_system_name VARCHAR(50) NOT NULL, 
    source_system_url VARCHAR(50) NOT NULL, 
    create_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

/*
	4.Exchange Rates per currency from source currency to destination currency. 
*/

CREATE TABLE IF NOT EXISTS CurrencyConversionDB.currency_exchange_rates 
(
    exchange_rate_id INT AUTO_INCREMENT PRIMARY KEY,
    source_currency_id INT,
    target_currency_id INT,
    source_system_id INT,
    rate DECIMAL(10, 4) NOT NULL,
    create_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (source_currency_id) REFERENCES currencies(currency_id),
    FOREIGN KEY (target_currency_id) REFERENCES currencies(currency_id),
    FOREIGN KEY (source_system_id) REFERENCES currency_source_system(source_system_id),
    UNIQUE KEY (source_currency_id, target_currency_id)
);