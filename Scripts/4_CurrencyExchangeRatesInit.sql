/*
	4.Exchange Rates per currency from source currency to destination currency. 
*/

CREATE TABLE IF NOT EXISTS CurrencyConversionDB.currency_exchange_rates 
(
    exchange_rate_id INT AUTO_INCREMENT PRIMARY KEY,
    source_currency_id INT,
    target_currency_id INT,
    source_system_id INT,
    rate DECIMAL(10, 6) NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (source_currency_id) REFERENCES currencies(currency_id),
    FOREIGN KEY (target_currency_id) REFERENCES currencies(currency_id),
    FOREIGN KEY (source_system_id) REFERENCES currency_source_system(source_system_id)
);

DELIMITER //
	
CREATE PROCEDURE CurrencyConversionDB.Ins_Currency_Exchange_Rate
(
	IN p_source_currency_id INT,
    IN p_target_currency_id INT, 
    IN p_source_system_id INT, 
    IN p_rate DECIMAL(10, 6)
)
BEGIN
    INSERT INTO CurrencyConversionDB.currency_exchange_rates
    (
		source_currency_id,
        target_currency_id,
        source_system_id,
        rate
    )
    VALUES(p_source_currency_id, p_target_currency_id, p_source_system_id, p_rate);
END;

CREATE PROCEDURE CurrencyConversionDB.Get_ReadOnly_Currency_Exchange_Rate
(
	IN p_source_currency_code VARCHAR(3), 
    IN p_target_currency_code VARCHAR(3)
)
BEGIN
    WITH LatestCurrencyRate AS (
	SELECT exchange_rate_id, 
		   cSource.currency_code As Source_Currency, 
		   cTarget.currency_code As Target_Currency, 
		   source_system_id, 
		   rate, 
           cer.created_date,
		   row_number() OVER (Partition BY source_currency_id, target_currency_id ORDER BY cer.created_date DESC) As LatestRateRowNum
		   FROM currencyconversiondb.currency_exchange_rates cer
           INNER JOIN currencyconversiondb.currencies cSource ON cSource.currency_id = cer.source_currency_id
           INNER JOIN currencyconversiondb.currencies cTarget ON cTarget.currency_id = cer.target_currency_id
           WHERE cSource.currency_code = p_source_currency_code AND 
				 cTarget.currency_code = p_target_currency_code 
	)
	SELECT exchange_rate_id, 
		   Source_Currency, 
		   Target_Currency, 
		   source_system_id, 
		   rate,
           created_date
	FROM LatestCurrencyRate
	WHERE LatestRateRowNum = 1;
    
END;

// DELIMITER 
