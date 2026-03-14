-- Script de exemplo com dados iniciais para testes
-- Executar após as migrations terem sido aplicadas

-- Inserir Países
INSERT INTO "Paises" ("Id", "Nome", "CodigoISO3", "CodigoONU", "CodigoDDI", "CodigoMoeda", "DefaultLocale", "Ativo")
VALUES
  ('BR', 'Brasil', 'BRA', 76, '+55', 'BRL', 'pt-BR', true),
  ('US', 'Estados Unidos', 'USA', 840, '+1', 'USD', 'en-US', true),
  ('FR', 'França', 'FRA', 250, '+33', 'EUR', 'fr-FR', true),
  ('DE', 'Alemanha', 'DEU', 276, '+49', 'EUR', 'de-DE', true),
  ('JP', 'Japão', 'JPN', 392, '+81', 'JPY', 'ja-JP', true);

-- Inserir Estados do Brasil
INSERT INTO "Estados" ("Id", "PaisId", "Nome", "Sigla", "Tipo", "Ativo")
VALUES
  ('BR-SP', 'BR', 'São Paulo', 'SP', 0, true),
  ('BR-RJ', 'BR', 'Rio de Janeiro', 'RJ', 0, true),
  ('BR-MG', 'BR', 'Minas Gerais', 'MG', 0, true),
  ('BR-SC', 'BR', 'Santa Catarina', 'SC', 0, true),
  ('BR-PR', 'BR', 'Paraná', 'PR', 0, true),
  ('BR-RS', 'BR', 'Rio Grande do Sul', 'RS', 0, true),
  ('BR-BA', 'BR', 'Bahia', 'BA', 0, true),
  ('BR-CE', 'BR', 'Ceará', 'CE', 0, true);

-- Inserir Estados dos EUA
INSERT INTO "Estados" ("Id", "PaisId", "Nome", "Sigla", "Tipo", "Ativo")
VALUES
  ('US-CA', 'US', 'Califórnia', 'CA', 0, true),
  ('US-NY', 'US', 'Nova York', 'NY', 0, true),
  ('US-TX', 'US', 'Texas', 'TX', 0, true),
  ('US-AK', 'US', 'Alaska', 'AK', 0, true),
  ('US-FL', 'US', 'Flórida', 'FL', 0, true),
  ('US-IL', 'US', 'Illinois', 'IL', 0, true);

-- Inserir Regiões da França
INSERT INTO "Estados" ("Id", "PaisId", "Nome", "Sigla", "Tipo", "Ativo")
VALUES
  ('FR-75', 'FR', 'Île-de-France', 'IDF', 2, true),
  ('FR-69', 'FR', 'Auvergne-Rhône-Alpes', 'ARA', 2, true),
  ('FR-13', 'FR', 'Provence-Alpes-Côte d''Azur', 'PACA', 2, true);

-- Inserir Bundesländer da Alemanha
INSERT INTO "Estados" ("Id", "PaisId", "Nome", "Sigla", "Tipo", "Ativo")
VALUES
  ('DE-BY', 'DE', 'Baviera', 'BY', 2, true),
  ('DE-BE', 'DE', 'Berlim', 'BE', 2, true),
  ('DE-BW', 'DE', 'Baden-Württemberg', 'BW', 2, true);

-- Inserir Prefeituras do Japão
INSERT INTO "Estados" ("Id", "PaisId", "Nome", "Sigla", "Tipo", "Ativo")
VALUES
  ('JP-13', 'JP', 'Tóquio', 'TYO', 3, true),
  ('JP-27', 'JP', 'Osaka', 'OSA', 3, true),
  ('JP-40', 'JP', 'Fukuoka', 'FUK', 3, true);

