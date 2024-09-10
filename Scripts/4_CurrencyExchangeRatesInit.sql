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
	IN p_source_currency_code INT
)
BEGIN
    SELECT c.currency_code, c.currency_name, cer.created_date, cer.rate
	FROM CurrencyConversionDB.currency_exchange_rates cer
	INNER JOIN CurrencyConversionDB.currencies c ON c.currency_id = cer.source_currency_id
	WHERE c.currency_code = p_source_currency_code
	AND cer.created_date = (
		SELECT MAX(cer2.created_date)
		FROM CurrencyConversionDB.currency_exchange_rates cer2
		WHERE cer2.source_currency_id = cer.source_currency_id
	);
    
END;

// DELIMITER 
