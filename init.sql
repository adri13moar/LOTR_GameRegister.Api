/* LOTR Game Register - Fixed Initialization Script
   Standardized for Docker deployment with manual IDs from Excel.
*/

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LOTR_GameRegister')
BEGIN
    CREATE DATABASE LOTR_GameRegister;
END
GO

USE LOTR_GameRegister;
GO

-- 1. DROP TABLES (Ordered by dependencies to avoid FK errors)
IF OBJECT_ID('dbo.GameHeroes', 'U') IS NOT NULL DROP TABLE dbo.GameHeroes;
IF OBJECT_ID('dbo.Games', 'U') IS NOT NULL DROP TABLE dbo.Games;
IF OBJECT_ID('dbo.Heroes', 'U') IS NOT NULL DROP TABLE dbo.Heroes;
IF OBJECT_ID('dbo.Quests', 'U') IS NOT NULL DROP TABLE dbo.Quests;
IF OBJECT_ID('dbo.Cycles', 'U') IS NOT NULL DROP TABLE dbo.Cycles;
IF OBJECT_ID('dbo.Spheres', 'U') IS NOT NULL DROP TABLE dbo.Spheres;
IF OBJECT_ID('dbo.Results', 'U') IS NOT NULL DROP TABLE dbo.Results;
IF OBJECT_ID('dbo.Difficulties', 'U') IS NOT NULL DROP TABLE dbo.Difficulties;
IF OBJECT_ID('dbo.ReasonsForDefeat', 'U') IS NOT NULL DROP TABLE dbo.ReasonsForDefeat;

-- 2. CREATE SCHEMA (Removed IDENTITY to use your Excel IDs)
CREATE TABLE [Spheres] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [Name_es] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Cycles] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Name_es] NVARCHAR(100) NOT NULL,
    [Category] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Quests] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Name_es] NVARCHAR(100) NOT NULL,
    [CycleId] INT FOREIGN KEY REFERENCES [Cycles](Id),
    [CommunityDifficulty] DECIMAL(3,2) NULL
);

CREATE TABLE [Heroes] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Name_es] NVARCHAR(100) NOT NULL,
    [SphereId] INT FOREIGN KEY REFERENCES [Spheres](Id),
    [StartingThreat] INT NOT NULL,
    [RingsDbId] NVARCHAR(50) NULL
);

CREATE TABLE [Results] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [Name_es] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Difficulties] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [Name_es] NVARCHAR(50) NOT NULL
);

CREATE TABLE [ReasonsForDefeat] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Name_es] NVARCHAR(100) NOT NULL
);

CREATE TABLE [Games] (
    [Id] INT PRIMARY KEY IDENTITY(1,1), -- Games will keep auto-ID
    [DatePlayed] DATE NOT NULL,
    [IsCampaignMode] BIT NOT NULL,
    [QuestId] INT FOREIGN KEY REFERENCES [Quests](Id),
    [DifficultyId] INT FOREIGN KEY REFERENCES [Difficulties](Id),
    [Spheres] INT NOT NULL,
    [DeadHeroes] INT NOT NULL,
    [ResultId] INT FOREIGN KEY REFERENCES [Results](Id),
    [ReasonForDefeatId] INT FOREIGN KEY REFERENCES [ReasonsForDefeat](Id) NULL,
    [Notes] NVARCHAR(MAX) NULL
);

-- 3. INSERT SEED DATA (Standardizing IDs with your Excel)

-- Spheres (Mapping based on your data)
INSERT INTO [Spheres] ([Id], [Name], [Name_es]) VALUES 
(1, 'Neutral', 'Neutral'), 
(2, 'Leadership', 'Liderazgo'), 
(3, 'Tactics', 'Táctica'), 
(4, 'Spirit', 'Espíritu'), 
(5, 'Lore', 'Saber'),
(6, 'Baggins', 'Bolsón'),
(7, 'Fellowship', 'Comunidad'),
(8, 'Council', 'Consejo');

-- Results
INSERT INTO [Results] ([Id], [Name], [Name_es]) VALUES 
(1, 'Win', 'Victoria'), 
(2, 'Loss', 'Derrota'), 
(3, 'Surrender', 'Abandono');

