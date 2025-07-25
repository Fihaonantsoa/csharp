-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : ven. 25 juil. 2025 à 12:33
-- Version du serveur : 10.4.32-MariaDB
-- Version de PHP : 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `voyage`
--

-- --------------------------------------------------------

--
-- Structure de la table `billet`
--

CREATE TABLE `billet` (
  `idbillet` varchar(20) NOT NULL,
  `idvoyageur` varchar(20) NOT NULL,
  `idvol` varchar(20) NOT NULL,
  `idreservation` varchar(20) NOT NULL,
  `classe` varchar(20) NOT NULL,
  `siege` int(11) NOT NULL,
  `prix` int(11) NOT NULL,
  `etat` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `billet`
--

INSERT INTO `billet` (`idbillet`, `idvoyageur`, `idvol`, `idreservation`, `classe`, `siege`, `prix`, `etat`) VALUES
('002', '10', 'Vol 001', '001', '1', 4, 62000, 'payé'),
('2', '1', 'Vol 001', '001', '2', 21, 4500, 'payé');

-- --------------------------------------------------------

--
-- Structure de la table `paiement`
--

CREATE TABLE `paiement` (
  `idpaie` varchar(20) NOT NULL,
  `idreservation` varchar(20) NOT NULL,
  `montant` int(11) NOT NULL,
  `datepaie` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `paiement`
--

INSERT INTO `paiement` (`idpaie`, `idreservation`, `montant`, `datepaie`) VALUES
('001', '001', 2540, '2025-08-11 10:48:27'),
('020', '003', 3200, '2026-07-19 05:30:24');

-- --------------------------------------------------------

--
-- Structure de la table `passager`
--

CREATE TABLE `passager` (
  `id` varchar(50) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `passeport` varchar(50) NOT NULL,
  `nationalite` varchar(50) NOT NULL,
  `telephone` varchar(50) NOT NULL,
  `idreserve` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `passager`
--

INSERT INTO `passager` (`id`, `nom`, `passeport`, `nationalite`, `telephone`, `idreserve`) VALUES
('001', 'azerty', 'pass', 'malagasy', '032 14 5', '004'),
('002', 'Rindra', 'lk oi lkl sqd ', 'malagasy', '02 32 56 48 12', '002');

-- --------------------------------------------------------

--
-- Structure de la table `reservation`
--

CREATE TABLE `reservation` (
  `idreserve` varchar(20) NOT NULL,
  `datereserve` datetime NOT NULL,
  `prix` int(11) NOT NULL,
  `modepaie` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `reservation`
--

INSERT INTO `reservation` (`idreserve`, `datereserve`, `prix`, `modepaie`) VALUES
('001', '2025-07-17 04:42:24', 25000, 'carte'),
('002', '2025-07-11 17:08:27', 7400, 'éspèce'),
('003', '2025-07-17 04:42:44', 25000, 'chèque'),
('004', '2025-07-25 13:14:40', 2500, 'éspèce');

-- --------------------------------------------------------

--
-- Structure de la table `vol`
--

CREATE TABLE `vol` (
  `idvol` varchar(50) NOT NULL,
  `datedep` datetime NOT NULL,
  `datearr` datetime NOT NULL,
  `statut` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `vol`
--

INSERT INTO `vol` (`idvol`, `datedep`, `datearr`, `statut`) VALUES
('Vol 001', '2025-10-08 13:28:28', '2025-10-08 23:28:28', 'en attente'),
('Vol 002', '2025-07-12 13:29:02', '2025-07-13 06:00:02', 'en attente'),
('Vol 003', '2025-07-12 13:29:20', '2025-07-12 16:29:20', 'en attente');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `billet`
--
ALTER TABLE `billet`
  ADD PRIMARY KEY (`idbillet`);

--
-- Index pour la table `paiement`
--
ALTER TABLE `paiement`
  ADD PRIMARY KEY (`idpaie`);

--
-- Index pour la table `passager`
--
ALTER TABLE `passager`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `reservation`
--
ALTER TABLE `reservation`
  ADD PRIMARY KEY (`idreserve`);

--
-- Index pour la table `vol`
--
ALTER TABLE `vol`
  ADD PRIMARY KEY (`idvol`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
