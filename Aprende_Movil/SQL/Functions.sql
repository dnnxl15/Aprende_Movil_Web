-- --------------------------------------------------------------------------------
--                           VALIDATE IF EXISTS
-- --------------------------------------------------------------------------------

-- Function that returns if the email user exists
-- Author: Esteban Coto Alfaro
-- Description: This function return a boolean is the email exist in the table user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `validateEmail`(`pEmail` VARCHAR(100)) RETURNS tinyint(1)
    NO SQL
BEGIN
RETURN NOT EXISTS(SELECT user.email FROM user WHERE user.email = pEmail);
END$$
DELIMITER ;

-- Function that returns if the medication per user exists
-- Author: Esteban Coto Alfaro
-- Description: This function return a boolean is the medication exist in the table medicationsPerUser.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `validateMedication`(`pEmail` VARCHAR(100), `pMedicine` VARCHAR(100)) RETURNS tinyint(1)
    NO SQL
BEGIN
RETURN NOT EXISTS(SELECT medicationsPerUser.userID, medicationsPerUser.medicineID
	FROM medicationsPerUser WHERE medicationsPerUser.userID = getUserID(pEmail) AND medicationsPerUser.medicineID = getMedicineID(pMedicine));
END$$
DELIMITER ;

-- --------------------------------------------------------------------------------
-- 							 GET ID BY RECIVING VARCHAR
-- -------------------------------------------------------------------------------- 

-- Function that return the user id
-- Author: Esteban Coto Alfaro
-- Description: This function return a int with the id of the user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getUserID`(`pEmail` VARCHAR(100)) RETURNS INT(11)
BEGIN
	DECLARE vUserID INT(11) DEFAULT -1;
  	SELECT userID INTO vUserID FROM user WHERE user.email = pEmail;
  	RETURN vUserID;
END$$
DELIMITER ;

-- Function that return the type id
-- Author: Esteban Coto Alfaro
-- Description: This function return a int with the id of the type.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getTypeID`(`pName` VARCHAR(100)) RETURNS INT(11)
BEGIN
	DECLARE vTypeID INT(11) DEFAULT -1;
  	SELECT typeID INTO vTypeID FROM TypeOfMeasure WHERE TypeOfMeasure.name = pName;
  	RETURN vTypeID;
END$$
DELIMITER ;

-- Function that return the medicine id
-- Author: Esteban Coto Alfaro
-- Description: This function return a int with the id of the medicine.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getMedicineID`(`pName` VARCHAR(100)) RETURNS INT(11)
BEGIN
	DECLARE vMedicineID INT(11) DEFAULT -1;
  	SELECT medicineID INTO vMedicineID FROM medicine WHERE medicine.name = pName;
  	RETURN vMedicineID;
END$$
DELIMITER ;

-- Function that return the payment id
-- Author: Esteban Coto Alfaro
-- Description: This function return a int with the id of the payment.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getPaymentID`(`pName` VARCHAR(100)) RETURNS INT(11)
BEGIN
	DECLARE vPaymentID INT(11) DEFAULT -1;
  	SELECT paymentID INTO vPaymentID FROM payment WHERE payment.name = pName;
  	RETURN vPaymentID;
END$$
DELIMITER ;

-- --------------------------------------------------------------------------------
-- 							 GET INFO BY RECIVING ID
-- -------------------------------------------------------------------------------- 

-- Function that return the medicine name
-- Author: Esteban Coto Alfaro
-- Description: This function return a varchar with the name of the medicine.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getMedicineName`(`pMedicineID` INT(11)) RETURNS VARCHAR(100)
BEGIN
	DECLARE vName VARCHAR(100) DEFAULT "";
  	SELECT name INTO vName FROM medicine WHERE medicine.medicineID = pMedicineID;
  	RETURN vName;
END$$
DELIMITER ;

-- Function that return the medicine name
-- Author: Esteban Coto Alfaro
-- Description: This function return a varchar with the name of the medicine.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getTypeName`(`pMedicineID` INT(11)) RETURNS VARCHAR(100)
BEGIN
	DECLARE vType VARCHAR(100) DEFAULT "";
  	SELECT tom.name INTO vType FROM TypeOfMeasure tom, medicine WHERE medicine.medicineID = pMedicineID AND medicine.typeID = tom.typeID;
  	RETURN vType;
END$$
DELIMITER ;

-- Function that return the payment name
-- Author: Esteban Coto Alfaro
-- Description: This function return a varchar with the name of the payment.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getPaymentName`(`pPaymentID` INT(11)) RETURNS VARCHAR(100)
BEGIN
	DECLARE vPayment VARCHAR(100) DEFAULT "";
  	SELECT payment.name INTO vPayment FROM Payment WHERE payment.paymentID = pPaymentID;
  	RETURN vPayment;
END$$
DELIMITER ;

-- --------------------------------------------------------------------------------
-- 							 GET NOTICED DATETIME
-- --------------------------------------------------------------------------------

-- Function that return the noticed datetime
-- Author: Esteban Coto Alfaro
-- Description: This function return a datetime with the time of the noticed.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getNoticed`(`pStartTime` DATETIME, `pNoticed` INT(11)) RETURNS DATETIME
BEGIN
	DECLARE vNoticed DATETIME DEFAULT NOW();
  	SELECT DATE_SUB(pStartTime, INTERVAL pNoticed MINUTE) INTO vNoticed;
  	RETURN vNoticed;
END$$
DELIMITER ;
