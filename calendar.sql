-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3307
-- Generation Time: Apr 21, 2024 at 05:15 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `calendar`
--

-- --------------------------------------------------------

--
-- Table structure for table `appointment`
--

CREATE TABLE `appointment` (
  `id` int(11) NOT NULL,
  `id_user` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Location` varchar(50) NOT NULL,
  `Start_time` datetime NOT NULL,
  `End_time` datetime NOT NULL,
  `reminder` tinyint(1) NOT NULL,
  `id_group` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `appointment`
--

INSERT INTO `appointment` (`id`, `id_user`, `Name`, `Location`, `Start_time`, `End_time`, `reminder`, `id_group`) VALUES
(7, 5, 'Anh', 'Anh', '2024-04-20 02:00:00', '2024-04-20 03:00:00', 1, 63556),
(7, 6, 'Anh', 'Anh', '2024-04-20 02:00:00', '2024-04-20 03:00:00', 1, 63556),
(8, 5, 'Anh', 'Anh', '2024-04-20 03:00:00', '2024-04-20 04:00:00', 1, 36896),
(8, 7, 'Anh', 'Anh', '2024-04-20 03:00:00', '2024-04-20 04:00:00', 0, 36896),
(9, 5, 'Anh', 'Anh', '2024-04-20 04:00:00', '2024-04-20 05:00:00', 0, NULL),
(10, 6, 'Anh', 'Anhhh', '2024-04-20 03:01:17', '2024-04-20 05:01:17', 1, 69567),
(11, 6, 'Anh', 'Anhh', '2024-03-27 03:01:41', '2024-03-29 03:01:41', 0, NULL),
(12, 7, 'Anh', 'Anh', '2024-04-20 04:00:00', '2024-04-20 05:00:00', 1, 56423),
(13, 5, 'Anh', 'Anh', '2024-04-21 03:09:42', '2024-04-22 03:09:42', 1, NULL),
(14, 7, 'Anh', 'Anh', '2024-04-21 03:11:46', '2024-04-22 03:11:46', 0, NULL),
(15, 7, 'Anh', 'Anh', '2024-04-19 03:12:47', '2024-04-20 00:12:47', 1, 56055),
(16, 5, 'Anh', 'Anh', '2024-04-18 03:22:12', '2024-04-19 03:22:12', 1, 43003),
(17, 5, 'Anh', 'Anh', '2024-04-28 03:32:28', '2024-04-29 03:32:28', 0, NULL),
(19, 5, 'Anh', 'Anh', '2024-04-07 03:36:33', '2024-04-08 03:36:33', 0, NULL),
(20, 5, 'anh', 'anh', '2024-04-02 03:41:53', '2024-04-03 03:41:53', 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `username`) VALUES
(5, 'ngoc anh'),
(6, 'hai'),
(7, 'na');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `appointment`
--
ALTER TABLE `appointment`
  ADD PRIMARY KEY (`id`,`id_user`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `appointment`
--
ALTER TABLE `appointment`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `appointment`
--
ALTER TABLE `appointment`
  ADD CONSTRAINT `id_user` FOREIGN KEY (`id_user`) REFERENCES `user` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
