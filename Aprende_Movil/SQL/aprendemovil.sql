-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 03-11-2018 a las 19:58:04
-- Versión del servidor: 10.1.28-MariaDB
-- Versión de PHP: 7.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `aprendemovil`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteReminder` (IN `pEmail` VARCHAR(100), IN `pReminder` VARCHAR(500))  NO SQL
    COMMENT 'Procedure that delete a reminder from calendar'
BEGIN 
	DELETE FROM calendar  
    WHERE getUserID(pEmail) = calendar.userID AND calendar.reminder = pReminder;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `finishMedication` (IN `pEmail` VARCHAR(100), IN `pMedicine` VARCHAR(100))  NO SQL
    COMMENT 'Procedure that finish medication'
BEGIN 
	UPDATE MedicationsPerUser
    SET medicationsPerUser.endDate = NOW()
    WHERE medicationsPerUser.userID = getUserID(pEmail) and medicationsPerUser.medicineID = getMedicineID(pMedicine);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getMedicine` ()  NO SQL
BEGIN
SELECT medicine.name AS name
FROM medicine;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getMedicineByUser` (IN `pEmail` VARCHAR(100))  NO SQL
BEGIN
SELECT medicine.name AS name
FROM medicine, medicationsPerUser mpu
WHERE mpu.userID = getUserID(pEmail) AND mpu.medicineID = medicine.medicineID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getMeticationByUser` (IN `pEmail` VARCHAR(100))  NO SQL
BEGIN
SELECT getMedicineName(mpu.medicineID) AS name, mpu.quantity, getTypeName(mpu.medicineID) AS measure, mpu.frequency, mpu.startDate, mpu.endDate
FROM medicationsPerUser mpu, medicine
WHERE mpu.userID = getUserID(pEmail) AND mpu.medicineID = medicine.medicineID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPayment` ()  NO SQL
BEGIN
SELECT payment.name AS name
FROM Payment;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPaymentsByUser` (IN `pEmail` VARCHAR(100))  NO SQL
BEGIN
SELECT pbu.amount, pbu.payday, getPaymentName(pbu.paymentID) AS payment 
FROM paysByUser pbu
WHERE pbu.userID = getUserID(pEmail);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getRemaninderByUser` (IN `pEmail` VARCHAR(100))  NO SQL
BEGIN
SELECT reminder, startTime, endTime, notice
FROM calendar
WHERE calendar.userID = getUserID(pEmail);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertCalendar` (IN `pReminder` VARCHAR(500), IN `pStartTime` DATETIME, IN `pEndTime` DATETIME, IN `pNotice` INT(11), IN `pEmail` VARCHAR(100))  NO SQL
    COMMENT 'Procedure that insert into the table Calendar'
BEGIN 
	INSERT INTO Calendar(calendarID, reminder, startTime, endTime, notice, userID)
	VALUES(NULL, pReminder, pStartTime, pEndTime, getNoticed(pStartTime, pNotice), getUserID(pEmail));
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertMedication` (IN `pQuantity` FLOAT(11), IN `pFrequency` INT(11), IN `pStartDate` DATETIME, IN `pEmail` VARCHAR(100), IN `pMedicine` VARCHAR(100), IN `pTypeOfMeasure` VARCHAR(100))  NO SQL
    COMMENT 'Procedure that insert into the table medicationsPerUser'
BEGIN 
	INSERT IGNORE INTO Medicine(medicineID, name, typeID)
	VALUES(NULL, pMedicine, getTypeID(pTypeOfMeasure));
	INSERT INTO MedicationsPerUser(medicationsID, quantity, frequency, startDate, endDate, userID, medicineID)
	VALUES(NULL, pQuantity, pFrequency, pStartDate, NULL, getUserID(pEmail), getMedicineID(pMedicine));
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertPayment` (IN `pName` VARCHAR(100), IN `pFrequency` INT(11))  NO SQL
    COMMENT 'Procedure that insert into the table Payment'