-- Reasons for Defeat
INSERT INTO [ReasonsForDefeat] ([Id], [Name], [Name_es]) VALUES 
(1, 'Heroes Defeated', 'Héroes muertos'),
(2, 'Threat Elimination', 'Amenaza'),
(3, 'Quest Condition', 'Condición de escenario'),
(4, 'Campaign Condition', 'Condición de campaña'),
(5, 'Location Block', 'Bloqueo de lugar'),
(6, 'Cheating', 'Trampitas');

-- Difficulties
INSERT INTO [Difficulties] ([Id], [Name], [Name_es]) VALUES 
(1, 'Easy', 'Fácil'), 
(2, 'Normal', 'Normal'), 
(3, 'Nightmare', 'Pesadilla');

-- Cycles (Example IDs from your list)
INSERT INTO [Cycles] ([Id], [Name], [Name_es], [Category]) VALUES 
(101, 'Shadows of Mirkwood', 'Sombras del Bosque Negro', 'Official'),
(102, 'Dwarrowdelf', 'La mina del Enano', 'Official'),
(103, 'Against the Shadow', 'Contra la Sombra', 'Official'),
(104, 'The Ring-maker', 'El Creador del Anillo', 'Official'),
(105, 'Angmar Awakened', 'Angmar Despertado', 'Official'),
(106, 'Dream-chaser', 'Cazador de Sueños', 'Official'),
(107, 'Haradrim', 'Los Haradrim', 'Official'),
(108, 'Ered Mithrin', 'Ered Mithrin', 'Official'),
(109, 'Vengeance of Mordor', 'La Venganza de Mordor', 'Official'),
(201, 'The Hobbit Saga', 'Saga El Hobbit', 'Hobbit Saga'),
(301, 'The Fellowship of the Ring', 'La Comunidad del Anillo', 'LOTR Saga'),
(302, 'The Two Towers', 'Las Dos Torres', 'LOTR Saga'),
(303, 'The Return of the King', 'El Retorno del Rey', 'LOTR Saga'),
(401, 'Print on Demand', 'Print on Demand', 'PoD'),
(501, 'Children of Eorl', 'Los Hijos de Eorl', 'ALEP'),
(502, 'Oaths of the Rohirrim', 'Juramentos de los Rohirrim', 'ALEP');

-- =====================================================
-- SEEDING ALL 121 QUESTS FROM EXCEL
-- =====================================================

-- 1. Shadows of Mirkwood Cycle (101)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(1, 'Passage Through Mirkwood', 'A través del Bosque Negro', 101, 2.4),
(2, 'Journey Down the Anduin', 'Travesía por el Anduin', 101, 5.4),
(3, 'Escape from Dol Guldur', 'Evasión de Dol Guldur', 101, 7.7),
(4, 'The Oath', 'La Promesa', 101, 3.5),
(5, 'The Caves of Nibin-Dûm', 'Las cuevas de Nibin-Dûm', 101, 4.9),
(6, 'The Hunt for Gollum', 'La Caza de Gollum', 101, 4.2),
(7, 'Conflict at the Carrock', 'Conflicto en la Carroca', 101, 6.2),
(8, 'A Journey to Rhosgobel', 'Viaje a Rhosgobel', 101, 5.9),
(9, 'The Hills of Emyn Muil', 'Las Colinas de Emyn Muil', 101, 2.8),
(10, 'The Dead Marshes', 'Las Ciénagas de los Muertos', 101, 4.1),
(11, 'Return to Mirkwood', 'Regreso al Bosque negro', 101, 7.1);

-- 2. Dwarrowdelf Cycle (102)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(12, 'Into the Pit', 'En el pozo', 102, 4.8),
(13, 'The Seventh Level', 'El Séptimo Nivel', 102, 4.2),
(14, 'Flight from Moria', 'Huida de Moria', 102, 6.0),
(15, 'The Redhorn Gate', 'La Puerta del Cuerno Rojo', 102, 5.4),
(16, 'Road to Rivendell', 'Camino a Rivendel', 102, 5.2),
(17, 'The Watcher in the Water', 'El Guardián del Agua', 102, 6.2),
(18, 'The Long Dark', 'La Larga oscuridad', 102, 3.7),
(19, 'Foundations of Stone', 'Cimientos de piedra', 102, 5.9),
(20, 'Shadow and Flame', 'Sombra y llama', 102, 7.6);