-- Inserir Cidades em São Paulo
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('BR-SP', 'São Paulo', '01310-100', -23.550520, -46.633308, true),
  ('BR-SP', 'Campinas', '13010-010', -22.889099, -47.057944, true),
  ('BR-SP', 'Sorocaba', '18087-000', -23.494933, -47.456721, true),
  ('BR-SP', 'Ribeirão Preto', '14015-120', -21.194897, -47.810439, true);

-- Inserir Cidades no Rio de Janeiro
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('BR-RJ', 'Rio de Janeiro', '20040-020', -22.906847, -43.192729, true),
  ('BR-RJ', 'Niterói', '24030-200', -22.883547, -43.097426, true),
  ('BR-RJ', 'Duque de Caxias', '25025-360', -22.794246, -43.308778, true);

-- Inserir Cidades em Santa Catarina
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('BR-SC', 'Florianópolis', '88010-000', -27.596592, -48.548569, true),
  ('BR-SC', 'Blumenau', '89010-010', -26.919262, -49.066206, true),
  ('BR-SC', 'Joinville', '89201-695', -26.304703, -48.844035, true);

-- Inserir Cidades na Califórnia (EUA)
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('US-CA', 'Los Angeles', '90001', 34.052235, -118.243683, true),
  ('US-CA', 'San Francisco', '94102', 37.774929, -122.419418, true),
  ('US-CA', 'San Diego', '92101', 32.715736, -117.161087, true),
  ('US-CA', 'Sacramento', '95814', 38.581572, -121.494400, true);

-- Inserir Cidades em Nova York (EUA)
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('US-NY', 'Nova York', '10001', 40.712776, -74.005974, true),
  ('US-NY', 'Buffalo', '14202', 42.886447, -78.878369, true),
  ('US-NY', 'Rochester', '14604', 43.161190, -77.610924, true);

-- Inserir Cidades em Paris (França)
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('FR-75', 'Paris', '75001', 48.856613, 2.352222, true),
  ('FR-75', 'Versailles', '78000', 48.801408, 2.130122, true),
  ('FR-75', 'Boulogne-Billancourt', '92100', 48.834343, 2.240060, true);

-- Inserir Cidades em Munique (Baviera, Alemanha)
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('DE-BY', 'Munique', '80331', 48.135125, 11.581981, true),
  ('DE-BY', 'Nuremberg', '90402', 49.451993, 11.076745, true),
  ('DE-BY', 'Augsburgo', '86150', 48.366665, 10.900000, true);

-- Inserir Cidades em Berlim (Alemanha)
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('DE-BE', 'Berlim', '10115', 52.520008, 13.404954, true),
  ('DE-BE', 'Potsdam', '14467', 52.390569, 13.064473, true);

-- Inserir Cidades em Tóquio (Japão)
INSERT INTO "Cidades" ("EstadoId", "Nome", "CodigoPostal", "Latitude", "Longitude", "Ativo")
VALUES
  ('JP-13', 'Tóquio', '100-0001', 35.676592, 139.650541, true),
  ('JP-13', 'Yokohama', '220-0012', 35.447713, 139.642501, true),
  ('JP-13', 'Kawasaki', '210-8577', 35.531289, 139.702394, true);

-- Verificar dados inseridos
SELECT COUNT(*) as total_paises FROM "Paises";
SELECT COUNT(*) as total_estados FROM "Estados";
SELECT COUNT(*) as total_cidades FROM "Cidades";

-- Exemplo de consultas
SELECT p."Id", p."Nome", COUNT(e."Id") as quantidade_estados
FROM "Paises" p
LEFT JOIN "Estados" e ON p."Id" = e."PaisId"
WHERE p."Ativo" = true
GROUP BY p."Id", p."Nome"
ORDER BY quantidade_estados DESC;

SELECT e."Id", e."Nome", COUNT(c."Id") as quantidade_cidades
FROM "Estados" e
LEFT JOIN "Cidades" c ON e."Id" = c."EstadoId"
WHERE e."Ativo" = true
GROUP BY e."Id", e."Nome"
ORDER BY quantidade_cidades DESC;

