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
-- Table structure for table `_projects`
--

DROP TABLE IF EXISTS `_projects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `_projects` (
  `id_Projects` int NOT NULL AUTO_INCREMENT,
  `Filename` varchar(50) NOT NULL DEFAULT (uuid()),
  `Loanamount` decimal(8,0) DEFAULT NULL,
  `Assignrdstaff` varchar(50) DEFAULT NULL,
  `Filedescription` varchar(5000) DEFAULT NULL,
  `staffnote` varchar(5000) DEFAULT NULL,
  `primaryborrower` varchar(50) DEFAULT NULL,
  `Primarybusiness` varchar(45) DEFAULT NULL,
  `startdate` date DEFAULT NULL,
  `desiredclosingdate` date DEFAULT NULL,
  `initialphase` varchar(45) DEFAULT NULL,
  `CreateDateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_Projects`),
  KEY `Assignrdstaff_FK_idx` (`Assignrdstaff`),
  KEY `primaryborrower_FK_idx` (`primaryborrower`),
  CONSTRAINT `Assignrdstaff_FK` FOREIGN KEY (`Assignrdstaff`) REFERENCES `_users` (`userId`),
  CONSTRAINT `primaryborrower_FK` FOREIGN KEY (`primaryborrower`) REFERENCES `_users` (`userId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `_projects`
--

LOCK TABLES `_projects` WRITE;
/*!40000 ALTER TABLE `_projects` DISABLE KEYS */;
INSERT INTO `_projects` VALUES (3,'File 1',300,'1','test string','test string','1','testing string','2024-01-01','2024-01-01','test string','2024-01-01 16:41:32'),(4,'File 2',500,'1','test string','test string','1','testing string','2024-01-01','2024-01-01','test string','2024-01-01 16:42:17');
/*!40000 ALTER TABLE `_projects` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-09 22:34:08
