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
-- Table structure for table `_states`
--

DROP TABLE IF EXISTS `_states`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `_states` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` char(2) NOT NULL DEFAULT '',
  `name` varchar(128) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `_states`
--

LOCK TABLES `_states` WRITE;
/*!40000 ALTER TABLE `_states` DISABLE KEYS */;
INSERT INTO `_states` VALUES (1,'AL','Alabama'),(2,'AK','Alaska'),(3,'AS','American Samoa'),(4,'AZ','Arizona'),(5,'AR','Arkansas'),(6,'CA','California'),(7,'CO','Colorado'),(8,'CT','Connecticut'),(9,'DE','Delaware'),(10,'DC','District of Columbia'),(11,'FM','Federated _states of Micronesia'),(12,'FL','Florida'),(13,'GA','Georgia'),(14,'GU','Guam'),(15,'HI','Hawaii'),(16,'ID','Idaho'),(17,'IL','Illinois'),(18,'IN','Indiana'),(19,'IA','Iowa'),(20,'KS','Kansas'),(21,'KY','Kentucky'),(22,'LA','Louisiana'),(23,'ME','Maine'),(24,'MH','Marshall Islands'),(25,'MD','Maryland'),(26,'MA','Massachusetts'),(27,'MI','Michigan'),(28,'MN','Minnesota'),(29,'MS','Mississippi'),(30,'MO','Missouri'),(31,'MT','Montana'),(32,'NE','Nebraska'),(33,'NV','Nevada'),(34,'NH','New Hampshire'),(35,'NJ','New Jersey'),(36,'NM','New Mexico'),(37,'NY','New York'),(38,'NC','North Carolina'),(39,'ND','North Dakota'),(40,'MP','Northern Mariana Islands'),(41,'OH','Ohio'),(42,'OK','Oklahoma'),(43,'OR','Oregon'),(44,'PW','Palau'),(45,'PA','Pennsylvania'),(46,'PR','Puerto Rico'),(47,'RI','Rhode Island'),(48,'SC','South Carolina'),(49,'SD','South Dakota'),(50,'TN','Tennessee'),(51,'TX','Texas'),(52,'UT','Utah'),(53,'VT','Vermont'),(54,'VI','Virgin Islands'),(55,'VA','Virginia'),(56,'WA','Washington'),(57,'WV','West Virginia'),(58,'WI','Wisconsin'),(59,'WY','Wyoming');
/*!40000 ALTER TABLE `_states` ENABLE KEYS */;
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