-- 3. Against the Shadow Cycle (103)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(58, 'The Steward''s Fear', 'El Temor del Senescal', 103, 5.2),
(59, 'The Druadan Forest', 'El Bosque de Druadan', 103, 4.4),
(60, 'Encounter at Amon Dîn', 'Encuentro en Amon Din', 103, 3.4),
(61, 'Assault on Osgiliath', 'Asalto a Osgiliath', 103, 7.3),
(62, 'The Blood of Gondor', 'La Sangre de Gondor', 103, 6.4),
(63, 'The Morgul Vale', 'El Valle de Morgul', 103, 8.2),
(107, 'Peril in Pelargir', 'Peligro en Pelargir', 103, 5.1),
(108, 'Into Ithilien', 'A Ithilien', 103, 7.8),
(109, 'The Siege of Cair Andros', 'El Asedio de Cair Andros', 103, 7.5);

-- 4. The Ring-maker Cycle (104)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(64, 'The Fords of Isen', 'Los Vados del Isen', 104, 5.0),
(65, 'The Toils of Dunland', 'La Trampa de las Tierras Brunas', 104, 5.5),
(66, 'The Three Trials', 'Las Tres Pruebas', 104, 7.1),
(67, 'Trouble in Tharbad', 'Problemas en Tharbad', 104, 4.9),
(68, 'The Nin-in-Eilph', 'Nin-in-Eilph', 104, 6.3),
(69, 'Celebrimbor''s Secret', 'El Secreto de Celebrimbor', 104, 6.7),
(70, 'The Antlered Crown', 'La Corona Astada', 104, 6.5),
(111, 'To Catch an Orc', 'Capturar a un Orco', 104, 5.6),
(112, 'Into Fangorn', 'A Fangorn', 104, 5.2);

-- 5. Angmar Awakened Cycle (105)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(71, 'Intruders in Chetwood', 'Intrusos en el Bosque de Chet', 105, 4.1),
(72, 'Weathered Rocks', 'Las Colinas de los Vientos', 105, 5.2),
(73, 'Deadman''s Dike', 'Muros de los Muertos', 105, 7.4),
(74, 'Wastes of Eriador', 'Los Páramos de Eriador', 105, 6.8),
(75, 'Escape from Mount Gram', 'Huida del Monte Gram', 105, 5.5),
(76, 'Across the Ettenmoors', 'A través de las Landas de Etten', 105, 5.7),
(77, 'The Treachery of Rhudaur', 'La Traición de Rhudaur', 105, 7.6),
(78, 'The Battle of Carn Dûm', 'La Batalla de Carn Dum', 105, 9.2),
(79, 'The Dread Realm', 'El Reino del Terror', 105, 8.5);

-- 6. Dream-chaser Cycle (106)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(80, 'Flight of the Stormcaller', 'La Huida de la Tormenta', 106, 4.8),
(81, 'The Thing in the Depths', 'La Cosa de las Profundidades', 106, 6.2),
(82, 'Temple of the Deceived', 'El Templo de los Engañados', 106, 6.9),
(83, 'The Drowned Ruins', 'Las Ruinas Anegadas', 106, 6.5),
(84, 'A Storm on Cobas Haven', 'Tormenta en la Bahía Cobas', 106, 7.3),
(85, 'The City of Corsairs', 'La Ciudad de los Corsarios', 106, 7.5),
(116, 'Voyage Across Belegaer', 'Travesía por el Belager', 106, 5.5),
(117, 'The Fate of Numenor', 'El Destino de Númenor', 106, 6.1),
(118, 'Raid on the Grey Havens', 'Incursión en los Puertos Grises', 106, 5.9);

-- 7. Haradrim Cycle (107)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(86, 'The Mumakil', 'Los Mümakil', 107, 5.4),
(87, 'Race Across Harad', 'Carrera por Harad', 107, 6.1),
(88, 'Beneath the Sands', 'Bajo las Arenas', 107, 5.9),
(89, 'The Black Serpent', 'La Serpiente Negra', 107, 6.8),
(90, 'The Dungeons of Cirith Gurat', 'Las Mazmorras de Cirith Gurat', 107, 7.2),
(91, 'The Crossings of Poros', 'Los Cruces del Poros', 107, 7.4),
(119, 'Escape from Umbar', 'Huida de Umbar', 107, 6.2),
(120, 'Desert Crossing', 'Travesía por el desierto', 107, 6.4),
(121, 'The Long Arm of Mordor', 'El Largo Brazo de Mordor', 107, 7.1);

