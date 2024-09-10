/* 
	2. Preloaded Currencies that system will support. 
*/
CREATE TABLE IF NOT EXISTS CurrencyConversionDB.currencies
(
    currency_id INT AUTO_INCREMENT PRIMARY KEY,
    currency_code VARCHAR(3) UNIQUE NOT NULL,
    currency_name VARCHAR(50) NOT NULL,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

DELIMITER //

CREATE PROCEDURE CurrencyConversionDB.Get_Currency (IN p_Code VARCHAR(3))
BEGIN
    SELECT currency_id,currency_code, currency_name, created_date
    FROM CurrencyConversionDB.currencies 
    WHERE currency_code = p_Code OR p_Code IS NULL;
END;

CREATE PROCEDURE CurrencyConversionDB.Ins_Currency (IN p_currency_code VARCHAR(3), p_currency_name VARCHAR(50))
BEGIN
    INSERT INTO CurrencyConversionDB.currencies
    (
		currency_code,
        currency_name
    )
    VALUES(p_currency_code, currency_name);
END;
 // DELIMITER 