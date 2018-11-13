/*-- --------------------------------------------------------------------------------
--                                      INSERTS
-- --------------------------------------------------------------------------------

-- Procedure to insert User
-- Author: Esteban Coto Alfaro
-- Description: This procedure insert into the table user, all the data.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertUser`(IN `pName` VARCHAR(100), IN `pLastname` VARCHAR(100), IN `pEmail` VARCHAR(100), IN `pPassword` VARCHAR(100), IN `pAddress` VARCHAR(100), IN `pIdentification` VARCHAR(100), IN `pDateOfBirth` DATE)
    NO SQL
    COMMENT 'Procedure that insert into the table user'
BEGIN 
	INSERT INTO user(userID, name, lastname, email, password, address, identification, dateOfBirth)
	VALUES(NULL, pName, pLastname, pEmail, pPassword, pAddress, pIdentification, pDateOfBirth);
END$$
DELIMITER ;

-- Procedure to insert TypeOfMeasure
-- Author: Esteban Coto Alfaro
-- Description: This procedure insert into the table typeOfMeasure, all the data.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertTypeOfMeasure`(IN `pName` VARCHAR(100))
    NO SQL
    COMMENT 'Procedure that insert into the table typeOfMeasure'
BEGIN 
	INSERT INTO typeOfMeasure(typeID, name)
	VALUES(NULL, pName);
END$$
DELIMITER ;

-- Procedure to insert Payment
-- Author: Esteban Coto Alfaro
-- Description: This procedure insert into the table Payment, all the data.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertPayment`(IN `pName` VARCHAR(100), IN `pFrequency` INT(11))
    NO SQL
    COMMENT 'Procedure that insert into the table Payment'
BEGIN 
	INSERT INTO Payment(paymentID, name, frequency)
	VALUES(NULL, pName, pFrequency);
END$$
DELIMITER ;

-- Procedure to insert MedicationsPerUser
-- Author: Esteban Coto Alfaro
-- Description: This procedure insert into the table medicationsPerUser, all the data.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertMedication`(IN `pQuantity` FLOAT(11), IN `pFrequency` INT(11), IN `pStartDate` DATE, IN `pEmail` VARCHAR(100), IN `pMedicine` VARCHAR(100), , IN `pTypeOfMeasure` VARCHAR(100))
    NO SQL
    COMMENT 'Procedure that insert into the table medicationsPerUser'
BEGIN 
	INSERT IGNORE INTO Medicine(medicineID, name, typeID)
	VALUES(NULL, pMedicine, getTypeID(pTypeOfMeasure));
	INSERT INTO MedicationsPerUser(medicationsID, quantity, frequency, startDate, endDate, userID, medicineID)
	VALUES(NULL, pQuantity, pFrequency, pStartDate, NULL, getUserID(pEmail), getMedicineID(pMedicine));
END$$
DELIMITER ;

-- Procedure to insert PaysByUser
-- Author: Esteban Coto Alfaro
-- Description: This procedure insert into the table PaysByUser, all the data.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertPaysByUser`(IN `pAmount` FLOAT(11), IN `pEmail` VARCHAR(100), IN `pPayment` VARCHAR(100))
    NO SQL
    COMMENT 'Procedure that insert into the table PaysByUser'
BEGIN 
	INSERT INTO PaysByUser(paysByUserID, amount, payday, userID, paymentID)
	VALUES(NULL, pAmount, NOW(), getUserID(pEmail), getPaymentID(pPayment));
END$$
DELIMITER ;

-- Procedure to insert Calendar
-- Author: Esteban Coto Alfaro
-- Description: This procedure insert into the table Calendar, all the data.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertCalendar`(IN `pReminder` VARCHAR(500), IN `pStartTime` DATETIME, IN `pEndTime` DATETIME, IN `pNotice` INT(11), IN `pEmail` VARCHAR(100))
    NO SQL
    COMMENT 'Procedure that insert into the table Calendar'
BEGIN 
	INSERT INTO Calendar(calendarID, reminder, startTime, endTime, notice, userID)
	VALUES(NULL, pReminder, pStartTime, pEndTime, getNotice(pStartTime, pNotice), getUserID(pEmail));
END$$
DELIMITER ;

-- --------------------------------------------------------------------------------
--                                      UPDATE
-- --------------------------------------------------------------------------------

-- Procedure to update the user data
-- Author: Esteban Coto Alfaro
-- Description: This procedure update into the table user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateUser`(IN `pEmail` VARCHAR(100), IN `pNewName` VARCHAR(100), IN `pNewLastname` VARCHAR(100),
	IN `pNewPassword` VARCHAR(100), IN `pNewAddress` VARCHAR(100), IN `pNewIdentification` VARCHAR(100), IN `pNewDateOfBirth` DATE)
    NO SQL
    COMMENT 'Procedure that update the user table'
BEGIN 
	UPDATE user  
    SET user.name = pNewName,
    user.lastname = pNewLastname,
    user.password = pNewPassword,
    user.address = pNewAddress,
    user.identification = pNewIdentification,
    user.dateOfBirth = pNewDateOfBirth 
    WHERE user.email = pEmail;
END$$
DELIMITER ;

-- Procedure to update the medication per user data
-- Author: Esteban Coto Alfaro
-- Description: This procedure update into the table medicationsPerUser.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateMedication`(IN `pEmail` VARCHAR(100), IN `pMedicine` VARCHAR(100), IN `pNewQuantity` FLOAT(11),
	IN `pNewFrequency` INT(11), IN `pNewStartDate` DATE)
    NO SQL
    COMMENT 'Procedure that update the medicationsPerUser table'
BEGIN 
	UPDATE medicationsPerUser  
    SET medicationsPerUser.quantity = pNewQuantity,
    medicationsPerUser.frequency = pNewFrequency,
    medicationsPerUser.startDate = pNewStartDate
    WHERE medicationsPerUser.userID = getUserID(pEmail) and medicationsPerUser.medicineID = getMedicineID(pMedicine);
END$$
DELIMITER ;

-- Procedure to update the calendar data
-- Author: Esteban Coto Alfaro
-- Description: This procedure update into the table calendar.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateCalendar`(IN `pEmail` VARCHAR(100), IN `pReminder` VARCHAR(500), IN `pNewReminder` VARCHAR(500),
	IN `pNewStartTime` DATETIME, IN `pNewEndTime` DATETIME, IN `pNewNotice` INT(11))
    NO SQL
    COMMENT 'Procedure that update the calendar table'
BEGIN 
	UPDATE calendar
    SET calendar.reminder = pNewReminder,
    calendar.startTime = pNewStartTime,
    calendar.endTime = pNewEndTime,
    calendar.notice = getNotice(pNewStartTime, pNewNotice)
    WHERE calendar.userID = getUserID(pEmail) and calendar.reminder = pReminder;
END$$
DELIMITER ;

-- Procedure to finish a medication data
-- Author: Esteban Coto Alfaro
-- Description: This procedure update into the table MedicationsPerUser.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `finishMedication`(IN `pEmail` VARCHAR(100), IN `pMedicine` VARCHAR(100))
    NO SQL
    COMMENT 'Procedure that finish medication'
BEGIN 
	UPDATE MedicationsPerUser
    SET medicationsPerUser.endDate = NOW()
    WHERE medicationsPerUser.userID = getUserID(pEmail) and medicationsPerUser.medicineID = getMedicineID(pMedicine);
END$$
DELIMITER ;

-- --------------------------------------------------------------------------------
--                                  DELETE
-- --------------------------------------------------------------------------------

-- Procedure to delete from calendar
-- Author: Esteban Coto Alfaro
-- Description: This procedure delete into the table calendar.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteReminder`(IN `pEmail` VARCHAR(100), IN `pReminder` VARCHAR(500))
    NO SQL
    COMMENT 'Procedure that delete a reminder from calendar'
BEGIN 
	DELETE FROM calendar  
    WHERE getUserID(pEmail) = calendar.userID AND calendar.reminder = pReminder;
END$$
DELIMITER ;

-- --------------------------------------------------------------------------------
--                                  GET INFORMATION
-- --------------------------------------------------------------------------------

-- Procedure to get medicine 
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the medicine.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getMedicine`()
    NO SQL
BEGIN
SELECT medicine.name AS name
FROM medicine;
END$$
DELIMITER ;

-- Procedure to get medicine of the user
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the medicine of a user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getMedicineByUser`(IN `pEmail` VARCHAR(100))
    NO SQL
BEGIN
SELECT medicine.name AS name
FROM medicine, medicationsPerUser mpu
WHERE mpu.userID = getUserID(pEmail) AND mpu.medicineID = medicine.medicineID;
END$$
DELIMITER ;

-- Procedure to get medication from a user
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the medication of a user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getMeticationByUser`(IN `pEmail` VARCHAR(100))
    NO SQL
BEGIN
SELECT getMedicineName(mpu.medicineID) AS name, mpu.quantity, getTypeName(mpu.medicineID) AS measure, mpu.frequency, mpu.startDate, mpu.endDate
FROM medicationsPerUser mpu, medicine
WHERE mpu.userID = getUserID(pEmail) AND mpu.medicineID = medicine.medicineID;
END$$
DELIMITER ;

-- Procedure to get reminder from a user
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the remainders of a user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getRemaninderByUser`(IN `pEmail` VARCHAR(100))
    NO SQL
BEGIN
SELECT reminder, startTime, endTime, notice
FROM calendar
WHERE calendar.userID = getUserID(pEmail);
END$$
DELIMITER ;

-- Procedure to get payments from a user
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the payments of a user.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getPaymentsByUser`(IN `pEmail` VARCHAR(100))
    NO SQL
BEGIN
SELECT pbu.amount, pbu.payday, getPaymentName(pbu.paymentID) AS payment 
FROM paysByUser pbu
WHERE pbu.userID = getUserID(pEmail);
END$$
DELIMITER ;

-- Procedure to get payments 
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the payments.
-- Last modification: 03/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getPayment`()
    NO SQL
BEGIN
SELECT payment.name AS name
FROM Payment;
END$$
DELIMITER ;
*/
-- Procedure to get reminders from a user by month
-- Author: Esteban Coto Alfaro
-- Description: This procedure get all the reminders of a user by month.
-- Last modification: 12/11/18

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getReminderByMonth`(IN `pEmail` VARCHAR(100), IN `pMonth` INT(11))
    NO SQL
BEGIN
SELECT reminder, startTime, endTime, notice
FROM calendar
WHERE calendar.userID = getUserID(pEmail) AND MONTH(startTime) = pMonth;
END$$
DELIMITER ;