-- 8. Ered Mithrin Cycle (108)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(92, 'The Withered Heath', 'El Brezal Marchito', 108, 5.8),
(93, 'Roam Across Rhovanion', 'Recorrido por Rhovanion', 108, 6.2),
(94, 'Fire in the Night', 'Fuego en la Noche', 108, 7.8),
(95, 'The Ghost of Framsburg', 'El Fantasma de Framsburgo', 108, 7.5),
(96, 'Mount Gundabad', 'El Monte Gundabad', 108, 8.4),
(97, 'The Fate of Wilderland', 'El Destino de las Tierras Ásperas', 108, 7.2),
(122, 'Journey Up the Anduin', 'Remontando el Anduin', 108, 5.4),
(123, 'Lost in Mirkwood', 'Perdidos en el Bosque Negro', 108, 5.8),
(124, 'The King''s Quest', 'La Misión del Rey', 108, 6.3);

-- 9. Vengeance of Mordor Cycle (109)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(98, 'Wrath and Ruin', 'Ira y Perdición', 109, 6.5),
(99, 'The City of Ulfast', 'La Ciudad de Ulfast', 109, 6.2),
(100, 'Challenge of the Wainriders', 'El desafío de los Aurigas', 109, 6.7),
(101, 'Under the Ash Mountains', 'Bajo las Montañas de Ceniza', 109, 7.8),
(102, 'The Land of Sorrow', 'La Tierra del Pesar', 109, 7.4),
(103, 'The Fortress of Nurn', 'La Fortaleza de Nurn', 109, 9.4),
(125, 'The River Running', 'El Río Rápido', 109, 5.9),
(126, 'Danger in Dorwinion', 'Peligro en Dorwinion', 109, 6.5),
(127, 'The Temple of Doom', 'El Templo Maldito', 109, 7.8);

-- 10. The Hobbit Saga (201)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(21, 'We Must Away, Ere Break of Day', 'Hemos de ir, antes de que el día nazca', 201, 6.4),
(22, 'Over the Misty Mountains Grim', 'Más allá de las hoscas y brumosas', 201, 5.5),
(23, 'Dungeons Deep and Caverns Dim', 'A mazmorras profundas y cavernas antiguas', 201, 6.3),
(24, 'Flies and Spiders', 'Moscas y arañas', 201, 6.1),
(25, 'The Lonely Mountain', 'La Montaña solitaria', 201, 7.0),
(26, 'The Battle of Five Armies', 'La Batalla de los Cinco Ejércitos', 201, 7.2);

-- 11. LOTR Saga (301, 302, 303)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(27, 'A Shadow of the Past', 'Una Sombra del Pasado', 301, 6.2),
(28, 'The Old Forest', 'El Bosque Viejo', 301, 5.7),
(29, 'Fog on the Barrow-downs', 'Niebla en las Quebradas de los Túmulos', 301, 7.5),
(30, 'A Knife in the Dark', 'Un Cuchillo en la Oscuridad', 301, 6.8),
(31, 'Flight to the Ford', 'Huyendo hacia el Vado', 301, 5.6),
(32, 'The Ring Goes South', 'El Anillo va hacia el Sur', 301, 6.1),
(33, 'Journey in the Dark', 'Viaje a la Oscuridad', 301, 8.0),
(34, 'Breaking of the Fellowship', 'La Disolución de la Comunidad', 301, 7.2),
(35, 'The Uruk-hai', 'Los Uruk-hai', 302, 6.3),
(36, 'Helm''s Deep', 'El Abismo de Helm', 302, 8.3),
(37, 'The Road to Isengard', 'El Camino a Isengard', 302, 6.6),
(38, 'The Passage of the Marshes', 'A Través de las Ciénagas', 302, 6.4),
(39, 'Journey to the Cross-roads', 'Viaje a la Encrucijada', 302, 8.3),
(40, 'Shelob''s Lair', 'El Antro de Ella-Laraña', 302, 7.6),
(41, 'The Passage of the Grey Company', 'El paso de la Compañía Gris', 303, 6.5),
(42, 'The Siege of Gondor', 'El Sitio de Gondor', 303, 6.5),
(43, 'The Battle of the Pelennor Fields', 'La Batalla de los Campos del Pelennor', 303, 8.0),
(44, 'The Tower of Cirith Ungol', 'La Torre de Cirith Ungol', 303, 7.3),
(45, 'The Black Gate Opens', 'La Puerta Negra se abre', 303, 9.1),
(46, 'Mount Doom', 'El Monte del Destino', 303, 9.1),
(47, 'The Scouring of the Shire', 'El Saneamiento de la Comarca', 303, 7.0);

