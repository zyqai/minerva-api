-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: minerva
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `_clients`
--

DROP TABLE IF EXISTS `_clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `_clients` (
  `clientId` int NOT NULL AUTO_INCREMENT,
  `userId` varchar(45) DEFAULT NULL,
  `tenantId` int NOT NULL,
  `clientName` varchar(255) DEFAULT NULL,
  `firstName` varchar(45) DEFAULT NULL,
  `lastName` varchar(45) DEFAULT NULL,
  `dob` datetime DEFAULT NULL,
  `socialsecuritynumber` varchar(45) DEFAULT NULL,
  `postalnumber` varchar(10) DEFAULT NULL,
  `stateid` int DEFAULT NULL,
  `clientAddress` varchar(255) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `preferredContact` varchar(45) DEFAULT NULL,
  `creditScore` varchar(45) DEFAULT NULL,
  `lendabilityScore` varchar(45) DEFAULT NULL,
  `marriedStatus` int DEFAULT NULL,
  `spouseClientId` int DEFAULT NULL,
  `rootFolder` varchar(255) DEFAULT NULL,
  `createdTime` timestamp NULL DEFAULT NULL,
  `modifiedTime` timestamp NULL DEFAULT NULL,
  `createdBy` int DEFAULT NULL,
  `modifiedBy` int DEFAULT NULL,
  PRIMARY KEY (`clientId`),
  KEY `userId_idx` (`userId`),
  KEY `clientId_idx` (`spouseClientId`),
  KEY `spouseClientId` (`spouseClientId`),
  KEY `tenantId_idx` (`tenantId`),
  KEY `StateId_idx` (`stateid`),
  CONSTRAINT `clientTenantId` FOREIGN KEY (`tenantId`) REFERENCES `_tenants` (`tenantId`),
  CONSTRAINT `clientUserId` FOREIGN KEY (`userId`) REFERENCES `_users` (`userId`),
  CONSTRAINT `StateId` FOREIGN KEY (`stateid`) REFERENCES `_states` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `_clients`
--

LOCK TABLES `_clients` WRITE;
/*!40000 ALTER TABLE `_clients` DISABLE KEYS */;
INSERT INTO `_clients` VALUES (4,'Santhosh',1,'Donthi Santhosh Kumar','Santhosh','Donthi','1989-02-21 00:00:00','123456789F','500090',1,'ho:204, goutham nagar,nizamabad','8125007777','santhosh@gmail.com','8125007779','750','750',0,0,'D://a//a','2024-01-09 16:57:04','2024-01-09 16:57:04',1,1),(8,'RamaKrishna',1,'Donthi RamaKrishna Kumar','RamaKrishna','RamaKrishna','1989-02-21 00:00:00','12345675559F','500090',1,'ho:204, goutham nagar,nizamabad','8125007723','santhosh@gmail.com','8125002379','750','750',0,0,'D://a//a','2024-01-09 17:00:48','2024-01-09 17:01:49',1,1);
/*!40000 ALTER TABLE `_clients` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-09 22:34:07