BEGIN 
	INSERT INTO Payment(paymentID, name, frequency)
	VALUES(NULL, pName, pFrequency);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertPaysByUser` (IN `pAmount` FLOAT(11), IN `pEmail` VARCHAR(100), IN `pPayment` VARCHAR(100))  NO SQL
    COMMENT 'Procedure that insert into the table PaysByUser'
BEGIN 
	INSERT INTO PaysByUser(paysByUserID, amount, payday, userID, paymentID)
	VALUES(NULL, pAmount, NOW(), getUserID(pEmail), getPaymentID(pPayment));
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertTypeOfMeasure` (IN `pName` VARCHAR(100))  NO SQL
    COMMENT 'Procedure that insert into the table typeOfMeasure'
BEGIN 
	INSERT INTO typeOfMeasure(typeID, name)
	VALUES(NULL, pName);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertUser` (IN `pName` VARCHAR(100), IN `pLastname` VARCHAR(100), IN `pEmail` VARCHAR(100), IN `pPassword` VARCHAR(100), IN `pAddress` VARCHAR(100), IN `pIdentification` VARCHAR(100), IN `pDateOfBirth` DATE)  NO SQL
    COMMENT 'Procedure that insert into the table user'
BEGIN 
	INSERT INTO user(userID, name, lastname, email, password, address, identification, dateOfBirth)
	VALUES(NULL, pName, pLastname, pEmail, pPassword, pAddress, pIdentification, pDateOfBirth);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateCalendar` (IN `pEmail` VARCHAR(100), IN `pReminder` VARCHAR(500), IN `pNewReminder` VARCHAR(500), IN `pNewStartTime` DATETIME, IN `pNewEndTime` DATETIME, IN `pNewNotice` INT(11))  NO SQL
    COMMENT 'Procedure that update the calendar table'
BEGIN 
	UPDATE calendar
    SET calendar.reminder = pNewReminder,
    calendar.startTime = pNewStartTime,
    calendar.endTime = pNewEndTime,
    calendar.notice = getNotice(pNewStartTime, pNewNotice)
    WHERE calendar.userID = getUserID(pEmail) and calendar.reminder = pReminder;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateMedication` (IN `pEmail` VARCHAR(100), IN `pMedicine` VARCHAR(100), IN `pNewQuantity` FLOAT(11), IN `pNewFrequency` INT(11), IN `pNewStartDate` DATE)  NO SQL
    COMMENT 'Procedure that update the medicationsPerUser table'
BEGIN 
	UPDATE medicationsPerUser  
    SET medicationsPerUser.quantity = pNewQuantity,
    medicationsPerUser.frequency = pNewFrequency,
    medicationsPerUser.startDate = pNewStartDate
    WHERE medicationsPerUser.userID = getUserID(pEmail) and medicationsPerUser.medicineID = getMedicineID(pMedicine);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateUser` (IN `pEmail` VARCHAR(100), IN `pNewName` VARCHAR(100), IN `pNewLastname` VARCHAR(100), IN `pNewPassword` VARCHAR(100), IN `pNewAddress` VARCHAR(100), IN `pNewIdentification` VARCHAR(100), IN `pNewDateOfBirth` DATE)  NO SQL
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

