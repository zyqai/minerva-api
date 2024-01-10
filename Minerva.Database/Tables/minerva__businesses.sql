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
-- Table structure for table `_businesses`
--

DROP TABLE IF EXISTS `_businesses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `_businesses` (
  `businessId` int NOT NULL,
  `tenantId` int NOT NULL,
  `businessName` varchar(255) DEFAULT NULL,
  `businessAddress` varchar(255) DEFAULT NULL,
  `businessType` varchar(45) DEFAULT NULL,
  `industry` varchar(45) DEFAULT NULL,
  `annualRevenue` decimal(6,2) DEFAULT NULL,
  `incorporationDate` date DEFAULT NULL,
  `businessRegistrationNumber` varchar(255) DEFAULT NULL,
  `rootDocumentFolder` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`businessId`),
  KEY `tenantId_idx` (`tenantId`),
  CONSTRAINT `businessTenantId` FOREIGN KEY (`tenantId`) REFERENCES `_tenants` (`tenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `_businesses`
--

LOCK TABLES `_businesses` WRITE;
/*!40000 ALTER TABLE `_businesses` DISABLE KEYS */;
INSERT INTO `_businesses` VALUES (1,1,'Santhosh','HYD','Kiran','Kiran',200.00,'2024-01-04','123456','~\\ts\\a'),(2,1,'sarithD','NZB','Kiran','Kiran',300.00,'2024-01-04','123456','~\\ts\\a');
/*!40000 ALTER TABLE `_businesses` ENABLE KEYS */;
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
