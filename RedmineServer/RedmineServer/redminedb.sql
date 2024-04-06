-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Ápr 06. 18:12
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `redminedb`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `developers`
--

CREATE TABLE `developers` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `developers`
--

INSERT INTO `developers` (`id`, `name`, `email`) VALUES
(1, 'Dev A', 'deva@example.com'),
(2, 'Dev B', 'devb@example.com'),
(3, 'Developer C', 'devc@example.com'),
(4, 'Developer D', 'devd@example.com');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `managers`
--

CREATE TABLE `managers` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `managers`
--

INSERT INTO `managers` (`id`, `name`, `email`, `password`) VALUES
(1, 'John Doe', 'johndoe@example.com', 'securepassword123'),
(2, 'Jane Smith', 'janesmith@example.com', 'securepassword456'),
(3, 'Alex Johnson', 'alexj@example.com', 'password789'),
(4, 'Samantha Ray', 'samanthar@example.com', 'password012');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `projects`
--

CREATE TABLE `projects` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `type_id` int(11) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `projects`
--

INSERT INTO `projects` (`id`, `name`, `type_id`, `description`) VALUES
(1, 'Project 1', 1, 'This is a web development project.'),
(2, 'Project 2', 2, 'This is a mobile app development project.'),
(3, 'Project 3', 3, 'This is a desktop application development project.'),
(4, 'Project 4', 4, 'This is a database design project.');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `project_developers`
--

CREATE TABLE `project_developers` (
  `developer_id` int(11) DEFAULT NULL,
  `project_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `project_developers`
--

INSERT INTO `project_developers` (`developer_id`, `project_id`) VALUES
(1, 1),
(2, 2),
(1, 3),
(2, 4),
(3, 1),
(4, 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `project_types`
--

CREATE TABLE `project_types` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `project_types`
--

INSERT INTO `project_types` (`id`, `name`) VALUES
(1, 'Web Development'),
(2, 'Mobile App Development'),
(3, 'Desktop Application Development'),
(4, 'Database Design');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tasks`
--

CREATE TABLE `tasks` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `project_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `deadline` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `tasks`
--

INSERT INTO `tasks` (`id`, `name`, `description`, `project_id`, `user_id`, `deadline`) VALUES
(1, 'Task 1 for Project 1', 'This is the first task for Project 1.', 1, 1, '2023-12-31 00:00:00'),
(2, 'Task 1 for Project 2', 'This is the first task for Project 2.', 2, 2, '2024-01-15 00:00:00'),
(3, 'Task 2 for Project 1', 'Another task for Project 1.', 1, 1, '2023-12-15 00:00:00'),
(4, 'Task 2 for Project 2', 'Another task for Project 2.', 2, 2, '2024-01-30 00:00:00'),
(5, 'Task for Project 3', 'Task for Project 3.', 3, 3, '2023-11-30 00:00:00'),
(6, 'Task for Project 4', 'Task for Project 4.', 4, 4, '2024-02-15 00:00:00');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `developers`
--
ALTER TABLE `developers`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `managers`
--
ALTER TABLE `managers`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `projects`
--
ALTER TABLE `projects`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `project_developers`
--
ALTER TABLE `project_developers`
  ADD KEY `project_id` (`project_id`),
  ADD KEY `developer_id` (`developer_id`);

--
-- A tábla indexei `project_types`
--
ALTER TABLE `project_types`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`id`),
  ADD KEY `project_id` (`project_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `projects`
--
ALTER TABLE `projects`
  ADD CONSTRAINT `projects_ibfk_1` FOREIGN KEY (`id`) REFERENCES `project_types` (`id`);

--
-- Megkötések a táblához `project_developers`
--
ALTER TABLE `project_developers`
  ADD CONSTRAINT `project_developers_ibfk_1` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`),
  ADD CONSTRAINT `project_developers_ibfk_2` FOREIGN KEY (`developer_id`) REFERENCES `developers` (`id`);

--
-- Megkötések a táblához `tasks`
--
ALTER TABLE `tasks`
  ADD CONSTRAINT `tasks_ibfk_1` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`),
  ADD CONSTRAINT `tasks_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `managers` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