--
-- Funciones
--
CREATE DEFINER=`root`@`localhost` FUNCTION `getMedicineID` (`pName` VARCHAR(100)) RETURNS INT(11) BEGIN
	DECLARE vMedicineID INT(11) DEFAULT -1;
  	SELECT medicineID INTO vMedicineID FROM medicine WHERE medicine.name = pName;
  	RETURN vMedicineID;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getMedicineName` (`pMedicineID` INT(11)) RETURNS VARCHAR(100) CHARSET latin1 BEGIN
	DECLARE vName VARCHAR(100) DEFAULT "";
  	SELECT name INTO vName FROM medicine WHERE medicine.medicineID = pMedicineID;
  	RETURN vName;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getNoticed` (`pStartTime` DATETIME, `pNoticed` INT(11)) RETURNS DATETIME BEGIN
	DECLARE vNoticed DATETIME DEFAULT NOW();
  	SELECT DATE_SUB(pStartTime, INTERVAL pNoticed MINUTE) INTO vNoticed;
  	RETURN vNoticed;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getPaymentID` (`pName` VARCHAR(100)) RETURNS INT(11) BEGIN
	DECLARE vPaymentID INT(11) DEFAULT -1;
  	SELECT paymentID INTO vPaymentID FROM payment WHERE payment.name = pName;
  	RETURN vPaymentID;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getPaymentName` (`pPaymentID` INT(11)) RETURNS VARCHAR(100) CHARSET latin1 BEGIN
	DECLARE vPayment VARCHAR(100) DEFAULT "";
  	SELECT payment.name INTO vPayment FROM Payment WHERE payment.paymentID = pPaymentID;
  	RETURN vPayment;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getTypeID` (`pName` VARCHAR(100)) RETURNS INT(11) BEGIN
	DECLARE vTypeID INT(11) DEFAULT -1;
  	SELECT typeID INTO vTypeID FROM TypeOfMeasure WHERE TypeOfMeasure.name = pName;
  	RETURN vTypeID;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getTypeName` (`pMedicineID` INT(11)) RETURNS VARCHAR(100) CHARSET latin1 BEGIN
	DECLARE vType VARCHAR(100) DEFAULT "";
  	SELECT tom.name INTO vType FROM TypeOfMeasure tom, medicine WHERE medicine.medicineID = pMedicineID AND medicine.typeID = tom.typeID;
  	RETURN vType;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getUserID` (`pEmail` VARCHAR(100)) RETURNS INT(11) BEGIN
	DECLARE vUserID INT(11) DEFAULT -1;
  	SELECT userID INTO vUserID FROM user WHERE user.email = pEmail;
  	RETURN vUserID;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `validateEmail` (`pEmail` VARCHAR(100)) RETURNS TINYINT(1) NO SQL
BEGIN
RETURN NOT EXISTS(SELECT user.email FROM user WHERE user.email = pEmail);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `validateMedication` (`pEmail` VARCHAR(100), `pMedicine` VARCHAR(100)) RETURNS TINYINT(1) NO SQL
BEGIN
RETURN NOT EXISTS(SELECT medicationsPerUser.userID, medicationsPerUser.medicineID
	FROM medicationsPerUser WHERE medicationsPerUser.userID = getUserID(pEmail) AND medicationsPerUser.medicineID = getMedicineID(pMedicine));
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `calendar`
--

CREATE TABLE `calendar` (
  `calendarID` int(11) NOT NULL,
  `reminder` varchar(500) NOT NULL,
  `startTime` datetime NOT NULL,
  `endTime` datetime NOT NULL,
  `notice` datetime NOT NULL,
  `userID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `calendar`
--