-- 12. Print on Demand (401)
INSERT INTO [Quests] ([Id], [Name], [Name_es], [CycleId], [CommunityDifficulty]) VALUES 
(48, 'The Battle of Lake-town', 'La Batalla de Ciudad del Lago', 401, 8.9),
(49, 'Massing at Osgiliath', 'Concentración en Osgiliath', 401, 7.3),
(50, 'The Stone of Erech', 'La Piedra de Erech', 401, 6.5),
(51, 'The Ruins of Belegost', 'Las Runias de Belegost', 401, 8.6),
(52, 'Murder at the Prancing Pony', 'Asesinato en el Poney Pisador', 401, 7.6),
(53, 'The Siege of Annúminas', 'El Sitio de Annuminas', 401, 8.2),
(54, 'Attack on Dol Guldur', 'Ataque a Dol Guldur', 401, 9.3),
(55, 'The Hunt for the Dreadnaught', 'La Caza del Dreadnaught', 401, 4.6),
(56, 'The Nine are Abroad', 'Los Nueve han salido', 401, 9.0),
(57, 'The Siege of Erebor', 'El Asedio de Erebor', 401, 7.0);

GO

-- Heroes (Using SphereId as FK)
INSERT INTO [Heroes] ([Id], [Name], [Name_es], [SphereId], [StartingThreat], [RingsDbId]) VALUES 
(1, 'Amarthiúl', 'Amarthiúl', 2, 10, '10001'),
(2, 'Aragorn (Spirit)', 'Aragorn (Espíritu)', 4, 12, '10021'),
(3, 'Aragorn (Leadership)', 'Aragorn (Liderazgo)', 2, 12, '01001'),
(4, 'Aragorn (Lore)', 'Aragorn (Saber)', 5, 12, '02001'),
(5, 'Aragorn (Tactics)', 'Aragorn (Táctica)', 3, 12, '06001'),
(6, 'Argalad', 'Argalad', 5, 9, '145001'),
(7, 'Arwen Undómiel', 'Arwen Undómiel', 4, 9, '10002'),
(8, 'Balin', 'Balin', 2, 9, '03002'),
(9, 'Treebeard', 'Bárbol', 5, 13, '10003'),
(10, 'Bard the Bowman', 'Bardo el Arquero', 3, 11, '03051'),
(11, 'Bard son of Brand', 'Bardo hijo de Brand', 5, 11, '172001'),
(12, 'Beorn', 'Beorn', 3, 12, '02005'),
(13, 'Beravor', 'Beravor', 5, 10, '01008'),
(14, 'Beregond (Spirit)', 'Beregond (Espíritu)', 4, 10, '121002'),
(15, 'Beregond (Tactics)', 'Beregond (Táctica)', 3, 10, '05001'),
(16, 'Bifur', 'Bifur', 5, 7, '03007'),
(17, 'Bilbo Baggins (Tactics)', 'Bilbo Bolsón (Táctica)', 3, 9, '164001'),
(18, 'Bilbo Baggins (Lore)', 'Bilbo Bolsón (Saber)', 5, 9, '02004'),
(19, 'Bombur', 'Bombur', 5, 8, '03053'),
(20, 'Boromir (Leadership)', 'Boromir (Liderazgo)', 2, 11, '05002'),
(21, 'Boromir (Tactics)', 'Boromir (Táctica)', 3, 11, '02002'),
(22, 'Brand son of Bain (Leadership)', 'Brand hijo de Bain (Lid.)', 2, 10, '02072'),
(23, 'Brand son of Bain (Tactics)', 'Brand hijo de Bain (Tác.)', 3, 10, '171001'),
(24, 'Caldara', 'Caldara', 4, 8, '06107'),
(25, 'Celeborn', 'Celeborn', 2, 11, '08001'),
(26, 'Círdan the Shipwright', 'Círdan el Carpintero de Ribera', 4, 12, '12001'),
(27, 'Dáin Ironfoot (Spirit)', 'Dáin Pie de Hierro (Esp.)', 4, 11, '165001'),
(28, 'Dáin Ironfoot (Leadership)', 'Dáin Pie de Hierro (Lid.)', 2, 11, '02116'),
(29, 'Damrod', 'Damrod', 5, 9, '10003'),
(30, 'Denethor (Leadership)', 'Denethor (Liderazgo)', 2, 8, '122001'),
(31, 'Denethor (Lore)', 'Denethor (Saber)', 5, 8, '01010'),
(32, 'Dori', 'Dori', 5, 10, '03009'),
(33, 'Dúnhere', 'Dúnhere', 4, 8, '01009'),
(34, 'Dwalin', 'Dwalin', 4, 9, '03001'),
(35, 'Eleanor', 'Eleanor', 4, 7, '01011'),
(36, 'Elfhelm', 'Elfhelm', 2, 10, '12002'),
(37, 'Elladan', 'Elladan', 3, 10, '03028'),
(38, 'Elrohir', 'Elrohir', 2, 10, '03052'),
(39, 'Elrond', 'Elrond', 5, 13, '03128'),
(40, 'Éomer (Leadership)', 'Éomer (Liderazgo)', 2, 10, '144001'),
(41, 'Éomer (Tactics)', 'Éomer (Táctica)', 3, 10, '06106'),
(42, 'Éowyn (Spirit)', 'Éowyn (Espíritu)', 4, 9, '01007'),
(43, 'Éowyn (Tactics)', 'Éowyn (Táctica)', 3, 9, '17002'),
(44, 'Erestor', 'Erestor', 5, 10, '10002'),
(45, 'Erkenbrand', 'Erkenbrand', 2, 10, '09002'),
(46, 'Faramir (Leadership)', 'Faramir (Liderazgo)', 2, 11, '06002'),
(47, 'Faramir (Lore)', 'Faramir (Saber)', 5, 11, '143001'),
(48, 'Fastred', 'Fastred', 4, 9, '142001'),
(49, 'Folco Boffin', 'Folco Boffin', 5, 7, '123001'),
(50, 'Frodo Baggins (Spirit)', 'Frodo Bolsón (Espíritu)', 4, 7, '02025'),
(51, 'Frodo Baggins (Leadership)', 'Frodo Bolsón (Liderazgo)', 2, 7, '10005'),
(52, 'Galadriel', 'Galadriel', 4, 9, '08146'),
(53, 'Galdor from the Havens', 'Galdor de los Puertos', 5, 9, '12003'),
(54, 'Gandalf', 'Gandalf', 1, 14, '09000'),
(55, 'Gildor Inglorion', 'Gildor Inglorion', 5, 9, '147001'),
(56, 'Gimli (Leadership)', 'Gimli (Liderazgo)', 2, 11, '17001'),
(57, 'Gimli (Tactics)', 'Gimli (Táctica)', 3, 11, '01004'),
(58, 'Gloín', 'Gloín', 2, 9, '01003'),
(59, 'Glorfindel (Spirit)', 'Glorfindel (Espíritu)', 4, 5, '03101'),
(60, 'Glorfindel (Lore)', 'Glorfindel (Saber)', 5, 12, '01006'),
(61, 'Fatty Bolger', 'Gordo Bolger', 4, 7, '06005'),
(62, 'Gríma', 'Gríma', 5, 9, '06003'),
(63, 'Grimbeorn the Old', 'Grimbeorn el Viejo', 3, 11, '146001'),
(64, 'Gwaihir', 'Gwaihir', 3, 13, '08108'),
(65, 'Halbarad', 'Halbarad', 2, 10, '03081'),
(66, 'Haldan', 'Haldan', 5, 10, '174001'),
(67, 'Haldir of Lórien', 'Haldir de Lórien', 5, 9, '06056'),
(68, 'Háma', 'Háma', 3, 9, '03006'),
(69, 'Hirgon', 'Hirgon', 3, 9, '143002'),
(70, 'Hirluin the Fair', 'Hirluin el Hermoso', 2, 8, '06079'),
(71, 'Idraen', 'Idraen', 4, 11, '08025'),
(72, 'Kahliel', 'Kahliel', 2, 10, '142002'),
(73, 'Lanwyn', 'Lanwyn', 4, 9, '144002'),
(74, 'Legolas (Spirit)', 'Legolas (Espíritu)', 4, 9, '122002'),
(75, 'Legolas (Tactics)', 'Legolas (Táctica)', 3, 9, '01005'),
(76, 'Lothíriel', 'Lothíriel', 4, 8, '172002'),
(77, 'Mablung', 'Mablung', 3, 10, '08084'),
(78, 'Merry (Spirit)', 'Merry (Espíritu)', 4, 6, '10004'),
(79, 'Merry (Tactics)', 'Merry (Táctica)', 3, 6, '06004'),
(80, 'Mirlonde', 'Mirlonde', 5, 8, '03032'),
(81, 'Na''asiyah', 'Na''asiyah', 3, 8, '121002'),
(82, 'Nori', 'Nori', 4, 9, '03082'),
(83, 'Óin', 'Óin', 4, 8, '0102'),
(84, 'Ori', 'Ori', 5, 8, '03005'),
(85, 'Pippin (Spirit)', 'Pippin (Espíritu)', 4, 6, '1108'),
(86, 'Pippin (Lore)', 'Pippin (Saber)', 5, 6, '06006'),
(87, 'Prince Imrahil (Leadership)', 'Príncipe Imrahil (Lid.)', 2, 11, '02050'),
(88, 'Prince Imrahil (Tactics)', 'Príncipe Imrahil (Tác.)', 3, 11, '123002'),
(89, 'Radagast', 'Radagast', 5, 11, '124001'),
(90, 'Quickbeam', 'Ramaviva', 3, 9, '10006'),
(91, 'Rossiel', 'Rossiel', 5, 8, '10028'),
(92, 'Sam Gamgee', 'Samsagaz Gamyi', 4, 8, '06029'),
(93, 'Saruman', 'Saruman', 5, 13, '141002'),
(94, 'Sméagol', 'Sméagol', 5, 3, '164002'),
(95, 'Thalin', 'Thalin', 3, 9, '01002'),
(96, 'Théoden (Spirit)', 'Théoden (Espíritu)', 4, 12, '03079'),
(97, 'Théoden (Tactics)', 'Théoden (Táctica)', 3, 12, '05003'),
(98, 'Théodred', 'Théodred', 2, 8, '01002'),
(99, 'Thorin Oakenshield', 'Thorin Escudo de Roble', 2, 12, '03127'),
(100, 'Thorin Stonehelm', 'Thorin Yelmo de Piedra', 3, 9, '147002'),
(101, 'Thranduil', 'Thranduil', 5, 9, '145002'),
(102, 'Thurindir', 'Thurindir', 5, 8, '141003'),
(103, 'Tom Cotton', 'Tom Cotton', 3, 8, '10029');
GO

