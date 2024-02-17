-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: _zyq
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
-- Table structure for table `_users`
--

DROP TABLE IF EXISTS `_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `_users` (
  `userId` varchar(45) NOT NULL,
  `tenantId` int DEFAULT NULL,
  `userName` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `isActive` tinyint DEFAULT '1',
  `isDeleted` tinyint DEFAULT '0',
  `createTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modifiedTime` timestamp NULL DEFAULT NULL,
  `createdBy` varchar(45) DEFAULT NULL,
  `modifiedBy` varchar(45) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `notificationsEnabled` tinyint DEFAULT NULL,
  `mfaEnabled` tinyint DEFAULT NULL,
  `isTenantUser` int DEFAULT NULL,
  `isAdminUser` int DEFAULT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) DEFAULT NULL,
  `Roles` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`userId`),
  KEY `userTenantId_idx` (`tenantId`),
  CONSTRAINT `userTenantId` FOREIGN KEY (`tenantId`) REFERENCES `_tenants` (`tenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `_users`
--

LOCK TABLES `_users` WRITE;
/*!40000 ALTER TABLE `_users` DISABLE KEYS */;
INSERT INTO `_users` VALUES ('2351ee03-cd32-11ee-b44c-cc4740007297',-1,'santhoshdonthi@gmail.com','santhoshdonthi@gmail.com',1,0,'2024-02-17 01:16:11',NULL,'bc18ffb9-cd30-11ee-b44c-cc4740007297',NULL,'8125007778',1,1,1,1,'santhosh kumar','donthi','TentntAdmin'),('8cbf58c4-cd38-11ee-b44c-cc4740007297',-1,'hiiamsanthu@gmail.com','hiiamsanthu@gmail.com',1,0,'2024-02-17 02:02:05',NULL,'bc18ffb9-cd30-11ee-b44c-cc4740007297',NULL,'8125007778',1,1,1,1,'santhosh kumar','donthi','admin'),('bc18ffb9-cd30-11ee-b44c-cc4740007297',-1,'rpthouti@gmail.com','rpthouti@gmail.com',1,0,'2024-02-17 01:06:09',NULL,'rpthouti@gmail.com',NULL,'8125007778',1,1,1,1,'santhosh kumar','donthi','admin');
/*!40000 ALTER TABLE `_users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-17  7:39:30