INSERT INTO `calendar` (`calendarID`, `reminder`, `startTime`, `endTime`, `notice`, `userID`) VALUES
(1, 'Examen de la vista', '2018-11-04 10:30:00', '2018-11-04 11:00:00', '2018-11-04 10:00:00', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `medicationsperuser`
--

CREATE TABLE `medicationsperuser` (
  `medicationsID` int(11) NOT NULL,
  `quantity` float NOT NULL,
  `frequency` int(11) NOT NULL,
  `startDate` datetime NOT NULL,
  `endDate` datetime DEFAULT NULL,
  `userID` int(11) NOT NULL,
  `medicineID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `medicationsperuser`
--

INSERT INTO `medicationsperuser` (`medicationsID`, `quantity`, `frequency`, `startDate`, `endDate`, `userID`, `medicineID`) VALUES
(1, 20, 8, '2018-11-03 00:00:00', '2018-11-03 12:12:22', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `medicine`
--

CREATE TABLE `medicine` (
  `medicineID` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `typeID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `medicine`
--

INSERT INTO `medicine` (`medicineID`, `name`, `typeID`) VALUES
(1, 'Panadol', 3),
(2, 'Alegra', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `payment`
--

CREATE TABLE `payment` (
  `paymentID` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `frequency` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `payment`
--

INSERT INTO `payment` (`paymentID`, `name`, `frequency`) VALUES
(1, 'Agua', 30),
(2, 'Luz', 30);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `paysbyuser`
--

CREATE TABLE `paysbyuser` (
  `paysByUserID` int(11) NOT NULL,
  `amount` float NOT NULL,
  `payday` datetime NOT NULL,
  `userID` int(11) NOT NULL,
  `paymentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `paysbyuser`
--

INSERT INTO `paysbyuser` (`paysByUserID`, `amount`, `payday`, `userID`, `paymentID`) VALUES
(1, 15000, '2018-11-03 12:55:17', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `typeofmeasure`
--

CREATE TABLE `typeofmeasure` (
  `typeID` int(11) NOT NULL,
  `name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `typeofmeasure`
--

INSERT INTO `typeofmeasure` (`typeID`, `name`) VALUES
(1, 'Gramos'),
(2, 'Mililitros'),
(3, 'Miligramos');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `user`
--

CREATE TABLE `user` (
  `userID` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `lastname` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `address` varchar(100) NOT NULL,
  `identification` varchar(100) NOT NULL,
  `dateOfBirth` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `user`
--

INSERT INTO `user` (`userID`, `name`, `lastname`, `email`, `password`, `address`, `identification`, `dateOfBirth`) VALUES
(1, 'Esteban', 'Coto', 'ecoto@gmail.com', '123456', 'San Jose', '12345678', '1998-01-30');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `calendar`
--
ALTER TABLE `calendar`
  ADD PRIMARY KEY (`calendarID`),
  ADD KEY `userID` (`userID`);

--
-- Indices de la tabla `medicationsperuser`
--
ALTER TABLE `medicationsperuser`
  ADD PRIMARY KEY (`medicationsID`),
  ADD KEY `userID` (`userID`),
  ADD KEY `medicineID` (`medicineID`);

--
-- Indices de la tabla `medicine`
--
ALTER TABLE `medicine`
  ADD PRIMARY KEY (`medicineID`),
  ADD KEY `typeID` (`typeID`);

--
-- Indices de la tabla `payment`
--
ALTER TABLE `payment`
  ADD PRIMARY KEY (`paymentID`);

--
-- Indices de la tabla `paysbyuser`
--
ALTER TABLE `paysbyuser`
  ADD PRIMARY KEY (`paysByUserID`),
  ADD KEY `userID` (`userID`),
  ADD KEY `paymentID` (`paymentID`);

--
-- Indices de la tabla `typeofmeasure`
--
ALTER TABLE `typeofmeasure`
  ADD PRIMARY KEY (`typeID`);

--
-- Indices de la tabla `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`userID`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `calendar`
--
ALTER TABLE `calendar`
  MODIFY `calendarID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `medicationsperuser`
--
ALTER TABLE `medicationsperuser`
  MODIFY `medicationsID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `medicine`
--
ALTER TABLE `medicine`
  MODIFY `medicineID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `payment`
--
ALTER TABLE `payment`
  MODIFY `paymentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `paysbyuser`
--
ALTER TABLE `paysbyuser`
  MODIFY `paysByUserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `typeofmeasure`
--
ALTER TABLE `typeofmeasure`
  MODIFY `typeID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `user`
--
ALTER TABLE `user`
  MODIFY `userID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `calendar`
--
ALTER TABLE `calendar`
  ADD CONSTRAINT `calendar_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`);

--
-- Filtros para la tabla `medicationsperuser`
--
ALTER TABLE `medicationsperuser`
  ADD CONSTRAINT `medicationsperuser_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`),
  ADD CONSTRAINT `medicationsperuser_ibfk_2` FOREIGN KEY (`medicineID`) REFERENCES `medicine` (`medicineID`);

--
-- Filtros para la tabla `medicine`
--
ALTER TABLE `medicine`
  ADD CONSTRAINT `medicine_ibfk_1` FOREIGN KEY (`typeID`) REFERENCES `typeofmeasure` (`typeID`);

--
-- Filtros para la tabla `paysbyuser`
--
ALTER TABLE `paysbyuser`
  ADD CONSTRAINT `paysbyuser_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`),
  ADD CONSTRAINT `paysbyuser_ibfk_2` FOREIGN KEY (`paymentID`) REFERENCES `payment` (`paymentID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
