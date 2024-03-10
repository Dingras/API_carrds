-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 10-03-2024 a las 23:31:35
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_carrd`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `invited_to_proyect`
--

CREATE TABLE `invited_to_proyect` (
  `id` int(11) NOT NULL,
  `status` tinyint(1) NOT NULL,
  `id_proyect` int(11) NOT NULL,
  `id_user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `invited_to_proyect`
--

INSERT INTO `invited_to_proyect` (`id`, `status`, `id_proyect`, `id_user`) VALUES
(16, 0, 35, 16),
(17, 0, 35, 17),
(18, 0, 35, 18);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proyects`
--

CREATE TABLE `proyects` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `description` text DEFAULT NULL,
  `created_by` int(11) NOT NULL COMMENT 'clave foranea users[id]',
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `proyects`
--

INSERT INTO `proyects` (`id`, `name`, `description`, `created_by`, `created_at`) VALUES
(35, 'Mi Primer Proyecto (Fernando)', 'Este es mi primer proyecto creado en la aplicacion Carrd', 15, '2024-03-10 22:16:11');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `springs`
--

CREATE TABLE `springs` (
  `id` int(11) NOT NULL,
  `id_proyect` int(11) NOT NULL,
  `title` text NOT NULL,
  `description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `springs`
--

INSERT INTO `springs` (`id`, `id_proyect`, `title`, `description`) VALUES
(24, 35, 'Do To!', 'Tareas para hacer'),
(25, 35, 'Revisión', 'Tareas en estado de revisión'),
(26, 35, 'Terminado', 'Tareas que pasaron el proceso de revisión y se encuentran finalizadas');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tasks`
--

CREATE TABLE `tasks` (
  `id` int(11) NOT NULL,
  `title` text NOT NULL,
  `status` tinyint(1) NOT NULL,
  `id_proyect` int(11) NOT NULL,
  `id_responsible` int(11) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `finalized_at` datetime NOT NULL,
  `time_limit` datetime NOT NULL,
  `info_text` text NOT NULL,
  `id_springs` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tasks`
--

INSERT INTO `tasks` (`id`, `title`, `status`, `id_proyect`, `id_responsible`, `created_at`, `finalized_at`, `time_limit`, `info_text`, `id_springs`) VALUES
(21, 'Base de datos', 0, 35, 15, '2024-03-10 22:20:23', '2024-03-09 20:58:14', '2024-03-11 00:00:00', 'Crear la base de datos para el proyecto', 26),
(22, 'API', 0, 35, 17, '2024-03-10 22:21:39', '2024-03-09 20:58:14', '2024-03-11 00:00:00', 'Crear API para el proyecto', 26),
(23, 'Aplicación web', 0, 35, 16, '2024-03-10 22:22:50', '2024-03-09 20:58:14', '2024-03-11 00:00:00', 'Crear una aplicación web usando React', 26),
(24, 'Evaluación', 0, 35, 18, '2024-03-10 22:24:51', '2024-03-09 20:58:14', '2024-03-11 00:00:00', 'Evaluar proyecto final', 24);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `name` text NOT NULL,
  `last_name` text NOT NULL,
  `email` text NOT NULL,
  `avatar_url` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `name`, `last_name`, `email`, `avatar_url`) VALUES
(15, 'dingras', 'fer', 'Fernando', 'Cosentino', 'feracosentino@gmail.com', 'https://cdn-icons-png.flaticon.com/512/6596/6596121.png'),
(16, 'pato', 'pato', 'Patricio', 'Castro', 'patocastro@gmail.com', 'https://cdn-icons-png.flaticon.com/512/6596/6596121.png'),
(17, 'rama', 'rama', 'Ramiro', 'Sansinanea', 'rama@hilet.com', 'https://cdn-icons-png.flaticon.com/512/6596/6596121.png'),
(18, 'martin', 'martin', 'Martin', 'Moreno', 'martin@hilet.com', 'https://cdn-icons-png.flaticon.com/512/6596/6596121.png');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `invited_to_proyect`
--
ALTER TABLE `invited_to_proyect`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `proyects`
--
ALTER TABLE `proyects`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `springs`
--
ALTER TABLE `springs`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `invited_to_proyect`
--
ALTER TABLE `invited_to_proyect`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `proyects`
--
ALTER TABLE `proyects`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT de la tabla `springs`
--
ALTER TABLE `springs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de la tabla `tasks`
--
ALTER TABLE `tasks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de la tabla `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