-- =====================================================
-- SEED DATA: 10 EXAMPLE GAMES 
-- =====================================================

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-01-01', 0, 1, 2, 3, 1, 1, NULL, 'Partida inicial con mazo de Rohan.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (1, 98, 0), (1, 41, 0), (1, 76, 1);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-01-02', 0, 2, 2, 3, 0, 1, NULL, 'Superado el Troll de las Colinas.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (2, 98, 0), (2, 41, 0), (2, 76, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-01-03', 0, 2, 2, 3, 0, 2, 5, 'Demasiados lugares en el área de preparación.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (3, 98, 0), (3, 41, 0), (3, 76, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-01-05', 0, 5, 2, 3, 0, 1, NULL, 'Mazo de Enanos.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (4, 28, 0), (4, 82, 0), (4, 84, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-01-10', 0, 13, 2, 3, 0, 1, NULL, 'Limpieza de trasgos en Moria.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (5, 28, 0), (5, 82, 0), (5, 84, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-01-12', 0, 14, 2, 3, 3, 2, 1, 'El Daño del Balrog fue imparable.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (6, 28, 1), (6, 82, 1), (6, 84, 1);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-02-01', 1, 27, 2, 2, 0, 1, NULL, 'Modo campaña: Frodo y compañía.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (7, 92, 0), (7, 79, 0), (7, 86, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-02-05', 1, 28, 2, 2, 0, 1, NULL, 'Campaña: El Viejo Hombre-Sauce derrotado.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (8, 92, 0), (8, 79, 0), (8, 86, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-02-10', 1, 29, 2, 2, 0, 2, 2, 'Eliminado por amenaza alta.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (9, 92, 0), (9, 79, 0), (9, 86, 0);

INSERT INTO [Games] ([DatePlayed], [IsCampaignMode], [QuestId], [DifficultyId], [Spheres], [DeadHeroes], [ResultId], [ReasonForDefeatId], [Notes])
VALUES ('2024-03-01', 0, 48, 2, 3, 1, 1, NULL, 'Partida épica contra Smaug.');

INSERT INTO [GameHeroes] ([GameId], [HeroId], [IsDead]) 
VALUES (10, 10, 0), (10, 95, 1), (10, 58, 0);
GO