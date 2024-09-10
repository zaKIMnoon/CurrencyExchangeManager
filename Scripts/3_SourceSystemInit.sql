/*
	3. The currency data that will populated based on the system. 
*/

CREATE TABLE IF NOT EXISTS CurrencyConversionDB.currency_source_system
(
	source_system_id INT AUTO_INCREMENT PRIMARY KEY, 
    source_system_name VARCHAR(50) NOT NULL, 
    source_system_url VARCHAR(50) NOT NULL, 
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

DELIMITER //

CREATE PROCEDURE CurrencyConversionDB.Get_CurrencySourceSystem(IN p_source_system_name VARCHAR(50))
BEGIN
    SELECT source_system_id,
			source_system_name,
            source_system_url,
            created_date
    FROM currencyconversiondb.currency_source_system
    WHERE source_system_name = p_source_system_name OR p_source_system_name IS NULL;
END;

CREATE PROCEDURE CurrencyConversionDB.Ins_CurrencySourceSystem
(
	IN p_source_system_name VARCHAR(50),
    IN p_source_system_url VARCHAR(50)
)
BEGIN
    INSERT INTO CurrencyConversionDB.currency_source_system
    (
		source_system_name,
        source_system_url
    )
    VALUES(p_source_system_name, p_source_system_url);
END;

 // DELIMITER 